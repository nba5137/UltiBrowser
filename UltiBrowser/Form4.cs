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
    public partial class Form4 : Form
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

        public Form4(Form1 f1)
        {
            InitializeComponent();
            // Set form1
            this._f1 = f1;
            Bm_name.Text = this._f1.name_to_edit;
            Bm_url.Text = this._f1.index_to_edit;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Updating new Bookmark name and url. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 

        private void update_bm()
        {
            if (!this._f1.Bookmark.ContainsKey(Bm_name.Text))
            {
                this._f1.Bookmark.Add(Bm_name.Text, Bm_url.Text);
                // Calling Add_pages function in form 1. 
                this._f1.Delete();
                this._f1.Add_pages();
            }
            else
            {
                this._f1.index_to_edit = Bm_url.Text;
                this._f1.Update();
            }
            this.Close();
            this._f1.Renable_3();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            update_bm();
        }

        /// <summary>
        /// Delete current bookmarks
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            this._f1.Delete();
            this.Close();
            this._f1.Renable_3();
        }

        private void Bm_name_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)ConsoleKey.Enter)
            {
                update_bm();
            }
        }

        private void Bm_url_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)ConsoleKey.Enter)
            {
                update_bm();
            }
        }
    }
}