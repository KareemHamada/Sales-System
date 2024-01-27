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
    public partial class Frm_Store : Form
    {
        public Frm_Store()
        {
            InitializeComponent();
        }
        Database db = new Database();
        DataTable tbl = new DataTable(); 
        DataTable tblGroup = new DataTable();
        private void AutoNumber()
        {
            tblGroup.Clear();
            tblGroup = db.readData("select Store_ID as 'رقم المخزن' ,Store_Name as 'اسم المخزن' from Store where CurrentState=1", "");
            DgvSearch.DataSource = tblGroup;
            tbl.Clear();
            tbl = db.readData("select max (Store_ID) from Store", "");

            if ((tbl.Rows[0][0].ToString() == DBNull.Value.ToString()))
            {

                txtID.Text = "1";
            }
            else
            {

                txtID.Text = (Convert.ToInt32(tbl.Rows[0][0]) + 1).ToString();
            }

            txtName.Clear();
            btnAdd.Enabled = true;
            btnNew.Enabled = true;
            btnDelete.Enabled = false;
            btnDeleteAll.Enabled = false;
            btnSave.Enabled = false;

        }

        
        private void Frm_Store_Load(object sender, EventArgs e)
        {
            AutoNumber();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "")
            {
                MessageBox.Show("من فضلك ادخل اسم المخزن");
                return;
            }
            db.executeData("insert into Store Values (" + txtID.Text + " ,N'" + txtName.Text + "',1)", "تم الادخال بنجاح", "");

            AutoNumber();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "")
            {
                MessageBox.Show("من فضلك ادخل اسم المخزن");
                return;
            }
            db.readData("update Store set Store_Name=N'" + txtName.Text + "'  where Store_ID=" + txtID.Text + "", "تم تعديل البيانات بنجاح");
            AutoNumber();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            AutoNumber();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("هل انتا متاكد من مسح البيانات", "تاكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                db.readData("update Store set CurrentState=0 where Store_ID=" + txtID.Text + "", "تم الحذف بنجاح");
                AutoNumber();
            }
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("هل انتا متاكد من مسح البيانات", "تاكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                db.readData("update Store set CurrentState=0", "تم الحذف بنجاح");
                //db.executeData("delete from Store ", "تم مسح البيانات بنجاح", "لا يمكن حذف المخازن قد يكون هناك مخزن متعلق بعمليات اخري عند حذفها يتم حذف هذه المخازن");
                AutoNumber();
            }
        }

        //private void btnFirst_Click(object sender, EventArgs e)
        //{
        //    row = 0;
        //    Show();
        //}

        //private void btnPrev_Click(object sender, EventArgs e)
        //{
        //    if (row == 0)
        //    {
        //        tbl.Clear();
        //        tbl = db.readData("select count(Store_ID) from Store", "");
        //        row = Convert.ToInt32(tbl.Rows[0][0]) - 1;
        //        Show();
        //    }
        //    else
        //    {


        //        row--;
        //        Show();
        //    }
        //}

        //private void btnNext_Click(object sender, EventArgs e)
        //{
        //    tbl.Clear();
        //    tbl = db.readData("select count(Store_ID) from Store", "");
        //    if (Convert.ToInt32(tbl.Rows[0][0]) - 1 == row)
        //    {
        //        row = 0;
        //        Show();
        //    }
        //    else
        //    {
        //        row++;
        //        Show();
        //    }
        //}

        //private void btnLast_Click(object sender, EventArgs e)
        //{
        //    tbl.Clear();
        //    tbl = db.readData("select count(Store_ID) from Store", "");
        //    row = Convert.ToInt32(tbl.Rows[0][0]) - 1;
        //    Show();
        //}

        private void DgvSearch_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (DgvSearch.Rows.Count >= 1)
                {
                    DataTable tblShow = new DataTable();
                    tblShow.Clear();
                    tblShow = db.readData("select * from Store where Store_ID=" + DgvSearch.CurrentRow.Cells[0].Value + "", "");

                    txtID.Text = tblShow.Rows[0][0].ToString();
                    txtName.Text = tblShow.Rows[0][1].ToString();

                    btnAdd.Enabled = false;
                    btnNew.Enabled = true;
                    btnDelete.Enabled = true;
                    btnDeleteAll.Enabled = true;
                    btnSave.Enabled = true;
                }

            }
            catch (Exception) { }
        }
    }
}
