
namespace Sales_Management
{
    partial class Frm_StockBankTransfire
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
            this.tblParent = new System.Windows.Forms.TableLayoutPanel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.cbxStock = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblMoneyStock = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblMoneyBank = new System.Windows.Forms.Label();
            this.rbtnFromStockTobank = new System.Windows.Forms.RadioButton();
            this.rbtnFromBankToStock = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.NudPrice = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.DtpDate = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.tblParent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NudPrice)).BeginInit();
            this.SuspendLayout();
            // 
            // tblParent
            // 
            this.tblParent.ColumnCount = 2;
            this.tblParent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblParent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblParent.Controls.Add(this.btnAdd, 0, 7);
            this.tblParent.Controls.Add(this.label5, 0, 0);
            this.tblParent.Controls.Add(this.cbxStock, 1, 0);
            this.tblParent.Controls.Add(this.label1, 0, 1);
            this.tblParent.Controls.Add(this.lblMoneyStock, 1, 1);
            this.tblParent.Controls.Add(this.label3, 0, 2);
            this.tblParent.Controls.Add(this.lblMoneyBank, 1, 2);
            this.tblParent.Controls.Add(this.rbtnFromStockTobank, 0, 3);
            this.tblParent.Controls.Add(this.rbtnFromBankToStock, 1, 3);
            this.tblParent.Controls.Add(this.label2, 0, 4);
            this.tblParent.Controls.Add(this.NudPrice, 1, 4);
            this.tblParent.Controls.Add(this.label4, 0, 5);
            this.tblParent.Controls.Add(this.DtpDate, 1, 5);
            this.tblParent.Controls.Add(this.label6, 0, 6);
            this.tblParent.Controls.Add(this.txtName, 1, 6);
            this.tblParent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblParent.Location = new System.Drawing.Point(0, 0);
            this.tblParent.Name = "tblParent";
            this.tblParent.RowCount = 8;
            this.tblParent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.06306F));
            this.tblParent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.71171F));
            this.tblParent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tblParent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tblParent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tblParent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tblParent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tblParent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tblParent.Size = new System.Drawing.Size(499, 475);
            this.tblParent.TabIndex = 0;
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnAdd.ForeColor = System.Drawing.Color.Black;
            this.btnAdd.Image = global::Sales_Management.Properties.Resources.plus_32;
            this.btnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdd.Location = new System.Drawing.Point(253, 417);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(243, 53);
            this.btnAdd.TabIndex = 59;
            this.btnAdd.Text = "اتمام عملية التحويل";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(327, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 40);
            this.label5.TabIndex = 31;
            this.label5.Text = "اختر خزنة:";
            // 
            // cbxStock
            // 
            this.cbxStock.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbxStock.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxStock.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxStock.FormattingEnabled = true;
            this.cbxStock.Location = new System.Drawing.Point(3, 7);
            this.cbxStock.Name = "cbxStock";
            this.cbxStock.Size = new System.Drawing.Size(244, 47);
            this.cbxStock.TabIndex = 32;
            this.cbxStock.SelectionChangeCommitted += new System.EventHandler(this.cbxStock_SelectionChangeCommitted);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(294, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(161, 40);
            this.label1.TabIndex = 33;
            this.label1.Text = "رصيد الخزنة المحددة:";
            // 
            // lblMoneyStock
            // 
            this.lblMoneyStock.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblMoneyStock.AutoSize = true;
            this.lblMoneyStock.ForeColor = System.Drawing.Color.Blue;
            this.lblMoneyStock.Location = new System.Drawing.Point(108, 69);
            this.lblMoneyStock.Name = "lblMoneyStock";
            this.lblMoneyStock.Size = new System.Drawing.Size(35, 40);
            this.lblMoneyStock.TabIndex = 48;
            this.lblMoneyStock.Text = "...";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(282, 126);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(185, 40);
            this.label3.TabIndex = 49;
            this.label3.Text = "رصيد البنك الحالى هو:";
            // 
            // lblMoneyBank
            // 
            this.lblMoneyBank.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblMoneyBank.AutoSize = true;
            this.lblMoneyBank.ForeColor = System.Drawing.Color.Blue;
            this.lblMoneyBank.Location = new System.Drawing.Point(108, 126);
            this.lblMoneyBank.Name = "lblMoneyBank";
            this.lblMoneyBank.Size = new System.Drawing.Size(35, 40);
            this.lblMoneyBank.TabIndex = 50;
            this.lblMoneyBank.Text = "...";
            // 
            // rbtnFromStockTobank
            // 
            this.rbtnFromStockTobank.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.rbtnFromStockTobank.AutoSize = true;
            this.rbtnFromStockTobank.Checked = true;
            this.rbtnFromStockTobank.Location = new System.Drawing.Point(260, 183);
            this.rbtnFromStockTobank.Name = "rbtnFromStockTobank";
            this.rbtnFromStockTobank.Size = new System.Drawing.Size(229, 44);
            this.rbtnFromStockTobank.TabIndex = 51;
            this.rbtnFromStockTobank.TabStop = true;
            this.rbtnFromStockTobank.Text = "تحويل من الخزنة الى البنك";
            this.rbtnFromStockTobank.UseVisualStyleBackColor = true;
            // 
            // rbtnFromBankToStock
            // 
            this.rbtnFromBankToStock.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.rbtnFromBankToStock.AutoSize = true;
            this.rbtnFromBankToStock.Location = new System.Drawing.Point(11, 183);
            this.rbtnFromBankToStock.Name = "rbtnFromBankToStock";
            this.rbtnFromBankToStock.Size = new System.Drawing.Size(229, 44);
            this.rbtnFromBankToStock.TabIndex = 52;
            this.rbtnFromBankToStock.Text = "تحويل من البنك الى الخزنة";
            this.rbtnFromBankToStock.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(303, 244);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(144, 40);
            this.label2.TabIndex = 53;
            this.label2.Text = "المبلغ المراد تحويلة:";
            // 
            // NudPrice
            // 
            this.NudPrice.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.NudPrice.DecimalPlaces = 2;
            this.NudPrice.Location = new System.Drawing.Point(3, 241);
            this.NudPrice.Maximum = new decimal(new int[] {
            1215752192,
            23,
            0,
            0});
            this.NudPrice.Name = "NudPrice";
            this.NudPrice.Size = new System.Drawing.Size(244, 46);
            this.NudPrice.TabIndex = 54;
            this.NudPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.NudPrice.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(314, 303);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(121, 40);
            this.label4.TabIndex = 55;
            this.label4.Text = "تاريخ التحويل:";
            // 
            // DtpDate
            // 
            this.DtpDate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.DtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DtpDate.Location = new System.Drawing.Point(3, 300);
            this.DtpDate.Name = "DtpDate";
            this.DtpDate.Size = new System.Drawing.Size(244, 46);
            this.DtpDate.TabIndex = 56;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(274, 362);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(201, 40);
            this.label6.TabIndex = 57;
            this.label6.Text = "اسم المسؤل عن التحويل:";
            // 
            // txtName
            // 
            this.txtName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtName.Location = new System.Drawing.Point(3, 359);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(244, 46);
            this.txtName.TabIndex = 58;
            this.txtName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Frm_StockBankTransfire
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 39F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(499, 475);
            this.Controls.Add(this.tblParent);
            this.Font = new System.Drawing.Font("Arabic Typesetting", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Frm_StockBankTransfire";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "تحويل رصيد من الخزنة الى البنك والعكس";
            this.Load += new System.EventHandler(this.Frm_StockBankTransfire_Load);
            this.tblParent.ResumeLayout(false);
            this.tblParent.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NudPrice)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblParent;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbxStock;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblMoneyStock;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblMoneyBank;
        private System.Windows.Forms.RadioButton rbtnFromStockTobank;
        private System.Windows.Forms.RadioButton rbtnFromBankToStock;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown NudPrice;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker DtpDate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Button btnAdd;
    }
}