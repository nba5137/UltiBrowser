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
        public static string index;
        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            toolStripStatusLabel1.Text = "Loading Completed";
            // Update index input box - textBox1. 
            index = (webBrowser1.Url).ToString();
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

        //Setup Dict for bookmarks
        public static Dictionary<string, string> Bookmark = new Dictionary<string, string>();

        private void button1_Click(object sender, EventArgs e)
        {
            // Add form2 as bookmarks window. 
            Form2 add_bookmark = new Form2(this);
            add_bookmark.Show();
        }


        // Update bookmarks by clicking Bookmarks button
        /*
         * Bookmark = Dict
         * Bookmarks = menustrip
         * 
         */

        /*
        private void Bookmarks_Click(object sender, EventArgs e)
        {
            //Moved to Add_pages function.
        }
        */

        /// <summary>
        /// Add_pages function. will be called by "Add" button in form2. 
        /// Did not implement it in form2, since modify data from another form is complicated.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Add_pages()
        {
            for (int i = 0; i < Bookmark.Count; i++)
            {
                string new_item = Bookmark.Keys.ElementAt(i);
                // Check repeated items
                if (Bookmarks.HasDropDownItems)
                {
                    int count = 0;
                    bool repeat = false;
                    foreach (ToolStripMenuItem DropDownItem in Bookmarks.DropDownItems)
                    {
                        count = count + 1;
                    }
                    for (int j = 0; j < count; j++)
                    {
                        if (Bookmarks.DropDownItems[j].Text == new_item)
                        {
                            repeat = true;
                            break;
                        }
                    }
                    if (repeat == false)
                    {
                        Bookmarks.DropDownItems.Add(new_item);
                    }
                    else
                    {
                        repeat = false;
                    }
                }
                else
                {
                    Bookmarks.DropDownItems.Add(new_item);
                }
            }
        }
    }
}
