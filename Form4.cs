using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Transcription
{
    public partial class Form4 : Form
    {string Part="";
        public Form4(string part)
        {
            InitializeComponent();
            Part=part;
            richTextBox1.SelectionIndent += 10;
        }

        private void button1_Click(object sender, EventArgs e)
        {if(richTextBox1.Text!=String.Empty)
        {StreamWriter writer = new StreamWriter("Note.txt", true);
                writer.WriteLine("part_" +Part+richTextBox1.Text);
                writer.Close();
        }
        this.Close();
            }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    
        
    }
}
