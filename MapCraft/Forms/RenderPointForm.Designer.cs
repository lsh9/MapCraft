namespace MapCraft.Forms
{
    partial class RenderPointForm
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
            this.tabControlInner = new System.Windows.Forms.TabControl();
            this.tabPageSingle = new System.Windows.Forms.TabPage();
            this.btnSingleColor = new System.Windows.Forms.Button();
            this.label21 = new System.Windows.Forms.Label();
            this.numericUpDownSize = new System.Windows.Forms.NumericUpDown();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.cboStyle = new System.Windows.Forms.ComboBox();
            this.tabPageUnique = new System.Windows.Forms.TabPage();
            this.AttributeTable = new System.Windows.Forms.DataGridView();
            this.btnGetUniqueValue = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.cbFields = new System.Windows.Forms.ComboBox();
            this.tabPageClass = new System.Windows.Forms.TabPage();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.Class_Apply = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.Class_OK = new System.Windows.Forms.Button();
            this.Class_ToSize = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.Class_FromSize = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.btnPointRenderApply = new System.Windows.Forms.Button();
            this.btnPointRendererConfirm = new System.Windows.Forms.Button();
            this.Class_bdrColor = new System.Windows.Forms.PictureBox();
            this.Class_ToColor = new System.Windows.Forms.PictureBox();
            this.Class_FromColor = new System.Windows.Forms.PictureBox();
            this.tabControlInner.SuspendLayout();
            this.tabPageSingle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSize)).BeginInit();
            this.tabPageUnique.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AttributeTable)).BeginInit();
            this.tabPageClass.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Class_bdrColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Class_ToColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Class_FromColor)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControlInner
            // 
            this.tabControlInner.CausesValidation = false;
            this.tabControlInner.Controls.Add(this.tabPageSingle);
            this.tabControlInner.Controls.Add(this.tabPageUnique);
            this.tabControlInner.Controls.Add(this.tabPageClass);
            this.tabControlInner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlInner.Location = new System.Drawing.Point(0, 0);
            this.tabControlInner.Name = "tabControlInner";
            this.tabControlInner.SelectedIndex = 0;
            this.tabControlInner.Size = new System.Drawing.Size(449, 441);
            this.tabControlInner.TabIndex = 3;
            this.tabControlInner.Enter += new System.EventHandler(this.tabControlInner_Enter);
            // 
            // tabPageSingle
            // 
            this.tabPageSingle.Controls.Add(this.btnSingleColor);
            this.tabPageSingle.Controls.Add(this.label21);
            this.tabPageSingle.Controls.Add(this.numericUpDownSize);
            this.tabPageSingle.Controls.Add(this.label25);
            this.tabPageSingle.Controls.Add(this.label26);
            this.tabPageSingle.Controls.Add(this.cboStyle);
            this.tabPageSingle.Location = new System.Drawing.Point(4, 22);
            this.tabPageSingle.Name = "tabPageSingle";
            this.tabPageSingle.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSingle.Size = new System.Drawing.Size(441, 415);
            this.tabPageSingle.TabIndex = 0;
            this.tabPageSingle.Text = "单一符号法";
            this.tabPageSingle.UseVisualStyleBackColor = true;
            this.tabPageSingle.Enter += new System.EventHandler(this.tabPageSingle_Enter);
            // 
            // btnSingleColor
            // 
            this.btnSingleColor.BackColor = System.Drawing.Color.Black;
            this.btnSingleColor.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnSingleColor.Location = new System.Drawing.Point(206, 218);
            this.btnSingleColor.Name = "btnSingleColor";
            this.btnSingleColor.Size = new System.Drawing.Size(98, 26);
            this.btnSingleColor.TabIndex = 48;
            this.btnSingleColor.UseVisualStyleBackColor = false;
            this.btnSingleColor.Click += new System.EventHandler(this.btnSingleColor_Click);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("宋体", 12F);
            this.label21.Location = new System.Drawing.Point(152, 221);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(47, 16);
            this.label21.TabIndex = 47;
            this.label21.Text = "颜色:";
            // 
            // numericUpDownSize
            // 
            this.numericUpDownSize.Font = new System.Drawing.Font("宋体", 12F);
            this.numericUpDownSize.Location = new System.Drawing.Point(206, 179);
            this.numericUpDownSize.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericUpDownSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownSize.Name = "numericUpDownSize";
            this.numericUpDownSize.Size = new System.Drawing.Size(98, 26);
            this.numericUpDownSize.TabIndex = 35;
            this.numericUpDownSize.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("宋体", 12F);
            this.label25.Location = new System.Drawing.Point(152, 181);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(47, 16);
            this.label25.TabIndex = 34;
            this.label25.Text = "尺寸:";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("宋体", 12F);
            this.label26.Location = new System.Drawing.Point(152, 143);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(47, 16);
            this.label26.TabIndex = 44;
            this.label26.Text = "样式:";
            // 
            // cboStyle
            // 
            this.cboStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStyle.FormattingEnabled = true;
            this.cboStyle.Items.AddRange(new object[] {
            "Circle",
            "SolidCircle",
            "Triangle",
            "SolidTriangle",
            "Square",
            "SolidSquare",
            "CircleDot",
            "CircleCircle"});
            this.cboStyle.Location = new System.Drawing.Point(206, 142);
            this.cboStyle.Name = "cboStyle";
            this.cboStyle.Size = new System.Drawing.Size(98, 20);
            this.cboStyle.TabIndex = 43;
            // 
            // tabPageUnique
            // 
            this.tabPageUnique.Controls.Add(this.AttributeTable);
            this.tabPageUnique.Controls.Add(this.btnGetUniqueValue);
            this.tabPageUnique.Controls.Add(this.label12);
            this.tabPageUnique.Controls.Add(this.cbFields);
            this.tabPageUnique.Location = new System.Drawing.Point(4, 22);
            this.tabPageUnique.Name = "tabPageUnique";
            this.tabPageUnique.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageUnique.Size = new System.Drawing.Size(441, 415);
            this.tabPageUnique.TabIndex = 1;
            this.tabPageUnique.Text = "唯一值法";
            this.tabPageUnique.UseVisualStyleBackColor = true;
            // 
            // AttributeTable
            // 
            this.AttributeTable.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.AttributeTable.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.AttributeTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.AttributeTable.Location = new System.Drawing.Point(0, 28);
            this.AttributeTable.Margin = new System.Windows.Forms.Padding(2);
            this.AttributeTable.Name = "AttributeTable";
            this.AttributeTable.RowTemplate.Height = 30;
            this.AttributeTable.Size = new System.Drawing.Size(434, 382);
            this.AttributeTable.TabIndex = 27;
            // 
            // btnGetUniqueValue
            // 
            this.btnGetUniqueValue.Location = new System.Drawing.Point(336, 2);
            this.btnGetUniqueValue.Margin = new System.Windows.Forms.Padding(2);
            this.btnGetUniqueValue.Name = "btnGetUniqueValue";
            this.btnGetUniqueValue.Size = new System.Drawing.Size(100, 23);
            this.btnGetUniqueValue.TabIndex = 24;
            this.btnGetUniqueValue.Text = "获取唯一值";
            this.btnGetUniqueValue.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(3, 6);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(29, 12);
            this.label12.TabIndex = 23;
            this.label12.Text = "字段";
            // 
            // cbFields
            // 
            this.cbFields.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbFields.FormattingEnabled = true;
            this.cbFields.Location = new System.Drawing.Point(48, 2);
            this.cbFields.Margin = new System.Windows.Forms.Padding(2);
            this.cbFields.Name = "cbFields";
            this.cbFields.Size = new System.Drawing.Size(82, 22);
            this.cbFields.TabIndex = 22;
            // 
            // tabPageClass
            // 
            this.tabPageClass.Controls.Add(this.numericUpDown1);
            this.tabPageClass.Controls.Add(this.label11);
            this.tabPageClass.Controls.Add(this.Class_Apply);
            this.tabPageClass.Controls.Add(this.label7);
            this.tabPageClass.Controls.Add(this.Class_OK);
            this.tabPageClass.Controls.Add(this.Class_ToSize);
            this.tabPageClass.Controls.Add(this.label8);
            this.tabPageClass.Controls.Add(this.Class_FromSize);
            this.tabPageClass.Controls.Add(this.label9);
            this.tabPageClass.Controls.Add(this.label10);
            this.tabPageClass.Controls.Add(this.label13);
            this.tabPageClass.Controls.Add(this.label14);
            this.tabPageClass.Controls.Add(this.label15);
            this.tabPageClass.Controls.Add(this.comboBox1);
            this.tabPageClass.Controls.Add(this.label16);
            this.tabPageClass.Controls.Add(this.Class_bdrColor);
            this.tabPageClass.Controls.Add(this.Class_ToColor);
            this.tabPageClass.Controls.Add(this.Class_FromColor);
            this.tabPageClass.Location = new System.Drawing.Point(4, 22);
            this.tabPageClass.Name = "tabPageClass";
            this.tabPageClass.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageClass.Size = new System.Drawing.Size(441, 415);
            this.tabPageClass.TabIndex = 2;
            this.tabPageClass.Text = "分级符号法";
            this.tabPageClass.UseVisualStyleBackColor = true;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(288, 29);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(120, 21);
            this.numericUpDown1.TabIndex = 58;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.Location = new System.Drawing.Point(169, 113);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(89, 12);
            this.label11.TabIndex = 56;
            this.label11.Text = "最高级别填充色";
            // 
            // Class_Apply
            // 
            this.Class_Apply.Location = new System.Drawing.Point(198, 333);
            this.Class_Apply.Margin = new System.Windows.Forms.Padding(2);
            this.Class_Apply.Name = "Class_Apply";
            this.Class_Apply.Size = new System.Drawing.Size(57, 23);
            this.Class_Apply.TabIndex = 55;
            this.Class_Apply.Text = "应用";
            this.Class_Apply.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(340, 113);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 53;
            this.label7.Text = "轮廓颜色";
            // 
            // Class_OK
            // 
            this.Class_OK.Location = new System.Drawing.Point(277, 333);
            this.Class_OK.Margin = new System.Windows.Forms.Padding(2);
            this.Class_OK.Name = "Class_OK";
            this.Class_OK.Size = new System.Drawing.Size(53, 23);
            this.Class_OK.TabIndex = 51;
            this.Class_OK.Text = "确定";
            this.Class_OK.UseVisualStyleBackColor = true;
            // 
            // Class_ToSize
            // 
            this.Class_ToSize.Location = new System.Drawing.Point(180, 208);
            this.Class_ToSize.Margin = new System.Windows.Forms.Padding(2);
            this.Class_ToSize.Name = "Class_ToSize";
            this.Class_ToSize.Size = new System.Drawing.Size(49, 21);
            this.Class_ToSize.TabIndex = 50;
            this.Class_ToSize.Text = "20";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(139, 210);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(17, 12);
            this.label8.TabIndex = 49;
            this.label8.Text = "to";
            // 
            // Class_FromSize
            // 
            this.Class_FromSize.Location = new System.Drawing.Point(67, 208);
            this.Class_FromSize.Margin = new System.Windows.Forms.Padding(2);
            this.Class_FromSize.Name = "Class_FromSize";
            this.Class_FromSize.Size = new System.Drawing.Size(48, 21);
            this.Class_FromSize.TabIndex = 48;
            this.Class_FromSize.Text = "5";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(65, 183);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 12);
            this.label9.TabIndex = 47;
            this.label9.Text = "尺寸";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(140, 144);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(17, 12);
            this.label10.TabIndex = 46;
            this.label10.Text = "to";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.Location = new System.Drawing.Point(39, 113);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(89, 12);
            this.label13.TabIndex = 43;
            this.label13.Text = "最低级别填充色";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(91, 135);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(0, 12);
            this.label14.TabIndex = 7;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(244, 33);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(29, 12);
            this.label15.TabIndex = 4;
            this.label15.Text = "级数";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(82, 28);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 20);
            this.comboBox1.TabIndex = 1;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(35, 31);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(29, 12);
            this.label16.TabIndex = 0;
            this.label16.Text = "字段";
            // 
            // btnPointRenderApply
            // 
            this.btnPointRenderApply.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnPointRenderApply.Location = new System.Drawing.Point(0, 441);
            this.btnPointRenderApply.Name = "btnPointRenderApply";
            this.btnPointRenderApply.Size = new System.Drawing.Size(449, 23);
            this.btnPointRenderApply.TabIndex = 51;
            this.btnPointRenderApply.Text = "应用";
            this.btnPointRenderApply.UseVisualStyleBackColor = true;
            this.btnPointRenderApply.Click += new System.EventHandler(this.btnPointRenderApply_Click);
            // 
            // btnPointRendererConfirm
            // 
            this.btnPointRendererConfirm.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnPointRendererConfirm.Location = new System.Drawing.Point(0, 464);
            this.btnPointRendererConfirm.Name = "btnPointRendererConfirm";
            this.btnPointRendererConfirm.Size = new System.Drawing.Size(449, 23);
            this.btnPointRendererConfirm.TabIndex = 49;
            this.btnPointRendererConfirm.Text = "确定";
            this.btnPointRendererConfirm.UseVisualStyleBackColor = true;
            this.btnPointRendererConfirm.Click += new System.EventHandler(this.btnPointRendererConfirm_Click);
            // 
            // Class_bdrColor
            // 
            this.Class_bdrColor.BackColor = System.Drawing.Color.Black;
            this.Class_bdrColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Class_bdrColor.Location = new System.Drawing.Point(353, 135);
            this.Class_bdrColor.Margin = new System.Windows.Forms.Padding(2);
            this.Class_bdrColor.Name = "Class_bdrColor";
            this.Class_bdrColor.Size = new System.Drawing.Size(28, 28);
            this.Class_bdrColor.TabIndex = 54;
            this.Class_bdrColor.TabStop = false;
            // 
            // Class_ToColor
            // 
            this.Class_ToColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.Class_ToColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Class_ToColor.Location = new System.Drawing.Point(195, 135);
            this.Class_ToColor.Margin = new System.Windows.Forms.Padding(2);
            this.Class_ToColor.Name = "Class_ToColor";
            this.Class_ToColor.Size = new System.Drawing.Size(28, 28);
            this.Class_ToColor.TabIndex = 45;
            this.Class_ToColor.TabStop = false;
            // 
            // Class_FromColor
            // 
            this.Class_FromColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.Class_FromColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Class_FromColor.Location = new System.Drawing.Point(67, 135);
            this.Class_FromColor.Margin = new System.Windows.Forms.Padding(2);
            this.Class_FromColor.Name = "Class_FromColor";
            this.Class_FromColor.Size = new System.Drawing.Size(28, 28);
            this.Class_FromColor.TabIndex = 44;
            this.Class_FromColor.TabStop = false;
            // 
            // RenderPointForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(449, 487);
            this.Controls.Add(this.tabControlInner);
            this.Controls.Add(this.btnPointRenderApply);
            this.Controls.Add(this.btnPointRendererConfirm);
            this.Name = "RenderPointForm";
            this.Text = "PointRendererForm";
            this.tabControlInner.ResumeLayout(false);
            this.tabPageSingle.ResumeLayout(false);
            this.tabPageSingle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSize)).EndInit();
            this.tabPageUnique.ResumeLayout(false);
            this.tabPageUnique.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AttributeTable)).EndInit();
            this.tabPageClass.ResumeLayout(false);
            this.tabPageClass.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Class_bdrColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Class_ToColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Class_FromColor)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlInner;
        private System.Windows.Forms.TabPage tabPageSingle;
        private System.Windows.Forms.Button btnSingleColor;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.NumericUpDown numericUpDownSize;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.ComboBox cboStyle;
        private System.Windows.Forms.TabPage tabPageUnique;
        private System.Windows.Forms.DataGridView AttributeTable;
        private System.Windows.Forms.Button btnGetUniqueValue;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cbFields;
        private System.Windows.Forms.TabPage tabPageClass;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button Class_Apply;
        private System.Windows.Forms.PictureBox Class_bdrColor;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button Class_OK;
        private System.Windows.Forms.TextBox Class_ToSize;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox Class_FromSize;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.PictureBox Class_ToColor;
        private System.Windows.Forms.PictureBox Class_FromColor;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button btnPointRendererConfirm;
        private System.Windows.Forms.Button btnPointRenderApply;
    }
}