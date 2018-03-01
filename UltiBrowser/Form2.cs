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
    public partial class Form2 : Form
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

        // In order to call Add_pages function in form 1.
        // Going to create a new form1 for storage & use. 
        private readonly Form1 _f1;

        public Form2(Form1 f1)
        {
            InitializeComponent();
            // Initialize with current index. 
            Bm_name.Text = "New page";
            textBox1.Text = Form1.index;

            // Set form1
            this._f1 = f1;
        }

        // Quit case
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            // Enable add button after closing form2. 
            this._f1.Renable();
        }

        /// <summary>
        /// Add case
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void add_case()
        {
            if (!Form1.Bookmark.ContainsKey(Bm_name.Text))
            {
                Form1.Bookmark.Add(Bm_name.Text, textBox1.Text);
                // Calling Add_pages function in form 1. 
                this._f1.Add_pages();
                this.Close();
                // Enable add button after closing form2. 
                this._f1.Renable();
            }
            else
            {
                MessageBox.Show("Please use another name for this bookmark. ");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            add_case();
        }

        // Press Enter in any textbox will go to add case. 
        private void Bm_name_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)ConsoleKey.Enter)
            {
                add_case();
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)ConsoleKey.Enter)
            {
                add_case();
            }
        }
    }
}
