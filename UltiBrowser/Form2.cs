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
        public Form2()
        {
            InitializeComponent();
            // Initialize with current index. 
            Bm_name.Text = "New page";
            textBox1.Text = Form1.index;
        }

        // Quit case
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        // Add case
        private void button1_Click(object sender, EventArgs e)
        {
            Form1.Bookmark.Add(Bm_name.Text, textBox1.Text);
            this.Close();
        }
    }
}
