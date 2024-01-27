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
    public partial class Frm_Deserved : Form
    {
        public Frm_Deserved()
        {
            InitializeComponent();
        }
        Database db = new Database();
        DataTable tbl = new DataTable();
        DataTable tblSearch = new DataTable();

        private void AutoNumber()
        {
            tblSearch.Clear();
            tblSearch = db.readData("SELECT Deserved.[Des_ID] as 'رقم المصروف',[Price] as 'المبلغ',[Date] as 'التاريخ',[Notes] as 'ملاحظات',Deserved_Type.Name as 'النوع' FROM [dbo].[Deserved],Deserved_Type where Deserved.CurrentState=1 and Deserved_Type.Des_ID = Deserved.Type_ID", "");
            DgvSearch.DataSource = tblSearch;

            tbl.Clear();
            tbl = db.readData("Select Max(Des_ID) from Deserved", "");
            if (tbl.Rows[0][0].ToString() == DBNull.Value.ToString())
            {
                txtID.Text = "1";
            }
            else
            {
                txtID.Text = (Convert.ToInt32(tbl.Rows[0][0]) + 1).ToString();
            }
            NudPrice.Value = 1;
            DtpDate.Text = DateTime.Now.ToShortDateString();
            txtNotes.Clear();

            btnAdd.Enabled = true;
            btnNew.Enabled = true;
            btnDelete.Enabled = false;
            btnDeleteAll.Enabled = false;
            btnSave.Enabled = false;

        }
        
        string stock_ID = "";
        private void Frm_Deserved_Load(object sender, EventArgs e)
        {
            AutoNumber();
            db.FillComboBox(cbxType, "select * from Deserved_Type where CurrentState=1", "Name", "Des_ID");
            stock_ID = Convert.ToString(Properties.Settings.Default.Stock_ID);

        }

       

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string d = DtpDate.Value.ToString("dd/MM/yyyy");
            if (cbxType.Items.Count <= 0)
            {
                MessageBox.Show("من فضلك ادخل الانواع اولا");
                return;
            }
            if (cbxType.SelectedValue == null)
            {
                MessageBox.Show("من فضلك ادخل نوعا صحيحا");
                return;
            }
            decimal stock_Money = 0;
            tbl.Clear();
            tbl = db.readData("select * from Stock where Stock_ID=" + stock_ID + "", "");
            stock_Money = Convert.ToDecimal(tbl.Rows[0][1]);

            if (Convert.ToDecimal(NudPrice.Value) > stock_Money)
            {
                MessageBox.Show("المبلغ الموجود فى الخزنة غير كافى لاجراء العملية", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            db.executeData("insert into Stock_Pull (Stock_ID , Money ,Date ,Name ,Type ,Reason) values (" + stock_ID + " ," + NudPrice.Value + " ,N'" + d + "' ,N'" + Properties.Settings.Default.USERNAME + "' ,N'مصروفات', N'" + txtNotes.Text + "') ", "", "");
            db.executeData("update stock set Money=Money - " + NudPrice.Value + " where Stock_ID=" + stock_ID + "", "", "");

            db.executeData("insert into Deserved Values (" + txtID.Text + " ," + NudPrice.Value + " , N'" + d + "' ,N'" + txtNotes.Text + "' ," + cbxType.SelectedValue + ",1)", "تم الادخال بنجاح", "");

            AutoNumber();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            AutoNumber();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(NudPrice.Value <= 0)
            {
                MessageBox.Show("لا يمكن ادخال اقل من 1", "تاكيد");
                return;
            }
            string d = DtpDate.Value.ToString("dd/MM/yyyy");

            db.executeData("Update Deserved set Price = " + NudPrice.Value + ",  Date = N'"+ d + "',Notes=N'"+txtNotes.Text+ "',Type_ID="+ cbxType.SelectedValue+ " where Des_ID=" + txtID.Text + "", "تم تعديل البيانات بنجاح", "");
            AutoNumber();
        }



        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("هل انت متاكد من مسح البيانات", "تاكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {

                db.executeData("Update Deserved set CurrentState=0 where Des_ID=" + txtID.Text + "", "تم الحذف بنجاح", "");

                AutoNumber();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("هل انت متاكد من مسح البيانات", "تاكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                db.executeData("Update Deserved set CurrentState=0", "تم الحذف بنجاح", "");
                AutoNumber();
            }
        }

        private void DgvSearch_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (DgvSearch.Rows.Count >= 1)
                {
                    txtID.Text = DgvSearch.CurrentRow.Cells[0].Value.ToString();
                    NudPrice.Value = Convert.ToDecimal(DgvSearch.CurrentRow.Cells[1].Value);

                    this.Text = DgvSearch.CurrentRow.Cells[2].Value.ToString();
                    DateTime dt = DateTime.ParseExact(this.Text, "dd/MM/yyyy", null);
                    DtpDate.Value = dt;

                    txtNotes.Text = DgvSearch.CurrentRow.Cells[3].Value.ToString();
                    cbxType.Text = DgvSearch.CurrentRow.Cells[4].Value.ToString();


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
