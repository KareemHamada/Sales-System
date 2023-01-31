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
    public partial class Frm_TaxesReport : Form
    {
        public Frm_TaxesReport()
        {
            InitializeComponent();
        }
        Database db = new Database();
        DataTable tbl = new DataTable();
        private void Frm_TaxesReport_Load(object sender, EventArgs e)
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

            string sale = "فاتورة مبيعات", buy = "فاتورة مشتريات", returnSale = "مرتجعات مبيعات", returnBuy = "مرتجعات مشتريات";
            if (checkSale.Checked == true)
            {
                sale = "فاتورة مبيعات";
            }
            else
            {
                sale = "";
            }
            if (checkBuy.Checked == true)
            {
                buy = "فاتورة مشتريات";
            }
            else
            {
                buy = "";
            }
            if (checkSaleReturn.Checked == true)
            {
                returnSale = "مرتجعات مبيعات";
            }
            else
            {
                returnSale = "";
            }
            if (checkBuyReturn.Checked == true)
            {
                returnBuy = "مرتجعات مشتريات";
            }
            else
            {
                returnBuy = "";
            }

            tbl = db.readData("SELECT [Order_ID] as 'رقم العملية',[Order_Num] as 'رقم الفاتورة',[Order_Type] as 'نوع العملية',[Tax_Type] as 'نوع الضريبة',[Sup_Name] as 'اسم المورد',[Cust_Name] as 'اسم العميل',[Total_Order] as 'اجمالى الفاتورة قبل الضريبة',[Total_Tax] as 'اجمالى الضريبة',[Total_AfterTax] as 'اجمالى الفاتورة بعد الضريبة',[Date] as 'التاريخ' FROM [dbo].[Taxes_Report] where Order_Type in (N'" + sale + "' ,N'" + buy + "' ,N'" + returnSale + "' ,N'" + returnBuy + "') and Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "'", "");

            DgvSearch.DataSource = tbl;

            decimal totalPrice = 0, totalTax = 0, totalAfterTax = 0;
            for (int i = 0; i <= DgvSearch.Rows.Count - 1; i++)
            {
                totalPrice += Convert.ToDecimal(DgvSearch.Rows[i].Cells[6].Value);
                totalTax += Convert.ToDecimal(DgvSearch.Rows[i].Cells[7].Value);
                totalAfterTax += Convert.ToDecimal(DgvSearch.Rows[i].Cells[8].Value);
            }
            txtTotal.Text = Math.Round(totalPrice, 2).ToString();
            txtTotalTax.Text = Math.Round(totalTax, 2).ToString();
            txtTotalAfterTax.Text = Math.Round(totalAfterTax, 2).ToString();
        }

      
        private void PrintAll()
        {
            string date1;//dd/MM/yyyy
            string date2;
            date1 = DtpFrom.Value.ToString("yyyy-MM-dd");
            date2 = DtpTo.Value.ToString("yyyy-MM-dd");

            DataTable tblRpt = new DataTable();
            string sale = "فاتورة مبيعات", buy = "فاتورة مشتريات", returnSale = "مرتجعات مبيعات", returnBuy = "مرتجعات مشتريات";
            if (checkSale.Checked == true)
            {
                sale = "فاتورة مبيعات";
            }
            else
            {
                sale = "";
            }
            if (checkBuy.Checked == true)
            {
                buy = "فاتورة مشتريات";
            }
            else
            {
                buy = "";
            }
            if (checkSaleReturn.Checked == true)
            {
                returnSale = "مرتجعات مبيعات";
            }
            else
            {
                returnSale = "";
            }
            if (checkBuyReturn.Checked == true)
            {
                returnBuy = "مرتجعات مشتريات";
            }
            else
            {
                returnBuy = "";
            }
            tblRpt.Clear();
            tblRpt = db.readData("SELECT [Order_ID] as 'رقم العملية',[Order_Num] as 'رقم الفاتورة',[Order_Type] as 'نوع العملية',[Tax_Type] as 'نوع الضريبة',[Sup_Name] as 'اسم المورد',[Cust_Name] as 'اسم العميل',[Total_Order] as 'اجمالى الفاتورة قبل الضريبة',[Total_Tax] as 'اجمالى الضريبة',[Total_AfterTax] as 'اجمالى الفاتورة بعد الضريبة',[Date] as 'التاريخ' FROM [dbo].[Taxes_Report] where Order_Type in (N'" + sale + "' ,N'" + buy + "' ,N'" + returnSale + "' ,N'" + returnBuy + "') and Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "' ", "");
            try
            {
                Frm_Printing frm = new Frm_Printing();

                frm.crystalReportViewer1.RefreshReport();

                RptTaxesReport rpt = new RptTaxesReport();


                rpt.SetDatabaseLogon("", "", @".\SQLEXPRESS", "Sales_System");
                rpt.SetDataSource(tblRpt);

                rpt.SetParameterValue("From", date1);
                rpt.SetParameterValue("To", date2);
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
            catch (Exception) { }
        }

        private void PrintSummaryAll()
        
        {
            string date1;
            string date2;
            date1 = DtpFrom.Value.ToString("yyyy-MM-dd");
            date2 = DtpTo.Value.ToString("yyyy-MM-dd");

            DataTable tblRptSale = new DataTable();
            tblRptSale.Clear();
            tblRptSale = db.readData("select ISNULL( sum(Total_Order) , 0),ISNULL(sum (Total_Tax) ,0),ISNULL( Sum(Total_AfterTax) , 0 ) from Taxes_Report where Order_Type =N'فاتورة مبيعات' and Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "' ", "");

            DataTable tblRptBuy = new DataTable();
            tblRptBuy.Clear();
            tblRptBuy = db.readData("select ISNULL( sum(Total_Order) , 0),ISNULL(sum (Total_Tax) ,0),ISNULL( Sum(Total_AfterTax) , 0 ) from Taxes_Report where Order_Type =N'فاتورة مشتريات' and Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "' ", "");

            DataTable tblRptSaleReturn = new DataTable();
            tblRptSaleReturn.Clear();
            tblRptSaleReturn = db.readData("select ISNULL( sum(Total_Order) , 0),ISNULL(sum (Total_Tax) ,0),ISNULL( Sum(Total_AfterTax) , 0 ) from Taxes_Report where Order_Type =N'مرتجعات مبيعات' and Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "' ", "");

            DataTable tblRptBuyReturn = new DataTable();
            tblRptBuyReturn.Clear();
            tblRptBuyReturn = db.readData("select ISNULL( sum(Total_Order) , 0),ISNULL(sum (Total_Tax) ,0),ISNULL( Sum(Total_AfterTax) , 0 ) from Taxes_Report where Order_Type =N'مرتجعات مشتريات' and Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "' ", "");

            decimal Sale_Total_Order = 0;
            decimal Sale_Total_Tax = 0;
            decimal Sale_Total_AfterTax = 0;

            decimal Buy_Total_Order = 0;
            decimal Buy_Total_Tax = 0;
            decimal Buy_Total_AfterTax = 0;

            decimal Buy_Return_Total_Order = 0;
            decimal Buy_Return_Total_Tax = 0;
            decimal Buy_Return_Total_AfterTax = 0;

            decimal Sale_Return_Total_Order = 0;
            decimal Sale_Return_Total_Tax = 0;
            decimal Sale_Return_Total_AfterTax = 0;
            if (tblRptBuyReturn.Rows[0][0].ToString() != DBNull.Value.ToString())
            {
                Buy_Return_Total_Order = Convert.ToDecimal(tblRptBuyReturn.Rows[0][0]);
            }
            if (tblRptBuyReturn.Rows[0][1].ToString() != DBNull.Value.ToString())
            {
                Buy_Return_Total_Tax = Convert.ToDecimal(tblRptBuyReturn.Rows[0][1]);
            }
            if (tblRptBuyReturn.Rows[0][2].ToString() != DBNull.Value.ToString())
            {
                Buy_Return_Total_AfterTax = Convert.ToDecimal(tblRptBuyReturn.Rows[0][2]);
            }



            if (tblRptSaleReturn.Rows[0][0].ToString() != DBNull.Value.ToString())
            {
                Sale_Return_Total_Order = Convert.ToDecimal(tblRptSaleReturn.Rows[0][0]);
            }
            if (tblRptSaleReturn.Rows[0][1].ToString() != DBNull.Value.ToString())
            {
                Sale_Return_Total_Tax = Convert.ToDecimal(tblRptSaleReturn.Rows[0][1]);
            }
            if (tblRptSaleReturn.Rows[0][2].ToString() != DBNull.Value.ToString())
            {
                Sale_Return_Total_AfterTax = Convert.ToDecimal(tblRptSaleReturn.Rows[0][2]);
            }



            if (tblRptBuy.Rows[0][0].ToString() != DBNull.Value.ToString())
            {
                Buy_Total_Order = Convert.ToDecimal(tblRptBuy.Rows[0][0]);
            }
            if (tblRptBuy.Rows[0][1].ToString() != DBNull.Value.ToString())
            {
                Buy_Total_Tax = Convert.ToDecimal(tblRptBuy.Rows[0][1]);
            }
            if (tblRptBuy.Rows[0][2].ToString() != DBNull.Value.ToString())
            {
                Buy_Total_AfterTax = Convert.ToDecimal(tblRptBuy.Rows[0][2]);
            }

            if (tblRptSale.Rows[0][0].ToString() != DBNull.Value.ToString())
            {
                Sale_Total_Order = Convert.ToDecimal(tblRptSale.Rows[0][0]);
            }
            if (tblRptSale.Rows[0][1].ToString() != DBNull.Value.ToString())
            {
                Sale_Total_Tax = Convert.ToDecimal(tblRptSale.Rows[0][1]);
            }
            if (tblRptSale.Rows[0][2].ToString() != DBNull.Value.ToString())
            {
                Sale_Total_AfterTax = Convert.ToDecimal(tblRptSale.Rows[0][2]);

            }
            decimal safy = (Sale_Total_Tax + Buy_Return_Total_Tax) - (Buy_Total_Tax + Sale_Return_Total_Tax);
            safy = Math.Round(safy, 2);
            string formula = "";
            if(safy < 0)
            {
                formula = "المبلغ المستحق لك " + Math.Abs(safy).ToString();
            }
            else if(safy == 0)
            {
                formula = "لا يوجد اي مبالغ مستحقة";
            }
            else
            {
                formula = "المبلغ المستحق عليك " + safy.ToString();
            }
            try
            {
                Frm_Printing frm = new Frm_Printing();

                frm.crystalReportViewer1.RefreshReport();

                RptTaxesReportAllsammury rpt = new RptTaxesReportAllsammury();


                rpt.SetDatabaseLogon("", "", @".\SQLEXPRESS", "Sales_System");
                //rpt.SetDataSource(tblRptSale);
                rpt.SetParameterValue("From", date1);
                rpt.SetParameterValue("To", date2);

                rpt.SetParameterValue("safy", formula);

                rpt.SetParameterValue("Buy_Return_Total_Order", Math.Round(Buy_Return_Total_Order, 2));
                rpt.SetParameterValue("Buy_Return_Total_Tax", Math.Round(Buy_Return_Total_Tax, 2));
                rpt.SetParameterValue("Buy_Return_Total_AfterTax", Math.Round(Buy_Return_Total_AfterTax, 2));

                rpt.SetParameterValue("Sale_Return_Total_Order", Math.Round(Sale_Return_Total_Order, 2));
                rpt.SetParameterValue("Sale_Return_Total_Tax", Math.Round(Sale_Return_Total_Tax, 2));
                rpt.SetParameterValue("Sale_Return_Total_AfterTax", Math.Round(Sale_Return_Total_AfterTax, 2));

                rpt.SetParameterValue("Buy_Total_Order", Math.Round(Buy_Total_Order, 2));
                rpt.SetParameterValue("Buy_Total_Tax", Math.Round(Buy_Total_Tax, 2));
                rpt.SetParameterValue("Buy_Total_AfterTax", Math.Round(Buy_Total_AfterTax, 2));

                rpt.SetParameterValue("Sale_Total_Order", Math.Round(Sale_Total_Order, 2));
                rpt.SetParameterValue("Sale_Total_Tax", Math.Round(Sale_Total_Tax, 2));
                rpt.SetParameterValue("Sale_Total_AfterTax", Math.Round(Sale_Total_AfterTax, 2));

                //rpt.SetParameterValue("@ToBuy", date2);
                //rpt.SetParameterValue("@FromReturnSale", date1);
                //rpt.SetParameterValue("@ToReturnSale", date2);
                //rpt.SetParameterValue("@FromReturnBuy", date1);
                //rpt.SetParameterValue("@ToReturnBuy", date2);
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
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private void PrintSummary()
        {
            string date1;
            string date2;
            date1 = DtpFrom.Value.ToString("yyyy-MM-dd");
            date2 = DtpTo.Value.ToString("yyyy-MM-dd");

            DataTable tblRpt = new DataTable();

            tblRpt.Clear();

            tblRpt = db.readData("select ISNULL( sum(Total_Order) , 0) as 'اجمالى فواتير المبيعات' ,ISNULL(sum (Total_Tax) ,0) as 'قيمه الضرائب مبيعات' ,ISNULL( Sum(Total_AfterTax) , 0 ) as 'السعر بعد الضرائب مبيعات' from Taxes_Report where Order_Type =N'فاتورة مبيعات' and Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "' ", "");
            try
            {
                Frm_Printing frm = new Frm_Printing();

                frm.crystalReportViewer1.RefreshReport();

                RptTaxesReportsammury rpt = new RptTaxesReportsammury();


                rpt.SetDatabaseLogon("", "", @".\SQLEXPRESS", "Sales_System");
                rpt.SetDataSource(tblRpt);

                rpt.SetParameterValue("@FromSale", date1);
                rpt.SetParameterValue("@ToSale", date2);
                rpt.SetParameterValue("@FromBuy", date1);
                rpt.SetParameterValue("@ToBuy", date2);
                rpt.SetParameterValue("@FromReturnSale", date1);
                rpt.SetParameterValue("@ToReturnSale", date2);
                rpt.SetParameterValue("@FromReturnBuy", date1);
                rpt.SetParameterValue("@ToReturnBuy", date2);
                frm.crystalReportViewer1.ReportSource = rpt;

                System.Drawing.Printing.PrintDocument printDocument = new System.Drawing.Printing.PrintDocument();
                rpt.PrintOptions.PrinterName = printDocument.PrinterSettings.PrinterName;
                // rpt.PrintToPrinter(1, true, 0, 0);
                frm.ShowDialog();
            }
            catch (Exception) { }
        }


    

        private void btnPrintSummary_Click(object sender, EventArgs e)
        {
            if (DgvSearch.Rows.Count >= 1)
            {

                PrintSummaryAll();
            }
        }

        private void btnPrintReport_Click(object sender, EventArgs e)
        {
            if (DgvSearch.Rows.Count >= 1)
            {

                PrintAll();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (DgvSearch.Rows.Count >= 1)
            {
                if (MessageBox.Show("هل انتا متاكد من مسح البيانات", "تاكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    db.executeData("delete from Taxes_Report where Order_ID=" + DgvSearch.CurrentRow.Cells[0].Value + "", "تم المسح بنجاح", "");

                    btnSearch_Click(null, null);
                }
            }
        }
    }
}
