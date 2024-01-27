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
    public partial class Frm_StoreGard : Form
    {
        public Frm_StoreGard()
        {
            InitializeComponent();
        }
        Database db = new Database();
        DataTable tbl = new DataTable();


        private void FillStore()
        {

            try
            {
                cbxStore.DataSource = db.readData("select * from Store where CurrentState=1", "");
                cbxStore.DisplayMember = "Store_Name";
                cbxStore.ValueMember = "Store_ID";
            }
            catch
            {

            }
        }
        private void FillGroup()
        {
            try
            {
                cbxGroups.DataSource = db.readData("select * from Products_Group where CurrentState=1", "");
                cbxGroups.DisplayMember = "Group_Name";
                cbxGroups.ValueMember = "Group_ID";
            }
            catch
            {

            }
            
        }
        private void Frm_StoreGard_Load(object sender, EventArgs e)
        {
            try
            {
                FillStore();
                FillGroup();
            }
            catch (Exception) { }
        }
        private void showTotal()
        {
            decimal totalRb7h = 0, totalBuy = 0, totalSale = 0;
            for (int i = 0; i <= DgvSearch.Rows.Count - 1; i++)
            {
                totalRb7h += (Convert.ToDecimal(DgvSearch.Rows[i].Cells[5].Value) - Convert.ToDecimal(DgvSearch.Rows[i].Cells[4].Value)) * Convert.ToDecimal(DgvSearch.Rows[i].Cells[3].Value);
                totalBuy += Convert.ToDecimal(DgvSearch.Rows[i].Cells[4].Value) * Convert.ToDecimal(DgvSearch.Rows[i].Cells[3].Value);
                totalSale += Convert.ToDecimal(DgvSearch.Rows[i].Cells[5].Value) * Convert.ToDecimal(DgvSearch.Rows[i].Cells[3].Value);
            }
            txtRb7h.Text = Math.Round(totalRb7h, 2).ToString();
            txtTotalBuy.Text = Math.Round(totalBuy, 2).ToString();
            txtTotalSale.Text = Math.Round(totalSale, 2).ToString();
        }

        private void txtBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtBarcode.Text != "")
                {
                    tbl.Clear();
                    if (rbtnAllStore.Checked)
                    {
                        if (rbtnAllGroups.Checked)
                        {
                            tbl = db.readData("SELECT [Products_Qty].[Pro_ID] as 'رقم المنتج',Products.Pro_Name as 'اسم المنتج',(select Store_Name from Store where Store.Store_ID = Products_Qty.Store_ID) as 'اسم المخزن',[Products_Qty].[Qty] as 'الكمية بالوحدة الكبري' ,[Buy_Price] as 'سعر الشراء بالوحدة الكبري',[Products_Qty].[Sale_Price_After_Tax] as 'سعر البيع بالوحدة الكبري' FROM [dbo].[Products_Qty],Products where Products.CurrentState=1 and Products.Pro_ID =[Products_Qty].Pro_ID and Products.Barcode=N'" + txtBarcode.Text + "'", "");
                        }
                        else
                        {
                            if (cbxGroups.SelectedValue == null)
                            {
                                MessageBox.Show("من فضلك اختر صنف صحيح");
                                return;
                            }
                            tbl = db.readData("SELECT [Products_Qty].[Pro_ID] as 'رقم المنتج',Products.Pro_Name as 'اسم المنتج',(select Store_Name from Store where Store.Store_ID = Products_Qty.Store_ID) as 'اسم المخزن',[Products_Qty].[Qty] as 'الكمية بالوحدة الكبري' ,[Buy_Price] as 'سعر الشراء بالوحدة الكبري',[Products_Qty].[Sale_Price_After_Tax] as 'سعر البيع بالوحدة الكبري' FROM [dbo].[Products_Qty],Products where Products.CurrentState=1 and Products.Pro_ID =[Products_Qty].Pro_ID and Products.Barcode=N'" + txtBarcode.Text + "' and Products.Group_ID=" + cbxGroups.SelectedValue + "", "");
                        }

                    }
                    else
                    {
                        if (cbxStore.SelectedValue == null)
                        {
                            MessageBox.Show("من فضلك اختر مخزن صحيح");
                            return;
                        }
                        if (rbtnAllGroups.Checked)
                        {
                            tbl = db.readData("SELECT [Products_Qty].[Pro_ID] as 'رقم المنتج',Products.Pro_Name as 'اسم المنتج',(select Store_Name from Store where Store.Store_ID = Products_Qty.Store_ID) as 'اسم المخزن',[Products_Qty].[Qty] as 'الكمية بالوحدة الكبري' ,[Buy_Price] as 'سعر الشراء بالوحدة الكبري',[Products_Qty].[Sale_Price_After_Tax] as 'سعر البيع بالوحدة الكبري' FROM [dbo].[Products_Qty],Products where Products.CurrentState=1 and Products.Pro_ID =[Products_Qty].Pro_ID and Products.Barcode=N'" + txtBarcode.Text + "' and Products_Qty.Store_ID=" + cbxStore.SelectedValue + "", "");
                        }
                        else
                        {
                            if (cbxGroups.SelectedValue == null)
                            {
                                MessageBox.Show("من فضلك اختر صنف صحيح");
                                return;
                            }
                            tbl = db.readData("SELECT [Products_Qty].[Pro_ID] as 'رقم المنتج',Products.Pro_Name as 'اسم المنتج',(select Store_Name from Store where Store.Store_ID = Products_Qty.Store_ID) as 'اسم المخزن',[Products_Qty].[Qty] as 'الكمية بالوحدة الكبري' ,[Buy_Price] as 'سعر الشراء بالوحدة الكبري',[Products_Qty].[Sale_Price_After_Tax] as 'سعر البيع بالوحدة الكبري' FROM [dbo].[Products_Qty],Products where Products.CurrentState=1 and Products.Pro_ID =[Products_Qty].Pro_ID and Products.Barcode=N'" + txtBarcode.Text + "' and Products_Qty.Store_ID=" + cbxStore.SelectedValue + " and Products.Group_ID=" + cbxGroups.SelectedValue + "", "");
                        }

                    }


                    DgvSearch.DataSource = tbl;
                    showTotal();
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            tbl.Clear();
            if (rbtnAllStore.Checked)
            {
                if (rbtnAllGroups.Checked)
                {
                    tbl = db.readData("SELECT [Products_Qty].[Pro_ID] as 'رقم المنتج',Products.Pro_Name as 'اسم المنتج',(select Store_Name from Store where Store.Store_ID = Products_Qty.Store_ID) as 'اسم المخزن',[Products_Qty].[Qty] as 'الكمية بالوحدة الكبري' ,[Buy_Price] as 'سعر الشراء بالوحدة الكبري',[Products_Qty].[Sale_Price_After_Tax] as 'سعر البيع بالوحدة الكبري' FROM [dbo].[Products_Qty],Products where Products.CurrentState=1 and Products.Pro_ID =[Products_Qty].Pro_ID", "");
                }
                else
                {
                    if (cbxGroups.SelectedValue == null)
                    {
                        MessageBox.Show("من فضلك اختر صنف صحيح");
                        return;
                    }
                    tbl = db.readData("SELECT [Products_Qty].[Pro_ID] as 'رقم المنتج',Products.Pro_Name as 'اسم المنتج',(select Store_Name from Store where Store.Store_ID = Products_Qty.Store_ID) as 'اسم المخزن',[Products_Qty].[Qty] as 'الكمية بالوحدة الكبري' ,[Buy_Price] as 'سعر الشراء بالوحدة الكبري',[Products_Qty].[Sale_Price_After_Tax] as 'سعر البيع بالوحدة الكبري' FROM [dbo].[Products_Qty],Products where Products.CurrentState=1 and Products.Pro_ID =[Products_Qty].Pro_ID and Products.Group_ID=" + cbxGroups.SelectedValue + "", "");
                }

            }
            else
            {
                if (cbxStore.SelectedValue == null)
                {
                    MessageBox.Show("من فضلك اختر مخزن صحيح");
                    return;
                }
                if (rbtnAllGroups.Checked)
                {
                    tbl = db.readData("SELECT [Products_Qty].[Pro_ID] as 'رقم المنتج',Products.Pro_Name as 'اسم المنتج',(select Store_Name from Store where Store.Store_ID = Products_Qty.Store_ID) as 'اسم المخزن',[Products_Qty].[Qty] as 'الكمية بالوحدة الكبري' ,[Buy_Price] as 'سعر الشراء بالوحدة الكبري',[Products_Qty].[Sale_Price_After_Tax] as 'سعر البيع بالوحدة الكبري' FROM [dbo].[Products_Qty],Products where Products.CurrentState=1 and Products.Pro_ID =[Products_Qty].Pro_ID and Products_Qty.Store_ID=" + cbxStore.SelectedValue + "", "");
                }
                else
                {
                    if (cbxGroups.SelectedValue == null)
                    {
                        MessageBox.Show("من فضلك اختر صنف صحيح");
                        return;
                    }
                    tbl = db.readData("SELECT [Products_Qty].[Pro_ID] as 'رقم المنتج',Products.Pro_Name as 'اسم المنتج',(select Store_Name from Store where Store.Store_ID = Products_Qty.Store_ID) as 'اسم المخزن',[Products_Qty].[Qty] as 'الكمية بالوحدة الكبري' ,[Buy_Price] as 'سعر الشراء بالوحدة الكبري',[Products_Qty].[Sale_Price_After_Tax] as 'سعر البيع بالوحدة الكبري' FROM [dbo].[Products_Qty],Products where Products.CurrentState=1 and Products.Pro_ID =[Products_Qty].Pro_ID and Products_Qty.Store_ID=" + cbxStore.SelectedValue + " and Products.Group_ID=" + cbxGroups.SelectedValue + "", "");
                }

            }



            DgvSearch.DataSource = tbl;
            showTotal();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            DataTable tblRpt = new DataTable();

            tblRpt.Clear();
            tblRpt = db.readData("SELECT [Products_Qty].[Pro_ID] as 'رقم المنتج',Products.Pro_Name as 'اسم المنتج',(select Store_Name from Store where Store.Store_ID = Products_Qty.Store_ID) as 'اسم المخزن',[Products_Qty].[Qty] as 'الكمية بالوحدة الكبري' ,[Buy_Price] as 'سعر الشراء بالوحدة الكبري',[Products_Qty].[Sale_Price_After_Tax] as 'سعر البيع بالوحدة الكبري',(([Products_Qty].Sale_Price_After_Tax - Buy_Price) * [Products_Qty].[Qty]) as 'الارباح المتوقعة' FROM [dbo].[Products_Qty],Products where Products.CurrentState=1 and Products.Pro_ID =[Products_Qty].Pro_ID", "");
            try
            {
                Frm_Printing frm = new Frm_Printing();

                frm.crystalReportViewer1.RefreshReport();
                RptStoreGard rpt = new RptStoreGard();
                rpt.SetDatabaseLogon("", "", @".\SQLEXPRESS", "Sales_System");
                rpt.SetDataSource(tblRpt);
                frm.crystalReportViewer1.ReportSource = rpt;

                System.Drawing.Printing.PrintDocument printDocument = new System.Drawing.Printing.PrintDocument();
                rpt.PrintOptions.PrinterName = Properties.Settings.Default.PrinterName;
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
    }
}
