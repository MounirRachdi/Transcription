using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Transcription
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent(); 
           

           

        }

        OleDbConnection conn = new OleDbConnection();
        string connection = "Provider=Microsoft.Jet.Oledb.4.0;Data Source=.\\Output.mdb";
        string historique = "";
        string path;
        int Numparticipant;
        int num = 0;
        string age = "";
        string sex = "";
        string nationalite = "";

        string NativeLng = "";
        string Institut = "";
        string NiveauG = "";
        string Niveauarabic = "";
        string yearofS = "";
        string Arabicperiodd = "";
        string Nonarabicperiode = "";
        string folderpath = "./PdfInput";



        public void refresh()
        {
            try
            {
                textBox3.Text = folderpath;
                DirectoryInfo dir = new DirectoryInfo(folderpath);
                FileInfo[] fichiers = dir.GetFiles();
                ((Control)this.tabControl1.TabPages["tabPage1"]).Enabled = true;
                ((Control)this.tabControl1.TabPages["tabPage2"]).Enabled = false;
                if (fichiers.Length == 0)
                {

                    Form5.Show("لقد انتهيت من كتابة جميع الملفات.", false);

                    ((Control)this.tabControl1.TabPages["tabPage1"]).Enabled = false;


                }
                else
                {
                    

                    axAcroPDF1.LoadFile(folderpath + "/" + fichiers[0]);
                    axAcroPDF1.Show();
                    //MessageBox.Show(chemin);
                    path = "" + fichiers[0];

                    textBox1.SelectionIndent += 10;

                }
            }
            catch (Exception ex)
            {

                Form5.Show("العملية لم تتم. خطأ رقم" + ex.HResult, false);


            }
           

        }
     


        private void button3_Click(object sender, EventArgs e)
        {
            if (Num.Text == string.Empty)
            {
                Form5.Show("لم تقم بإدخال العدد التسلسلي للمشترك...!", false);
            }
            else
                if (verify(Num.Text) == true)
                {
                    ((Control)this.tabControl1.TabPages["tabPage1"]).Enabled = false;
                    string vide = "غير محدد";
                    num = Convert.ToInt32(Num.Text);
                    Numparticipant = Convert.ToInt32(Num.Text);
                    age = Age.Text == string.Empty ? vide : Age.Text;
                    sex = comboBox1.SelectedItem == null ? vide : comboBox1.SelectedItem.ToString();
                    nationalite = Nationalite.Text == string.Empty ? vide : Nationalite.Text;
                    NativeLng = NativeL.Text == string.Empty ? vide : NativeL.Text;
                    Institut = Institu.Text == string.Empty ? vide : Institu.Text;
                    NiveauG = NiveauGen.Text == string.Empty ? vide : NiveauGen.Text;
                    Niveauarabic = NiveauArab.Text == string.Empty ? vide : NiveauArab.Text;
                    yearofS = YersOS.Text == string.Empty ? vide : YersOS.Text;
                    Arabicperiodd = Arabicperiod.Text == string.Empty ? vide : Arabicperiod.Text;
                    Nonarabicperiode = Nonarabicperiod.Text == string.Empty ? vide : Nonarabicperiod.Text;


                    string sql = "insert into Profile (Num,Age,Gender,Nationality,Nativelanguage,Institution,Generallevel,Arabiclevel,yearsOS,Arabicperiod,Nonarbicperoid,Filepath)values(" + num + ",'" + age + "','" + sex + "','" + nationalite + "','" + NativeLng + "','" +
                         Institut + "','" + NiveauG + "','" + Niveauarabic + "','" + yearofS + "','" + Arabicperiodd + "','" + Nonarabicperiode + "','" + path + "')";
                    
                    mouve("./PdfInput/" + path, "./PdfOutput/" + path);


                    try
                    {
                        conn.ConnectionString = connection;
                        conn.Open();

                        OleDbCommand cmd = new OleDbCommand(sql, conn);
                        cmd.ExecuteNonQuery();
                        textBox2.Text = Num.Text;
                        Form5.Show("تم الحفظ بنجاح...!", false);
                         File.WriteAllText("Unfinished.txt", Num.Text);
                        historique = " حفظ معلومات المشترك عدد" + Num.Text + "\n";
                       
                        //  tabControl1.SelectedTab = tabPage2;
                        richTextBox1.Text = historique;
                        ((Control)this.tabControl1.TabPages["tabPage2"]).Enabled = true;
                        textBox2.ReadOnly = true;
                        tabControl1.SelectedTab = tabPage2;
                       

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
                else
                {
                    Form5.Show("لقد قمت بإدخال هذا المشترك سابقاً...!\n هل ترغب في تعديل بياناته ؟", true);
                    if (Form5.result == DialogResult.Yes)
                    {
                        Form3 frm3 = new Form3(Convert.ToInt32(Num.Text));
                        frm3.Show();
                    }

                }
        }


        void vider()
        {
            Num.Text = string.Empty;
            this.Age.Text = null;
            this.Institu.Text = string.Empty;
            this.Nationalite.Text = string.Empty;
            NiveauGen.Text = string.Empty;
            NiveauArab.Text = string.Empty;
            NativeL.Text = string.Empty;
            YersOS.Text = null;
            Arabicperiod.Text = null;
            Nonarabicperiod.Text = null;
            Num.Select(0,0);
        }




        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Form5.Show("هل قمت بحفض النص الحالي؟", true);
                if (Form5.result == DialogResult.Yes)
                {
                    Form5.Show("هل انتهيت من كتابة جميع إنتاجات المشترك الحالي؟", true);
                    if (Form5.result == DialogResult.Yes)
                    {
                        File.WriteAllText("Unfinished.txt", "");
                        DirectoryInfo dir = new DirectoryInfo(folderpath);
                        FileInfo[] fichiers = dir.GetFiles();

                        if (fichiers.Length == 0)
                        {
                            Form5.Show("لقد انتهيت من كتابة جميع الملفات.", false);
                            ((Control)this.tabControl1.TabPages["tabPage2"]).Enabled = false;
                           
                        }
                        else
                        {
                            path = "" + fichiers[0];
                            axAcroPDF1.LoadFile(folderpath + "/" + fichiers[0]);
                            tabControl1.SelectedTab = tabPage1;
                            ((Control)this.tabControl1.TabPages["tabPage1"]).Enabled = true;
                            ((Control)this.tabControl1.TabPages["tabPage2"]).Enabled = false;
                            vider();



                            //  MessageBox.Show(sql);
                            if (richTextBox1.Text != "")
                            {


                                string sql = "UPDATE Profile SET Historique   ='" + richTextBox1.Text + "' where Num = " + Numparticipant;

                                conn.Open();

                                OleDbCommand cmd = new OleDbCommand(sql, conn);
                                cmd.ExecuteNonQuery();

                            }
                        }
                    }
                }
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
       
             
           
        void mouve(string sourceFile ,string destinationFile )
        {
            try
            {
               /* string sourceFile = @"" + folderpath + "/" + file;
                string destinationFile = @"./PdfOutput/" + file;*/

                // To move a file or folder to a new location:
                System.IO.File.Move(@""+sourceFile, @""+destinationFile);
            }

            catch (Exception ex)
            {
                string errorinfo = "العملية لم تتم. خطأ رقم" + ex.HResult + "\n" + ex.ToString();

                Form5.Show(errorinfo, false);
                   
                
            }

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
                            historique = " كتابة وحفظ النص" + " " + text_type + "\n";

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
                                richTextBox1.Text += historique;
                                File.WriteAllText(Fname, textBox1.Text,Encoding.UTF8);
                                textBox1.Text = "";

                                //vider();
                                tabControl1.SelectedTab = tabPage2;

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


        private void button2_Click_3(object sender, EventArgs e)
        {
            Form5.Show("هل تريد فعلا غلق البرنامج؟", true);
            if (Form5.result == DialogResult.Yes)
                
            {
                Form5.Show("هل أتممت كتابة نصوص المشترك الحالي؟", true);
            if (Form5.result == DialogResult.Yes)
            {
                File.WriteAllText("Unfinished.txt", "");
                    DirectoryInfo dir = new DirectoryInfo(folderpath);
                    FileInfo[] fichiers = dir.GetFiles();

                    if (fichiers.Length == 0)
                    {
                        Form5.Show("لقد انتهيت من كتابة جميع الملفات.", false);
                        
                    }
                    else
                    {
                        path = "" + fichiers[0];


                        File.WriteAllText("unfinished.txt","");
                        if (richTextBox1.Text != "")
                        {
                            string sql = "UPDATE Profile SET Historique   ='" + richTextBox1.Text + "' where Num = " + Convert.ToInt32(textBox2.Text);
                            try
                            {
                                conn.ConnectionString = connection;
                                conn.Open();
                                OleDbCommand cmd = new OleDbCommand(sql, conn);
                                cmd.ExecuteNonQuery();
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
                        Close();
                                            }
                }
                else
                    Close();
            }
        }

       

        private void button5_Click(object sender, EventArgs e)
        {
            Form2 frm = new Form2();
            frm.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //string folderName = "";
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                folderpath = folderBrowserDialog1.SelectedPath;
                textBox3.Text = folderpath;
                refresh();
            }

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }
        public Boolean verify(string num)
        {
            conn.ConnectionString = connection;
            string requete = "select Num from Profile where Num=" + num ;




            try
            {

                conn.Open();

                OleDbCommand cmd = new OleDbCommand(requete, conn);
                cmd.ExecuteNonQuery();
                return cmd.ExecuteScalar() == null;
            }
            catch (Exception ex)
            {
                Form5.Show("العملية لم تتم. خطأ رقم" + ex.HResult, false);
               
            }
            finally
            {
                conn.Close();
            }
            return false;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (textBox2.Text!=string.Empty)
            { textBox1.Text += "[كلمة غير مفهومة]";
             textBox1.Focus(); 
                textBox1.Select(textBox1.Text.Length, 0);
            
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != string.Empty)
            {
                Form4 frm4 = new Form4(textBox2.Text);
                frm4.Show();
                 
            }

        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            if (textBox2.Text != string.Empty)
            {   textBox1.Text += "[معلومة شخصية]";
             textBox1.Focus(); textBox1.Select(textBox1.Text.Length, 0);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != string.Empty)
            { textBox1.Text += "[كلمة أو عبارة محذوفة]";
            textBox1.Focus(); textBox1.Select(textBox1.Text.Length, 0);
            }

        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != string.Empty)
            {
                textBox1.Text += "[كلمة أو عبارة مضافة]";
             textBox1.Focus(); textBox1.Select(textBox1.Text.Length, 0);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                refresh();
                string unfinished = File.ReadAllText("Unfinished.txt");

                if (unfinished != string.Empty)
                    Form5.Show("لم تقم بإتمام إدخال إنتاجات المشترك عدد" + unfinished + "\n أتمها أولا...!", false);
                if (Form5.result == DialogResult.Yes)
                {
                    Form3 frm3 = new Form3(Convert.ToInt32(unfinished));
                    frm3.Show();
                }
            }
            catch (Exception ex)
            {

                Form5.Show("العملية لم تتم. خطأ رقم" + ex.HResult, false);


            }
        }

        private void button12_Click(object sender, EventArgs e)
        {

            try
            {
                refresh();
                string unfinished = File.ReadAllText("Unfinished.txt");

                if (unfinished != string.Empty)
                    Form5.Show("لم تقم بإتمام إدخال إنتاجات المشترك عدد" + unfinished + "\n أتمها أولا...!", false);
                if (Form5.result == DialogResult.Yes)
                {
                    Form3 frm3 = new Form3(Convert.ToInt32(unfinished));
                    frm3.Show();
                }
            }
            catch (Exception ex)
            {

                Form5.Show("العملية لم تتم. خطأ رقم" + ex.HResult, false);


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
