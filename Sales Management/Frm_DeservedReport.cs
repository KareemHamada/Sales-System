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
    public partial class Frm_DeservedReport : Form
    {
        public Frm_DeservedReport()
        {
            InitializeComponent();
        }
        Database db = new Database();
        DataTable tbl = new DataTable();
        private void Frm_DeservedReport_Load(object sender, EventArgs e)
        {
            DtpFrom.Text = DateTime.Now.ToShortDateString();
            DtpTo.Text = DateTime.Now.ToShortDateString();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string date1 = DtpFrom.Value.ToString("yyyy-MM-dd");
            string date2 = DtpTo.Value.ToString("yyyy-MM-dd");

            tbl.Clear();
            tbl = db.readData("select Deserved.Des_ID as 'رقم العملية', Price as 'المبلغ المدفوع',Date as 'تاريخ الدفع',Notes as 'ملاحظات',Deserved_Type.Name as 'النوع' from Deserved,Deserved_Type where Deserved.Type_ID = Deserved_Type.Des_ID and CONVERT(date,Date,105) between '" + date1 + "' and '" + date2 + "' ", "");

            if (tbl.Rows.Count >= 1)
            {
                DgvSearch.DataSource = tbl;

                decimal sum = 0;
                for (int i = 0; i < tbl.Rows.Count; i++)
                {
                    sum += Convert.ToDecimal(tbl.Rows[i][1]);
                }

                txtTotal.Text = Math.Round(sum, 2).ToString();
            }
            else
            {
                txtTotal.Text = "0";
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string date1 = DtpFrom.Value.ToString("yyyy-MM-dd");
            string date2 = DtpTo.Value.ToString("yyyy-MM-dd");

            if (MessageBox.Show("هل انت متاكد من مسح البيانات", "تاكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                db.executeData("Delete from Deserved where CONVERT(date,Date,105) between '" + date1 + "' and '" + date2 + "' ", "تم مسح البيانات بنجاح", "");
                btnSearch_Click(null, null);
            }
        }
    }
}
