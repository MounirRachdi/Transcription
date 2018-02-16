using System;
using System.IO;
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
    public partial class Form3 : Form
    {
        OleDbConnection conn = new OleDbConnection();
        String connection = "Provider=Microsoft.Jet.Oledb.4.0;Data Source=.\\Output.mdb";
       
        int num = 0;
        string age = "";
        string sex = "";
        string nationalite = "";
        string path;
        string NativeLng = "";
        string Institut = "";
        string NiveauG = "";
        string Niveauarabic = "";
        string yearofS = "";
        string Arabicperiodd = "";
        string Nonarabicperiode = "";
        string Text1 = "";
        string Text2 = "";
        string Text3 = "";
        string Text4 = "";
        string Text5 = "";
        string Text6 = "";

        public Form3(int num)
        {
           
            
       InitializeComponent();
       this.Show();
       textBox1.SelectionIndent += 10;
       this.num = num;
         
        chargeText(num);
        
            
   }
        public Form3()
        {
            InitializeComponent();
        }
      

        private void button3_Click(object sender, EventArgs e)
        {
            update();
        }
        public void update()
        {
             string vide = "غير محدد";

             if (Num1.Text == string.Empty)
             {
                 Form5.Show("لم تقم بإدخال العدد التسلسلي للمشترك...!", false);
              
             }

             else
             {
                 num = Convert.ToInt32(Num1.Text);

                 age = Age1.Text == string.Empty ? vide : Age1.Text;
                 sex = comboBox1.SelectedItem == null ? vide : comboBox1.SelectedItem.ToString();
                 nationalite = Nationalite1.Text == string.Empty ? vide : Nationalite1.Text;
                 NativeLng = NativeL.Text == string.Empty ? vide : NativeL.Text;
                 Institut = Institu.Text == string.Empty ? vide : Institu.Text;
                 NiveauG = NiveauGen.Text == string.Empty ? vide : NiveauGen.Text;
                 Niveauarabic = NiveauArab.Text == string.Empty ? vide : NiveauArab.Text;
                 yearofS = YersOS.Text == string.Empty ? vide : YersOS.Text;
                 Arabicperiodd = Arabicperiod.Text == string.Empty ? vide : Arabicperiod.Text;
                 Nonarabicperiode = Nonarabicperiod.Text == string.Empty ? vide : Nonarabicperiod.Text;
                 string historique = richTextBox1.Text + " تغييرمعلومات المشترك عدد " + Num1.Text;

                 string sql = "UPDATE Profile SET Age='" + age + "',Gender ='" + sex + "',Nationality='" + nationalite + "',Nativelanguage='" + NativeLng + "',Institution='" + Institut + "',Generallevel='" + NiveauG + "',Arabiclevel='" + Niveauarabic + "',yearsOS='" + yearofS + "',Arabicperiod='" + Arabicperiodd + "', Nonarbicperoid='" + Nonarabicperiode + "',Historique ='" + historique + "' where Num=" + num;

                 /* string sql = "insert into Profile (Num,Age,Gender,Nationality,Nativelanguage,Institution,Generallevel,Arabiclevel,yearsOS,Arabicperiod,Nonarbicperoid,Filepath)values
                      (" + num + ",'" + age + "','" + sex + "','" + nationalite + "','" + NativeLng + "','" +
                       Institut + "','" + NiveauG + "','" + Niveauarabic + "','" + yearofS + "','" + Arabicperiodd + "','" + Nonarabicperiode +"','"+path+"')";
                  */



                 try
                 {
                     conn.ConnectionString = connection;
                     conn.Open();

                     OleDbCommand cmd = new OleDbCommand(sql, conn);
                     cmd.ExecuteNonQuery();

                     Form5.Show("تم الحفظ بنجاح...!", false);
                    
                     richTextBox1.Text = historique;




                 }
                 catch (Exception ex)
                 {


                     Form5.Show("العملية لم تتم. خطأ رقم" + ex.HResult, false);
                    
                 }
                 finally
                 {
                     conn.Close();
                 }
             }
        }

        private void button2_Click(object sender, EventArgs e)
        {
             Form5.Show("هل تريد فعلا الغلق ؟", true);
            if (Form5.result == DialogResult.Yes)
                
            {
                Form5.Show("هل أتممت حفظ التغييرات", true);
            if (Form5.result == DialogResult.Yes)
                File.WriteAllText("Unfinished.txt", "");
                Close();
            }
        }
      
        
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            update();
        }

        private void button4_Click(object sender, EventArgs e)
        {

            if (textBox1.Text == string.Empty)
            {
                Form5.Show("لم تقم بإدخال النص!" + "\n" + "أدخل النص قبل الضغط على زر الحفظ", false);

            }
            else
            {
                string text_type = "";
                string text = "";
                if (radioButton1.Checked)
                {
                    text = "Text1";
                    text_type = "T1";
                }
                else if (radioButton5.Checked)
                {
                    text = "Text2";
                    text_type = "T2";
                }
                else if (radioButton6.Checked)
                {
                    text = "Text3";
                    text_type = "T3";
                }

                else if (radioButton2.Checked)
                {
                    text = "Text4";
                    text_type = "T4";
                }
                else if (radioButton4.Checked)
                {
                    text = "Text5";
                    text_type = "T5";
                }
                else if (radioButton3.Checked)
                {
                    text = "Text6";
                    text_type = "T6";
                }
                if (text == "")
                {
                    Form5.Show("لم تقم بإدخال نوع النص!" + "\n" + "أدخل نوع النص قبل الضغط على زر الحفظ", false);

                }
                else
                {


                    string sql = "";
                    conn.ConnectionString = connection;

                    string Fname = "./TxtOutput/Part" + textBox2.Text + "_" + text_type + ".txt";
                    sql = "UPDATE Profile SET " + text + " ='" + Fname + "' where Num = " + Convert.ToInt32(textBox2.Text);

                    //  MessageBox.Show(sql);

                    try
                    {

                        conn.Open();

                        OleDbCommand cmd = new OleDbCommand(sql, conn);
                        cmd.ExecuteNonQuery();
                        //textBox2.Text = Num.Text;
                        Form5.Show("تم الحفظ بنجاح...!", false);

                        //richTextBox1.Text += historique;
                        File.WriteAllText(Fname, textBox1.Text, Encoding.UTF8);
                        textBox1.Text = "";

                        //vider();

                    }
                    catch (Exception ex)
                    {
                        Form5.Show("العملية لم تتم. خطأ رقم" + ex.HResult, false);

                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
            
        }
       

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Text = "";
            try
            {
                if (radioButton1.Checked)
                    if (Text1 != "")
                        if (File.Exists(Text1))
                        {


                            textBox1.Text = File.ReadAllText(Text1, Encoding.UTF8);
                            
                        }
                     
            }
            catch (Exception ex)
            {
                Form5.Show("العملية لم تتم. خطأ رقم" + ex.HResult, false);
           
            }
        }
        private void chargeText(int num)
        {
            string req = "select Num,Age,Gender,Nationality,Nativelanguage,Institution,Generallevel,Arabiclevel,yearsOS,Arabicperiod,Nonarbicperoid,Filepath,Historique,Text1,Text2,Text3,Text4,Text5,Text6 from Profile  where Num=" + num; 
            try
            {
                conn.ConnectionString = connection;
                conn.Open();
                OleDbCommand cmd = new OleDbCommand(req, conn);
                OleDbDataReader reader = cmd.ExecuteReader();
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);
                Num1.Text = Convert.ToString(num);
                textBox2.Text = Convert.ToString(num);
                foreach (DataRow row in dataTable.Rows)
                {
                    
                    Age1.Text = row["Age"].ToString() ;
                    comboBox1.SelectedIndex = row["Gender"].ToString() == "ذكر" ? 0 : 1;
                    Nationalite1.Text = row["Nationality"].ToString();
                    NativeL.Text = row["Nativelanguage"].ToString();
                    Institu.Text = row["Institution"].ToString();
                    NiveauGen.Text = row["Generallevel"].ToString();
                    NiveauArab.Text = row["Arabiclevel"].ToString();
                    Arabicperiod.Text = row["Arabicperiod"].ToString();
                    YersOS.Text = row["yearsOS"].ToString();
                    Nonarabicperiod.Text = row["Nonarbicperoid"].ToString();
                    richTextBox1.Text = row["Historique"].ToString();
                    path = row["Filepath"].ToString();
                    axAcroPDF1.LoadFile("./PdfOutput/" + path);
                    textBox1.SelectionIndent += 10;
                    
                    Text1 = row["Text1"].ToString();
                    Text2 = row["Text2"].ToString(); 
                    Text3 = row["Text3"].ToString();
                    Text4 = row["Text4"].ToString(); 
                    Text5 = row["Text5"].ToString();
                    Text6 = row["Text6"].ToString();
                }

                // Always call Close when done reading.
                reader.Close();
 
  
 
            
               
 


            }
            catch (Exception ex)
            {


                Form5.Show("العملية لم تتم. خطأ رقم" + ex.HResult,false);
               
            }
             finally
                {
                    conn.Close();
                }


        
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Text ="";
            try
            {
                if (radioButton5.Checked)
                {
                    if (Text2 != "")
                        if (File.Exists(Text2))
                        {


                            textBox1.Text = File.ReadAllText(Text2, Encoding.UTF8);
                             
                        }
                     
                }
            }
            catch (Exception ex)
            {
                Form5.Show("العملية لم تتم. خطأ رقم" + ex.HResult, false);
               
            }
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Text = "";
            try
            {
                if (radioButton6.Checked)
                {
                    if (Text3 != "")
                        if (File.Exists(Text3))
                        {


                            textBox1.Text = File.ReadAllText(Text3, Encoding.UTF8);

                        }
                }
            }
            catch (Exception ex)
            {
                Form5.Show("العملية لم تتم. خطأ رقم" + ex.HResult, false);
              
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Text = "";
            try
            {
                if (radioButton2.Checked)
                {
                    if (Text4 != "")
                        if (File.Exists(Text4))
                        {


                            textBox1.Text = File.ReadAllText(Text4, Encoding.UTF8);

                        }
                }
            }
            catch (Exception ex)
            {
                Form5.Show("العملية لم تتم. خطأ رقم" + ex.HResult, false);
                
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Text = "";
            try
            {
                if (radioButton4.Checked)
                {
                    if (Text5 != "")
                        if (File.Exists(Text5))
                        {


                            textBox1.Text = File.ReadAllText(Text5, Encoding.UTF8);

                        }
                }
            }
            catch (Exception ex)
            {
                Form5.Show("العملية لم تتم. خطأ رقم" + ex.HResult, false);
                
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Text = "";
            try
            {
                if (radioButton3.Checked)
                {
                    if (Text6 != "")
                        if (File.Exists(Text6))
                        {


                            textBox1.Text = File.ReadAllText(Text6, Encoding.UTF8);

                        }
                }
            }
            catch (Exception ex)
            {
                Form5.Show("العملية لم تتم. خطأ رقم" + ex.HResult, false);
                
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != string.Empty)
            { textBox1.Text += "[كلمة غير مفهومة]";
            textBox1.Focus(); textBox1.Select(textBox1.Text.Length, 0);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != string.Empty)
            {
                textBox1.Text += "[معلومة شخصية]";
             textBox1.Focus(); textBox1.Select(textBox1.Text.Length, 0);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != string.Empty)
            {
                Form4 frm4 = new Form4(textBox2.Text);
                frm4.Show();
                 
            }

        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void button7_Click_1(object sender, EventArgs e)
        {

        }

        private void button2_Click_3(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != string.Empty)
            { textBox1.Text += "[كلمة أو عبارة محذوفة]";
            textBox1.Focus(); textBox1.Select(textBox1.Text.Length, 0);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != string.Empty)
            {
                textBox1.Text += "[كلمة أو عبارة مضافة]";
                textBox1.Focus(); textBox1.Select(textBox1.Text.Length, 0);
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != string.Empty)
            {
                textBox1.Text += "[إبدال كلمة أو عبارة ب][ ]";
                textBox1.Focus(); textBox1.Select(textBox1.Text.Length, 0);
            }
           
        }

       

       
     
       

    }
}
