using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web;

namespace UltiBrowser
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Menu issues
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        // Option - Exit
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        // Menu - About
        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Simple web browser built in C# by Sibo Song. ");
        }

        /// <summary>
        /// Web index Access issues
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        // Button - Access the index input in textBox1. 
        private void Access()
        {
            toolStripStatusLabel1.Text = "Loading...";
            webBrowser1.Navigate(textBox1.Text);
        }
            
        private void access_Click(object sender, EventArgs e)
        {
            Access();
        }

        // Press 'Enter' in textBox1 to access a website. 
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)ConsoleKey.Enter)
            {
                Access();
            }
        }

        /// <summary>
        /// Loading Progress issues. Loading bar etc.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void webBrowser1_ProgressChanged(object sender, WebBrowserProgressChangedEventArgs e)
        {
            // To avoid zero error. 
            if (e.CurrentProgress > 0 && e.MaximumProgress > 0)
            {
                int Progress_Value = (int)(e.CurrentProgress / e.MaximumProgress * 100);
                //exceed issue proof. 
                if (Progress_Value >= 100 )
                {
                    Progress_Value = 100;
                }
                toolStripProgressBar1.ProgressBar.Value = Progress_Value;
            }
        }

        // Actions once complete loading.
        /*  
         * Setting string variables for backward and forward buttons. 
         * 0 is current index. 
         * 1 is index where gonna return to.
         * 
         */

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            toolStripStatusLabel1.Text = "Loading Completed";
            // Update index input box - textBox1. 
            string index = (webBrowser1.Url).ToString();
            textBox1.Text = index;
        }

        private void backward_Click(object sender, EventArgs e)
        {
            webBrowser1.GoBack();
        }

        private void forward_Click(object sender, EventArgs e)
        {
            webBrowser1.GoForward();
        }

    }
}
