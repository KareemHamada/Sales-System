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
    public partial class Frm_ProductRequired : Form
    {
        public Frm_ProductRequired()
        {
            InitializeComponent();
        }
        Database db = new Database();
        DataTable tbl = new DataTable();
        private void Frm_ProductRequired_Load(object sender, EventArgs e)
        {
            tbl.Clear();
            tbl = db.readData("select Pro_Name as 'اسم المنتج' ,Qty as 'الكمية الموجوده منه' ,MinQty as 'حد الطلب' from Products where Products.CurrentState=1 and MinQty >=1 and Qty <= MinQty ", "");

            DgvSearch.DataSource = tbl;
            txtTotal.Text = tbl.Rows.Count + "";
        }
    }
}
