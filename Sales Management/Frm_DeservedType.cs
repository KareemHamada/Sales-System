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
    public partial class Frm_DeservedType : Form
    {
        public Frm_DeservedType()
        {
            InitializeComponent();
        }
        Database db = new Database();
        DataTable tbl = new DataTable();
        DataTable tblSearch = new DataTable();

        private void AutoNumber()
        {
            tblSearch.Clear();
            tblSearch = db.readData("SELECT [Des_ID] as 'رقم النوع',[Name] as 'اسم النوع'FROM [dbo].[Deserved_Type] where CurrentState=1", "");
            DgvSearch.DataSource = tblSearch;
            tbl.Clear();
            tbl = db.readData("Select Max(Des_ID) from Deserved_Type", "");
            if (tbl.Rows[0][0].ToString() == DBNull.Value.ToString())
            {
                txtID.Text = "1";
            }
            else
            {
                txtID.Text = (Convert.ToInt32(tbl.Rows[0][0]) + 1).ToString();
            }
            txtName.Clear();
            txtName.Clear();

            btnAdd.Enabled = true;
            btnNew.Enabled = true;
            btnDelete.Enabled = false;
            btnDeleteAll.Enabled = false;
            btnSave.Enabled = false;

        }

        private void Frm_DeservedType_Load(object sender, EventArgs e)
        {
            AutoNumber();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "")
            {
                MessageBox.Show("من فضلك ادخل اسم النوع");
                return;
            }
            db.executeData("insert into Deserved_Type values (" + txtID.Text + ",N'" + txtName.Text + "',1)", "تم الادخال بنجاح", "");
            AutoNumber();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            AutoNumber();
        }

        //private void btnFirst_Click(object sender, EventArgs e)
        //{
        //    row = 0;
        //    show();
        //}

        private void btnSave_Click(object sender, EventArgs e)
        {
            db.executeData("Update Deserved_Type set Name = N'" + txtName.Text + "' where Des_ID=" + txtID.Text + "", "تم تعديل البيانات بنجاح", "");
            AutoNumber();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("هل انت متاكد من مسح البيانات", "تاكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                db.executeData("Update Deserved_Type set CurrentState=0 where Des_ID=" + txtID.Text + "", "تم الحذف بنجاح", "");
                AutoNumber();
            }
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("هل انت متاكد من مسح البيانات", "تاكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                db.executeData("Update Deserved_Type set CurrentState=0", "تم الحذف بنجاح", "");
                AutoNumber();
            }
        }

        //private void btnExit_Click(object sender, EventArgs e)
        //{
        //    Close();
        //}

        //private void btnLast_Click(object sender, EventArgs e)
        //{
        //    tbl.Clear();
        //    tbl = db.readData("select count(Des_ID) from Deserved_Type", "");
        //    row = Convert.ToInt32(tbl.Rows[0][0]) - 1;
        //    show();
        //}

        //private void btnNext_Click(object sender, EventArgs e)
        //{
        //    tbl.Clear();
        //    tbl = db.readData("select count(Des_ID) from Deserved_Type", "");

        //    if (Convert.ToInt32(tbl.Rows[0][0]) - 1 == row)
        //    {
        //        row = 0;
        //        show();
        //    }
        //    else
        //    {
        //        row++;
        //        show();
        //    }
        //}

        //private void btnPrev_Click(object sender, EventArgs e)
        //{
        //    tbl.Clear();
        //    tbl = db.readData("select count(Des_ID) from Deserved_Type", "");

        //    if (row == 0)
        //    {
        //        row = Convert.ToInt32(tbl.Rows[0][0]) - 1;
        //        show();
        //    }
        //    else
        //    {
        //        row--;
        //        show();
        //    }
        //}

        private void DgvSearch_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (DgvSearch.Rows.Count >= 1)
                {
                    txtID.Text = DgvSearch.CurrentRow.Cells[0].Value.ToString();
                    txtName.Text = DgvSearch.CurrentRow.Cells[1].Value.ToString();
                   
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
