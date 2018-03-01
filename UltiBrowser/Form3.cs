using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UltiBrowser
{
    public partial class Form3 : Form
    {
        // Disable close button. 
        // Method from https://stackoverflow.com/questions/7301825/windows-forms-how-to-hide-close-x-button
        private const int CP_NOCLOSE_BUTTON = 0x200;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }

        private readonly Form1 _f1;

        public Form3(Form1 f1)
        {
            InitializeComponent();
            // Set form1
            this._f1 = f1;
        }

        private void setting()
        {
            Form1.home_index = textBox1.Text;
            this.Close();
            // Enable add button after closing form3. 
            this._f1.Renable_2();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            setting();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)ConsoleKey.Enter)
            {
                setting();
            }
        }

        // Quit case
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            // Enable add button after closing form2. 
            this._f1.Renable_2();
        }
    }
}