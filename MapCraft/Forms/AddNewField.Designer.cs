namespace MapCraft.Forms
{
    partial class AddNewField
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.tbxFieldName = new System.Windows.Forms.TextBox();
            this.cbxFieldType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnConfirmAdd = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tbxFieldName);
            this.panel1.Controls.Add(this.cbxFieldType);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(68, 136);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(707, 314);
            this.panel1.TabIndex = 3;
            // 
            // tbxFieldName
            // 
            this.tbxFieldName.Location = new System.Drawing.Point(163, 172);
            this.tbxFieldName.Name = "tbxFieldName";
            this.tbxFieldName.Size = new System.Drawing.Size(403, 35);
            this.tbxFieldName.TabIndex = 3;
            // 
            // cbxFieldType
            // 
            this.cbxFieldType.FormattingEnabled = true;
            this.cbxFieldType.Location = new System.Drawing.Point(163, 39);
            this.cbxFieldType.Name = "cbxFieldType";
            this.cbxFieldType.Size = new System.Drawing.Size(403, 32);
            this.cbxFieldType.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "字段类型：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 175);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 24);
            this.label2.TabIndex = 1;
            this.label2.Text = "字段名称：";
            // 
            // btnConfirmAdd
            // 
            this.btnConfirmAdd.Location = new System.Drawing.Point(266, 531);
            this.btnConfirmAdd.Name = "btnConfirmAdd";
            this.btnConfirmAdd.Size = new System.Drawing.Size(253, 64);
            this.btnConfirmAdd.TabIndex = 4;
            this.btnConfirmAdd.Text = "添加字段";
            this.btnConfirmAdd.UseVisualStyleBackColor = true;
            // 
            // AddNewField
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(848, 712);
            this.Controls.Add(this.btnConfirmAdd);
            this.Controls.Add(this.panel1);
            this.Name = "AddNewField";
            this.Text = "AddNewField";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox tbxFieldName;
        private System.Windows.Forms.ComboBox cbxFieldType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnConfirmAdd;
    }
}