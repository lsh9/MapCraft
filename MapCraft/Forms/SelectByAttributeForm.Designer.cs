namespace MapCraft.Forms
{
    partial class SelectByAttributeForm
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
            this.SelectBoxLayer = new System.Windows.Forms.ComboBox();
            this.labelLayers = new System.Windows.Forms.Label();
            this.ListBoxFields = new System.Windows.Forms.ListBox();
            this.btnEq = new System.Windows.Forms.Button();
            this.btnGt = new System.Windows.Forms.Button();
            this.btnLt = new System.Windows.Forms.Button();
            this.btnNeq = new System.Windows.Forms.Button();
            this.btnLike = new System.Windows.Forms.Button();
            this.btnGe = new System.Windows.Forms.Button();
            this.btnLe = new System.Windows.Forms.Button();
            this.btnAnd = new System.Windows.Forms.Button();
            this.btnOr = new System.Windows.Forms.Button();
            this.btnGetUniqueValue = new System.Windows.Forms.Button();
            this.ListBoxUniqueValues = new System.Windows.Forms.ListBox();
            this.TextBoxSQL = new System.Windows.Forms.TextBox();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnValidate = new System.Windows.Forms.Button();
            this.labelFieldList = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnNot = new System.Windows.Forms.Button();
            this.btnIn = new System.Windows.Forms.Button();
            this.btnIs = new System.Windows.Forms.Button();
            this.labelSQL = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // SelectBoxLayer
            // 
            this.SelectBoxLayer.DropDownHeight = 120;
            this.SelectBoxLayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SelectBoxLayer.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.SelectBoxLayer.FormattingEnabled = true;
            this.SelectBoxLayer.IntegralHeight = false;
            this.SelectBoxLayer.Location = new System.Drawing.Point(90, 21);
            this.SelectBoxLayer.Name = "SelectBoxLayer";
            this.SelectBoxLayer.Size = new System.Drawing.Size(222, 22);
            this.SelectBoxLayer.TabIndex = 0;
            this.SelectBoxLayer.SelectionChangeCommitted += new System.EventHandler(this.SelectBoxLayer_SelectionChangeCommitted);
            // 
            // labelLayers
            // 
            this.labelLayers.AutoSize = true;
            this.labelLayers.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelLayers.Location = new System.Drawing.Point(40, 24);
            this.labelLayers.Name = "labelLayers";
            this.labelLayers.Size = new System.Drawing.Size(42, 14);
            this.labelLayers.TabIndex = 1;
            this.labelLayers.Text = "图层:";
            // 
            // ListBoxFields
            // 
            this.ListBoxFields.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ListBoxFields.FormattingEnabled = true;
            this.ListBoxFields.ItemHeight = 14;
            this.ListBoxFields.Location = new System.Drawing.Point(43, 68);
            this.ListBoxFields.Name = "ListBoxFields";
            this.ListBoxFields.Size = new System.Drawing.Size(269, 130);
            this.ListBoxFields.TabIndex = 2;
            this.ListBoxFields.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ListBoxFields_MouseClick);
            this.ListBoxFields.SelectedIndexChanged += new System.EventHandler(this.ListBoxFields_SelectedIndexChanged);
            this.ListBoxFields.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ListBoxFields_MouseDoubleClick);
            // 
            // btnEq
            // 
            this.btnEq.Location = new System.Drawing.Point(44, 232);
            this.btnEq.Name = "btnEq";
            this.btnEq.Size = new System.Drawing.Size(40, 30);
            this.btnEq.TabIndex = 3;
            this.btnEq.Text = "=";
            this.btnEq.UseVisualStyleBackColor = true;
            this.btnEq.Click += new System.EventHandler(this.btnEq_Click);
            // 
            // btnGt
            // 
            this.btnGt.Location = new System.Drawing.Point(43, 264);
            this.btnGt.Name = "btnGt";
            this.btnGt.Size = new System.Drawing.Size(40, 30);
            this.btnGt.TabIndex = 4;
            this.btnGt.Text = ">";
            this.btnGt.UseVisualStyleBackColor = true;
            this.btnGt.Click += new System.EventHandler(this.btnGt_Click);
            // 
            // btnLt
            // 
            this.btnLt.Location = new System.Drawing.Point(43, 296);
            this.btnLt.Name = "btnLt";
            this.btnLt.Size = new System.Drawing.Size(40, 30);
            this.btnLt.TabIndex = 5;
            this.btnLt.Text = "<";
            this.btnLt.UseVisualStyleBackColor = true;
            this.btnLt.Click += new System.EventHandler(this.btnLt_Click);
            // 
            // btnNeq
            // 
            this.btnNeq.Location = new System.Drawing.Point(84, 232);
            this.btnNeq.Name = "btnNeq";
            this.btnNeq.Size = new System.Drawing.Size(40, 30);
            this.btnNeq.TabIndex = 6;
            this.btnNeq.Text = "<>";
            this.btnNeq.UseVisualStyleBackColor = true;
            this.btnNeq.Click += new System.EventHandler(this.btnNeq_Click);
            // 
            // btnLike
            // 
            this.btnLike.Location = new System.Drawing.Point(125, 232);
            this.btnLike.Name = "btnLike";
            this.btnLike.Size = new System.Drawing.Size(40, 30);
            this.btnLike.TabIndex = 7;
            this.btnLike.Text = "Like";
            this.btnLike.UseVisualStyleBackColor = true;
            this.btnLike.Click += new System.EventHandler(this.btnLike_Click);
            // 
            // btnGe
            // 
            this.btnGe.Location = new System.Drawing.Point(84, 264);
            this.btnGe.Name = "btnGe";
            this.btnGe.Size = new System.Drawing.Size(40, 30);
            this.btnGe.TabIndex = 8;
            this.btnGe.Text = ">=";
            this.btnGe.UseVisualStyleBackColor = true;
            this.btnGe.Click += new System.EventHandler(this.btnGe_Click);
            // 
            // btnLe
            // 
            this.btnLe.Location = new System.Drawing.Point(84, 296);
            this.btnLe.Name = "btnLe";
            this.btnLe.Size = new System.Drawing.Size(40, 30);
            this.btnLe.TabIndex = 9;
            this.btnLe.Text = "<=";
            this.btnLe.UseVisualStyleBackColor = true;
            this.btnLe.Click += new System.EventHandler(this.btnLe_Click);
            // 
            // btnAnd
            // 
            this.btnAnd.Location = new System.Drawing.Point(125, 264);
            this.btnAnd.Name = "btnAnd";
            this.btnAnd.Size = new System.Drawing.Size(40, 30);
            this.btnAnd.TabIndex = 10;
            this.btnAnd.Text = "And";
            this.btnAnd.UseVisualStyleBackColor = true;
            this.btnAnd.Click += new System.EventHandler(this.btnAnd_Click);
            // 
            // btnOr
            // 
            this.btnOr.Location = new System.Drawing.Point(125, 296);
            this.btnOr.Name = "btnOr";
            this.btnOr.Size = new System.Drawing.Size(40, 30);
            this.btnOr.TabIndex = 11;
            this.btnOr.Text = "Or";
            this.btnOr.UseVisualStyleBackColor = true;
            this.btnOr.Click += new System.EventHandler(this.btnOr_Click);
            // 
            // btnGetUniqueValue
            // 
            this.btnGetUniqueValue.Location = new System.Drawing.Point(181, 361);
            this.btnGetUniqueValue.Name = "btnGetUniqueValue";
            this.btnGetUniqueValue.Size = new System.Drawing.Size(131, 30);
            this.btnGetUniqueValue.TabIndex = 19;
            this.btnGetUniqueValue.Text = "获取唯一值";
            this.btnGetUniqueValue.UseVisualStyleBackColor = true;
            this.btnGetUniqueValue.Click += new System.EventHandler(this.btnGetUniqueValue_Click);
            // 
            // ListBoxUniqueValues
            // 
            this.ListBoxUniqueValues.FormattingEnabled = true;
            this.ListBoxUniqueValues.ItemHeight = 12;
            this.ListBoxUniqueValues.Location = new System.Drawing.Point(181, 232);
            this.ListBoxUniqueValues.Name = "ListBoxUniqueValues";
            this.ListBoxUniqueValues.Size = new System.Drawing.Size(131, 124);
            this.ListBoxUniqueValues.TabIndex = 20;
            this.ListBoxUniqueValues.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ListBoxUniqueValues_MouseDoubleClick);
            // 
            // TextBoxSQL
            // 
            this.TextBoxSQL.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TextBoxSQL.Location = new System.Drawing.Point(43, 412);
            this.TextBoxSQL.Multiline = true;
            this.TextBoxSQL.Name = "TextBoxSQL";
            this.TextBoxSQL.Size = new System.Drawing.Size(269, 77);
            this.TextBoxSQL.TabIndex = 22;
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(241, 505);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(60, 30);
            this.btnApply.TabIndex = 23;
            this.btnApply.Text = "应用";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(175, 505);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(60, 30);
            this.btnConfirm.TabIndex = 26;
            this.btnConfirm.Text = "确定";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnValidate
            // 
            this.btnValidate.Location = new System.Drawing.Point(109, 505);
            this.btnValidate.Name = "btnValidate";
            this.btnValidate.Size = new System.Drawing.Size(60, 30);
            this.btnValidate.TabIndex = 25;
            this.btnValidate.Text = "验证";
            this.btnValidate.UseVisualStyleBackColor = true;
            this.btnValidate.Click += new System.EventHandler(this.btnValidate_Click);
            // 
            // labelFieldList
            // 
            this.labelFieldList.AutoSize = true;
            this.labelFieldList.BackColor = System.Drawing.SystemColors.Control;
            this.labelFieldList.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelFieldList.Location = new System.Drawing.Point(147, 53);
            this.labelFieldList.Name = "labelFieldList";
            this.labelFieldList.Size = new System.Drawing.Size(53, 12);
            this.labelFieldList.TabIndex = 28;
            this.labelFieldList.Text = "字段列表";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(43, 505);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(60, 31);
            this.btnClear.TabIndex = 29;
            this.btnClear.Text = "清除";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnNot
            // 
            this.btnNot.Location = new System.Drawing.Point(125, 332);
            this.btnNot.Name = "btnNot";
            this.btnNot.Size = new System.Drawing.Size(40, 30);
            this.btnNot.TabIndex = 32;
            this.btnNot.Text = "Not";
            this.btnNot.UseVisualStyleBackColor = true;
            this.btnNot.Click += new System.EventHandler(this.btnNot_Click);
            // 
            // btnIn
            // 
            this.btnIn.Location = new System.Drawing.Point(84, 332);
            this.btnIn.Name = "btnIn";
            this.btnIn.Size = new System.Drawing.Size(40, 30);
            this.btnIn.TabIndex = 31;
            this.btnIn.Text = "In";
            this.btnIn.UseVisualStyleBackColor = true;
            this.btnIn.Click += new System.EventHandler(this.btnIn_Click);
            // 
            // btnIs
            // 
            this.btnIs.Location = new System.Drawing.Point(43, 332);
            this.btnIs.Name = "btnIs";
            this.btnIs.Size = new System.Drawing.Size(40, 30);
            this.btnIs.TabIndex = 30;
            this.btnIs.Text = "Is";
            this.btnIs.UseVisualStyleBackColor = true;
            this.btnIs.Click += new System.EventHandler(this.btnIs_Click);
            // 
            // labelSQL
            // 
            this.labelSQL.AutoSize = true;
            this.labelSQL.BackColor = System.Drawing.SystemColors.Control;
            this.labelSQL.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelSQL.Location = new System.Drawing.Point(42, 397);
            this.labelSQL.Name = "labelSQL";
            this.labelSQL.Size = new System.Drawing.Size(0, 12);
            this.labelSQL.TabIndex = 33;
            // 
            // SelectByAttributeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(347, 549);
            this.Controls.Add(this.labelSQL);
            this.Controls.Add(this.btnNot);
            this.Controls.Add(this.btnIn);
            this.Controls.Add(this.btnIs);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.labelFieldList);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.btnValidate);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.TextBoxSQL);
            this.Controls.Add(this.ListBoxUniqueValues);
            this.Controls.Add(this.btnGetUniqueValue);
            this.Controls.Add(this.btnOr);
            this.Controls.Add(this.btnAnd);
            this.Controls.Add(this.btnLe);
            this.Controls.Add(this.btnGe);
            this.Controls.Add(this.btnLike);
            this.Controls.Add(this.btnNeq);
            this.Controls.Add(this.btnLt);
            this.Controls.Add(this.btnGt);
            this.Controls.Add(this.btnEq);
            this.Controls.Add(this.ListBoxFields);
            this.Controls.Add(this.labelLayers);
            this.Controls.Add(this.SelectBoxLayer);
            this.Name = "SelectByAttributeForm";
            this.Text = "按属性查询";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox SelectBoxLayer;
        private System.Windows.Forms.Label labelLayers;
        private System.Windows.Forms.Button btnEq;
        private System.Windows.Forms.Button btnGt;
        private System.Windows.Forms.Button btnLt;
        private System.Windows.Forms.Button btnNeq;
        private System.Windows.Forms.Button btnLike;
        private System.Windows.Forms.Button btnGe;
        private System.Windows.Forms.Button btnLe;
        private System.Windows.Forms.Button btnAnd;
        private System.Windows.Forms.Button btnOr;
        private System.Windows.Forms.Button btnGetUniqueValue;
        private System.Windows.Forms.ListBox ListBoxUniqueValues;
        private System.Windows.Forms.TextBox TextBoxSQL;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnValidate;
        private System.Windows.Forms.ListBox ListBoxFields;
        private System.Windows.Forms.Label labelFieldList;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnNot;
        private System.Windows.Forms.Button btnIn;
        private System.Windows.Forms.Button btnIs;
        private System.Windows.Forms.Label labelSQL;
    }
}