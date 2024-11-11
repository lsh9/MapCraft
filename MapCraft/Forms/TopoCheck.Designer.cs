namespace MapCraft.Forms
{
    partial class TopoCheck
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
            this.labelLayers = new System.Windows.Forms.Label();
            this.lblLayerName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbTopoRule = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblLayerType = new System.Windows.Forms.Label();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.lblTolerance = new System.Windows.Forms.Label();
            this.btnGeoDefaultTol = new System.Windows.Forms.Button();
            this.btnProjDefaultTol = new System.Windows.Forms.Button();
            this.txtTolerance = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // labelLayers
            // 
            this.labelLayers.AutoSize = true;
            this.labelLayers.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelLayers.Location = new System.Drawing.Point(13, 9);
            this.labelLayers.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelLayers.Name = "labelLayers";
            this.labelLayers.Size = new System.Drawing.Size(69, 20);
            this.labelLayers.TabIndex = 2;
            this.labelLayers.Text = "图层：";
            this.labelLayers.Click += new System.EventHandler(this.labelLayers_Click);
            // 
            // lblLayerName
            // 
            this.lblLayerName.AutoSize = true;
            this.lblLayerName.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblLayerName.Location = new System.Drawing.Point(90, 9);
            this.lblLayerName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLayerName.Name = "lblLayerName";
            this.lblLayerName.Size = new System.Drawing.Size(189, 20);
            this.lblLayerName.TabIndex = 3;
            this.lblLayerName.Text = "Default Layer Name";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(13, 87);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "规则：";
            // 
            // cbTopoRule
            // 
            this.cbTopoRule.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTopoRule.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbTopoRule.FormattingEnabled = true;
            this.cbTopoRule.Location = new System.Drawing.Point(89, 84);
            this.cbTopoRule.Name = "cbTopoRule";
            this.cbTopoRule.Size = new System.Drawing.Size(347, 28);
            this.cbTopoRule.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(13, 45);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "类型：";
            // 
            // lblLayerType
            // 
            this.lblLayerType.AutoSize = true;
            this.lblLayerType.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblLayerType.Location = new System.Drawing.Point(90, 45);
            this.lblLayerType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLayerType.Name = "lblLayerType";
            this.lblLayerType.Size = new System.Drawing.Size(189, 20);
            this.lblLayerType.TabIndex = 7;
            this.lblLayerType.Text = "Default Layer Type";
            // 
            // btnConfirm
            // 
            this.btnConfirm.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnConfirm.Location = new System.Drawing.Point(170, 219);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(97, 35);
            this.btnConfirm.TabIndex = 8;
            this.btnConfirm.Text = "确定";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // lblTolerance
            // 
            this.lblTolerance.AutoSize = true;
            this.lblTolerance.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTolerance.Location = new System.Drawing.Point(13, 128);
            this.lblTolerance.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTolerance.Name = "lblTolerance";
            this.lblTolerance.Size = new System.Drawing.Size(69, 20);
            this.lblTolerance.TabIndex = 9;
            this.lblTolerance.Text = "容差：";
            // 
            // btnGeoDefaultTol
            // 
            this.btnGeoDefaultTol.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnGeoDefaultTol.Location = new System.Drawing.Point(17, 173);
            this.btnGeoDefaultTol.Name = "btnGeoDefaultTol";
            this.btnGeoDefaultTol.Size = new System.Drawing.Size(201, 35);
            this.btnGeoDefaultTol.TabIndex = 11;
            this.btnGeoDefaultTol.Text = "地理坐标默认容差";
            this.btnGeoDefaultTol.UseVisualStyleBackColor = true;
            this.btnGeoDefaultTol.Click += new System.EventHandler(this.btnGeoDefaultTol_Click);
            // 
            // btnProjDefaultTol
            // 
            this.btnProjDefaultTol.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnProjDefaultTol.Location = new System.Drawing.Point(235, 173);
            this.btnProjDefaultTol.Name = "btnProjDefaultTol";
            this.btnProjDefaultTol.Size = new System.Drawing.Size(201, 35);
            this.btnProjDefaultTol.TabIndex = 12;
            this.btnProjDefaultTol.Text = "投影坐标默认容差";
            this.btnProjDefaultTol.UseVisualStyleBackColor = true;
            this.btnProjDefaultTol.Click += new System.EventHandler(this.btnProjDefaultTol_Click);
            // 
            // txtTolerance
            // 
            this.txtTolerance.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtTolerance.Location = new System.Drawing.Point(93, 125);
            this.txtTolerance.Name = "txtTolerance";
            this.txtTolerance.Size = new System.Drawing.Size(343, 30);
            this.txtTolerance.TabIndex = 13;
            this.txtTolerance.Text = "0.0002";
            // 
            // TopoCheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(448, 266);
            this.Controls.Add(this.txtTolerance);
            this.Controls.Add(this.btnProjDefaultTol);
            this.Controls.Add(this.btnGeoDefaultTol);
            this.Controls.Add(this.lblTolerance);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.lblLayerType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbTopoRule);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblLayerName);
            this.Controls.Add(this.labelLayers);
            this.Name = "TopoCheck";
            this.Text = "TopoCheck";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelLayers;
        private System.Windows.Forms.Label lblLayerName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbTopoRule;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblLayerType;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Label lblTolerance;
        private System.Windows.Forms.Button btnGeoDefaultTol;
        private System.Windows.Forms.Button btnProjDefaultTol;
        private System.Windows.Forms.TextBox txtTolerance;
    }
}