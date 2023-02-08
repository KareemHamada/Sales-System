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
    public partial class Frm_CustomerMoney : Form
    {
        public Frm_CustomerMoney()
        {
            InitializeComponent();
        }
        Database db = new Database();
        DataTable tbl = new DataTable();
        string stock_ID = "";
        private void Frm_CustomerMoney_Load(object sender, EventArgs e)
        {

            try
            {
                db.FillComboBox(cbxCustomer, "select * from Customers", "Cust_Name", "Cust_ID");
            }
            catch (Exception) { }

            //DtpDate.Text = DateTime.Now.ToShortDateString();
            //tbl.Clear();
            //tbl = db.readData("SELECT [Order_ID] as 'رقم الفاتورة',[Cus_Name] as 'العميل',[Price] as 'المبلغ المستحق',[Order_Date] as 'تاريخ الفاتورة',[Reminder_Date] as 'تاريخ الاستحقاق' FROM [dbo].[Customer_Money] ", "");

            //DgvSearch.DataSource = tbl;

            //decimal totalPrice = 0;
            //for (int i = 0; i <= DgvSearch.Rows.Count - 1; i++)
            //{
            //    totalPrice += Convert.ToDecimal(DgvSearch.Rows[i].Cells[2].Value);

            //}
            //txtTotal.Text = Math.Round(totalPrice, 2).ToString();
            txtTotal.Text = "0";
            stock_ID = Convert.ToString(Properties.Settings.Default.Stock_ID);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            tbl.Clear();

            if (rbtnAllCust.Checked)
            {
                tbl = db.readData("SELECT [Order_ID] as 'رقم الفاتورة',[Cus_Name] as 'العميل',[Price] as 'المبلغ المستحق',[Order_Date] as 'تاريخ الفاتورة',[Reminder_Date] as 'تاريخ الاستحقاق' FROM [dbo].[Customer_Money] ", "");
            }
            else if (rbtnOneCustomer.Checked)
            {
                tbl = db.readData("SELECT [Order_ID] as 'رقم الفاتورة',[Cus_Name] as 'العميل',[Price] as 'المبلغ المستحق',[Order_Date] as 'تاريخ الفاتورة',[Reminder_Date] as 'تاريخ الاستحقاق' FROM [dbo].[Customer_Money] where Cus_Name=N'" + cbxCustomer.Text + "' ", "");

            }
            DgvSearch.DataSource = tbl;

            decimal totalPrice = 0;
            for (int i = 0; i <= DgvSearch.Rows.Count - 1; i++)
            {
                totalPrice += Convert.ToDecimal(DgvSearch.Rows[i].Cells[2].Value);

            }
            txtTotal.Text = Math.Round(totalPrice, 2).ToString();
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            if (DgvSearch.Rows.Count >= 1)
            {

                string d = DateTime.Now.ToString("dd/MM/yyyy");

                if (rbtnPayAll.Checked)
                {
                    if (MessageBox.Show("هل انت متاكد من تسديد المبلغ", "تاكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        if (rbtnAllCust.Checked) { 
                            MessageBox.Show("من فضلك حدد اسم عميل", "تاكيد");
                            return; 
                        }

                        db.executeData("delete from Customer_Money where Order_ID=" + DgvSearch.CurrentRow.Cells[0].Value + " and Price =" + DgvSearch.CurrentRow.Cells[2].Value + "", "", "");

                        db.executeData("insert into Customer_Report values (" + DgvSearch.CurrentRow.Cells[0].Value + " , " + DgvSearch.CurrentRow.Cells[2].Value + " , '" + d + "' , N'" + cbxCustomer.Text + "')", "تم تسديد المبلغ بنجاح", "");

                        // update stock
                        db.executeData("insert into Stock_Insert (Stock_ID , Money ,Date ,Name ,Type ,Reason) values (" + stock_ID + " ," + DgvSearch.CurrentRow.Cells[2].Value + " ,N'" + d + "' ,N'" + Properties.Settings.Default.USERNAME + "' ,N'مستحقات من عملاء', N'') ", "", "");
                        db.executeData("update stock set Money=Money + " + DgvSearch.CurrentRow.Cells[2].Value + " where Stock_ID=" + stock_ID + "", "", "");

                        btnSearch_Click(null, null);

                    }
                }
                else if (rbtnPayPart.Checked)
                {

                    if (MessageBox.Show("هل انت متاكد من تسديد المبلغ", "تاكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        if (rbtnAllCust.Checked) { 
                            MessageBox.Show("من فضلك حدد اسم عميل", "تاكيد");
                            return; 
                        }

                        decimal money = Convert.ToDecimal(DgvSearch.CurrentRow.Cells[2].Value) - NudPrice.Value;
                        db.executeData("update Customer_Money set Price=" + money + " where Order_ID=" + DgvSearch.CurrentRow.Cells[0].Value + " and Price=" + DgvSearch.CurrentRow.Cells[2].Value + "", "", "");
                        db.executeData("insert into Customer_Report values (" + DgvSearch.CurrentRow.Cells[0].Value + " , " + NudPrice.Value + " , '" + d + "' , N'" + cbxCustomer.Text + "')", "تم تسديد المبلغ بنجاح", "");
                        
                        // update stock
                        db.executeData("insert into Stock_Insert (Stock_ID , Money ,Date ,Name ,Type ,Reason) values (" + stock_ID + " ," + NudPrice.Value + " ,N'" + d + "' ,N'" + Properties.Settings.Default.USERNAME + "' ,N'مستحقات من عملاء', N'') ", "", "");
                        db.executeData("update stock set Money=Money + " + NudPrice.Value + " where Stock_ID=" + stock_ID + "", "", "");

                        btnSearch_Click(null, null);

                    }
                }
            }
        }

        private void PrintOneCustomer()
        {
            string name = Convert.ToString(cbxCustomer.Text);
            DataTable tblRpt = new DataTable();

            tblRpt.Clear();
            tblRpt = db.readData("SELECT [Order_ID] as 'رقم الفاتورة',[Cus_Name] as 'العميل',[Price] as 'المبلغ المستحق',[Order_Date] as 'تاريخ الفاتورة',[Reminder_Date] as 'تاريخ الاستحقاق' FROM [dbo].[Customer_Money] where Cus_Name=N'" + name + "'", "");
            try
            {
                Frm_Printing frm = new Frm_Printing();

                frm.crystalReportViewer1.RefreshReport();
                if (Properties.Settings.Default.SalePrintKind == "8CM")
                {
                    RptCustomerMoney rpt = new RptCustomerMoney();


                    rpt.SetDatabaseLogon("", "", @".\SQLEXPRESS", "Sales_System");
                    rpt.SetDataSource(tblRpt);
                    rpt.SetParameterValue("Name", name);
                    frm.crystalReportViewer1.ReportSource = rpt;

                    System.Drawing.Printing.PrintDocument printDocument = new System.Drawing.Printing.PrintDocument();
                    rpt.PrintOptions.PrinterName = printDocument.PrinterSettings.PrinterName;
                    if (Properties.Settings.Default.ShowBeforePrint)
                    {
                        frm.ShowDialog();
                    }
                    else
                    {
                        rpt.PrintToPrinter(1, true, 0, 0);
                    }
                }
                else if (Properties.Settings.Default.SalePrintKind == "A4")
                {
                    RptCustomerMoneyA4 rpt = new RptCustomerMoneyA4();
                    rpt.SetDatabaseLogon("", "", @".\SQLEXPRESS", "Sales_System");
                    rpt.SetDataSource(tblRpt);
                    rpt.SetParameterValue("Name", name);
                    frm.crystalReportViewer1.ReportSource = rpt;

                    System.Drawing.Printing.PrintDocument printDocument = new System.Drawing.Printing.PrintDocument();
                    rpt.PrintOptions.PrinterName = printDocument.PrinterSettings.PrinterName;
                    if (Properties.Settings.Default.ShowBeforePrint)
                    {
                        frm.ShowDialog();
                    }
                    else
                    {
                        rpt.PrintToPrinter(1, true, 0, 0);
                    }
                }
                
            }
            catch (Exception) { }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (rbtnOneCustomer.Checked)
            {
                if (DgvSearch.Rows.Count >= 1)
                {
                    PrintOneCustomer();

                }
            }
            else
            {
                MessageBox.Show("اختر عميل محدد");
            }
        }
    }
}
