using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Sales_Management
{
    public partial class Frm_Login : Form
    {
        //Thread t;
        public Frm_Login()
        {
            InitializeComponent();
            //try
            //{
            //    t = new Thread(new ThreadStart(StartSplash));
            //    t.Start();
            //    Thread.Sleep(5000);
            //    t.Abort();
            //}
            //catch (Exception) { }
        }
        //private void StartSplash()
        //{
        //    try
        //    {
        //        Application.Run(new Splash());
        //    }
        //    catch (Exception) { }
        //}


        Database db = new Database();
        DataTable tbl = new DataTable();

        // to check if db exists or not
        private bool checkDatabase()
        {
            SqlConnection conn = new SqlConnection(@"Data Source=.\SQLEXPRESS;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("",conn);
            SqlDataReader rdr;
            try
            {
                cmd.CommandText = "exec sys.sp_databases";
                conn.Open();
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    if (rdr.GetString(0) == "Sales_System")
                    {
                        return true;
                        break;
                    }
                }
            }
            catch (Exception) {
                return false;
            }
            
            conn.Close();
            rdr.Dispose();
            cmd.Dispose();
            return false;
        }
        // to create db if not exists
        private void createDatabase()
        {
            bool check = checkDatabase();
            if(check == false)
            {

                try
                {
                    var fileContent = File.ReadAllText(Application.StartupPath + @"\Sales_System_Script.sql");
                    var sqlqueries = fileContent.Split(new[] { "GO" }, StringSplitOptions.RemoveEmptyEntries);
                    var con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Integrated Security=True");
                    var cmd = new SqlCommand("query", con);
                    con.Open();

                    foreach (var query in sqlqueries)
                    {
                        cmd.CommandText = query;
                        cmd.ExecuteNonQuery();
                    }

                    con.Close();
                }
                catch (Exception) { }

            }
        }
        private void Frm_Login_Load(object sender, EventArgs e)
        {
            try
            {
                createDatabase();
            }
            catch (Exception) { }
            txtUserName.Clear();
            txtUserName.Focus();
        }

        private void txtUserName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {

                txtPassword.Focus();
            }
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {

                btnLogin_Click(null, null);
            }
        }
        private bool Trial()
        {
            //Properties.Settings.Default.Reset();
            int num = Properties.Settings.Default.Trial;
            int thisNum = num + 1;
            Properties.Settings.Default.Trial = thisNum;
            Properties.Settings.Default.Save();
            int constTrial = 50;
            if(thisNum >= constTrial)
            {
                MessageBox.Show("هذه نسخة تجريبية تم انتهائها", "تاكيد",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return false;
            }
            else
            {
                int baky = constTrial - thisNum;
                MessageBox.Show("هذه نسخة تجريبية و متبقي لك عدد مرات تسجيل دخول " + baky , "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return true;
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if(Properties.Settings.Default.Product_Key == "No")
            {
                Frm_Serial frm = new Frm_Serial();
                frm.ShowDialog();
            }
            else
            {
                tbl.Clear();
                if (rbtnManager.Checked)
                    tbl = db.readData("select * from Users where User_Name=N'" + txtUserName.Text + "' and User_Password=N'" + txtPassword.Text + "' and Type=N'مدير'", "");
                else if (rbtnEmp.Checked)
                    tbl = db.readData("select * from Users where User_Name=N'" + txtUserName.Text + "' and User_Password=N'" + txtPassword.Text + "' and Type=N'مستخدم عادى'", "");
                if (tbl.Rows.Count <= 0)
                {
                    DataTable tblStock = new DataTable();
                    tblStock = db.readData("select * from Stock_Data", "");
                    if (tblStock.Rows.Count <= 0)
                    {
                        db.executeData("insert into Stock_Data Values (1,N'الخزنة الرئيسية') ", "", "");
                        db.executeData("insert into Stock Values (1,0) ", "", "");
                        db.executeData("insert into Bank Values(0)", "", "");
                    }
                    string type = "مدير";
                    db.executeData("insert into Users values (1 ,N'921' ,N'921',N'" + type + "',1,0)", "", "");
                    
                    db.executeData("insert into User_Setting Values (1, 1,1,1,1,1,1,1,1,1,1,1,1,1)", "", "");
                    db.executeData("insert into User_Customer Values (1, 1,1,1)", "", "");
                    db.executeData("insert into User_Supplier Values (1, 1,1,1)", "", "");
                    db.executeData("insert into User_Buy Values (1, 1,1,1)", "", "");
                    db.executeData("insert into User_Sale Values (1 , 1,1,1,1)", "", "");
                    db.executeData("insert into User_Return Values (1 , 1,1)", "", "");
                    db.executeData("insert into User_StockBank Values (1 , 1,1,1,1,1,1,1,1,1)", "", "");
                    db.executeData("insert into User_Emp Values (1 , 1,1,1,1,1,1,1)", "", "");
                    db.executeData("insert into User_Deserved Values (1 , 1,1,1,1)", "", "");
                    db.executeData("insert into User_BackUp Values (1, 1,1)", "", "");

                    tbl.Clear();
                    if (rbtnManager.Checked)
                        tbl = db.readData("select * from Users where User_Name=N'" + txtUserName.Text + "' and User_Password=N'" + txtPassword.Text + "' and Type=N'مدير'", "");
                    else if (rbtnEmp.Checked)
                        tbl = db.readData("select * from Users where User_Name=N'" + txtUserName.Text + "' and User_Password=N'" + txtPassword.Text + "' and Type=N'مستخدم عادى'", "");

                }
                if (tbl.Rows.Count >= 1)
                {
                    //bool check = Trial();
                    //if (check == false)
                    //{
                    //    return;
                    //}
                    Properties.Settings.Default.USERNAME = txtUserName.Text;
                    Properties.Settings.Default.Stock_ID = Convert.ToInt32(tbl.Rows[0][4]);
                    Properties.Settings.Default.User_ID = Convert.ToInt32(tbl.Rows[0][0]);
                    Properties.Settings.Default.Save();
                    this.Hide();
                    Form1 frm = new Form1();
                    frm.ShowDialog();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("كلمة السر او اسم المستخدم  خطا", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
