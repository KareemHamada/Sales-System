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
    public partial class Frm_SupplierMoney : Form
    {
        public Frm_SupplierMoney()
        {
            InitializeComponent();
        }
        Database db = new Database();
        DataTable tbl = new DataTable();
        string stock_ID = "";
        private void Frm_SupplierMoney_Load(object sender, EventArgs e)
        {
            stock_ID = Convert.ToString(Properties.Settings.Default.Stock_ID);
            try
            {
                db.FillComboBox(cbxSupplier, "select * from Suppliers where CurrentState=1", "Sup_Name", "Sup_ID");
            }
            catch (Exception) { }

            txtTotal.Text = "0";
            //tbl.Clear();
            //tbl = db.readData("SELECT [Order_ID] as 'رقم الفاتورة',Suppliers.Sup_Name as 'اسم المورد',[Price] as 'السعر',[Order_Date] as 'تاريخ الفاتورة',[Reminder_Date] as 'تاريخ الاستحقاق' FROM [dbo].[Supplier_Money],Suppliers where Suppliers.Sup_ID =[Supplier_Money].Sup_ID", "");

            //DgvSearch.DataSource = tbl;

            //decimal totalPrice = 0;
            //for (int i = 0; i <= DgvSearch.Rows.Count - 1; i++)
            //{
            //    totalPrice += Convert.ToDecimal(DgvSearch.Rows[i].Cells[2].Value);

            //}
            //txtTotal.Text = Math.Round(totalPrice, 2).ToString();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            
            tbl.Clear();

            if (rbtnAllSup.Checked)
            {
                tbl = db.readData("SELECT [Order_ID] as 'رقم الفاتورة',Suppliers.Sup_Name as 'اسم المورد',[Price] as 'السعر',[Order_Date] as 'تاريخ الفاتورة',[Reminder_Date] as 'تاريخ الاستحقاق' FROM [dbo].[Supplier_Money],Suppliers where Suppliers.Sup_ID =[Supplier_Money].Sup_ID", "");
            }
            else if (rbtnOneSupplier.Checked)
            {
                if (cbxSupplier.SelectedValue == null)
                {
                    MessageBox.Show("من فضلك اختر مورد صحيح");
                    return;
                }
                tbl = db.readData("SELECT [Order_ID] as 'رقم الفاتورة',Suppliers.Sup_Name as 'اسم المورد',[Price] as 'السعر',[Order_Date] as 'تاريخ الفاتورة',[Reminder_Date] as 'تاريخ الاستحقاق' FROM [dbo].[Supplier_Money],Suppliers where Suppliers.Sup_ID =[Supplier_Money].Sup_ID and Suppliers.Sup_ID=" + cbxSupplier.SelectedValue + " ", "");


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
                //MessageBox.Show(DateTime.Now.ToString("dd/MM/yyyy"));
                //MessageBox.Show(d);
                if (rbtnAllSup.Checked)
                {
                    MessageBox.Show("من فضلك حدد اسم مورد", "تاكيد");
                    return;
                }
                if (cbxSupplier.SelectedValue == null)
                {
                    MessageBox.Show("من فضلك اختر مورد صحيح");
                    return;
                }

                decimal stock_Money = 0;
                DataTable tblStock = new DataTable();
                tblStock.Clear();
                tblStock = db.readData("select * from Stock where Stock_ID=" + stock_ID + "", "");

                stock_Money = Convert.ToDecimal(tblStock.Rows[0][1]);
                //MessageBox.Show(DgvSearch.CurrentRow.Cells[2].Value.ToString());
                if (rbtnPayAll.Checked)
                {   
                    if (Convert.ToDecimal(DgvSearch.CurrentRow.Cells[2].Value) > stock_Money)
                    {
                        MessageBox.Show("المبلغ الموجود فى الخزنة غير كافى لاجراء العملية", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (MessageBox.Show("هل انت متاكد من تسديد المبلغ", "تاكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        
                        db.executeData("delete from Supplier_Money where Order_ID=" + DgvSearch.CurrentRow.Cells[0].Value + " and Price =" + DgvSearch.CurrentRow.Cells[2].Value + "", "", "");

                        db.executeData("insert into Supplier_Report values (" + DgvSearch.CurrentRow.Cells[0].Value + " , " + DgvSearch.CurrentRow.Cells[2].Value + " , '" + d + "' , " + cbxSupplier.SelectedValue + ")", "تم تسديد المبلغ بنجاح", "");

                        db.executeData("insert into Stock_Pull (Stock_ID , Money ,Date ,Name ,Type ,Reason) values (" + stock_ID + " ," + DgvSearch.CurrentRow.Cells[2].Value + " ,N'" + d + "' ,N'" + Properties.Settings.Default.USERNAME + "' ,N'مستحقات الى موردين', N'') ", "", "");
                        db.executeData("update stock set Money=Money - " + DgvSearch.CurrentRow.Cells[2].Value + " where Stock_ID=" + stock_ID + "", "", "");

                        btnSearch_Click(null, null);

                    }
                }

                else if (rbtnPayPart.Checked)
                {
                    if (Convert.ToDecimal(NudPrice.Value) > stock_Money)
                    {
                        MessageBox.Show("المبلغ الموجود فى الخزنة غير كافى لاجراء العملية", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (MessageBox.Show("هل انت متاكد من تسديد المبلغ", "تاكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        decimal money = Convert.ToDecimal(DgvSearch.CurrentRow.Cells[2].Value) - NudPrice.Value;

                        db.executeData("update Supplier_Money set Price=" + money + " where Order_ID=" + DgvSearch.CurrentRow.Cells[0].Value + " and Price=" + DgvSearch.CurrentRow.Cells[2].Value + "", "", "");
                        db.executeData("insert  into Supplier_Report values (" + DgvSearch.CurrentRow.Cells[0].Value + " , " + NudPrice.Value + " , '" + d + "' , " + cbxSupplier.SelectedValue + ")", "تم تسديد المبلغ بنجاح", "");

                        db.executeData("insert into Stock_Pull (Stock_ID , Money ,Date ,Name ,Type ,Reason) values (" + stock_ID + " ," + NudPrice.Value + " ,N'" + d + "' ,N'" + Properties.Settings.Default.USERNAME + "' ,N'مستحقات الى موردين', N'') ", "", "");
                        db.executeData("update stock set Money=Money - " + NudPrice.Value + " where Stock_ID=" + stock_ID + "", "", "");

                        btnSearch_Click(null, null);

                    }
                }
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (rbtnOneSupplier.Checked)
            {
                if (DgvSearch.Rows.Count >= 1)
                {
                    PrintOneSupplier();

                }
            }
            else
            {
                MessageBox.Show("من فضلك اختر مورد محدد", "تاكيد");
            }
        }

        private void PrintOneSupplier()
        {
            int id = Convert.ToInt32(cbxSupplier.SelectedValue);
            DataTable tblRpt = new DataTable();

            tblRpt.Clear();
            tblRpt = db.readData("SELECT [Order_ID] as 'رقم الفاتورة',[Supplier_Money].Sup_ID as 'رقم المورد',Suppliers.Sup_Name as 'اسم المورد',[Price] as 'السعر',[Order_Date] as 'تاريخ الفاتورة',[Reminder_Date] as 'تاريخ الاستحقاق' FROM [dbo].[Supplier_Money],Suppliers where Suppliers.Sup_ID =[Supplier_Money].Sup_ID and Suppliers.Sup_ID=" + id + "", "");
            try
            {
                Frm_Printing frm = new Frm_Printing();

                frm.crystalReportViewer1.RefreshReport();
                if (Properties.Settings.Default.SalePrintKind == "8CM")
                {
                    RptSupplierMoney rpt = new RptSupplierMoney();
                    rpt.SetDatabaseLogon("", "", @".\SQLEXPRESS", "Sales_System");
                    rpt.SetDataSource(tblRpt);
                    rpt.SetParameterValue("ID", id);
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
                    RptSupplierMoneyA4 rpt = new RptSupplierMoneyA4();


                    rpt.SetDatabaseLogon("", "", @".\SQLEXPRESS", "Sales_System");
                    rpt.SetDataSource(tblRpt);
                    rpt.SetParameterValue("ID", id);
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
    }
}
