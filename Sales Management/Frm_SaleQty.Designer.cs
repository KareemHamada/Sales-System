
namespace Sales_Management
{
    partial class Frm_SaleQty
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
            this.tlpParent = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSalePrice = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDiscount = new System.Windows.Forms.TextBox();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.btnEnter = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cbxUnit = new System.Windows.Forms.ComboBox();
            this.tlpParent.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpParent
            // 
            this.tlpParent.ColumnCount = 2;
            this.tlpParent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.2714F));
            this.tlpParent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 69.7286F));
            this.tlpParent.Controls.Add(this.label1, 0, 0);
            this.tlpParent.Controls.Add(this.label3, 0, 2);
            this.tlpParent.Controls.Add(this.txtSalePrice, 1, 2);
            this.tlpParent.Controls.Add(this.label2, 0, 3);
            this.tlpParent.Controls.Add(this.txtDiscount, 1, 0);
            this.tlpParent.Controls.Add(this.txtQty, 1, 3);
            this.tlpParent.Controls.Add(this.btnEnter, 1, 4);
            this.tlpParent.Controls.Add(this.label4, 0, 1);
            this.tlpParent.Controls.Add(this.cbxUnit, 1, 1);
            this.tlpParent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpParent.Location = new System.Drawing.Point(0, 0);
            this.tlpParent.Name = "tlpParent";
            this.tlpParent.RowCount = 5;
            this.tlpParent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpParent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpParent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpParent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpParent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpParent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpParent.Size = new System.Drawing.Size(406, 312);
            this.tlpParent.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(309, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 40);
            this.label1.TabIndex = 2;
            this.label1.Text = "الخصم :";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(296, 135);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 40);
            this.label3.TabIndex = 4;
            this.label3.Text = "سعر البيع :";
            // 
            // txtSalePrice
            // 
            this.txtSalePrice.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtSalePrice.Location = new System.Drawing.Point(15, 132);
            this.txtSalePrice.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.txtSalePrice.Name = "txtSalePrice";
            this.txtSalePrice.ReadOnly = true;
            this.txtSalePrice.Size = new System.Drawing.Size(255, 46);
            this.txtSalePrice.TabIndex = 17;
            this.txtSalePrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSalePrice.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSalePrice_KeyPress);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(310, 197);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 40);
            this.label2.TabIndex = 3;
            this.label2.Text = "الكمية :";
            // 
            // txtDiscount
            // 
            this.txtDiscount.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtDiscount.Location = new System.Drawing.Point(15, 8);
            this.txtDiscount.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.txtDiscount.Name = "txtDiscount";
            this.txtDiscount.Size = new System.Drawing.Size(255, 46);
            this.txtDiscount.TabIndex = 18;
            this.txtDiscount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtDiscount.TextChanged += new System.EventHandler(this.txtDiscount_TextChanged);
            this.txtDiscount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDiscount_KeyPress);
            // 
            // txtQty
            // 
            this.txtQty.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtQty.Location = new System.Drawing.Point(14, 194);
            this.txtQty.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(256, 46);
            this.txtQty.TabIndex = 19;
            this.txtQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtQty.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtQty_KeyPress);
            // 
            // btnEnter
            // 
            this.btnEnter.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnEnter.ForeColor = System.Drawing.Color.Black;
            this.btnEnter.Image = global::Sales_Management.Properties.Resources.floppy_disk;
            this.btnEnter.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEnter.Location = new System.Drawing.Point(8, 255);
            this.btnEnter.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnEnter.Name = "btnEnter";
            this.btnEnter.Size = new System.Drawing.Size(269, 50);
            this.btnEnter.TabIndex = 20;
            this.btnEnter.Text = "اضغط انتر";
            this.btnEnter.UseVisualStyleBackColor = true;
            this.btnEnter.Click += new System.EventHandler(this.btnEnter_Click);
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(295, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 40);
            this.label4.TabIndex = 34;
            this.label4.Text = "اختر وحدة:";
            // 
            // cbxUnit
            // 
            this.cbxUnit.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbxUnit.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxUnit.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxUnit.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxUnit.FormattingEnabled = true;
            this.cbxUnit.Location = new System.Drawing.Point(11, 74);
            this.cbxUnit.Name = "cbxUnit";
            this.cbxUnit.Size = new System.Drawing.Size(262, 38);
            this.cbxUnit.TabIndex = 35;
            this.cbxUnit.SelectionChangeCommitted += new System.EventHandler(this.cbxUnit_SelectionChangeCommitted);
            // 
            // Frm_SaleQty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 39F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(406, 312);
            this.Controls.Add(this.tlpParent);
            this.Font = new System.Drawing.Font("Arabic Typesetting", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "Frm_SaleQty";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Frm_SaleQty";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_SaleQty_FormClosing);
            this.Load += new System.EventHandler(this.Frm_SaleQty_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Frm_SaleQty_KeyDown);
            this.tlpParent.ResumeLayout(false);
            this.tlpParent.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpParent;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSalePrice;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDiscount;
        private System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.Button btnEnter;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbxUnit;
    }
}