
namespace Sales_Management
{
    partial class Frm_SalesReport
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tplParent = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.DgvSearch = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.txtOrderNumber = new System.Windows.Forms.TextBox();
            this.checkOrderNumber = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.DtpTo = new System.Windows.Forms.DateTimePicker();
            this.DtpFrom = new System.Windows.Forms.DateTimePicker();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.cbxUser = new System.Windows.Forms.ComboBox();
            this.rbtnOneUser = new System.Windows.Forms.RadioButton();
            this.rbtnAllUser = new System.Windows.Forms.RadioButton();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnPrintAll = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.label7 = new System.Windows.Forms.Label();
            this.btnDeleteSelected = new System.Windows.Forms.Button();
            this.btnPrintOrder = new System.Windows.Forms.Button();
            this.txtUserRb7h = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtTotalTax = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtPriceTax = new System.Windows.Forms.TextBox();
            this.tplParent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvSearch)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // tplParent
            // 
            this.tplParent.ColumnCount = 1;
            this.tplParent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tplParent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tplParent.Controls.Add(this.label3, 0, 0);
            this.tplParent.Controls.Add(this.DgvSearch, 0, 2);
            this.tplParent.Controls.Add(this.tableLayoutPanel1, 0, 1);
            this.tplParent.Controls.Add(this.tableLayoutPanel2, 0, 3);
            this.tplParent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tplParent.Location = new System.Drawing.Point(0, 0);
            this.tplParent.Name = "tplParent";
            this.tplParent.RowCount = 4;
            this.tplParent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.90378F));
            this.tplParent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 19.24399F));
            this.tplParent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 53.78007F));
            this.tplParent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 19.41581F));
            this.tplParent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tplParent.Size = new System.Drawing.Size(1269, 582);
            this.tplParent.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arabic Typesetting", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(539, 2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(192, 40);
            this.label3.TabIndex = 23;
            this.label3.Text = "اجمالي المبيعات في فترة";
            // 
            // DgvSearch
            // 
            this.DgvSearch.AllowUserToAddRows = false;
            this.DgvSearch.AllowUserToDeleteRows = false;
            this.DgvSearch.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.DgvSearch.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.DgvSearch.BackgroundColor = System.Drawing.Color.White;
            this.DgvSearch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Beige;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arabic Typesetting", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DgvSearch.DefaultCellStyle = dataGridViewCellStyle1;
            this.DgvSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DgvSearch.Location = new System.Drawing.Point(3, 159);
            this.DgvSearch.Name = "DgvSearch";
            this.DgvSearch.ReadOnly = true;
            this.DgvSearch.RowHeadersWidth = 51;
            this.DgvSearch.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.DgvSearch.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Blue;
            this.DgvSearch.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.DgvSearch.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Blue;
            this.DgvSearch.RowTemplate.Height = 24;
            this.DgvSearch.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DgvSearch.Size = new System.Drawing.Size(1263, 305);
            this.DgvSearch.TabIndex = 11;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 45);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 54.05405F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45.94595F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1269, 111);
            this.tableLayoutPanel1.TabIndex = 24;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 7;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.89928F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.20144F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.61151F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 31.47287F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.83721F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 223F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 430F));
            this.tableLayoutPanel4.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.txtOrderNumber, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.checkOrderNumber, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.label4, 4, 0);
            this.tableLayoutPanel4.Controls.Add(this.DtpTo, 5, 0);
            this.tableLayoutPanel4.Controls.Add(this.DtpFrom, 3, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 59);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1269, 52);
            this.tableLayoutPanel4.TabIndex = 26;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(913, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 40);
            this.label2.TabIndex = 34;
            this.label2.Text = "من:";
            // 
            // txtOrderNumber
            // 
            this.txtOrderNumber.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtOrderNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOrderNumber.Location = new System.Drawing.Point(971, 3);
            this.txtOrderNumber.Name = "txtOrderNumber";
            this.txtOrderNumber.Size = new System.Drawing.Size(136, 46);
            this.txtOrderNumber.TabIndex = 33;
            this.txtOrderNumber.Text = "1";
            this.txtOrderNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtOrderNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtOrderNumber_KeyPress);
            // 
            // checkOrderNumber
            // 
            this.checkOrderNumber.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.checkOrderNumber.AutoSize = true;
            this.checkOrderNumber.Location = new System.Drawing.Point(1131, 4);
            this.checkOrderNumber.Name = "checkOrderNumber";
            this.checkOrderNumber.Size = new System.Drawing.Size(118, 44);
            this.checkOrderNumber.TabIndex = 1;
            this.checkOrderNumber.Text = "رقم الفاتورة";
            this.checkOrderNumber.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(661, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 40);
            this.label4.TabIndex = 36;
            this.label4.Text = "الى:";
            // 
            // DtpTo
            // 
            this.DtpTo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.DtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DtpTo.Location = new System.Drawing.Point(436, 3);
            this.DtpTo.Name = "DtpTo";
            this.DtpTo.Size = new System.Drawing.Size(217, 46);
            this.DtpTo.TabIndex = 37;
            // 
            // DtpFrom
            // 
            this.DtpFrom.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.DtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DtpFrom.Location = new System.Drawing.Point(713, 3);
            this.DtpFrom.Name = "DtpFrom";
            this.DtpFrom.Size = new System.Drawing.Size(187, 46);
            this.DtpFrom.TabIndex = 35;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 6;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.56639F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.50881F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.89784F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.33927F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.68041F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 313F));
            this.tableLayoutPanel3.Controls.Add(this.cbxUser, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.rbtnOneUser, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.rbtnAllUser, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnSearch, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnPrintAll, 4, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnDelete, 5, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 59F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 59F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 59F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 59F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1269, 59);
            this.tableLayoutPanel3.TabIndex = 25;
            // 
            // cbxUser
            // 
            this.cbxUser.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbxUser.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxUser.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxUser.FormattingEnabled = true;
            this.cbxUser.Location = new System.Drawing.Point(671, 17);
            this.cbxUser.Name = "cbxUser";
            this.cbxUser.Size = new System.Drawing.Size(251, 47);
            this.cbxUser.TabIndex = 13;
            // 
            // rbtnOneUser
            // 
            this.rbtnOneUser.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.rbtnOneUser.AutoSize = true;
            this.rbtnOneUser.ForeColor = System.Drawing.Color.Blue;
            this.rbtnOneUser.Location = new System.Drawing.Point(936, 7);
            this.rbtnOneUser.Name = "rbtnOneUser";
            this.rbtnOneUser.Size = new System.Drawing.Size(145, 44);
            this.rbtnOneUser.TabIndex = 12;
            this.rbtnOneUser.Text = "مستخدم محدد";
            this.rbtnOneUser.UseVisualStyleBackColor = true;
            // 
            // rbtnAllUser
            // 
            this.rbtnAllUser.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.rbtnAllUser.AutoSize = true;
            this.rbtnAllUser.Checked = true;
            this.rbtnAllUser.ForeColor = System.Drawing.Color.Blue;
            this.rbtnAllUser.Location = new System.Drawing.Point(1104, 7);
            this.rbtnAllUser.Name = "rbtnAllUser";
            this.rbtnAllUser.Size = new System.Drawing.Size(154, 44);
            this.rbtnAllUser.TabIndex = 11;
            this.rbtnAllUser.TabStop = true;
            this.rbtnAllUser.Text = "كل المستخدمين";
            this.rbtnAllUser.UseVisualStyleBackColor = true;
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSearch.ForeColor = System.Drawing.Color.Black;
            this.btnSearch.Image = global::Sales_Management.Properties.Resources.search1;
            this.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearch.Location = new System.Drawing.Point(534, 6);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(131, 46);
            this.btnSearch.TabIndex = 14;
            this.btnSearch.Text = "بحث";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnPrintAll
            // 
            this.btnPrintAll.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnPrintAll.ForeColor = System.Drawing.Color.Black;
            this.btnPrintAll.Image = global::Sales_Management.Properties.Resources.printer1;
            this.btnPrintAll.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrintAll.Location = new System.Drawing.Point(318, 6);
            this.btnPrintAll.Name = "btnPrintAll";
            this.btnPrintAll.Size = new System.Drawing.Size(210, 46);
            this.btnPrintAll.TabIndex = 25;
            this.btnPrintAll.Text = "طباعة تقرير مفصل";
            this.btnPrintAll.UseVisualStyleBackColor = true;
            this.btnPrintAll.Click += new System.EventHandler(this.btnPrintAll_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnDelete.ForeColor = System.Drawing.Color.Black;
            this.btnDelete.Image = global::Sales_Management.Properties.Resources.remove;
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDelete.Location = new System.Drawing.Point(20, 6);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(275, 47);
            this.btnDelete.TabIndex = 26;
            this.btnDelete.Text = "مسح البيانات في هذه الفترة";
            this.btnDelete.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Visible = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel6, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel5, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 467);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1269, 115);
            this.tableLayoutPanel2.TabIndex = 25;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 6;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.48752F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.51052F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.61905F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.33333F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.047619F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250F));
            this.tableLayoutPanel6.Controls.Add(this.label7, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.btnDeleteSelected, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.btnPrintOrder, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.txtUserRb7h, 3, 0);
            this.tableLayoutPanel6.Controls.Add(this.label8, 4, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(0, 57);
            this.tableLayoutPanel6.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 1;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(1269, 58);
            this.tableLayoutPanel6.TabIndex = 14;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(532, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(237, 40);
            this.label7.TabIndex = 44;
            this.label7.Text = "اجمالى نسبه الربح للبائع المحدد:";
            // 
            // btnDeleteSelected
            // 
            this.btnDeleteSelected.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnDeleteSelected.Font = new System.Drawing.Font("Arabic Typesetting", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteSelected.ForeColor = System.Drawing.Color.Black;
            this.btnDeleteSelected.Image = global::Sales_Management.Properties.Resources.remove;
            this.btnDeleteSelected.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDeleteSelected.Location = new System.Drawing.Point(785, 6);
            this.btnDeleteSelected.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnDeleteSelected.Name = "btnDeleteSelected";
            this.btnDeleteSelected.Size = new System.Drawing.Size(211, 46);
            this.btnDeleteSelected.TabIndex = 43;
            this.btnDeleteSelected.Text = "حذف الفاتورة المحددة";
            this.btnDeleteSelected.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDeleteSelected.UseVisualStyleBackColor = true;
            this.btnDeleteSelected.Click += new System.EventHandler(this.btnDeleteSelected_Click);
            // 
            // btnPrintOrder
            // 
            this.btnPrintOrder.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnPrintOrder.ForeColor = System.Drawing.Color.Black;
            this.btnPrintOrder.Image = global::Sales_Management.Properties.Resources.printer1;
            this.btnPrintOrder.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrintOrder.Location = new System.Drawing.Point(1003, 6);
            this.btnPrintOrder.Name = "btnPrintOrder";
            this.btnPrintOrder.Size = new System.Drawing.Size(263, 46);
            this.btnPrintOrder.TabIndex = 23;
            this.btnPrintOrder.Text = "طباعة الفاتورة المحددة";
            this.btnPrintOrder.UseVisualStyleBackColor = true;
            this.btnPrintOrder.Click += new System.EventHandler(this.btnPrintOrder_Click);
            // 
            // txtUserRb7h
            // 
            this.txtUserRb7h.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtUserRb7h.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUserRb7h.Location = new System.Drawing.Point(306, 6);
            this.txtUserRb7h.Name = "txtUserRb7h";
            this.txtUserRb7h.ReadOnly = true;
            this.txtUserRb7h.Size = new System.Drawing.Size(211, 46);
            this.txtUserRb7h.TabIndex = 45;
            this.txtUserRb7h.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(262, 9);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(38, 40);
            this.label8.TabIndex = 46;
            this.label8.Text = "%";
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 6;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.48752F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.51052F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.6195F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.59847F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.78967F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 247F));
            this.tableLayoutPanel5.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.txtTotal, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.label5, 2, 0);
            this.tableLayoutPanel5.Controls.Add(this.txtTotalTax, 3, 0);
            this.tableLayoutPanel5.Controls.Add(this.label6, 4, 0);
            this.tableLayoutPanel5.Controls.Add(this.txtPriceTax, 5, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel5.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(1269, 57);
            this.tableLayoutPanel5.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(1005, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(259, 40);
            this.label1.TabIndex = 20;
            this.label1.Text = "اجمالى مبلغ المبيعات قبل الضريبة:";
            // 
            // txtTotal
            // 
            this.txtTotal.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTotal.Location = new System.Drawing.Point(793, 5);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.ReadOnly = true;
            this.txtTotal.Size = new System.Drawing.Size(194, 46);
            this.txtTotal.TabIndex = 21;
            this.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(678, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 57);
            this.label5.TabIndex = 38;
            this.label5.Text = "اجمالى الضريبة:";
            // 
            // txtTotalTax
            // 
            this.txtTotalTax.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtTotalTax.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTotalTax.Location = new System.Drawing.Point(455, 5);
            this.txtTotalTax.Name = "txtTotalTax";
            this.txtTotalTax.ReadOnly = true;
            this.txtTotalTax.Size = new System.Drawing.Size(194, 46);
            this.txtTotalTax.TabIndex = 39;
            this.txtTotalTax.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(255, 8);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(193, 40);
            this.label6.TabIndex = 40;
            this.label6.Text = "اجمالى البيع بعد الضريبة:";
            // 
            // txtPriceTax
            // 
            this.txtPriceTax.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtPriceTax.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPriceTax.Location = new System.Drawing.Point(19, 5);
            this.txtPriceTax.Name = "txtPriceTax";
            this.txtPriceTax.ReadOnly = true;
            this.txtPriceTax.Size = new System.Drawing.Size(213, 46);
            this.txtPriceTax.TabIndex = 41;
            this.txtPriceTax.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Frm_SalesReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 39F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1269, 582);
            this.Controls.Add(this.tplParent);
            this.Font = new System.Drawing.Font("Arabic Typesetting", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Frm_SalesReport";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "تقارير المبيعات";
            this.Load += new System.EventHandler(this.Frm_SalesReport_Load);
            this.tplParent.ResumeLayout(false);
            this.tplParent.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvSearch)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tplParent;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView DgvSearch;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtOrderNumber;
        private System.Windows.Forms.CheckBox checkOrderNumber;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker DtpTo;
        private System.Windows.Forms.DateTimePicker DtpFrom;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.RadioButton rbtnOneUser;
        private System.Windows.Forms.RadioButton rbtnAllUser;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnPrintAll;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.ComboBox cbxUser;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnDeleteSelected;
        private System.Windows.Forms.Button btnPrintOrder;
        private System.Windows.Forms.TextBox txtUserRb7h;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtTotalTax;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtPriceTax;
    }
}