namespace IMS
{
    partial class frmAddProduct
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAddProduct));
            this.label10 = new System.Windows.Forms.Label();
            this.txtMinstock = new System.Windows.Forms.ComboBox();
            this.txtQty = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblbrand = new System.Windows.Forms.Label();
            this.txtFormulation = new System.Windows.Forms.ComboBox();
            this.lblgeneric = new System.Windows.Forms.Label();
            this.lblcategory = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblunit = new System.Windows.Forms.Label();
            this.lblformulation = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtMaxstock = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtBrand = new System.Windows.Forms.ComboBox();
            this.aaa = new System.Windows.Forms.Label();
            this.txtCategory = new System.Windows.Forms.ComboBox();
            this.txtUnit = new System.Windows.Forms.ComboBox();
            this.txtGeneric = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblContext = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.Red;
            this.label10.Location = new System.Drawing.Point(86, 161);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(122, 13);
            this.label10.TabIndex = 119;
            this.label10.Text = "*ในกรณีที่ไม่ใช่เวชภัณฑ์";
            // 
            // txtMinstock
            // 
            this.txtMinstock.FormattingEnabled = true;
            this.txtMinstock.Location = new System.Drawing.Point(89, 272);
            this.txtMinstock.Name = "txtMinstock";
            this.txtMinstock.Size = new System.Drawing.Size(185, 21);
            this.txtMinstock.TabIndex = 104;
            // 
            // txtQty
            // 
            this.txtQty.FormattingEnabled = true;
            this.txtQty.Location = new System.Drawing.Point(88, 184);
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(185, 21);
            this.txtQty.TabIndex = 117;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(14, 139);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(64, 13);
            this.label9.TabIndex = 113;
            this.label9.Text = "ความเข้มข้น";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(42, 190);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(40, 13);
            this.label8.TabIndex = 118;
            this.label8.Text = "จำนวน";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(27, 217);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 13);
            this.label6.TabIndex = 115;
            this.label6.Text = "หน่วยนับ";
            // 
            // lblbrand
            // 
            this.lblbrand.AutoSize = true;
            this.lblbrand.Location = new System.Drawing.Point(288, 55);
            this.lblbrand.Name = "lblbrand";
            this.lblbrand.Size = new System.Drawing.Size(23, 13);
            this.lblbrand.TabIndex = 107;
            this.lblbrand.Text = "ฟฟ";
            this.lblbrand.Visible = false;
            // 
            // txtFormulation
            // 
            this.txtFormulation.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtFormulation.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.txtFormulation.BackColor = System.Drawing.Color.White;
            this.txtFormulation.FormattingEnabled = true;
            this.txtFormulation.Location = new System.Drawing.Point(89, 136);
            this.txtFormulation.Name = "txtFormulation";
            this.txtFormulation.Size = new System.Drawing.Size(185, 21);
            this.txtFormulation.TabIndex = 101;
            this.txtFormulation.TextChanged += new System.EventHandler(this.TxtFormulation_TextChanged);
            // 
            // lblgeneric
            // 
            this.lblgeneric.AutoSize = true;
            this.lblgeneric.Location = new System.Drawing.Point(288, 80);
            this.lblgeneric.Name = "lblgeneric";
            this.lblgeneric.Size = new System.Drawing.Size(23, 13);
            this.lblgeneric.TabIndex = 108;
            this.lblgeneric.Text = "ฟฟ";
            this.lblgeneric.Visible = false;
            // 
            // lblcategory
            // 
            this.lblcategory.AutoSize = true;
            this.lblcategory.Location = new System.Drawing.Point(288, 110);
            this.lblcategory.Name = "lblcategory";
            this.lblcategory.Size = new System.Drawing.Size(23, 13);
            this.lblcategory.TabIndex = 109;
            this.lblcategory.Text = "ฟฟ";
            this.lblcategory.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(30, 110);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 13);
            this.label5.TabIndex = 112;
            this.label5.Text = "หมวดหมู่";
            // 
            // lblunit
            // 
            this.lblunit.AutoSize = true;
            this.lblunit.Location = new System.Drawing.Point(288, 196);
            this.lblunit.Name = "lblunit";
            this.lblunit.Size = new System.Drawing.Size(23, 13);
            this.lblunit.TabIndex = 111;
            this.lblunit.Text = "ฟฟ";
            this.lblunit.Visible = false;
            // 
            // lblformulation
            // 
            this.lblformulation.AutoSize = true;
            this.lblformulation.Location = new System.Drawing.Point(288, 144);
            this.lblformulation.Name = "lblformulation";
            this.lblformulation.Size = new System.Drawing.Size(23, 13);
            this.lblformulation.TabIndex = 110;
            this.lblformulation.Text = "ฟฟ";
            this.lblformulation.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(31, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 13);
            this.label4.TabIndex = 116;
            this.label4.Text = "ชื่อสามัญ";
            // 
            // txtMaxstock
            // 
            this.txtMaxstock.FormattingEnabled = true;
            this.txtMaxstock.Location = new System.Drawing.Point(89, 242);
            this.txtMaxstock.Name = "txtMaxstock";
            this.txtMaxstock.Size = new System.Drawing.Size(185, 21);
            this.txtMaxstock.TabIndex = 103;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 114;
            this.label3.Text = "ชื่อการค้า";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 275);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 13);
            this.label7.TabIndex = 106;
            this.label7.Text = "คงคลังน้อยสุด";
            // 
            // txtBrand
            // 
            this.txtBrand.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtBrand.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.txtBrand.BackColor = System.Drawing.Color.White;
            this.txtBrand.FormattingEnabled = true;
            this.txtBrand.Location = new System.Drawing.Point(89, 47);
            this.txtBrand.Name = "txtBrand";
            this.txtBrand.Size = new System.Drawing.Size(186, 21);
            this.txtBrand.TabIndex = 98;
            this.txtBrand.TextChanged += new System.EventHandler(this.TxtBrand_TextChanged);
            // 
            // aaa
            // 
            this.aaa.AutoSize = true;
            this.aaa.Location = new System.Drawing.Point(17, 245);
            this.aaa.Name = "aaa";
            this.aaa.Size = new System.Drawing.Size(61, 13);
            this.aaa.TabIndex = 105;
            this.aaa.Text = "คงคลังสูงสุด";
            // 
            // txtCategory
            // 
            this.txtCategory.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtCategory.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.txtCategory.BackColor = System.Drawing.Color.White;
            this.txtCategory.FormattingEnabled = true;
            this.txtCategory.Location = new System.Drawing.Point(89, 107);
            this.txtCategory.Name = "txtCategory";
            this.txtCategory.Size = new System.Drawing.Size(185, 21);
            this.txtCategory.TabIndex = 100;
            this.txtCategory.TextChanged += new System.EventHandler(this.TxtCategory_TextChanged);
            // 
            // txtUnit
            // 
            this.txtUnit.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtUnit.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.txtUnit.BackColor = System.Drawing.Color.White;
            this.txtUnit.FormattingEnabled = true;
            this.txtUnit.Location = new System.Drawing.Point(88, 214);
            this.txtUnit.Name = "txtUnit";
            this.txtUnit.Size = new System.Drawing.Size(185, 21);
            this.txtUnit.TabIndex = 102;
            this.txtUnit.TextChanged += new System.EventHandler(this.TxtUnit_TextChanged);
            // 
            // txtGeneric
            // 
            this.txtGeneric.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtGeneric.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.txtGeneric.BackColor = System.Drawing.Color.White;
            this.txtGeneric.FormattingEnabled = true;
            this.txtGeneric.Location = new System.Drawing.Point(89, 77);
            this.txtGeneric.Name = "txtGeneric";
            this.txtGeneric.Size = new System.Drawing.Size(186, 21);
            this.txtGeneric.TabIndex = 99;
            this.txtGeneric.TextChanged += new System.EventHandler(this.TxtGeneric_TextChanged);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.LimeGreen;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Location = new System.Drawing.Point(116, 303);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 21);
            this.btnSave.TabIndex = 120;
            this.btnSave.Text = "บันทึก";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnUpdate.FlatAppearance.BorderSize = 0;
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdate.ForeColor = System.Drawing.Color.Black;
            this.btnUpdate.Location = new System.Drawing.Point(116, 303);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 21);
            this.btnUpdate.TabIndex = 121;
            this.btnUpdate.Text = "บันทึกแก้ไข";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.BtnUpdate_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Red;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Location = new System.Drawing.Point(200, 302);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 21);
            this.btnCancel.TabIndex = 122;
            this.btnCancel.Text = "ยกเลิก";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(207, 161);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 13);
            this.label1.TabIndex = 123;
            this.label1.Text = "ให้ใส่ค่าความเข้มข้นเป็น 0";
            // 
            // lblContext
            // 
            this.lblContext.AutoSize = true;
            this.lblContext.ForeColor = System.Drawing.Color.Blue;
            this.lblContext.Location = new System.Drawing.Point(86, 18);
            this.lblContext.Name = "lblContext";
            this.lblContext.Size = new System.Drawing.Size(35, 13);
            this.lblContext.TabIndex = 124;
            this.lblContext.Text = "label2";
            // 
            // frmAddProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(366, 340);
            this.Controls.Add(this.lblContext);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtMinstock);
            this.Controls.Add(this.txtQty);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblbrand);
            this.Controls.Add(this.txtFormulation);
            this.Controls.Add(this.lblgeneric);
            this.Controls.Add(this.lblcategory);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblunit);
            this.Controls.Add(this.lblformulation);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtMaxstock);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtBrand);
            this.Controls.Add(this.aaa);
            this.Controls.Add(this.txtCategory);
            this.Controls.Add(this.txtUnit);
            this.Controls.Add(this.txtGeneric);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmAddProduct";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "IMS v0.1";
            this.Load += new System.EventHandler(this.FrmAddProduct_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.Label lblbrand;
        public System.Windows.Forms.Label lblgeneric;
        public System.Windows.Forms.Label lblcategory;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.Label lblunit;
        public System.Windows.Forms.Label lblformulation;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label aaa;
        public System.Windows.Forms.Button btnSave;
        public System.Windows.Forms.Button btnUpdate;
        public System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.ComboBox txtMinstock;
        public System.Windows.Forms.ComboBox txtQty;
        public System.Windows.Forms.ComboBox txtFormulation;
        public System.Windows.Forms.ComboBox txtMaxstock;
        public System.Windows.Forms.ComboBox txtBrand;
        public System.Windows.Forms.ComboBox txtCategory;
        public System.Windows.Forms.ComboBox txtUnit;
        public System.Windows.Forms.ComboBox txtGeneric;
        public System.Windows.Forms.Label lblContext;
    }
}