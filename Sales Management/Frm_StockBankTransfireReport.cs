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
    public partial class Frm_StockBankTransfireReport : Form
    {
        public Frm_StockBankTransfireReport()
        {
            InitializeComponent();
        }
        Database db = new Database();
        DataTable tbl = new DataTable();
        private void Frm_StockBankTransfireReport_Load(object sender, EventArgs e)
        {
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
            //if (rbtnAll.Checked == true)
            //{
            //    tbl = db.readData("SELECT[Order_ID] as 'رقم العملية',[Money] as 'المبلغ' ,[Date] as 'التاريخ',[From_] as 'تحويل من',[To_] as 'تحويل الى',[Name] as 'اسم الشخص المسؤل عن التحويل' FROM  [dbo].[StockBank_Transfire] where Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "' ", "");
            //}
            if (rbtnFromStocktoBank.Checked)
            {
                tbl = db.readData("SELECT Distinct [Order_ID] as 'رقم العملية',[StockBank_Transfire].[Money] as 'المبلغ' ,[Date] as 'التاريخ',(select Stock_Name from Stock_Data where Stock_Data.Stock_ID = StockBank_Transfire.From_) as 'تحويل من',[To_] as 'تحويل الى',[Name] as 'اسم الشخص المسؤل عن التحويل' FROM [dbo].[StockBank_Transfire],Stock where To_ = N'البنك' and Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "' ", "");

            }
            else if (rbtnFromBanktoStock.Checked)
            {
                tbl = db.readData("SELECT Distinct [Order_ID] as 'رقم العملية',[StockBank_Transfire].[Money] as 'المبلغ' ,[Date] as 'التاريخ',[From_] as 'تحويل من',(select Stock_Name from Stock_Data where Stock_Data.Stock_ID = StockBank_Transfire.To_) as 'تحويل الى',[Name] as 'اسم الشخص المسؤل عن التحويل' FROM [dbo].[StockBank_Transfire] where From_ = N'البنك' and Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "' ", "");
            }
            if (tbl.Rows.Count >= 1)
            {
                DgvSearch.DataSource = tbl;
                decimal Sum = 0;
                for (int i = 0; i <= tbl.Rows.Count - 1; i++)
                {
                    Sum += Convert.ToDecimal(tbl.Rows[i][1]);
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
                if (rbtnFromStocktoBank.Checked)
                {
                    db.executeData("delete from StockBank_Transfire where To_ = N'البنك' and Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "'", "تم مسح البيانات بنجاح", "");
                }
                else
                {
                    db.executeData("delete from StockBank_Transfire where From_ = N'البنك' and Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "'", "تم مسح البيانات بنجاح", "");
                }
                

                btnSearch_Click(null, null);
                //tbl.Clear();
                //DgvSearch.DataSource = tbl;
            }
        }
    }
}
