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
    public partial class Frm_ProductGroup : Form
    {
        public Frm_ProductGroup()
        {
            InitializeComponent();
        }
        Database db = new Database();
        DataTable tbl = new DataTable(); 
        DataTable tblGroup = new DataTable();
        private void AutoNumber()
        {
            tblGroup.Clear();
            tblGroup = db.readData("select Group_ID as 'رقم المجموعة' ,Group_Name as 'اسم المجموعة' from Products_Group where CurrentState=1", "");
            DgvSearch.DataSource = tblGroup;
            tbl.Clear();
            tbl = db.readData("select max(Group_ID) from Products_Group", "");

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

        private void Frm_ProductGroup_Load(object sender, EventArgs e)
        {
            AutoNumber();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            AutoNumber();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "")
            {
                MessageBox.Show("من فضلك ادخل اسم المجموعة");
                return;
            }
            db.executeData("insert into Products_Group Values (" + txtID.Text + " ,N'" + txtName.Text + "',1)", "تم الادخال بنجاح", "");

            AutoNumber();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "")
            {
                MessageBox.Show("من فضلك ادخل اسم المجموعة");
                return;
            }
            db.executeData("update Products_Group set Group_Name=N'" + txtName.Text + "'  where Group_ID=" + txtID.Text + "", "تم تعديل البيانات بنجاح","");
            AutoNumber();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("هل انتا متاكد من مسح البيانات", "تاكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                db.executeData("update Products_Group set CurrentState=0 where Group_ID=" + txtID.Text + "", "تم الحذف بنجاح","");
                AutoNumber();
            }
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("هل انتا متاكد من مسح البيانات", "تاكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                db.executeData("update Products_Group set CurrentState=0", "تم الحذف بنجاح", "");
                //db.executeData("delete from Products_Group ", "تم مسح البيانات بنجاح", "لا يمكن حذف جميع الاصناف قد يكون هناك صنف متعلق بعمليات اخري عند حذفها يتم حذف هذا الصنف");
                AutoNumber();
            }
        }

        private void DgvSearch_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (DgvSearch.Rows.Count >= 1)
                {
                    DataTable tblShow = new DataTable();
                    tblShow.Clear();
                    tblShow = db.readData("select * from Products_Group where Group_ID=" + DgvSearch.CurrentRow.Cells[0].Value + "", "");

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
