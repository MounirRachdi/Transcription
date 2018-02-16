using System;
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
    
    public partial class Form5 : Form
    {
        static Form5 MsgBox;
        
      public static DialogResult result = DialogResult.No;
     //   bool click = false;
        
        public Form5()
        {
            InitializeComponent();
            button1.Select();
           
            

        }
     
        
      public static DialogResult Show(string Text, bool rep)
{
     MsgBox = new Form5();
     MsgBox.label1.Text = Text;
  
    
    MsgBox.label1.SelectionIndent += 10;
     if (rep == false)
         MsgBox.No.Hide();
     MsgBox.ShowDialog();
     return result;
}
////////////////////////////////////////////////////////////////////////////////


        private void button1_Click_1(object sender, EventArgs e)
      {
         /* if (repp == false)
              this.Close();
          else
          */
          { result = DialogResult.Yes; MsgBox.Close(); }

        }

        private void No_Click(object sender, EventArgs e)
        {
            result = DialogResult.No;
            MsgBox.Close();
            
        }
    }
}
