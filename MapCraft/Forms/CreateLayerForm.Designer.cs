namespace MapCraft.Forms
{
    partial class CreateLayerForm
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
            this.textBoxSavePath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSavePath = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.chbLayerType = new System.Windows.Forms.ComboBox();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.textBoxLayrName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBoxSavePath
            // 
            this.textBoxSavePath.Location = new System.Drawing.Point(124, 83);
            this.textBoxSavePath.Name = "textBoxSavePath";
            this.textBoxSavePath.Size = new System.Drawing.Size(219, 21);
            this.textBoxSavePath.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F);
            this.label2.Location = new System.Drawing.Point(28, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "存储路径：";
            // 
            // btnSavePath
            // 
            this.btnSavePath.Location = new System.Drawing.Point(368, 83);
            this.btnSavePath.Name = "btnSavePath";
            this.btnSavePath.Size = new System.Drawing.Size(31, 24);
            this.btnSavePath.TabIndex = 4;
            this.btnSavePath.UseVisualStyleBackColor = true;
            this.btnSavePath.Click += new System.EventHandler(this.btnSavePath_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F);
            this.label3.Location = new System.Drawing.Point(28, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "几何类型：";
            // 
            // chbLayerType
            // 
            this.chbLayerType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.chbLayerType.FormattingEnabled = true;
            this.chbLayerType.Location = new System.Drawing.Point(124, 34);
            this.chbLayerType.Name = "chbLayerType";
            this.chbLayerType.Size = new System.Drawing.Size(219, 20);
            this.chbLayerType.TabIndex = 6;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(166, 174);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(100, 42);
            this.btnConfirm.TabIndex = 7;
            this.btnConfirm.Text = "创建";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // textBoxLayrName
            // 
            this.textBoxLayrName.Location = new System.Drawing.Point(124, 125);
            this.textBoxLayrName.Name = "textBoxLayrName";
            this.textBoxLayrName.Size = new System.Drawing.Size(219, 21);
            this.textBoxLayrName.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F);
            this.label1.Location = new System.Drawing.Point(28, 130);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 16);
            this.label1.TabIndex = 8;
            this.label1.Text = "图层名称：";
            // 
            // CreateLayerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 240);
            this.Controls.Add(this.textBoxLayrName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.chbLayerType);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnSavePath);
            this.Controls.Add(this.textBoxSavePath);
            this.Controls.Add(this.label2);
            this.Name = "CreateLayerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "新建图层";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBoxSavePath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSavePath;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox chbLayerType;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.TextBox textBoxLayrName;
        private System.Windows.Forms.Label label1;
    }
}