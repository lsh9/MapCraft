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
            this.cbLayers = new System.Windows.Forms.ComboBox();
            this.labelLayers = new System.Windows.Forms.Label();
            this.listBoxFields = new System.Windows.Forms.ListBox();
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
            this.listBoxUniqueValues = new System.Windows.Forms.ListBox();
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
            this.btnNull = new System.Windows.Forms.Button();
            this.btnLine = new System.Windows.Forms.Button();
            this.btnBracket = new System.Windows.Forms.Button();
            this.btnPercnt = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cbLayers
            // 
            this.cbLayers.DropDownHeight = 120;
            this.cbLayers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLayers.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbLayers.FormattingEnabled = true;
            this.cbLayers.IntegralHeight = false;
            this.cbLayers.Location = new System.Drawing.Point(90, 21);
            this.cbLayers.Name = "cbLayers";
            this.cbLayers.Size = new System.Drawing.Size(222, 25);
            this.cbLayers.TabIndex = 0;
            this.cbLayers.SelectedIndexChanged += new System.EventHandler(this.cbLayer_SelectedIndexChanged);
            // 
            // labelLayers
            // 
            this.labelLayers.AutoSize = true;
            this.labelLayers.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelLayers.Location = new System.Drawing.Point(40, 24);
            this.labelLayers.Name = "labelLayers";
            this.labelLayers.Size = new System.Drawing.Size(53, 18);
            this.labelLayers.TabIndex = 1;
            this.labelLayers.Text = "图层:";
            // 
            // listBoxFields
            // 
            this.listBoxFields.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listBoxFields.FormattingEnabled = true;
            this.listBoxFields.ItemHeight = 17;
            this.listBoxFields.Location = new System.Drawing.Point(43, 68);
            this.listBoxFields.Name = "listBoxFields";
            this.listBoxFields.Size = new System.Drawing.Size(269, 123);
            this.listBoxFields.TabIndex = 2;
            this.listBoxFields.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ListBoxFields_MouseClick);
            this.listBoxFields.SelectedIndexChanged += new System.EventHandler(this.ListBoxFields_SelectedIndexChanged);
            this.listBoxFields.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ListBoxFields_MouseDoubleClick);
            // 
            // btnEq
            // 
            this.btnEq.Location = new System.Drawing.Point(44, 219);
            this.btnEq.Name = "btnEq";
            this.btnEq.Size = new System.Drawing.Size(40, 30);
            this.btnEq.TabIndex = 3;
            this.btnEq.Text = "=";
            this.btnEq.UseVisualStyleBackColor = true;
            this.btnEq.Click += new System.EventHandler(this.btnEq_Click);
            // 
            // btnGt
            // 
            this.btnGt.Location = new System.Drawing.Point(43, 251);
            this.btnGt.Name = "btnGt";
            this.btnGt.Size = new System.Drawing.Size(40, 30);
            this.btnGt.TabIndex = 4;
            this.btnGt.Text = ">";
            this.btnGt.UseVisualStyleBackColor = true;
            this.btnGt.Click += new System.EventHandler(this.btnGt_Click);
            // 
            // btnLt
            // 
            this.btnLt.Location = new System.Drawing.Point(43, 283);
            this.btnLt.Name = "btnLt";
            this.btnLt.Size = new System.Drawing.Size(40, 30);
            this.btnLt.TabIndex = 5;
            this.btnLt.Text = "<";
            this.btnLt.UseVisualStyleBackColor = true;
            this.btnLt.Click += new System.EventHandler(this.btnLt_Click);
            // 
            // btnNeq
            // 
            this.btnNeq.Location = new System.Drawing.Point(84, 219);
            this.btnNeq.Name = "btnNeq";
            this.btnNeq.Size = new System.Drawing.Size(40, 30);
            this.btnNeq.TabIndex = 6;
            this.btnNeq.Text = "<>";
            this.btnNeq.UseVisualStyleBackColor = true;
            this.btnNeq.Click += new System.EventHandler(this.btnNeq_Click);
            // 
            // btnLike
            // 
            this.btnLike.Location = new System.Drawing.Point(125, 219);
            this.btnLike.Name = "btnLike";
            this.btnLike.Size = new System.Drawing.Size(40, 30);
            this.btnLike.TabIndex = 7;
            this.btnLike.Text = "Like";
            this.btnLike.UseVisualStyleBackColor = true;
            this.btnLike.Click += new System.EventHandler(this.btnLike_Click);
            // 
            // btnGe
            // 
            this.btnGe.Location = new System.Drawing.Point(84, 251);
            this.btnGe.Name = "btnGe";
            this.btnGe.Size = new System.Drawing.Size(40, 30);
            this.btnGe.TabIndex = 8;
            this.btnGe.Text = ">=";
            this.btnGe.UseVisualStyleBackColor = true;
            this.btnGe.Click += new System.EventHandler(this.btnGe_Click);
            // 
            // btnLe
            // 
            this.btnLe.Location = new System.Drawing.Point(84, 283);
            this.btnLe.Name = "btnLe";
            this.btnLe.Size = new System.Drawing.Size(40, 30);
            this.btnLe.TabIndex = 9;
            this.btnLe.Text = "<=";
            this.btnLe.UseVisualStyleBackColor = true;
            this.btnLe.Click += new System.EventHandler(this.btnLe_Click);
            // 
            // btnAnd
            // 
            this.btnAnd.Location = new System.Drawing.Point(125, 251);
            this.btnAnd.Name = "btnAnd";
            this.btnAnd.Size = new System.Drawing.Size(40, 30);
            this.btnAnd.TabIndex = 10;
            this.btnAnd.Text = "And";
            this.btnAnd.UseVisualStyleBackColor = true;
            this.btnAnd.Click += new System.EventHandler(this.btnAnd_Click);
            // 
            // btnOr
            // 
            this.btnOr.Location = new System.Drawing.Point(125, 283);
            this.btnOr.Name = "btnOr";
            this.btnOr.Size = new System.Drawing.Size(40, 30);
            this.btnOr.TabIndex = 11;
            this.btnOr.Text = "Or";
            this.btnOr.UseVisualStyleBackColor = true;
            this.btnOr.Click += new System.EventHandler(this.btnOr_Click);
            // 
            // btnGetUniqueValue
            // 
            this.btnGetUniqueValue.Location = new System.Drawing.Point(181, 348);
            this.btnGetUniqueValue.Name = "btnGetUniqueValue";
            this.btnGetUniqueValue.Size = new System.Drawing.Size(131, 30);
            this.btnGetUniqueValue.TabIndex = 19;
            this.btnGetUniqueValue.Text = "获取唯一值";
            this.btnGetUniqueValue.UseVisualStyleBackColor = true;
            this.btnGetUniqueValue.Click += new System.EventHandler(this.btnGetUniqueValue_Click);
            // 
            // listBoxUniqueValues
            // 
            this.listBoxUniqueValues.FormattingEnabled = true;
            this.listBoxUniqueValues.ItemHeight = 12;
            this.listBoxUniqueValues.Location = new System.Drawing.Point(181, 219);
            this.listBoxUniqueValues.Name = "listBoxUniqueValues";
            this.listBoxUniqueValues.Size = new System.Drawing.Size(131, 124);
            this.listBoxUniqueValues.TabIndex = 20;
            this.listBoxUniqueValues.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ListBoxUniqueValues_MouseDoubleClick);
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
            this.labelFieldList.Size = new System.Drawing.Size(67, 15);
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
            this.btnNot.Location = new System.Drawing.Point(125, 319);
            this.btnNot.Name = "btnNot";
            this.btnNot.Size = new System.Drawing.Size(40, 30);
            this.btnNot.TabIndex = 32;
            this.btnNot.Text = "Not";
            this.btnNot.UseVisualStyleBackColor = true;
            this.btnNot.Click += new System.EventHandler(this.btnNot_Click);
            // 
            // btnIn
            // 
            this.btnIn.Location = new System.Drawing.Point(84, 319);
            this.btnIn.Name = "btnIn";
            this.btnIn.Size = new System.Drawing.Size(40, 30);
            this.btnIn.TabIndex = 31;
            this.btnIn.Text = "In";
            this.btnIn.UseVisualStyleBackColor = true;
            this.btnIn.Click += new System.EventHandler(this.btnIn_Click);
            // 
            // btnIs
            // 
            this.btnIs.Location = new System.Drawing.Point(43, 319);
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
            this.labelSQL.Size = new System.Drawing.Size(0, 15);
            this.labelSQL.TabIndex = 33;
            // 
            // btnNull
            // 
            this.btnNull.Location = new System.Drawing.Point(125, 355);
            this.btnNull.Name = "btnNull";
            this.btnNull.Size = new System.Drawing.Size(40, 30);
            this.btnNull.TabIndex = 36;
            this.btnNull.Text = "Null";
            this.btnNull.UseVisualStyleBackColor = true;
            this.btnNull.Click += new System.EventHandler(this.btnNull_Click);
            // 
            // btnLine
            // 
            this.btnLine.Location = new System.Drawing.Point(84, 355);
            this.btnLine.Name = "btnLine";
            this.btnLine.Size = new System.Drawing.Size(19, 30);
            this.btnLine.TabIndex = 35;
            this.btnLine.Text = "_";
            this.btnLine.UseVisualStyleBackColor = true;
            this.btnLine.Click += new System.EventHandler(this.btnLine_Click);
            // 
            // btnBracket
            // 
            this.btnBracket.Location = new System.Drawing.Point(43, 355);
            this.btnBracket.Name = "btnBracket";
            this.btnBracket.Size = new System.Drawing.Size(40, 30);
            this.btnBracket.TabIndex = 34;
            this.btnBracket.Text = "()";
            this.btnBracket.UseVisualStyleBackColor = true;
            this.btnBracket.Click += new System.EventHandler(this.btnBracket_Click);
            // 
            // btnPercnt
            // 
            this.btnPercnt.Location = new System.Drawing.Point(105, 355);
            this.btnPercnt.Name = "btnPercnt";
            this.btnPercnt.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnPercnt.Size = new System.Drawing.Size(19, 30);
            this.btnPercnt.TabIndex = 37;
            this.btnPercnt.Text = "%";
            this.btnPercnt.UseVisualStyleBackColor = true;
            this.btnPercnt.Click += new System.EventHandler(this.btnPercnt_Click);
            // 
            // SelectByAttributeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(347, 549);
            this.Controls.Add(this.btnPercnt);
            this.Controls.Add(this.btnNull);
            this.Controls.Add(this.btnLine);
            this.Controls.Add(this.btnBracket);
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
            this.Controls.Add(this.listBoxUniqueValues);
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
            this.Controls.Add(this.listBoxFields);
            this.Controls.Add(this.labelLayers);
            this.Controls.Add(this.cbLayers);
            this.Name = "SelectByAttributeForm";
            this.Text = "按属性查询";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbLayers;
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
        private System.Windows.Forms.ListBox listBoxUniqueValues;
        private System.Windows.Forms.TextBox TextBoxSQL;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnValidate;
        private System.Windows.Forms.ListBox listBoxFields;
        private System.Windows.Forms.Label labelFieldList;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnNot;
        private System.Windows.Forms.Button btnIn;
        private System.Windows.Forms.Button btnIs;
        private System.Windows.Forms.Label labelSQL;
        private System.Windows.Forms.Button btnNull;
        private System.Windows.Forms.Button btnLine;
        private System.Windows.Forms.Button btnBracket;
        private System.Windows.Forms.Button btnPercnt;
    }
}