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
    
    public partial class Form6: Form
    {
        static Form6 MsgBox;

        public static DialogResult result ;
     //   bool click = false;

        public Form6()
        {
            InitializeComponent();
            button2.Select();
        }
        
      public static DialogResult Show(string Text)
{
     MsgBox = new Form6();
     MsgBox.label1.Text = Text;
    // MsgBox.button1.Text = btnOK;
    // MsgBox.No.Text = btnCancel;
    // MsgBox.Text = Caption;
  
    MsgBox.label1.SelectionIndent += 10;
    
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

        private void button2_Click(object sender, EventArgs e)
        {
            result = DialogResult.Cancel;
            this.Close();
        }
    }
}
