namespace MapCraft.Forms
{
    partial class LayerDetailForm
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
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSavePath = new System.Windows.Forms.Button();
            this.textBoxSavePath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbBoxLayers = new System.Windows.Forms.ComboBox();
            this.textBoxShapeType = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxMaxY = new System.Windows.Forms.TextBox();
            this.textBoxMinX = new System.Windows.Forms.TextBox();
            this.textBoxMaxX = new System.Windows.Forms.TextBox();
            this.textBoxMinY = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Location = new System.Drawing.Point(149, 107);
            this.textBoxDescription.Multiline = true;
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.Size = new System.Drawing.Size(219, 47);
            this.textBoxDescription.TabIndex = 22;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 12F);
            this.label4.Location = new System.Drawing.Point(53, 112);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 16);
            this.label4.TabIndex = 21;
            this.label4.Text = "图层描述：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F);
            this.label3.Location = new System.Drawing.Point(53, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 16);
            this.label3.TabIndex = 18;
            this.label3.Text = "图层名：";
            // 
            // btnSavePath
            // 
            this.btnSavePath.Location = new System.Drawing.Point(389, 180);
            this.btnSavePath.Name = "btnSavePath";
            this.btnSavePath.Size = new System.Drawing.Size(31, 24);
            this.btnSavePath.TabIndex = 17;
            this.btnSavePath.Text = "...";
            this.btnSavePath.UseVisualStyleBackColor = true;
            this.btnSavePath.Click += new System.EventHandler(this.btnSavePath_Click);
            // 
            // textBoxSavePath
            // 
            this.textBoxSavePath.Location = new System.Drawing.Point(149, 180);
            this.textBoxSavePath.Name = "textBoxSavePath";
            this.textBoxSavePath.Size = new System.Drawing.Size(219, 21);
            this.textBoxSavePath.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F);
            this.label2.Location = new System.Drawing.Point(53, 185);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 16);
            this.label2.TabIndex = 15;
            this.label2.Text = "数据源：";
            // 
            // cbBoxLayers
            // 
            this.cbBoxLayers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBoxLayers.FormattingEnabled = true;
            this.cbBoxLayers.Location = new System.Drawing.Point(149, 26);
            this.cbBoxLayers.Name = "cbBoxLayers";
            this.cbBoxLayers.Size = new System.Drawing.Size(219, 20);
            this.cbBoxLayers.TabIndex = 19;
            this.cbBoxLayers.SelectedIndexChanged += new System.EventHandler(this.cbBoxLayers_SelectedIndexChanged);
            // 
            // textBoxShapeType
            // 
            this.textBoxShapeType.Enabled = false;
            this.textBoxShapeType.Location = new System.Drawing.Point(149, 235);
            this.textBoxShapeType.Name = "textBoxShapeType";
            this.textBoxShapeType.ReadOnly = true;
            this.textBoxShapeType.Size = new System.Drawing.Size(219, 21);
            this.textBoxShapeType.TabIndex = 24;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F);
            this.label1.Location = new System.Drawing.Point(53, 240);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 16);
            this.label1.TabIndex = 23;
            this.label1.Text = "几何类型：";
            // 
            // textBoxMaxY
            // 
            this.textBoxMaxY.Enabled = false;
            this.textBoxMaxY.Location = new System.Drawing.Point(191, 305);
            this.textBoxMaxY.Name = "textBoxMaxY";
            this.textBoxMaxY.ReadOnly = true;
            this.textBoxMaxY.Size = new System.Drawing.Size(109, 21);
            this.textBoxMaxY.TabIndex = 26;
            // 
            // textBoxMinX
            // 
            this.textBoxMinX.Enabled = false;
            this.textBoxMinX.Location = new System.Drawing.Point(111, 353);
            this.textBoxMinX.Name = "textBoxMinX";
            this.textBoxMinX.ReadOnly = true;
            this.textBoxMinX.Size = new System.Drawing.Size(109, 21);
            this.textBoxMinX.TabIndex = 28;
            // 
            // textBoxMaxX
            // 
            this.textBoxMaxX.Enabled = false;
            this.textBoxMaxX.Location = new System.Drawing.Point(276, 353);
            this.textBoxMaxX.Name = "textBoxMaxX";
            this.textBoxMaxX.ReadOnly = true;
            this.textBoxMaxX.Size = new System.Drawing.Size(109, 21);
            this.textBoxMaxX.TabIndex = 30;
            // 
            // textBoxMinY
            // 
            this.textBoxMinY.Enabled = false;
            this.textBoxMinY.Location = new System.Drawing.Point(191, 392);
            this.textBoxMinY.Name = "textBoxMinY";
            this.textBoxMinY.ReadOnly = true;
            this.textBoxMinY.Size = new System.Drawing.Size(109, 21);
            this.textBoxMinY.TabIndex = 32;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 12F);
            this.label5.Location = new System.Drawing.Point(53, 310);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 16);
            this.label5.TabIndex = 33;
            this.label5.Text = "范围：";
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(191, 436);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(100, 31);
            this.btnConfirm.TabIndex = 34;
            this.btnConfirm.Text = "保存";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 12F);
            this.label6.Location = new System.Drawing.Point(53, 71);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 16);
            this.label6.TabIndex = 35;
            this.label6.Text = "图层名：";
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(149, 72);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(219, 21);
            this.textBoxName.TabIndex = 36;
            // 
            // LayerDetailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(453, 524);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxMinY);
            this.Controls.Add(this.textBoxMaxX);
            this.Controls.Add(this.textBoxMinX);
            this.Controls.Add(this.textBoxMaxY);
            this.Controls.Add(this.textBoxShapeType);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxDescription);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbBoxLayers);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnSavePath);
            this.Controls.Add(this.textBoxSavePath);
            this.Controls.Add(this.label2);
            this.Name = "LayerDetailForm";
            this.Text = "图层详细信息";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSavePath;
        private System.Windows.Forms.TextBox textBoxSavePath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbBoxLayers;
        private System.Windows.Forms.TextBox textBoxShapeType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxMaxY;
        private System.Windows.Forms.TextBox textBoxMinX;
        private System.Windows.Forms.TextBox textBoxMaxX;
        private System.Windows.Forms.TextBox textBoxMinY;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxName;
    }
}