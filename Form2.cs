using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Transcription
{
    public partial class Form2 : Form
    {int num=0;
       
        public Form2()
        {
            InitializeComponent();
        }
        OleDbConnection conn = new OleDbConnection();
        String connection = "Provider=Microsoft.Jet.Oledb.4.0;Data Source=.\\Output.mdb";
        private void Form2_Load(object sender, EventArgs e)
        {
            selestData();
            
        }
        
        void selestData()
        {



            string req = "select Num,Age,Gender,Nationality,Nativelanguage,Institution,Generallevel,Arabiclevel,yearsOS,Arabicperiod,Nonarbicperoid,Filepath,Historique from Profile";
            try
            {
                conn.ConnectionString = connection;
                conn.Open();
                OleDbCommand cmd = new OleDbCommand(req, conn);
                OleDbDataAdapter adap = new OleDbDataAdapter(req, conn);
                DataSet dataset = new DataSet();
                adap.Fill(dataset);

                dataGridView1.DataSource = dataset.Tables[0];
                dataGridView1.Columns[0].HeaderText = "مشارك عدد";
                dataGridView1.Columns[1].HeaderText = "العمر";
                dataGridView1.Columns[2].HeaderText = "الجنس";
                dataGridView1.Columns[3].HeaderText = "الجنسيّة";
                dataGridView1.Columns[4].HeaderText = "اللّغة الأم";
                dataGridView1.Columns[5].HeaderText = "المؤسّسة التّعليمية";
                dataGridView1.Columns[6].HeaderText = "المستوى الدّراسي العامّ";
                dataGridView1.Columns[7].HeaderText = "المستوى الدّراسي في العربيّة";
                dataGridView1.Columns[8].HeaderText = "عدد سنوات التعلّم ";
                dataGridView1.Columns[9].HeaderText = "فترة دراسة في البلدان النّاطقة بالعربيّة";
                dataGridView1.Columns[10].HeaderText = "فترة دراسة في البلدان غير النّاطقة بالعربيّة";

                dataGridView1.Columns[12].HeaderText = "سجل التسجيلات";
                dataGridView1.Columns[11].HeaderText = "إسم الملف";
                dataGridView1.Visible = true;

            }
            catch (Exception ex)
            {

                Form5.Show ("العملية لم تتم. خطأ رقم" + ex.HResult,false);
                
            }
             finally
                {
                    conn.Close();
                }


        }



        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {


            num = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
          string  path = dataGridView1.Rows[e.RowIndex].Cells[11].Value.ToString();

            Form6.Show("هل تريد تعديل او حذف التسجيل ؟");
            if (Form6.result == DialogResult.No)
            {
                Form3 frm3 = new Form3(num);
                frm3.Show(); frm3.Activate();
            }
            else
                if (Form6.result == DialogResult.Yes)
            {
                string sql = "DELETE FROM Profile where Num=" + num;
                
                try
                {
                    conn.ConnectionString = connection;
                    conn.Open();

                    OleDbCommand cmd = new OleDbCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                    mouve("./PdfOutput/" + path, "./PdfInput/" + path);
                    Form5.Show("تم الحذف بنجاح...!", false);
                    
                }
                catch (Exception ex)
                {
                    Form5.Show("العملية لم تتم. خطأ رقم" + ex.HResult, false);

                }
                finally
                {
                    conn.Close();
                    selestData();
                }


            }
        }

        void mouve(string sourceFile, string destinationFile)
        {
            try
            {
                /* string sourceFile = @"" + folderpath + "/" + file;
                 string destinationFile = @"./PdfOutput/" + file;*/

                // To move a file or folder to a new location:
                System.IO.File.Move(@"" + sourceFile, @"" + destinationFile);
            }

            catch (Exception ex)
            {
                string errorinfo = "العملية لم تتم. خطأ رقم" + ex.HResult + "\n" + ex.ToString();

                Form5.Show(errorinfo, false);


            }

        }
      

       
        

       
        

      
    
    }
}
