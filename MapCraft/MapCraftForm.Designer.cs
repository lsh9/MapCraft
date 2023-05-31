namespace MapCraft
{
    partial class MapCraftForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MapCraftForm));
            MyMapObjects.moLayers moLayers1 = new MyMapObjects.moLayers();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.新建ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.另存为ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tStripMapOperator = new System.Windows.Forms.ToolStrip();
            this.选择ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.按属性选择ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.按位置选择ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnAddData = new System.Windows.Forms.ToolStripButton();
            this.btnZoomIn = new System.Windows.Forms.ToolStripButton();
            this.btnZoomOut = new System.Windows.Forms.ToolStripButton();
            this.btnFullExtent = new System.Windows.Forms.ToolStripButton();
            this.btnFixedZoomIn = new System.Windows.Forms.ToolStripButton();
            this.btnFixedZoomOut = new System.Windows.Forms.ToolStripButton();
            this.btnPan = new System.Windows.Forms.ToolStripButton();
            this.btnSelectByLocation = new System.Windows.Forms.ToolStripButton();
            this.btnSelectByAttribute = new System.Windows.Forms.ToolStripButton();
            this.btnClearSelection = new System.Windows.Forms.ToolStripButton();
            this.btnIdentify = new System.Windows.Forms.ToolStripButton();
            this.tStripFeatureEditor = new System.Windows.Forms.ToolStrip();
            this.ddbtnEditor = new System.Windows.Forms.ToolStripDropDownButton();
            this.开始编辑ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.结束编辑ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.保存编辑内容ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnStartEdit = new System.Windows.Forms.ToolStripButton();
            this.btnCreateFeature = new System.Windows.Forms.ToolStripButton();
            this.btnMoveNode = new System.Windows.Forms.ToolStripButton();
            this.btnAddNode = new System.Windows.Forms.ToolStripButton();
            this.btnDeleteNode = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.MapScaleButton = new System.Windows.Forms.ToolStripSplitButton();
            this.coordinateStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.cbxProjectionCS = new System.Windows.Forms.CheckBox();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.moMapControl1 = new MyMapObjects.moMapControl();
            this.menuStrip1.SuspendLayout();
            this.tStripMapOperator.SuspendLayout();
            this.tStripFeatureEditor.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem,
            this.选择ToolStripMenuItem,
            this.帮助ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1491, 39);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新建ToolStripMenuItem,
            this.打开ToolStripMenuItem,
            this.保存ToolStripMenuItem,
            this.另存为ToolStripMenuItem});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(82, 35);
            this.文件ToolStripMenuItem.Text = "文件";
            // 
            // 新建ToolStripMenuItem
            // 
            this.新建ToolStripMenuItem.Name = "新建ToolStripMenuItem";
            this.新建ToolStripMenuItem.Size = new System.Drawing.Size(359, 44);
            this.新建ToolStripMenuItem.Text = "新建";
            // 
            // 打开ToolStripMenuItem
            // 
            this.打开ToolStripMenuItem.Name = "打开ToolStripMenuItem";
            this.打开ToolStripMenuItem.Size = new System.Drawing.Size(359, 44);
            this.打开ToolStripMenuItem.Text = "打开";
            // 
            // 保存ToolStripMenuItem
            // 
            this.保存ToolStripMenuItem.Name = "保存ToolStripMenuItem";
            this.保存ToolStripMenuItem.Size = new System.Drawing.Size(359, 44);
            this.保存ToolStripMenuItem.Text = "保存";
            // 
            // 另存为ToolStripMenuItem
            // 
            this.另存为ToolStripMenuItem.Name = "另存为ToolStripMenuItem";
            this.另存为ToolStripMenuItem.Size = new System.Drawing.Size(359, 44);
            this.另存为ToolStripMenuItem.Text = "另存为";
            // 
            // tStripMapOperator
            // 
            this.tStripMapOperator.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tStripMapOperator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAddData,
            this.btnZoomIn,
            this.btnZoomOut,
            this.btnPan,
            this.btnFullExtent,
            this.btnFixedZoomIn,
            this.btnFixedZoomOut,
            this.btnSelectByLocation,
            this.btnSelectByAttribute,
            this.btnClearSelection,
            this.btnIdentify});
            this.tStripMapOperator.Location = new System.Drawing.Point(0, 39);
            this.tStripMapOperator.Name = "tStripMapOperator";
            this.tStripMapOperator.Size = new System.Drawing.Size(1491, 42);
            this.tStripMapOperator.TabIndex = 1;
            this.tStripMapOperator.Text = "toolStrip1";
            // 
            // 选择ToolStripMenuItem
            // 
            this.选择ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.按属性选择ToolStripMenuItem,
            this.按位置选择ToolStripMenuItem});
            this.选择ToolStripMenuItem.Name = "选择ToolStripMenuItem";
            this.选择ToolStripMenuItem.Size = new System.Drawing.Size(82, 35);
            this.选择ToolStripMenuItem.Text = "选择";
            // 
            // 按属性选择ToolStripMenuItem
            // 
            this.按属性选择ToolStripMenuItem.Name = "按属性选择ToolStripMenuItem";
            this.按属性选择ToolStripMenuItem.Size = new System.Drawing.Size(359, 44);
            this.按属性选择ToolStripMenuItem.Text = "按属性选择";
            // 
            // 按位置选择ToolStripMenuItem
            // 
            this.按位置选择ToolStripMenuItem.Name = "按位置选择ToolStripMenuItem";
            this.按位置选择ToolStripMenuItem.Size = new System.Drawing.Size(359, 44);
            this.按位置选择ToolStripMenuItem.Text = "按位置选择";
            // 
            // 帮助ToolStripMenuItem
            // 
            this.帮助ToolStripMenuItem.Name = "帮助ToolStripMenuItem";
            this.帮助ToolStripMenuItem.Size = new System.Drawing.Size(82, 35);
            this.帮助ToolStripMenuItem.Text = "帮助";
            // 
            // btnAddData
            // 
            this.btnAddData.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAddData.Image = global::MapCraft.Properties.Resources.添加数据;
            this.btnAddData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddData.Name = "btnAddData";
            this.btnAddData.Size = new System.Drawing.Size(46, 36);
            this.btnAddData.Text = "toolStripButton1";
            // 
            // btnZoomIn
            // 
            this.btnZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnZoomIn.Image = global::MapCraft.Properties.Resources.放大;
            this.btnZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnZoomIn.Name = "btnZoomIn";
            this.btnZoomIn.Size = new System.Drawing.Size(46, 36);
            this.btnZoomIn.Text = "toolStripButton2";
            // 
            // btnZoomOut
            // 
            this.btnZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnZoomOut.Image = global::MapCraft.Properties.Resources.缩小;
            this.btnZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnZoomOut.Name = "btnZoomOut";
            this.btnZoomOut.Size = new System.Drawing.Size(46, 36);
            this.btnZoomOut.Text = "toolStripButton3";
            // 
            // btnFullExtent
            // 
            this.btnFullExtent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFullExtent.Image = global::MapCraft.Properties.Resources.全图;
            this.btnFullExtent.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFullExtent.Name = "btnFullExtent";
            this.btnFullExtent.Size = new System.Drawing.Size(46, 36);
            this.btnFullExtent.Text = "toolStripButton4";
            // 
            // btnFixedZoomIn
            // 
            this.btnFixedZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFixedZoomIn.Image = global::MapCraft.Properties.Resources.固定比例放大;
            this.btnFixedZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFixedZoomIn.Name = "btnFixedZoomIn";
            this.btnFixedZoomIn.Size = new System.Drawing.Size(46, 36);
            this.btnFixedZoomIn.Text = "toolStripButton5";
            // 
            // btnFixedZoomOut
            // 
            this.btnFixedZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFixedZoomOut.Image = global::MapCraft.Properties.Resources.固定比例缩小;
            this.btnFixedZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFixedZoomOut.Name = "btnFixedZoomOut";
            this.btnFixedZoomOut.Size = new System.Drawing.Size(46, 36);
            this.btnFixedZoomOut.Text = "toolStripButton6";
            // 
            // btnPan
            // 
            this.btnPan.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPan.Image = global::MapCraft.Properties.Resources.漫游;
            this.btnPan.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPan.Name = "btnPan";
            this.btnPan.Size = new System.Drawing.Size(46, 36);
            this.btnPan.Text = "toolStripButton7";
            // 
            // btnSelectByLocation
            // 
            this.btnSelectByLocation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSelectByLocation.Image = global::MapCraft.Properties.Resources.按位置选择;
            this.btnSelectByLocation.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSelectByLocation.Name = "btnSelectByLocation";
            this.btnSelectByLocation.Size = new System.Drawing.Size(46, 36);
            this.btnSelectByLocation.Text = "toolStripButton8";
            // 
            // btnSelectByAttribute
            // 
            this.btnSelectByAttribute.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSelectByAttribute.Image = global::MapCraft.Properties.Resources.按属性选择;
            this.btnSelectByAttribute.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSelectByAttribute.Name = "btnSelectByAttribute";
            this.btnSelectByAttribute.Size = new System.Drawing.Size(46, 36);
            this.btnSelectByAttribute.Text = "toolStripButton9";
            // 
            // btnClearSelection
            // 
            this.btnClearSelection.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnClearSelection.Image = global::MapCraft.Properties.Resources.清除选择;
            this.btnClearSelection.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnClearSelection.Name = "btnClearSelection";
            this.btnClearSelection.Size = new System.Drawing.Size(46, 36);
            this.btnClearSelection.Text = "toolStripButton10";
            // 
            // btnIdentify
            // 
            this.btnIdentify.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnIdentify.Image = global::MapCraft.Properties.Resources.识别;
            this.btnIdentify.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnIdentify.Name = "btnIdentify";
            this.btnIdentify.Size = new System.Drawing.Size(46, 36);
            this.btnIdentify.Text = "toolStripButton11";
            // 
            // tStripFeatureEditor
            // 
            this.tStripFeatureEditor.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tStripFeatureEditor.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ddbtnEditor,
            this.btnStartEdit,
            this.btnCreateFeature,
            this.btnMoveNode,
            this.btnAddNode,
            this.btnDeleteNode});
            this.tStripFeatureEditor.Location = new System.Drawing.Point(0, 81);
            this.tStripFeatureEditor.Name = "tStripFeatureEditor";
            this.tStripFeatureEditor.Size = new System.Drawing.Size(1491, 42);
            this.tStripFeatureEditor.TabIndex = 2;
            this.tStripFeatureEditor.Text = "toolStrip2";
            // 
            // ddbtnEditor
            // 
            this.ddbtnEditor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ddbtnEditor.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.开始编辑ToolStripMenuItem,
            this.结束编辑ToolStripMenuItem,
            this.保存编辑内容ToolStripMenuItem});
            this.ddbtnEditor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ddbtnEditor.Name = "ddbtnEditor";
            this.ddbtnEditor.Size = new System.Drawing.Size(108, 36);
            this.ddbtnEditor.Text = "编辑器";
            // 
            // 开始编辑ToolStripMenuItem
            // 
            this.开始编辑ToolStripMenuItem.Name = "开始编辑ToolStripMenuItem";
            this.开始编辑ToolStripMenuItem.Size = new System.Drawing.Size(359, 44);
            this.开始编辑ToolStripMenuItem.Text = "开始编辑";
            // 
            // 结束编辑ToolStripMenuItem
            // 
            this.结束编辑ToolStripMenuItem.Name = "结束编辑ToolStripMenuItem";
            this.结束编辑ToolStripMenuItem.Size = new System.Drawing.Size(359, 44);
            this.结束编辑ToolStripMenuItem.Text = "结束编辑";
            // 
            // 保存编辑内容ToolStripMenuItem
            // 
            this.保存编辑内容ToolStripMenuItem.Name = "保存编辑内容ToolStripMenuItem";
            this.保存编辑内容ToolStripMenuItem.Size = new System.Drawing.Size(359, 44);
            this.保存编辑内容ToolStripMenuItem.Text = "保存编辑内容";
            // 
            // btnStartEdit
            // 
            this.btnStartEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnStartEdit.Image = global::MapCraft.Properties.Resources.编辑要素;
            this.btnStartEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnStartEdit.Name = "btnStartEdit";
            this.btnStartEdit.Size = new System.Drawing.Size(46, 36);
            this.btnStartEdit.Text = "toolStripButton12";
            // 
            // btnCreateFeature
            // 
            this.btnCreateFeature.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCreateFeature.Image = global::MapCraft.Properties.Resources.创建要素;
            this.btnCreateFeature.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCreateFeature.Name = "btnCreateFeature";
            this.btnCreateFeature.Size = new System.Drawing.Size(46, 36);
            this.btnCreateFeature.Text = "toolStripButton13";
            // 
            // btnMoveNode
            // 
            this.btnMoveNode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnMoveNode.Image = global::MapCraft.Properties.Resources.移动节点;
            this.btnMoveNode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMoveNode.Name = "btnMoveNode";
            this.btnMoveNode.Size = new System.Drawing.Size(46, 36);
            this.btnMoveNode.Text = "toolStripButton14";
            // 
            // btnAddNode
            // 
            this.btnAddNode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAddNode.Image = global::MapCraft.Properties.Resources.添加结点;
            this.btnAddNode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddNode.Name = "btnAddNode";
            this.btnAddNode.Size = new System.Drawing.Size(46, 36);
            this.btnAddNode.Text = "toolStripButton15";
            // 
            // btnDeleteNode
            // 
            this.btnDeleteNode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDeleteNode.Image = global::MapCraft.Properties.Resources.删除节点;
            this.btnDeleteNode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDeleteNode.Name = "btnDeleteNode";
            this.btnDeleteNode.Size = new System.Drawing.Size(46, 36);
            this.btnDeleteNode.Text = "toolStripButton16";
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MapScaleButton,
            this.coordinateStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 1072);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1491, 41);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // MapScaleButton
            // 
            this.MapScaleButton.AutoSize = false;
            this.MapScaleButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.MapScaleButton.Image = ((System.Drawing.Image)(resources.GetObject("MapScaleButton.Image")));
            this.MapScaleButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.MapScaleButton.Margin = new System.Windows.Forms.Padding(0, 4, 10, 0);
            this.MapScaleButton.Name = "MapScaleButton";
            this.MapScaleButton.Size = new System.Drawing.Size(250, 37);
            this.MapScaleButton.Text = "地图比例尺";
            // 
            // coordinateStatusLabel
            // 
            this.coordinateStatusLabel.AutoSize = false;
            this.coordinateStatusLabel.Name = "coordinateStatusLabel";
            this.coordinateStatusLabel.Size = new System.Drawing.Size(250, 31);
            this.coordinateStatusLabel.Text = "坐标";
            // 
            // cbxProjectionCS
            // 
            this.cbxProjectionCS.AutoSize = true;
            this.cbxProjectionCS.Location = new System.Drawing.Point(785, 1079);
            this.cbxProjectionCS.Name = "cbxProjectionCS";
            this.cbxProjectionCS.Size = new System.Drawing.Size(162, 28);
            this.cbxProjectionCS.TabIndex = 4;
            this.cbxProjectionCS.Text = "投影坐标系";
            this.cbxProjectionCS.UseVisualStyleBackColor = true;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Left;
            this.treeView1.Location = new System.Drawing.Point(0, 123);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(314, 949);
            this.treeView1.TabIndex = 5;
            // 
            // moMapControl1
            // 
            this.moMapControl1.BackColor = System.Drawing.Color.White;
            this.moMapControl1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.moMapControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.moMapControl1.FlashColor = System.Drawing.Color.Green;
            this.moMapControl1.Layers = moLayers1;
            this.moMapControl1.Location = new System.Drawing.Point(314, 123);
            this.moMapControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.moMapControl1.Name = "moMapControl1";
            this.moMapControl1.SelectionColor = System.Drawing.Color.Cyan;
            this.moMapControl1.Size = new System.Drawing.Size(1177, 949);
            this.moMapControl1.TabIndex = 6;
            // 
            // MapCraftForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1491, 1113);
            this.Controls.Add(this.moMapControl1);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.cbxProjectionCS);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tStripFeatureEditor);
            this.Controls.Add(this.tStripMapOperator);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MapCraftForm";
            this.Text = "MapCraft";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tStripMapOperator.ResumeLayout(false);
            this.tStripMapOperator.PerformLayout();
            this.tStripFeatureEditor.ResumeLayout(false);
            this.tStripFeatureEditor.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 新建ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打开ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 保存ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 另存为ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 选择ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 按属性选择ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 按位置选择ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 帮助ToolStripMenuItem;
        private System.Windows.Forms.ToolStrip tStripMapOperator;
        private System.Windows.Forms.ToolStripButton btnAddData;
        private System.Windows.Forms.ToolStripButton btnZoomIn;
        private System.Windows.Forms.ToolStripButton btnZoomOut;
        private System.Windows.Forms.ToolStripButton btnPan;
        private System.Windows.Forms.ToolStripButton btnFullExtent;
        private System.Windows.Forms.ToolStripButton btnFixedZoomIn;
        private System.Windows.Forms.ToolStripButton btnFixedZoomOut;
        private System.Windows.Forms.ToolStripButton btnSelectByLocation;
        private System.Windows.Forms.ToolStripButton btnSelectByAttribute;
        private System.Windows.Forms.ToolStripButton btnClearSelection;
        private System.Windows.Forms.ToolStripButton btnIdentify;
        private System.Windows.Forms.ToolStrip tStripFeatureEditor;
        private System.Windows.Forms.ToolStripDropDownButton ddbtnEditor;
        private System.Windows.Forms.ToolStripMenuItem 开始编辑ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 结束编辑ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 保存编辑内容ToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton btnStartEdit;
        private System.Windows.Forms.ToolStripButton btnCreateFeature;
        private System.Windows.Forms.ToolStripButton btnMoveNode;
        private System.Windows.Forms.ToolStripButton btnAddNode;
        private System.Windows.Forms.ToolStripButton btnDeleteNode;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripSplitButton MapScaleButton;
        private System.Windows.Forms.ToolStripStatusLabel coordinateStatusLabel;
        private System.Windows.Forms.CheckBox cbxProjectionCS;
        private System.Windows.Forms.TreeView treeView1;
        private MyMapObjects.moMapControl moMapControl1;
    }
}

