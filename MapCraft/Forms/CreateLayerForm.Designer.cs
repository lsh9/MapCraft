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
            this.cbBoxLayerType = new System.Windows.Forms.ComboBox();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.textBoxFieldName = new System.Windows.Forms.TextBox();
            this.labelFieldName = new System.Windows.Forms.Label();
            this.cbBoxValueType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAddField = new System.Windows.Forms.Button();
            this.listBoxFields = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
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
            this.label2.Text = "保存路径：";
            // 
            // btnSavePath
            // 
            this.btnSavePath.Location = new System.Drawing.Point(364, 83);
            this.btnSavePath.Name = "btnSavePath";
            this.btnSavePath.Size = new System.Drawing.Size(31, 24);
            this.btnSavePath.TabIndex = 4;
            this.btnSavePath.Text = "...";
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
            // cbBoxLayerType
            // 
            this.cbBoxLayerType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBoxLayerType.FormattingEnabled = true;
            this.cbBoxLayerType.Location = new System.Drawing.Point(124, 34);
            this.cbBoxLayerType.Name = "cbBoxLayerType";
            this.cbBoxLayerType.Size = new System.Drawing.Size(219, 20);
            this.cbBoxLayerType.TabIndex = 6;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(168, 429);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(100, 31);
            this.btnConfirm.TabIndex = 7;
            this.btnConfirm.Text = "创建图层";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // textBoxFieldName
            // 
            this.textBoxFieldName.Location = new System.Drawing.Point(124, 144);
            this.textBoxFieldName.Name = "textBoxFieldName";
            this.textBoxFieldName.Size = new System.Drawing.Size(219, 21);
            this.textBoxFieldName.TabIndex = 9;
            // 
            // labelFieldName
            // 
            this.labelFieldName.AutoSize = true;
            this.labelFieldName.Font = new System.Drawing.Font("宋体", 12F);
            this.labelFieldName.Location = new System.Drawing.Point(28, 149);
            this.labelFieldName.Name = "labelFieldName";
            this.labelFieldName.Size = new System.Drawing.Size(87, 16);
            this.labelFieldName.TabIndex = 8;
            this.labelFieldName.Text = "字段名称：";
            // 
            // cbBoxValueType
            // 
            this.cbBoxValueType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBoxValueType.FormattingEnabled = true;
            this.cbBoxValueType.Items.AddRange(new object[] {
            "Int16",
            "Int32",
            "Int64",
            "Single",
            "Double",
            "Text(文本)"});
            this.cbBoxValueType.Location = new System.Drawing.Point(124, 189);
            this.cbBoxValueType.Name = "cbBoxValueType";
            this.cbBoxValueType.Size = new System.Drawing.Size(219, 20);
            this.cbBoxValueType.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F);
            this.label1.Location = new System.Drawing.Point(28, 193);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 16);
            this.label1.TabIndex = 10;
            this.label1.Text = "字段类型：";
            // 
            // btnAddField
            // 
            this.btnAddField.Location = new System.Drawing.Point(124, 236);
            this.btnAddField.Name = "btnAddField";
            this.btnAddField.Size = new System.Drawing.Size(100, 31);
            this.btnAddField.TabIndex = 12;
            this.btnAddField.Text = "添加字段";
            this.btnAddField.UseVisualStyleBackColor = true;
            this.btnAddField.Click += new System.EventHandler(this.btnAddField_Click);
            // 
            // listBoxFields
            // 
            this.listBoxFields.FormattingEnabled = true;
            this.listBoxFields.ItemHeight = 12;
            this.listBoxFields.Location = new System.Drawing.Point(124, 284);
            this.listBoxFields.Name = "listBoxFields";
            this.listBoxFields.Size = new System.Drawing.Size(219, 124);
            this.listBoxFields.TabIndex = 13;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(243, 236);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 31);
            this.button1.TabIndex = 14;
            this.button1.Text = "删除字段";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // CreateLayerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 496);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listBoxFields);
            this.Controls.Add(this.btnAddField);
            this.Controls.Add(this.cbBoxValueType);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxFieldName);
            this.Controls.Add(this.labelFieldName);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.cbBoxLayerType);
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
        private System.Windows.Forms.ComboBox cbBoxLayerType;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.TextBox textBoxFieldName;
        private System.Windows.Forms.Label labelFieldName;
        private System.Windows.Forms.ComboBox cbBoxValueType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAddField;
        private System.Windows.Forms.ListBox listBoxFields;
        private System.Windows.Forms.Button button1;
    }
}