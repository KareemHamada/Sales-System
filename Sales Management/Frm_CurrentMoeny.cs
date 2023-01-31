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
    public partial class Frm_CurrentMoeny : Form
    {

        public Frm_CurrentMoeny()
        {
            InitializeComponent();
        }
        Database db = new Database();
        DataTable tbl = new DataTable();
        public void FillStock()
        {

            cbxStock.DataSource = db.readData("select * from Stock_Data", "");
            cbxStock.DisplayMember = "Stock_Name";
            cbxStock.ValueMember = "Stock_ID";
        }
        private void onLoadScreen()
        {
            //db.FillComboBox(cbxStock, "select * from Stock_Data", "Stock_Name", "Stock_ID");
            FillStock();
            tbl.Clear();
            DataTable tblBank = new DataTable();
            tblBank.Clear();
            tbl = db.readData("select * from Stock where Stock_ID=" + cbxStock.SelectedValue + "", "");
            if (tbl.Rows.Count <= 0)
            {
                db.executeData("insert into Stock values (" + cbxStock.SelectedValue + " , 0)", "", "");
                tbl = db.readData("select * from Stock where Stock_ID=" + cbxStock.SelectedValue + "", "");
            }
            if (Convert.ToDecimal(tbl.Rows[0][1]) <= 0)
            {
                lblMoneyStock.Text = "0";
            }
            else if (Convert.ToDecimal(tbl.Rows[0][1]) >= 1)
            {
                lblMoneyStock.Text = Convert.ToDecimal(tbl.Rows[0][1]) + "";
            }

            tblBank = db.readData("select * from Bank", "");
            if (Convert.ToDecimal(tblBank.Rows[0][0]) <= 0)
            {
                lblMoneyBank.Text = "0";
            }
            else if (Convert.ToDecimal(tblBank.Rows[0][0]) >= 1)
            {
                lblMoneyBank.Text = Convert.ToDecimal(tblBank.Rows[0][0]) + "";
            }

        }
        private void Frm_CurrentMoeny_Load(object sender, EventArgs e)
        {
            try
            {
                onLoadScreen();
            }
            catch (Exception) { }
        }

        private void cbxStock_SelectionChangeCommitted(object sender, EventArgs e)
        {
            tbl.Clear();
            tbl = db.readData("select * from Stock where Stock_ID=" + cbxStock.SelectedValue + "", "");
            if (tbl.Rows.Count <= 0)
            {
                db.executeData("insert into Stock values (" + cbxStock.SelectedValue + " , 0)", "", "");
                tbl = db.readData("select * from Stock where Stock_ID=" + cbxStock.SelectedValue + "", "");
            }
            if (Convert.ToDecimal(tbl.Rows[0][1]) <= 0)
            {
                lblMoneyStock.Text = "0";
            }
            else if (Convert.ToDecimal(tbl.Rows[0][1]) >= 1)
            {
                lblMoneyStock.Text = Convert.ToDecimal(tbl.Rows[0][1]) + "";
            }
        }

        //private void btnStock_Click(object sender, EventArgs e)
        //{
        //    Frm_StockAddMoney frm = new Frm_StockAddMoney();
        //    frm.ShowDialog();
        //}

        //private void btnbank_Click(object sender, EventArgs e)
        //{
        //    Frm_BankAddMoney frm = new Frm_BankAddMoney();
        //    frm.ShowDialog();
        //}
    }
}
