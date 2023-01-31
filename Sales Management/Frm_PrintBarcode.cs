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
    public partial class Frm_PrintBarcode : Form
    {
        public Frm_PrintBarcode()
        {
            InitializeComponent();
        }
        Database db = new Database();
        string pro_ID;
        private void FillPro()
        {
            cbxProducts.DataSource = db.readData("select * from Products", "");
            cbxProducts.DisplayMember = "Pro_Name";
            cbxProducts.ValueMember = "Pro_ID";
        }
        private void Frm_PrintBarcode_Load(object sender, EventArgs e)
        {
            FillPro();

            txtProName.Text = Properties.Settings.Default.Pro_Name;
            txtBarcode.Text = Properties.Settings.Default.Pro_Barcode;
            txtSalesPrice.Text = Convert.ToString(Properties.Settings.Default.Pro_Price);
            cbxProducts.SelectedValue = Convert.ToInt32(Properties.Settings.Default.Pro_Barcode_ID);
        }

        private void btnRandomBarcode_Click(object sender, EventArgs e)
        {
            DataTable tbl = new DataTable();
            tbl.Clear();
            tbl = db.readData("select * from Random_Barcode", "");
            if (tbl.Rows.Count <= 0)
            {

                txtBarcode.Text = "1000000";
                db.executeData("insert into Random_Barcode values (1000000)", "", "");
            }
            else
            {
                txtBarcode.Text = (Convert.ToInt64(tbl.Rows[0][0]) + 1).ToString();
                db.executeData("update Random_Barcode set Barcode=N'" + (Convert.ToInt64(tbl.Rows[0][0]) + 1) + "'", "", "");
            }
        }

        private void btnSaveBarcode_Click(object sender, EventArgs e)
        {

            DataTable tbl = new DataTable();
            tbl.Clear();
            tbl = db.readData("select * from Random_Barcode ", "");
            if (tbl.Rows.Count <= 0)
            {

                db.executeData("insert into Random_Barcode values (N'" + txtBarcode.Text + "')", "تم الحفظ بنجاح", "");
            }
            else
            {

                db.executeData("update Random_Barcode set Barcode=N'" + txtBarcode.Text + "'", "تم الحفظ بنجاح", "");
            }
        }

        private void cbxProducts_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cbxProducts.Items.Count >= 1)
            {
                DataTable tbl = new DataTable();
                tbl.Clear();
                tbl = db.readData("select * from Products where Pro_ID=" + cbxProducts.SelectedValue + " ", "");

                if (tbl.Rows.Count >= 1)
                {
                    txtProName.Text = tbl.Rows[0][1].ToString();
                    txtBarcode.Text = tbl.Rows[0][7].ToString();
                    txtSalesPrice.Text = tbl.Rows[0][6].ToString();
                }

            }
        }

        private void btnPrintPreview_Click(object sender, EventArgs e)
        {
            if (txtBarcode.Text == "" || txtProName.Text == "" || txtSalesPrice.Text == "")
            {
                MessageBox.Show("من فضلك اكمل البيانات", "تاكيد");
                return;
            }
            if(cbxProducts.SelectedValue == null)
            {
                MessageBox.Show("من فضلك اختر اسم منتجا صحيحا ", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DataSet1 DS = new DataSet1();

            DS.Clear();
            RptCrystalReport rpt = new RptCrystalReport();
            DS.Tables["PrintBarcode"].Rows.Add(txtProName.Text, txtBarcode.Text, txtSalesPrice.Text, "*" + txtBarcode.Text.Trim() + "*");
            rpt.SetDataSource(DS);

            Frm_Printing frm = new Frm_Printing();
            frm.crystalReportViewer1.ReportSource = rpt;
            frm.crystalReportViewer1.Refresh();
            frm.ShowDialog();
            Properties.Settings.Default.Pro_Barcode = txtBarcode.Text;
            Properties.Settings.Default.Save();
            pro_ID = cbxProducts.SelectedValue.ToString();
            db.executeData("update Products set Barcode=N'" + txtBarcode.Text + "' where Pro_ID=N'" + pro_ID + "'", "", "");
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (txtBarcode.Text == "" || txtProName.Text == "" || txtSalesPrice.Text == "")
            {
                MessageBox.Show("من فضلك اكمل البيانات", "تاكيد");
                return;
            }
            if (cbxProducts.SelectedValue == null)
            {
                MessageBox.Show("من فضلك اختر اسم منتجا صحيحا ", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DataSet1 DS = new DataSet1();

            DS.Clear();
            RptCrystalReport rpt = new RptCrystalReport();
            DS.Tables["PrintBarcode"].Rows.Add(txtProName.Text, txtBarcode.Text, txtSalesPrice.Text, "*" + txtBarcode.Text.Trim() + "*");

            rpt.SetDataSource(DS);

            Frm_Printing frm = new Frm_Printing();
            frm.crystalReportViewer1.ReportSource = rpt;
            frm.crystalReportViewer1.Refresh();
            //frm.ShowDialog();
            System.Drawing.Printing.PrintDocument printDocument = new System.Drawing.Printing.PrintDocument();
            rpt.PrintOptions.PrinterName = printDocument.PrinterSettings.PrinterName;
            rpt.PrintToPrinter(1, true, 0, 0);

            Properties.Settings.Default.Pro_Barcode = txtBarcode.Text;
            Properties.Settings.Default.Save();

            db.executeData("update Products set Barcode=N'" + txtBarcode.Text + "' where Pro_Name=N'" + txtProName.Text + "'", "", "");
        }

        private void txtBarcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8)
            {
                e.Handled = true;
            }
        }

        private void cbxProducts_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if(cbxProducts.Items.Count <= 0)
                {
                    MessageBox.Show("من فضلك ادخل المنتجات اولا ", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (cbxProducts.SelectedValue != null)
                {
                    if (cbxProducts.Items.Count >= 1)
                    {
                        DataTable tbl = new DataTable();
                        tbl.Clear();
                        tbl = db.readData("select * from Products where Pro_ID=" + cbxProducts.SelectedValue + " ", "");

                        if (tbl.Rows.Count >= 1)
                        {
                            txtProName.Text = tbl.Rows[0][1].ToString();
                            txtBarcode.Text = tbl.Rows[0][7].ToString();
                            txtSalesPrice.Text = tbl.Rows[0][6].ToString();
                        }

                    }
                }
                else
                {
                    MessageBox.Show("من فضلك اختر اسم منتجا صحيحا ", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
