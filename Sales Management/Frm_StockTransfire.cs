using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sales_Management
{
    public partial class Frm_StockTransfire : Form
    {
        public Frm_StockTransfire()
        {
            InitializeComponent();
        }
        Database db = new Database();
        DataTable tbl = new DataTable();
     
        private void onLoadScreen()
        {
            db.FillComboBox(cbxStockFrom, "select * from Stock_Data", "Stock_Name", "Stock_ID");
            db.FillComboBox(cbxStockTo, "select * from Stock_Data", "Stock_Name", "Stock_ID");

            // stock from
            tbl.Clear();
            tbl = db.readData("select * from Stock where Stock_ID=" + cbxStockFrom.SelectedValue + "", "");
            if (tbl.Rows.Count <= 0)
            {
                db.executeData("insert into Stock values (" + cbxStockFrom.SelectedValue + " , 0)", "", "");
                tbl = db.readData("select * from Stock where Stock_ID=" + cbxStockFrom.SelectedValue + "", "");
            }
            if (Convert.ToDecimal(tbl.Rows[0][1]) <= 0)
            {
                lblMoney1.Text = "0";
            }
            else if (Convert.ToDecimal(tbl.Rows[0][1]) >= 1)
            {
                lblMoney1.Text = Convert.ToDecimal(tbl.Rows[0][1]) + "";
            }

            //stock to
            tbl.Clear();
            tbl = db.readData("select * from Stock where Stock_ID=" + cbxStockTo.SelectedValue + "", "");
            if (tbl.Rows.Count <= 0)
            {
                db.executeData("insert into Stock values (" + cbxStockTo.SelectedValue + " , 0)", "", "");
                tbl = db.readData("select * from Stock where Stock_ID=" + cbxStockTo.SelectedValue + "", "");
            }
            if (Convert.ToDecimal(tbl.Rows[0][1]) <= 0)
            {
                lblMoney2.Text = "0";
            }
            else if (Convert.ToDecimal(tbl.Rows[0][1]) >= 1)
            {
                lblMoney2.Text = Convert.ToDecimal(tbl.Rows[0][1]) + "";
            }

            NudPrice.Value = 1;
            txtName.Clear();
            txtreason.Clear();
            //try
            //{
            //    cbxStockFrom.SelectedIndex = 0;
            //    cbxStockTo.SelectedIndex = 0;
            //}
            //catch { }

        }
        private void Frm_StockTransfire_Load(object sender, EventArgs e)
        {
            try
            {
                onLoadScreen();
            }
            catch (Exception) { }
            
        }

        private void fromstock1()
        {
            tbl.Clear();
            tbl = db.readData("select * from Stock where Stock_ID=" + cbxStockFrom.SelectedValue + "", "");
            if (tbl.Rows.Count <= 0)
            {
                db.executeData("insert into Stock values (" + cbxStockFrom.SelectedValue + " , 0)", "", "");
                tbl = db.readData("select * from Stock where Stock_ID=" + cbxStockFrom.SelectedValue + "", "");
            }
            if (Convert.ToDecimal(tbl.Rows[0][1]) <= 0)
            {
                lblMoney1.Text = "0";
            }
            else if (Convert.ToDecimal(tbl.Rows[0][1]) >= 1)
            {
                lblMoney1.Text = Convert.ToDecimal(tbl.Rows[0][1]) + "";
            }
            NudPrice.Value = 1;
            txtName.Clear();
            txtreason.Clear();
        }
        private void tostock2()
        {
            tbl.Clear();
            tbl = db.readData("select * from Stock where Stock_ID=" + cbxStockTo.SelectedValue + "", "");
            if (tbl.Rows.Count <= 0)
            {
                db.executeData("insert into Stock values (" + cbxStockTo.SelectedValue + " , 0)", "", "");
                tbl = db.readData("select * from Stock where Stock_ID=" + cbxStockTo.SelectedValue + "", "");
            }
            if (Convert.ToDecimal(tbl.Rows[0][1]) <= 0)
            {
                lblMoney2.Text = "0";
            }
            else if (Convert.ToDecimal(tbl.Rows[0][1]) >= 1)
            {
                lblMoney2.Text = Convert.ToDecimal(tbl.Rows[0][1]) + "";
            }
            NudPrice.Value = 1;
            txtName.Clear();
            txtreason.Clear();
        }

        private void cbxStockFrom_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                fromstock1();
            }
            catch (Exception) { }
        }

        private void cbxStockTo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                tostock2();
            }
            catch (Exception) { }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string d = DtpDate.Value.ToString("dd/MM/yyyy");
                if (cbxStockTo.Items.Count <= 0) { 
                    MessageBox.Show("من فضلك املئ بيانات الخزنات اولا", "تاكيد"); 
                    return; 
                }
                if (Convert.ToInt32(cbxStockFrom.SelectedValue) == Convert.ToInt32(cbxStockTo.SelectedValue)) 
                { 
                    MessageBox.Show("لا يمكن تحويل رصيد لنفس الخزنة", "تاكيد"); 
                    return; 
                }

                if (NudPrice.Value > Convert.ToDecimal(lblMoney1.Text))
                { 
                    MessageBox.Show("الرصيد الموجود فى الخزنة لا يكفى للتحويل", "تاكيد"); 
                    return; 
                }
                if (NudPrice.Value <= 0)
                {
                    MessageBox.Show("ادخل مبلغ اكبر من 0", "تاكيد");
                    return;
                }
                if (txtName.Text == "")
                {
                    MessageBox.Show("من فضلك ادخل اسم الشخص المسؤل عن التحويل", "تاكيد"); 
                    return;
                }
                if (cbxStockFrom.SelectedValue == null || cbxStockTo.SelectedValue == null)
                {
                    MessageBox.Show("من فضلك اختر خزنة صحيحة", "تاكيد");
                    return;
                }
            
                db.executeData("update Stock set Money=Money - " + NudPrice.Value + " where Stock_ID=" + cbxStockFrom.SelectedValue + "", "", "");

                db.executeData("update Stock set Money=Money + " + NudPrice.Value + " where Stock_ID=" + cbxStockTo.SelectedValue + "", "", "");

                db.executeData("insert into Stock_Pull (Stock_ID , Money ,Date ,Name ,Type ,Reason) values (" + cbxStockFrom.SelectedValue + " ," + NudPrice.Value + " ,N'" + d + "' ,N'" + txtName.Text + "' ,N'تحويل الى خزنة', N'" + txtreason.Text + "') ", "", "");

                db.executeData("insert into Stock_Insert (Stock_ID , Money ,Date ,Name ,Type ,Reason) values (" + cbxStockFrom.SelectedValue + " ," + NudPrice.Value + " ,N'" + d + "' ,N'" + txtName.Text + "' ,N'تحويل من خزنة اخرى', N'" + txtreason.Text + "') ", "", "");

                db.executeData("insert into Stock_Transfire ( Money ,Date ,From_ ,To_ ,Name ,Reason) values (" + NudPrice.Value + " ,N'" + d + "' ," + cbxStockFrom.SelectedValue + " , " + cbxStockTo.SelectedValue + ",N'" + txtName.Text + "' , N'" + txtreason.Text + "') ", "تمت عملية التحويل بنجاح", "");

                onLoadScreen();
            }
            catch (Exception) { }
        }
    }
}
