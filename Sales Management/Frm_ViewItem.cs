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
    public partial class Frm_ViewItem : Form
    {
        public Frm_ViewItem()
        {
            InitializeComponent();
        }

        Database db = new Database();
        DataTable tbl = new DataTable();

        private void FillGroup()
        {
            cbxGroup.DataSource = db.readData("select * from Products_Group", "");
            cbxGroup.DisplayMember = "Group_Name";
            cbxGroup.ValueMember = "Group_ID";
        }
        private void showAllItems()
        {
            tbl.Clear();
            tbl = db.readData("SELECT [Pro_ID] as ' رقم المنتج',[Pro_Name] as 'اسم المنتج',(select Unit_Name from Unit where [Products].Main_UnitID = Unit.Unit_ID) as 'الوحدة الرئيسية',[Qty] as 'الكمية الكلية',[Sale_Price_Before_Tax] as 'سعر التجزئه قبل الضريبة',[Tax_Value] as ' % الضريبة',[Sale_Price_After_Tax] as 'سعر التجزئه بعد الضريبة',(select Unit_Name from Unit where [Products].Sale_UnitID = Unit.Unit_ID) as 'وحدة البيع',(select Unit_Name from Unit where [Products].Buy_UnitID = Unit.Unit_ID) as 'وحدة الشراء',[Barcode] as 'الباركود' ,[MinQty] as 'الحد الادنى',[MaxDiscount] as 'اقصى خصم مسموح',[IS_Tax] as 'هل خاضع للضريبة',Products_Group.Group_Name as 'اسم المجموعة' FROM [dbo].[Products],Products_Group where Products_Group.Group_ID=[Products].Group_ID", "");
            DgvSearch.DataSource = tbl;
            showTotal();

        }

        private void showTotal()
        {
            decimal totalQty = 0;
            //decimal totalGomla = 0;
            decimal totalPart = 0;
            decimal totalAfterTax = 0;
            for (int i = 0; i <= DgvSearch.Rows.Count - 1; i++)
            {
                totalQty += Convert.ToDecimal(DgvSearch.Rows[i].Cells[3].Value);

                //totalGomla += Convert.ToDecimal(DgvSearch.Rows[i].Cells[3].Value) * Convert.ToDecimal(DgvSearch.Rows[i].Cells[4].Value);

                totalPart += Convert.ToDecimal(DgvSearch.Rows[i].Cells[3].Value) * Convert.ToDecimal(DgvSearch.Rows[i].Cells[4].Value);
                totalAfterTax += Convert.ToDecimal(DgvSearch.Rows[i].Cells[3].Value) * Convert.ToDecimal(DgvSearch.Rows[i].Cells[6].Value);

            }
            txtTotalQty.Text = Math.Round(totalQty, 2).ToString();
            //txtTotalGomla.Text = Math.Round(totalGomla, 2).ToString();
            txtTotalPart.Text = Math.Round(totalPart, 2).ToString();
            txtTotalAfterTax.Text = Math.Round(totalAfterTax, 2).ToString();
        }
        private void Frm_ViewItem_Load(object sender, EventArgs e)
        {
            FillGroup();
            showAllItems();
        }

        private void btnGroup_Click(object sender, EventArgs e)
        {
            if(cbxGroup.SelectedValue == null)
            {
                MessageBox.Show("من فضلك اختر مجموعة صحيحة");
                return;
            }
            tbl.Clear();

            tbl = db.readData("SELECT [Pro_ID] as ' رقم المنتج',[Pro_Name] as 'اسم المنتج',(select Unit_Name from Unit where [Products].Main_UnitID = Unit.Unit_ID) as 'الوحدة الرئيسية',[Qty] as 'الكمية الكلية',[Sale_Price_Before_Tax] as 'سعر التجزئه قبل الضريبة',[Tax_Value] as ' % الضريبة',[Sale_Price_After_Tax] as 'سعر التجزئه بعد الضريبة',(select Unit_Name from Unit where [Products].Sale_UnitID = Unit.Unit_ID) as 'وحدة البيع',(select Unit_Name from Unit where [Products].Buy_UnitID = Unit.Unit_ID) as 'وحدة الشراء',[Barcode] as 'الباركود' ,[MinQty] as 'الحد الادنى',[MaxDiscount] as 'اقصى خصم مسموح',[IS_Tax] as 'هل خاضع للضريبة',Products_Group.Group_Name as 'اسم المجموعة' FROM [dbo].[Products],Products_Group where Products_Group.Group_ID=[Products].Group_ID and Products.Group_ID=" + cbxGroup.SelectedValue + "", "");

            DgvSearch.DataSource = tbl;

            showTotal();
        }

        private void btnBarcode_Click(object sender, EventArgs e)
        {
            tbl.Clear();
            tbl = db.readData("SELECT [Pro_ID] as ' رقم المنتج',[Pro_Name] as 'اسم المنتج',(select Unit_Name from Unit where [Products].Main_UnitID = Unit.Unit_ID) as 'الوحدة الرئيسية',[Qty] as 'الكمية الكلية',[Sale_Price_Before_Tax] as 'سعر التجزئه قبل الضريبة',[Tax_Value] as 'الضريبة %',[Sale_Price_After_Tax] as 'سعر التجزئه بعد الضريبة',(select Unit_Name from Unit where [Products].Sale_UnitID = Unit.Unit_ID) as 'وحدة البيع',(select Unit_Name from Unit where [Products].Buy_UnitID = Unit.Unit_ID) as 'وحدة الشراء',[Barcode] as 'الباركود' ,[MinQty] as 'الحد الادنى',[MaxDiscount] as 'اقصى خصم مسموح',[IS_Tax] as 'هل خاضع للضريبة',Products_Group.Group_Name as 'اسم المجموعة' FROM [dbo].[Products],Products_Group where Products_Group.Group_ID=[Products].Group_ID and Products.[Barcode]=N'" + txtBarcode.Text + "'", "");

            DgvSearch.DataSource = tbl;
            showTotal();
        }

        private void btnName_Click(object sender, EventArgs e)
        {
            tbl.Clear();
            tbl = db.readData("SELECT [Pro_ID] as ' رقم المنتج',[Pro_Name] as 'اسم المنتج',(select Unit_Name from Unit where [Products].Main_UnitID = Unit.Unit_ID) as 'الوحدة الرئيسية',[Qty] as 'الكمية الكلية',[Sale_Price_Before_Tax] as 'سعر التجزئه قبل الضريبة',[Tax_Value] as ' % الضريبة',[Sale_Price_After_Tax] as 'سعر التجزئه بعد الضريبة',(select Unit_Name from Unit where [Products].Sale_UnitID = Unit.Unit_ID) as 'وحدة البيع',(select Unit_Name from Unit where [Products].Buy_UnitID = Unit.Unit_ID) as 'وحدة الشراء',[Barcode] as 'الباركود' ,[MinQty] as 'الحد الادنى',[MaxDiscount] as 'اقصى خصم مسموح',[IS_Tax] as 'هل خاضع للضريبة',Products_Group.Group_Name as 'اسم المجموعة' FROM [dbo].[Products],Products_Group where Products_Group.Group_ID=[Products].Group_ID and [Pro_Name] like N'%" + txtName.Text + "%'", "");

            DgvSearch.DataSource = tbl;
            showTotal();
        }
      

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            if (DgvSearch.Rows.Count > 0)
            {
                if (MessageBox.Show("هل انتا متاكد من مسح البيانات", "تاكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    db.readData("delete from Products_Qty ", "");
                    db.readData("delete from Products_Unit ", "");
                    db.readData("delete from Products", "تم مسح البيانات بنجاح");
                    showAllItems();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            
            if (DgvSearch.Rows.Count > 0)
            {
                if (MessageBox.Show("هل انتا متاكد من مسح البيانات", "تاكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    db.readData("delete from Products_Qty where Pro_ID=" + DgvSearch.CurrentRow.Cells[0].Value + "", "");
                    db.readData("delete from Products_Unit where Pro_ID=" + DgvSearch.CurrentRow.Cells[0].Value + "", "");
                    db.readData("delete from Products where Pro_ID=" + DgvSearch.CurrentRow.Cells[0].Value + "", "تم مسح البيانات بنجاح");
                    showAllItems();

                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (DgvSearch.Rows.Count > 0)
            {
                Properties.Settings.Default.Update_Pro = DgvSearch.CurrentRow.Cells[0].Value.ToString();
                Properties.Settings.Default.Save();
                Frm_Products frm = new Frm_Products();
                frm.updateMode = true;
                frm.ShowDialog();
                showAllItems();
                FillGroup();
            }
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            Frm_Products frm = new Frm_Products();
            frm.ShowDialog();
            showAllItems();
            FillGroup();
        }

        private void btnViewAll_Click(object sender, EventArgs e)
        {
            showAllItems();
            FillGroup();
        }

        
    }
}
