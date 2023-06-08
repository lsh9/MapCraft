namespace MapCraft.Forms
{
    partial class IdentifyForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridViewAttributes = new System.Windows.Forms.DataGridView();
            this.treeViewFeatures = new System.Windows.Forms.TreeView();
            this.cbBoxLayers = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAttributes)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F);
            this.label1.Location = new System.Drawing.Point(33, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "图层 ：";
            // 
            // dataGridViewAttributes
            // 
            this.dataGridViewAttributes.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridViewAttributes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewAttributes.GridColor = System.Drawing.SystemColors.ButtonShadow;
            this.dataGridViewAttributes.Location = new System.Drawing.Point(27, 196);
            this.dataGridViewAttributes.Name = "dataGridViewAttributes";
            this.dataGridViewAttributes.RowHeadersWidth = 51;
            this.dataGridViewAttributes.RowTemplate.Height = 23;
            this.dataGridViewAttributes.Size = new System.Drawing.Size(293, 235);
            this.dataGridViewAttributes.TabIndex = 3;
            // 
            // treeViewFeatures
            // 
            this.treeViewFeatures.Location = new System.Drawing.Point(27, 59);
            this.treeViewFeatures.Name = "treeViewFeatures";
            this.treeViewFeatures.Size = new System.Drawing.Size(293, 118);
            this.treeViewFeatures.TabIndex = 2;
            this.treeViewFeatures.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewFeatures_NodeMouseClick);
            // 
            // cbBoxLayers
            // 
            this.cbBoxLayers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBoxLayers.FormattingEnabled = true;
            this.cbBoxLayers.Location = new System.Drawing.Point(90, 21);
            this.cbBoxLayers.Name = "cbBoxLayers";
            this.cbBoxLayers.Size = new System.Drawing.Size(219, 20);
            this.cbBoxLayers.TabIndex = 7;
            this.cbBoxLayers.SelectedIndexChanged += new System.EventHandler(this.cbBoxLayers_SelectedIndexChanged);
            // 
            // IdentifyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(348, 461);
            this.Controls.Add(this.cbBoxLayers);
            this.Controls.Add(this.dataGridViewAttributes);
            this.Controls.Add(this.treeViewFeatures);
            this.Controls.Add(this.label1);
            this.Name = "IdentifyForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "识别";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAttributes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridViewAttributes;
        private System.Windows.Forms.TreeView treeViewFeatures;
        private System.Windows.Forms.ComboBox cbBoxLayers;
    }
}