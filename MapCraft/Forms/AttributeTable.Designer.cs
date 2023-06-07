namespace MapCraft.Forms
{
    partial class AttributeTable
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AttributeTable));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.开始编辑 = new System.Windows.Forms.ToolStripButton();
            this.添加字段 = new System.Windows.Forms.ToolStripButton();
            this.删除字段 = new System.Windows.Forms.ToolStripButton();
            this.停止编辑 = new System.Windows.Forms.ToolStripButton();
            this.按属性选择 = new System.Windows.Forms.ToolStripButton();
            this.全部选择 = new System.Windows.Forms.ToolStripButton();
            this.清除选择 = new System.Windows.Forms.ToolStripButton();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.Nameshow = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblSelectedNum = new System.Windows.Forms.ToolStripStatusLabel();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.toolStrip1.SuspendLayout();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.开始编辑,
            this.添加字段,
            this.删除字段,
            this.停止编辑,
            this.按属性选择,
            this.全部选择,
            this.清除选择});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(736, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // 开始编辑
            // 
            this.开始编辑.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.开始编辑.Image = ((System.Drawing.Image)(resources.GetObject("开始编辑.Image")));
            this.开始编辑.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.开始编辑.Name = "开始编辑";
            this.开始编辑.Size = new System.Drawing.Size(60, 22);
            this.开始编辑.Text = "开始编辑";
            this.开始编辑.Click += new System.EventHandler(this.开始编辑_Click);
            // 
            // 添加字段
            // 
            this.添加字段.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.添加字段.Image = ((System.Drawing.Image)(resources.GetObject("添加字段.Image")));
            this.添加字段.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.添加字段.Name = "添加字段";
            this.添加字段.Size = new System.Drawing.Size(60, 22);
            this.添加字段.Text = "添加字段";
            this.添加字段.Click += new System.EventHandler(this.添加字段_Click);
            // 
            // 删除字段
            // 
            this.删除字段.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.删除字段.Image = ((System.Drawing.Image)(resources.GetObject("删除字段.Image")));
            this.删除字段.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.删除字段.Name = "删除字段";
            this.删除字段.Size = new System.Drawing.Size(60, 22);
            this.删除字段.Text = "删除字段";
            this.删除字段.Click += new System.EventHandler(this.删除字段_Click);
            // 
            // 停止编辑
            // 
            this.停止编辑.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.停止编辑.Image = ((System.Drawing.Image)(resources.GetObject("停止编辑.Image")));
            this.停止编辑.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.停止编辑.Name = "停止编辑";
            this.停止编辑.Size = new System.Drawing.Size(60, 22);
            this.停止编辑.Text = "停止编辑";
            this.停止编辑.Click += new System.EventHandler(this.停止编辑_Click);
            // 
            // 按属性选择
            // 
            this.按属性选择.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.按属性选择.Image = ((System.Drawing.Image)(resources.GetObject("按属性选择.Image")));
            this.按属性选择.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.按属性选择.Name = "按属性选择";
            this.按属性选择.Size = new System.Drawing.Size(72, 22);
            this.按属性选择.Text = "按属性选择";
            this.按属性选择.Click += new System.EventHandler(this.按属性选择_Click);
            // 
            // 全部选择
            // 
            this.全部选择.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.全部选择.Image = ((System.Drawing.Image)(resources.GetObject("全部选择.Image")));
            this.全部选择.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.全部选择.Name = "全部选择";
            this.全部选择.Size = new System.Drawing.Size(60, 22);
            this.全部选择.Text = "全部选择";
            this.全部选择.Click += new System.EventHandler(this.全部选择_Click);
            // 
            // 清除选择
            // 
            this.清除选择.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.清除选择.Image = ((System.Drawing.Image)(resources.GetObject("清除选择.Image")));
            this.清除选择.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.清除选择.Name = "清除选择";
            this.清除选择.Size = new System.Drawing.Size(60, 22);
            this.清除选择.Text = "清除选择";
            this.清除选择.Click += new System.EventHandler(this.清除选择_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Nameshow,
            this.lblSelectedNum});
            this.statusStrip.Location = new System.Drawing.Point(0, 420);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(736, 22);
            this.statusStrip.TabIndex = 1;
            this.statusStrip.Text = "statusStrip1";
            // 
            // Nameshow
            // 
            this.Nameshow.Name = "Nameshow";
            this.Nameshow.Size = new System.Drawing.Size(40, 17);
            this.Nameshow.Text = "name";
            // 
            // lblSelectedNum
            // 
            this.lblSelectedNum.AutoSize = false;
            this.lblSelectedNum.Name = "lblSelectedNum";
            this.lblSelectedNum.Size = new System.Drawing.Size(90, 17);
            this.lblSelectedNum.Text = "0/0已选择";
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.Location = new System.Drawing.Point(0, 25);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowTemplate.Height = 23;
            this.dataGridView.Size = new System.Drawing.Size(736, 395);
            this.dataGridView.TabIndex = 2;
            this.dataGridView.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView_CellMouseUp);
            this.dataGridView.CellParsing += new System.Windows.Forms.DataGridViewCellParsingEventHandler(this.dataGridView_CellParsing);
            this.dataGridView.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView_ColumnHeaderMouseClick);
            this.dataGridView.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView_RowHeaderMouseClick);
            // 
            // AttributeTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(736, 442);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.toolStrip1);
            this.Name = "AttributeTable";
            this.Text = "属性表";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton 开始编辑;
        private System.Windows.Forms.ToolStripButton 添加字段;
        private System.Windows.Forms.ToolStripButton 删除字段;
        private System.Windows.Forms.ToolStripButton 停止编辑;
        private System.Windows.Forms.ToolStripButton 按属性选择;
        private System.Windows.Forms.ToolStripButton 全部选择;
        private System.Windows.Forms.ToolStripButton 清除选择;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.ToolStripStatusLabel Nameshow;
        private System.Windows.Forms.ToolStripStatusLabel lblSelectedNum;
    }
}