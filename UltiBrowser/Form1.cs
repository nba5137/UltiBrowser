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
        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            toolStripStatusLabel1.Text = "Loading Completed";
            // Update index input box - textBox1. 
            string index = (webBrowser1.Url).ToString();
            textBox1.Text = index;
        }

        /// <summary>
        /// Functional buttons. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        
        // Backward
        private void backward_Click(object sender, EventArgs e)
        {
            webBrowser1.GoBack();
        }

        // Forward
        private void forward_Click(object sender, EventArgs e)
        {
            webBrowser1.GoForward();
        }

        // Refresh
        private void refresh_Click(object sender, EventArgs e)
        {
            webBrowser1.Refresh();
        }

        // Homepage
        private void home_Click(object sender, EventArgs e)
        {
            webBrowser1.GoHome();
        }

        /// <summary>
        /// Bookmarks case
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        //Setup class for bookmarks
        public class Bookmark
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public string url { get; set; }
        }
        // Create a list for bookmarks.
        public List<Bookmark> bookmarks = new List<Bookmark>();

        private void button1_Click(object sender, EventArgs e)
        {
            // Add form2 as bookmarks window. 
            Form2 add_bookmark = new Form2();
            add_bookmark.Show();
        }

    }
}
