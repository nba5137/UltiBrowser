using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Web.Script.Serialization;

namespace UltiBrowser
{
    public partial class Form1 : Form
    {
        // Setup Dict for bookmarks
        public Dictionary<string, string> Bookmark = new Dictionary<string, string>();
        // Setup string for homepage
        public string home_index;
        public string name_to_edit;  //use to get bookmark_name in Form4 and check in dict for updating
        public string index_to_edit;

        public Form1()
        {
            InitializeComponent();
            // Creating a file for storing 
            if (!File.Exists("fav"))
            {
                FileStream newfile = File.Create("fav");
                newfile.Close();
            }

            if (!File.Exists("hp"))
            {
                FileStream newfile_2 = File.Create("hp");
                newfile_2.Close();
            }
            // Reading from the file
            Bookmark = new JavaScriptSerializer()
                .Deserialize<Dictionary<string, string>>(File.ReadAllText("fav"));

            StreamReader home_file =
                new StreamReader("hp");
            home_index = home_file.ReadLine();
            if (home_index == null)
            {
                home_index = "about:blank";
            }
            home_file.Close();
            webBrowser1.Navigate(home_index);
            // Adding to menustripitem 
            Add_pages();
            // Hiding script errors
            webBrowser1.ScriptErrorsSuppressed = true;
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

        private void button2_Click(object sender, EventArgs e)
        {
            // Add form2 as bookmarks window. 
            Form3 set_hp = new Form3(this);
            set_hp.Show();
            // Disable this button once form2 open. 
            button2.Enabled = false;
        }

        private void home_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate(home_index);
        }

        /// <summary>
        /// Bookmarks case
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void button1_Click(object sender, EventArgs e)
        {
            // Add form2 as bookmarks window. 
            Form2 add_bookmark = new Form2(this);
            add_bookmark.Show();
            // Disable this button once form2 open. 
            button1.Enabled = false;
        }

        /// <summary>
        /// Edit case. For delete or modify bookmarks
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        
        private void Edit_Click(object sender, EventArgs e)
        {
            // form4 as Edit_Bookmarks window.
            ToolStripMenuItem loc = (ToolStripMenuItem)sender;
            name_to_edit = loc.OwnerItem.Text;
            for (int i = 0; i < Bookmark.Count; i++)
            {
                if (name_to_edit == Bookmark.Keys.ElementAt(i))
                {
                    index_to_edit = Bookmark[Bookmark.Keys.ElementAt(i)];
                }
            }
            Form4 edit_bookmarks = new Form4(this);
            edit_bookmarks.Show();
            Bookmarks.Enabled = false;
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
            if (Bookmark == null)
            {
                return;
            }
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
                        Bookmarks.DropDownItems[count].Name = new_item;
                        // after added new item, add Edit option to it.
                        ToolStripMenuItem new_one = Bookmarks.DropDownItems[count] as ToolStripMenuItem;
                        new_one.DropDownItems.Add("Edit");
                        ToolStripMenuItem for_add_edit = new_one.DropDownItems[0] as ToolStripMenuItem;
                        for_add_edit.Click += new EventHandler(Edit_Click);
                        // and then add click event to it. 
                        Bookmarks.DropDownItems[count].Click += new EventHandler(Check_Dict);
                    }
                    else
                    {
                        repeat = false;
                    }
                    
                }
                else
                {
                    Bookmarks.DropDownItems.Add(new_item);
                    Bookmarks.DropDownItems[0].Name = new_item;
                    Bookmarks.DropDownItems[0].Click += new EventHandler(Check_Dict);
                    // Add Edit option to it. 
                    ToolStripMenuItem new_one = Bookmarks.DropDownItems[0] as ToolStripMenuItem;
                    new_one.DropDownItems.Add("Edit");
                    ToolStripMenuItem for_add_edit = new_one.DropDownItems[0] as ToolStripMenuItem;
                    for_add_edit.Click += new EventHandler(Edit_Click);
                }
            }
            // Writing into the file "fav" for storing  
            File.WriteAllText("fav", new JavaScriptSerializer().Serialize(Bookmark));
        }

        // Renable add button function
        public void Renable()
        {
            // Enable add button after closing form2. 
            button1.Enabled = true;
        }
        public void Renable_2()
        {
            // Enable add button after closing form3. 
            button2.Enabled = true;
        }
        public void Renable_3()
        {
            // Enable add button after closing form3. 
            Bookmarks.Enabled = true;
        }

        /// <summary>
        /// Check_Dict function. Used as checking index according to bookmark name.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Check_Dict(object sender, EventArgs e)
        {
            ToolStripMenuItem key = (ToolStripMenuItem)sender;
            for (int i = 0; i < Bookmark.Count; i++)
            {
                if (key.Text == Bookmark.Keys.ElementAt(i))
                {
                    webBrowser1.Navigate(Bookmark[Bookmark.Keys.ElementAt(i)]);
                }
            }
        }

        /// <summary>
        /// Update url for same bookmark name. 
        /// </summary>
        public void Update()
        {
            Bookmark[name_to_edit] = index_to_edit;
            // Writing into the file "fav" for storing  
            File.WriteAllText("fav", new JavaScriptSerializer().Serialize(Bookmark));
        }

        /// <summary>
        /// Delete the chosen bookmark
        /// </summary>
        public void Delete()
        {
            Bookmarks.DropDownItems.RemoveByKey(name_to_edit);
            // also delete from the dictionary.
            Bookmark.Remove(name_to_edit);
            // Writing into the file "fav" for storing  
            File.WriteAllText("fav", new JavaScriptSerializer().Serialize(Bookmark));
        }

    }
}
