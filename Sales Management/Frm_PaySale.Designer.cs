
namespace Sales_Management
{
    partial class Frm_PaySale
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
            this.tlpParent1 = new System.Windows.Forms.TableLayoutPanel();
            this.tlpParent = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtMadfoua = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMatloub = new System.Windows.Forms.TextBox();
            this.txtBakey = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnReturn = new System.Windows.Forms.Button();
            this.btnEnter = new System.Windows.Forms.Button();
            this.checkVisa = new System.Windows.Forms.CheckBox();
            this.tlpParent1.SuspendLayout();
            this.tlpParent.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpParent1
            // 
            this.tlpParent1.ColumnCount = 1;
            this.tlpParent1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpParent1.Controls.Add(this.tlpParent, 0, 0);
            this.tlpParent1.Controls.Add(this.tableLayoutPanel1, 0, 2);
            this.tlpParent1.Controls.Add(this.checkVisa, 0, 1);
            this.tlpParent1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpParent1.Location = new System.Drawing.Point(0, 0);
            this.tlpParent1.Name = "tlpParent1";
            this.tlpParent1.RowCount = 3;
            this.tlpParent1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60.13289F));
            this.tlpParent1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tlpParent1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 39.86711F));
            this.tlpParent1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpParent1.Size = new System.Drawing.Size(410, 355);
            this.tlpParent1.TabIndex = 1;
            // 
            // tlpParent
            // 
            this.tlpParent.ColumnCount = 2;
            this.tlpParent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.2714F));
            this.tlpParent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 69.7286F));
            this.tlpParent.Controls.Add(this.label1, 0, 0);
            this.tlpParent.Controls.Add(this.label3, 0, 1);
            this.tlpParent.Controls.Add(this.txtMadfoua, 1, 1);
            this.tlpParent.Controls.Add(this.label2, 0, 2);
            this.tlpParent.Controls.Add(this.txtMatloub, 1, 0);
            this.tlpParent.Controls.Add(this.txtBakey, 1, 2);
            this.tlpParent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpParent.Location = new System.Drawing.Point(0, 0);
            this.tlpParent.Margin = new System.Windows.Forms.Padding(0);
            this.tlpParent.Name = "tlpParent";
            this.tlpParent.RowCount = 3;
            this.tlpParent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpParent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpParent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpParent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpParent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpParent.Size = new System.Drawing.Size(410, 184);
            this.tlpParent.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(304, 10);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 40);
            this.label1.TabIndex = 2;
            this.label1.Text = "المطلوب :";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(308, 71);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 40);
            this.label3.TabIndex = 4;
            this.label3.Text = "المدفوع :";
            // 
            // txtMadfoua
            // 
            this.txtMadfoua.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtMadfoua.Font = new System.Drawing.Font("Arabic Typesetting", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMadfoua.Location = new System.Drawing.Point(16, 68);
            this.txtMadfoua.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.txtMadfoua.Name = "txtMadfoua";
            this.txtMadfoua.Size = new System.Drawing.Size(255, 46);
            this.txtMadfoua.TabIndex = 17;
            this.txtMadfoua.Text = "0";
            this.txtMadfoua.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtMadfoua.TextChanged += new System.EventHandler(this.txtMadfoua_TextChanged);
            this.txtMadfoua.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMadfoua_KeyPress);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(315, 133);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 40);
            this.label2.TabIndex = 3;
            this.label2.Text = "الباقي :";
            // 
            // txtMatloub
            // 
            this.txtMatloub.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtMatloub.Font = new System.Drawing.Font("Arabic Typesetting", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMatloub.Location = new System.Drawing.Point(16, 7);
            this.txtMatloub.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.txtMatloub.Name = "txtMatloub";
            this.txtMatloub.ReadOnly = true;
            this.txtMatloub.Size = new System.Drawing.Size(255, 46);
            this.txtMatloub.TabIndex = 18;
            this.txtMatloub.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtBakey
            // 
            this.txtBakey.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtBakey.Font = new System.Drawing.Font("Arabic Typesetting", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBakey.Location = new System.Drawing.Point(15, 130);
            this.txtBakey.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.txtBakey.Name = "txtBakey";
            this.txtBakey.ReadOnly = true;
            this.txtBakey.Size = new System.Drawing.Size(256, 46);
            this.txtBakey.TabIndex = 19;
            this.txtBakey.Text = "0";
            this.txtBakey.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.btnReturn, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnEnter, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 232);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(410, 123);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // btnReturn
            // 
            this.btnReturn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnReturn.ForeColor = System.Drawing.Color.Black;
            this.btnReturn.Image = global::Sales_Management.Properties.Resources.undo__1_;
            this.btnReturn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReturn.Location = new System.Drawing.Point(43, 67);
            this.btnReturn.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(325, 50);
            this.btnReturn.TabIndex = 22;
            this.btnReturn.Text = "رجوع F12";
            this.btnReturn.UseVisualStyleBackColor = true;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // btnEnter
            // 
            this.btnEnter.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnEnter.ForeColor = System.Drawing.Color.Black;
            this.btnEnter.Image = global::Sales_Management.Properties.Resources.floppy_disk;
            this.btnEnter.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEnter.Location = new System.Drawing.Point(45, 6);
            this.btnEnter.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnEnter.Name = "btnEnter";
            this.btnEnter.Size = new System.Drawing.Size(321, 49);
            this.btnEnter.TabIndex = 21;
            this.btnEnter.Text = "للحفظ و الطباعة اضغط انتر Enter";
            this.btnEnter.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEnter.UseVisualStyleBackColor = true;
            this.btnEnter.Click += new System.EventHandler(this.btnEnter_Click);
            // 
            // checkVisa
            // 
            this.checkVisa.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.checkVisa.AutoSize = true;
            this.checkVisa.Location = new System.Drawing.Point(77, 187);
            this.checkVisa.Name = "checkVisa";
            this.checkVisa.Size = new System.Drawing.Size(257, 42);
            this.checkVisa.TabIndex = 4;
            this.checkVisa.Text = "دفع عن طريق البطاقة الاتمانيه";
            this.checkVisa.UseVisualStyleBackColor = true;
            // 
            // Frm_PaySale
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 39F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(410, 355);
            this.Controls.Add(this.tlpParent1);
            this.Font = new System.Drawing.Font("Arabic Typesetting", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "Frm_PaySale";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Frm_PaySale";
            this.Load += new System.EventHandler(this.Frm_PaySale_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Frm_PaySale_KeyDown);
            this.tlpParent1.ResumeLayout(false);
            this.tlpParent1.PerformLayout();
            this.tlpParent.ResumeLayout(false);
            this.tlpParent.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpParent1;
        private System.Windows.Forms.TableLayoutPanel tlpParent;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMatloub;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.Button btnEnter;
        private System.Windows.Forms.CheckBox checkVisa;
        private System.Windows.Forms.TextBox txtBakey;
        private System.Windows.Forms.TextBox txtMadfoua;
    }
}