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
    public partial class Frm_StockPullMoneyReport : Form
    {
        public Frm_StockPullMoneyReport()
        {
            InitializeComponent();
        }
        Database db = new Database();
        DataTable tbl = new DataTable();
        private void Frm_StockPullMoneyReport_Load(object sender, EventArgs e)
        {
            try
            {
                db.FillComboBox(cbxStock, "select * from Stock_Data", "Stock_Name", "Stock_ID");
            }
            catch (Exception) { }
            DtpFrom.Text = DateTime.Now.ToShortDateString();
            DtpTo.Text = DateTime.Now.ToShortDateString();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string date1;
            string date2;
            date1 = DtpFrom.Value.ToString("yyyy-MM-dd");
            date2 = DtpTo.Value.ToString("yyyy-MM-dd");
            tbl.Clear();
            if (rbtnAllStock.Checked)
            {
                tbl = db.readData("SELECT [Order_ID] as 'رقم العملية',Stock_Data.Stock_Name as 'اسم الخزنة',[Money] as 'المبلغ',[Date] as 'تاريخ العملية',[Name] as 'المسؤل عن السحب' ,[Type] as 'نوع السحب',[Reason] as 'السبب' FROM [dbo].[Stock_Pull],Stock_Data where Stock_Data.Stock_ID =[Stock_Pull].Stock_ID and Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "' ", "");
            }
            else
            {
                if(cbxStock.Items.Count > 0)
                {
                    if (cbxStock.SelectedValue == null)
                    {
                        MessageBox.Show("من فضلك اختر خزنة صحيحة", "تاكيد");
                        return;
                    }
                    tbl = db.readData("SELECT [Order_ID] as 'رقم العملية',Stock_Data.Stock_Name as 'اسم الخزنة',[Money] as 'المبلغ',[Date] as 'تاريخ العملية',[Name] as 'المسؤل عن السحب' ,[Type] as 'نوع السحب',[Reason] as 'السبب' FROM [dbo].[Stock_Pull],Stock_Data where Stock_Data.Stock_ID =[Stock_Pull].Stock_ID and [Stock_Pull].Stock_ID="+cbxStock.SelectedValue+" and Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "' ", "");
                }
            }
            

            if (tbl.Rows.Count >= 1)
            {
                DgvSearch.DataSource = tbl;
                decimal Sum = 0;
                for (int i = 0; i <= tbl.Rows.Count - 1; i++)
                {
                    Sum += Convert.ToDecimal(tbl.Rows[i][2]);
                }

                txtTotal.Text = Math.Round(Sum, 2).ToString();
            }
            else
            { txtTotal.Text = "0"; }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string date1;
            string date2;
            date1 = DtpFrom.Value.ToString("yyyy-MM-dd");
            date2 = DtpTo.Value.ToString("yyyy-MM-dd");
            if (MessageBox.Show("هل انتا متاكد من مسح البيانات", "تاكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (rbtnAllStock.Checked)
                {
                    db.executeData("delete from Stock_Pull where Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "'", "تم مسح البيانات بنجاح", "");
                }
                else
                {
                    if(cbxStock.Items.Count > 0)
                    {
                        db.executeData("delete from Stock_Pull where [Stock_Pull].Stock_ID=" + cbxStock.SelectedValue + " and Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "'", "تم مسح البيانات بنجاح", "");
                    }
                }

                btnSearch_Click(null, null);
                //tbl.Clear();
                //DgvSearch.DataSource = tbl;
            }


        }
    }
}
