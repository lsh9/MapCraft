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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MapCraftForm));
            MyMapObjects.moLayers moLayers1 = new MyMapObjects.moLayers();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.新建地图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.新建图层ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.另存为ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.选择ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.按属性选择ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.按位置选择ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tStripMapOperator = new System.Windows.Forms.ToolStrip();
            this.btnAddData = new System.Windows.Forms.ToolStripButton();
            this.btnZoomIn = new System.Windows.Forms.ToolStripButton();
            this.btnZoomOut = new System.Windows.Forms.ToolStripButton();
            this.btnPan = new System.Windows.Forms.ToolStripButton();
            this.btnFullExtent = new System.Windows.Forms.ToolStripButton();
            this.btnFixedZoomIn = new System.Windows.Forms.ToolStripButton();
            this.btnFixedZoomOut = new System.Windows.Forms.ToolStripButton();
            this.btnSelectByLocation = new System.Windows.Forms.ToolStripButton();
            this.btnSelectByAttribute = new System.Windows.Forms.ToolStripButton();
            this.btnClearSelection = new System.Windows.Forms.ToolStripButton();
            this.btnIdentify = new System.Windows.Forms.ToolStripButton();
            this.tStripFeatureEditor = new System.Windows.Forms.ToolStrip();
            this.ddbtnEditor = new System.Windows.Forms.ToolStripDropDownButton();
            this.开始编辑ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.结束编辑ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.保存编辑内容ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnMoveFeature = new System.Windows.Forms.ToolStripButton();
            this.btnCreateFeature = new System.Windows.Forms.ToolStripButton();
            this.btnMoveNode = new System.Windows.Forms.ToolStripButton();
            this.btnAddNode = new System.Windows.Forms.ToolStripButton();
            this.btnDeleteNode = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.MapScaleButton = new System.Windows.Forms.ToolStripSplitButton();
            this.toolStripMenuItem10000000 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3000000 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1000000 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem500000 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem100000 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem10000 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1000 = new System.Windows.Forms.ToolStripMenuItem();
            this.coordinateStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.cbxProjectionCS = new System.Windows.Forms.CheckBox();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.LayerRightMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.移动图层ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.置顶ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.置底ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.上移ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.下移ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开属性表 = new System.Windows.Forms.ToolStripMenuItem();
            this.缩放至图层ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.渲染 = new System.Windows.Forms.ToolStripMenuItem();
            this.移除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.编辑ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.另存为 = new System.Windows.Forms.ToolStripMenuItem();
            this.CreateFeatureRightMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.完成部件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.完成绘制ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moMapControl1 = new MyMapObjects.moMapControl();
            this.DelFeatureRightMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.删除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.tStripMapOperator.SuspendLayout();
            this.tStripFeatureEditor.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.LayerRightMenu.SuspendLayout();
            this.CreateFeatureRightMenu.SuspendLayout();
            this.DelFeatureRightMenu.SuspendLayout();
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
            this.menuStrip1.Size = new System.Drawing.Size(1492, 39);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新建地图ToolStripMenuItem,
            this.新建图层ToolStripMenuItem,
            this.打开ToolStripMenuItem,
            this.保存ToolStripMenuItem,
            this.另存为ToolStripMenuItem,
            this.导出ToolStripMenuItem});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(82, 35);
            this.文件ToolStripMenuItem.Text = "文件";
            // 
            // 新建地图ToolStripMenuItem
            // 
            this.新建地图ToolStripMenuItem.Name = "新建地图ToolStripMenuItem";
            this.新建地图ToolStripMenuItem.Size = new System.Drawing.Size(243, 44);
            this.新建地图ToolStripMenuItem.Text = "新建地图";
            this.新建地图ToolStripMenuItem.Click += new System.EventHandler(this.新建地图ToolStripMenuItem_Click);
            // 
            // 新建图层ToolStripMenuItem
            // 
            this.新建图层ToolStripMenuItem.Name = "新建图层ToolStripMenuItem";
            this.新建图层ToolStripMenuItem.Size = new System.Drawing.Size(243, 44);
            this.新建图层ToolStripMenuItem.Text = "新建图层";
            this.新建图层ToolStripMenuItem.Click += new System.EventHandler(this.新建图层ToolStripMenuItem_Click);
            // 
            // 打开ToolStripMenuItem
            // 
            this.打开ToolStripMenuItem.Name = "打开ToolStripMenuItem";
            this.打开ToolStripMenuItem.Size = new System.Drawing.Size(243, 44);
            this.打开ToolStripMenuItem.Text = "打开地图";
            this.打开ToolStripMenuItem.Click += new System.EventHandler(this.打开地图ToolStripMenuItem_Click);
            // 
            // 保存ToolStripMenuItem
            // 
            this.保存ToolStripMenuItem.Name = "保存ToolStripMenuItem";
            this.保存ToolStripMenuItem.Size = new System.Drawing.Size(243, 44);
            this.保存ToolStripMenuItem.Text = "保存";
            this.保存ToolStripMenuItem.Click += new System.EventHandler(this.保存ToolStripMenuItem_Click);
            // 
            // 另存为ToolStripMenuItem
            // 
            this.另存为ToolStripMenuItem.Name = "另存为ToolStripMenuItem";
            this.另存为ToolStripMenuItem.Size = new System.Drawing.Size(243, 44);
            this.另存为ToolStripMenuItem.Text = "另存为";
            this.另存为ToolStripMenuItem.Click += new System.EventHandler(this.另存为ToolStripMenuItem_Click);
            // 
            // 导出ToolStripMenuItem
            // 
            this.导出ToolStripMenuItem.Name = "导出ToolStripMenuItem";
            this.导出ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.导出ToolStripMenuItem.Text = "导出";
            this.导出ToolStripMenuItem.Click += new System.EventHandler(this.导出ToolStripMenuItem_Click);
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
            this.按属性选择ToolStripMenuItem.Size = new System.Drawing.Size(267, 44);
            this.按属性选择ToolStripMenuItem.Text = "按属性选择";
            this.按属性选择ToolStripMenuItem.Click += new System.EventHandler(this.按属性选择ToolStripMenuItem_Click);
            // 
            // 按位置选择ToolStripMenuItem
            // 
            this.按位置选择ToolStripMenuItem.Name = "按位置选择ToolStripMenuItem";
            this.按位置选择ToolStripMenuItem.Size = new System.Drawing.Size(267, 44);
            this.按位置选择ToolStripMenuItem.Text = "按位置选择";
            this.按位置选择ToolStripMenuItem.Click += new System.EventHandler(this.按位置选择ToolStripMenuItem_Click);
            // 
            // 帮助ToolStripMenuItem
            // 
            this.帮助ToolStripMenuItem.Name = "帮助ToolStripMenuItem";
            this.帮助ToolStripMenuItem.Size = new System.Drawing.Size(82, 35);
            this.帮助ToolStripMenuItem.Text = "帮助";
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
            this.tStripMapOperator.Padding = new System.Windows.Forms.Padding(0);
            this.tStripMapOperator.Size = new System.Drawing.Size(1492, 42);
            this.tStripMapOperator.TabIndex = 1;
            this.tStripMapOperator.Text = "toolStrip1";
            this.tStripMapOperator.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.tStripMapOperator_ItemClicked);
            // 
            // btnAddData
            // 
            this.btnAddData.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAddData.Image = global::MapCraft.Properties.Resources.添加数据;
            this.btnAddData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddData.Name = "btnAddData";
            this.btnAddData.Size = new System.Drawing.Size(46, 36);
            this.btnAddData.Text = "toolStripButton1";
            this.btnAddData.Click += new System.EventHandler(this.btnAddData_Click);
            // 
            // btnZoomIn
            // 
            this.btnZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnZoomIn.Image = global::MapCraft.Properties.Resources.放大;
            this.btnZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnZoomIn.Name = "btnZoomIn";
            this.btnZoomIn.Size = new System.Drawing.Size(46, 36);
            this.btnZoomIn.Text = "toolStripButton2";
            this.btnZoomIn.Click += new System.EventHandler(this.btnZoomIn_Click);
            // 
            // btnZoomOut
            // 
            this.btnZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnZoomOut.Image = global::MapCraft.Properties.Resources.缩小;
            this.btnZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnZoomOut.Name = "btnZoomOut";
            this.btnZoomOut.Size = new System.Drawing.Size(46, 36);
            this.btnZoomOut.Text = "toolStripButton3";
            this.btnZoomOut.Click += new System.EventHandler(this.btnZoomOut_Click);
            // 
            // btnPan
            // 
            this.btnPan.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPan.Image = global::MapCraft.Properties.Resources.漫游;
            this.btnPan.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPan.Name = "btnPan";
            this.btnPan.Size = new System.Drawing.Size(46, 36);
            this.btnPan.Text = "toolStripButton7";
            this.btnPan.Click += new System.EventHandler(this.btnPan_Click);
            // 
            // btnFullExtent
            // 
            this.btnFullExtent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFullExtent.Image = global::MapCraft.Properties.Resources.全图;
            this.btnFullExtent.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFullExtent.Name = "btnFullExtent";
            this.btnFullExtent.Size = new System.Drawing.Size(46, 36);
            this.btnFullExtent.Text = "toolStripButton4";
            this.btnFullExtent.Click += new System.EventHandler(this.btnFullExtent_Click);
            // 
            // btnFixedZoomIn
            // 
            this.btnFixedZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFixedZoomIn.Image = global::MapCraft.Properties.Resources.固定比例放大;
            this.btnFixedZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFixedZoomIn.Name = "btnFixedZoomIn";
            this.btnFixedZoomIn.Size = new System.Drawing.Size(46, 36);
            this.btnFixedZoomIn.Text = "toolStripButton5";
            this.btnFixedZoomIn.Click += new System.EventHandler(this.btnFixedZoomIn_Click);
            // 
            // btnFixedZoomOut
            // 
            this.btnFixedZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFixedZoomOut.Image = global::MapCraft.Properties.Resources.固定比例缩小;
            this.btnFixedZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFixedZoomOut.Name = "btnFixedZoomOut";
            this.btnFixedZoomOut.Size = new System.Drawing.Size(46, 36);
            this.btnFixedZoomOut.Text = "toolStripButton6";
            this.btnFixedZoomOut.Click += new System.EventHandler(this.btnFixedZoomOut_Click);
            // 
            // btnSelectByLocation
            // 
            this.btnSelectByLocation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSelectByLocation.Image = global::MapCraft.Properties.Resources.按位置选择;
            this.btnSelectByLocation.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSelectByLocation.Name = "btnSelectByLocation";
            this.btnSelectByLocation.Size = new System.Drawing.Size(46, 36);
            this.btnSelectByLocation.Text = "toolStripButton8";
            this.btnSelectByLocation.Click += new System.EventHandler(this.btnSelectByLocation_Click);
            // 
            // btnSelectByAttribute
            // 
            this.btnSelectByAttribute.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSelectByAttribute.Image = global::MapCraft.Properties.Resources.按属性选择;
            this.btnSelectByAttribute.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSelectByAttribute.Name = "btnSelectByAttribute";
            this.btnSelectByAttribute.Size = new System.Drawing.Size(46, 36);
            this.btnSelectByAttribute.Text = "toolStripButton9";
            this.btnSelectByAttribute.Click += new System.EventHandler(this.btnSelectByAttribute_Click);
            // 
            // btnClearSelection
            // 
            this.btnClearSelection.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnClearSelection.Image = global::MapCraft.Properties.Resources.清除选择;
            this.btnClearSelection.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnClearSelection.Name = "btnClearSelection";
            this.btnClearSelection.Size = new System.Drawing.Size(46, 36);
            this.btnClearSelection.Text = "toolStripButton10";
            this.btnClearSelection.Click += new System.EventHandler(this.btnClearSelection_Click);
            // 
            // btnIdentify
            // 
            this.btnIdentify.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnIdentify.Image = global::MapCraft.Properties.Resources.识别;
            this.btnIdentify.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnIdentify.Name = "btnIdentify";
            this.btnIdentify.Size = new System.Drawing.Size(46, 36);
            this.btnIdentify.Text = "toolStripButton11";
            this.btnIdentify.Click += new System.EventHandler(this.btnIdentify_Click);
            // 
            // tStripFeatureEditor
            // 
            this.tStripFeatureEditor.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tStripFeatureEditor.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ddbtnEditor,
            this.btnMoveFeature,
            this.btnCreateFeature,
            this.btnMoveNode,
            this.btnAddNode,
            this.btnDeleteNode});
            this.tStripFeatureEditor.Location = new System.Drawing.Point(0, 81);
            this.tStripFeatureEditor.Name = "tStripFeatureEditor";
            this.tStripFeatureEditor.Padding = new System.Windows.Forms.Padding(0);
            this.tStripFeatureEditor.Size = new System.Drawing.Size(1492, 42);
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
            this.ddbtnEditor.Click += new System.EventHandler(this.ddbtnEditor_Click);
            // 
            // 开始编辑ToolStripMenuItem
            // 
            this.开始编辑ToolStripMenuItem.Enabled = false;
            this.开始编辑ToolStripMenuItem.Name = "开始编辑ToolStripMenuItem";
            this.开始编辑ToolStripMenuItem.Size = new System.Drawing.Size(291, 44);
            this.开始编辑ToolStripMenuItem.Text = "开始编辑";
            this.开始编辑ToolStripMenuItem.Click += new System.EventHandler(this.开始编辑ToolStripMenuItem_Click);
            // 
            // 结束编辑ToolStripMenuItem
            // 
            this.结束编辑ToolStripMenuItem.Enabled = false;
            this.结束编辑ToolStripMenuItem.Name = "结束编辑ToolStripMenuItem";
            this.结束编辑ToolStripMenuItem.Size = new System.Drawing.Size(291, 44);
            this.结束编辑ToolStripMenuItem.Text = "结束编辑";
            this.结束编辑ToolStripMenuItem.Click += new System.EventHandler(this.结束编辑ToolStripMenuItem_Click);
            // 
            // 保存编辑内容ToolStripMenuItem
            // 
            this.保存编辑内容ToolStripMenuItem.Enabled = false;
            this.保存编辑内容ToolStripMenuItem.Name = "保存编辑内容ToolStripMenuItem";
            this.保存编辑内容ToolStripMenuItem.Size = new System.Drawing.Size(291, 44);
            this.保存编辑内容ToolStripMenuItem.Text = "保存编辑内容";
            this.保存编辑内容ToolStripMenuItem.Click += new System.EventHandler(this.保存编辑内容ToolStripMenuItem_Click);
            // 
            // btnMoveFeature
            // 
            this.btnMoveFeature.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnMoveFeature.Enabled = false;
            this.btnMoveFeature.Image = global::MapCraft.Properties.Resources.编辑要素;
            this.btnMoveFeature.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMoveFeature.Name = "btnMoveFeature";
            this.btnMoveFeature.Size = new System.Drawing.Size(46, 36);
            this.btnMoveFeature.Text = "toolStripButton12";
            this.btnMoveFeature.Click += new System.EventHandler(this.btnMoveFeature_Click);
            // 
            // btnCreateFeature
            // 
            this.btnCreateFeature.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCreateFeature.Enabled = false;
            this.btnCreateFeature.Image = global::MapCraft.Properties.Resources.创建要素;
            this.btnCreateFeature.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCreateFeature.Name = "btnCreateFeature";
            this.btnCreateFeature.Size = new System.Drawing.Size(46, 36);
            this.btnCreateFeature.Text = "toolStripButton13";
            this.btnCreateFeature.Click += new System.EventHandler(this.btnCreateFeature_Click);
            // 
            // btnMoveNode
            // 
            this.btnMoveNode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnMoveNode.Enabled = false;
            this.btnMoveNode.Image = global::MapCraft.Properties.Resources.移动节点;
            this.btnMoveNode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMoveNode.Name = "btnMoveNode";
            this.btnMoveNode.Size = new System.Drawing.Size(46, 36);
            this.btnMoveNode.Text = "toolStripButton14";
            this.btnMoveNode.Click += new System.EventHandler(this.btnMoveNode_Click);
            // 
            // btnAddNode
            // 
            this.btnAddNode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAddNode.Enabled = false;
            this.btnAddNode.Image = global::MapCraft.Properties.Resources.添加结点;
            this.btnAddNode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddNode.Name = "btnAddNode";
            this.btnAddNode.Size = new System.Drawing.Size(46, 36);
            this.btnAddNode.Text = "toolStripButton15";
            this.btnAddNode.Click += new System.EventHandler(this.btnAddNode_Click);
            // 
            // btnDeleteNode
            // 
            this.btnDeleteNode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDeleteNode.Enabled = false;
            this.btnDeleteNode.Image = global::MapCraft.Properties.Resources.删除节点;
            this.btnDeleteNode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDeleteNode.Name = "btnDeleteNode";
            this.btnDeleteNode.Size = new System.Drawing.Size(46, 36);
            this.btnDeleteNode.Text = "toolStripButton16";
            this.btnDeleteNode.Click += new System.EventHandler(this.btnDeleteNode_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MapScaleButton,
            this.coordinateStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 1019);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(0, 0, 14, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1492, 41);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // MapScaleButton
            // 
            this.MapScaleButton.AutoSize = false;
            this.MapScaleButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.MapScaleButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem10000000,
            this.toolStripMenuItem3000000,
            this.toolStripMenuItem1000000,
            this.toolStripMenuItem500000,
            this.toolStripMenuItem100000,
            this.toolStripMenuItem10000,
            this.toolStripMenuItem1000});
            this.MapScaleButton.Image = ((System.Drawing.Image)(resources.GetObject("MapScaleButton.Image")));
            this.MapScaleButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.MapScaleButton.Margin = new System.Windows.Forms.Padding(0, 4, 10, 0);
            this.MapScaleButton.Name = "MapScaleButton";
            this.MapScaleButton.Size = new System.Drawing.Size(250, 37);
            this.MapScaleButton.Text = "地图比例尺";
            // 
            // toolStripMenuItem10000000
            // 
            this.toolStripMenuItem10000000.Name = "toolStripMenuItem10000000";
            this.toolStripMenuItem10000000.Size = new System.Drawing.Size(148, 22);
            this.toolStripMenuItem10000000.Text = "1:10,000,000";
            this.toolStripMenuItem10000000.Click += new System.EventHandler(this.toolStripMenuItem10000000_Click);
            // 
            // toolStripMenuItem3000000
            // 
            this.toolStripMenuItem3000000.Name = "toolStripMenuItem3000000";
            this.toolStripMenuItem3000000.Size = new System.Drawing.Size(148, 22);
            this.toolStripMenuItem3000000.Text = "1:3,000,000";
            this.toolStripMenuItem3000000.Click += new System.EventHandler(this.toolStripMenuItem3000000_Click);
            // 
            // toolStripMenuItem1000000
            // 
            this.toolStripMenuItem1000000.Name = "toolStripMenuItem1000000";
            this.toolStripMenuItem1000000.Size = new System.Drawing.Size(148, 22);
            this.toolStripMenuItem1000000.Text = "1:1,000,000";
            this.toolStripMenuItem1000000.Click += new System.EventHandler(this.toolStripMenuItem1000000_Click);
            // 
            // toolStripMenuItem500000
            // 
            this.toolStripMenuItem500000.Name = "toolStripMenuItem500000";
            this.toolStripMenuItem500000.Size = new System.Drawing.Size(148, 22);
            this.toolStripMenuItem500000.Text = "1:500,000";
            this.toolStripMenuItem500000.Click += new System.EventHandler(this.toolStripMenuItem500000_Click);
            // 
            // toolStripMenuItem100000
            // 
            this.toolStripMenuItem100000.Name = "toolStripMenuItem100000";
            this.toolStripMenuItem100000.Size = new System.Drawing.Size(148, 22);
            this.toolStripMenuItem100000.Text = "1:100,000";
            this.toolStripMenuItem100000.Click += new System.EventHandler(this.toolStripMenuItem100000_Click);
            // 
            // toolStripMenuItem10000
            // 
            this.toolStripMenuItem10000.Name = "toolStripMenuItem10000";
            this.toolStripMenuItem10000.Size = new System.Drawing.Size(148, 22);
            this.toolStripMenuItem10000.Text = "1:10,000";
            this.toolStripMenuItem10000.Click += new System.EventHandler(this.toolStripMenuItem10000_Click);
            // 
            // toolStripMenuItem1000
            // 
            this.toolStripMenuItem1000.Name = "toolStripMenuItem1000";
            this.toolStripMenuItem1000.Size = new System.Drawing.Size(148, 22);
            this.toolStripMenuItem1000.Text = "1:1,000";
            this.toolStripMenuItem1000.Click += new System.EventHandler(this.toolStripMenuItem1000_Click);
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
            this.cbxProjectionCS.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.cbxProjectionCS.Location = new System.Drawing.Point(0, 991);
            this.cbxProjectionCS.Margin = new System.Windows.Forms.Padding(4);
            this.cbxProjectionCS.Name = "cbxProjectionCS";
            this.cbxProjectionCS.Size = new System.Drawing.Size(1492, 28);
            this.cbxProjectionCS.TabIndex = 4;
            this.cbxProjectionCS.Text = "投影坐标系";
            this.cbxProjectionCS.UseVisualStyleBackColor = true;
            // 
            // treeView1
            // 
            this.treeView1.AllowDrop = true;
            this.treeView1.CheckBoxes = true;
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Left;
            this.treeView1.Location = new System.Drawing.Point(0, 123);
            this.treeView1.Margin = new System.Windows.Forms.Padding(4);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(314, 868);
            this.treeView1.TabIndex = 5;
            this.treeView1.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterCheck);
            this.treeView1.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.TreeView1_ItemDrag);
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);

            this.treeView1.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseDoubleClick);
            this.treeView1.DragDrop += new System.Windows.Forms.DragEventHandler(this.TreeView1_DragDrop);
            this.treeView1.DragEnter += new System.Windows.Forms.DragEventHandler(this.TreeView1_DragEnter);
            // 
            // LayerRightMenu
            // 
            this.LayerRightMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.LayerRightMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.移动图层ToolStripMenuItem,
            this.打开属性表,
            this.缩放至图层ToolStripMenuItem,
            this.渲染,
            this.移除ToolStripMenuItem,
            this.编辑ToolStripMenuItem,
            this.另存为});
            this.LayerRightMenu.Name = "contextMenuStrip1";
            this.LayerRightMenu.Size = new System.Drawing.Size(137, 158);
            // 
            // 移动图层ToolStripMenuItem
            // 
            this.移动图层ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.置顶ToolStripMenuItem,
            this.置底ToolStripMenuItem,
            this.上移ToolStripMenuItem,
            this.下移ToolStripMenuItem});
            this.移动图层ToolStripMenuItem.Name = "移动图层ToolStripMenuItem";
            this.移动图层ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.移动图层ToolStripMenuItem.Text = "移动";
            // 
            // 置顶ToolStripMenuItem
            // 
            this.置顶ToolStripMenuItem.Name = "置顶ToolStripMenuItem";
            this.置顶ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.置顶ToolStripMenuItem.Text = "置顶";
            this.置顶ToolStripMenuItem.Click += new System.EventHandler(this.置顶ToolStripMenuItem_Click);
            // 
            // 置底ToolStripMenuItem
            // 
            this.置底ToolStripMenuItem.Name = "置底ToolStripMenuItem";
            this.置底ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.置底ToolStripMenuItem.Text = "置底";
            this.置底ToolStripMenuItem.Click += new System.EventHandler(this.置底ToolStripMenuItem_Click);
            // 
            // 上移ToolStripMenuItem
            // 
            this.上移ToolStripMenuItem.Name = "上移ToolStripMenuItem";
            this.上移ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.上移ToolStripMenuItem.Text = "上移";
            this.上移ToolStripMenuItem.Click += new System.EventHandler(this.上移ToolStripMenuItem_Click);
            // 
            // 下移ToolStripMenuItem
            // 
            this.下移ToolStripMenuItem.Name = "下移ToolStripMenuItem";
            this.下移ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.下移ToolStripMenuItem.Text = "下移";
            this.下移ToolStripMenuItem.Click += new System.EventHandler(this.下移ToolStripMenuItem_Click);
            // 
            // 打开属性表
            // 
            this.打开属性表.Name = "打开属性表";
            this.打开属性表.Size = new System.Drawing.Size(136, 22);
            this.打开属性表.Click += new System.EventHandler(this.打开属性表_Click);
            // 
            // 缩放至图层ToolStripMenuItem
            // 
            this.缩放至图层ToolStripMenuItem.Name = "缩放至图层ToolStripMenuItem";
            this.缩放至图层ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.缩放至图层ToolStripMenuItem.Text = "缩放至图层";
            this.缩放至图层ToolStripMenuItem.Click += new System.EventHandler(this.缩放至图层ToolStripMenuItem_Click);
            // 
            // 渲染
            // 
            this.渲染.Name = "渲染";
            this.渲染.Size = new System.Drawing.Size(136, 22);
            this.渲染.Text = "渲染";
            this.渲染.Click += new System.EventHandler(this.渲染ToolStripMenuItem_Click);
            // 
            // 移除ToolStripMenuItem
            // 
            this.移除ToolStripMenuItem.Name = "移除ToolStripMenuItem";
            this.移除ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.移除ToolStripMenuItem.Text = "移除";
            this.移除ToolStripMenuItem.Click += new System.EventHandler(this.移除ToolStripMenuItem_Click);
            // 
            // 编辑ToolStripMenuItem
            // 
            this.编辑ToolStripMenuItem.Name = "编辑ToolStripMenuItem";
            this.编辑ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.编辑ToolStripMenuItem.Text = "编辑要素";
            // 
            // 另存为
            // 
            this.另存为.Name = "另存为";

            // 
            // CreateFeatureRightMenu
            // 
            this.CreateFeatureRightMenu.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.CreateFeatureRightMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.完成部件ToolStripMenuItem,
            this.完成绘制ToolStripMenuItem});
            this.CreateFeatureRightMenu.Name = "CreateFeatureRightMenu";
            this.CreateFeatureRightMenu.Size = new System.Drawing.Size(185, 80);
            // 
            // 完成部件ToolStripMenuItem
            // 
            this.完成部件ToolStripMenuItem.Name = "完成部件ToolStripMenuItem";
            this.完成部件ToolStripMenuItem.Size = new System.Drawing.Size(184, 38);
            this.完成部件ToolStripMenuItem.Text = "完成部件";
            this.完成部件ToolStripMenuItem.Click += new System.EventHandler(this.完成部件ToolStripMenuItem_Click);
            // 
            // 完成绘制ToolStripMenuItem
            // 
            this.完成绘制ToolStripMenuItem.Name = "完成绘制ToolStripMenuItem";
            this.完成绘制ToolStripMenuItem.Size = new System.Drawing.Size(184, 38);
            this.完成绘制ToolStripMenuItem.Text = "完成绘制";
            this.完成绘制ToolStripMenuItem.Click += new System.EventHandler(this.完成绘制ToolStripMenuItem_Click);
            // 
            this.另存为.Size = new System.Drawing.Size(136, 22);
            this.另存为.Text = "另存为";
            this.另存为.Click += new System.EventHandler(this.另存为_Click);
            // 
            // moMapControl1
            // 
            this.moMapControl1.BackColor = System.Drawing.Color.White;
            this.moMapControl1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.moMapControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.moMapControl1.FlashColor = System.Drawing.Color.Green;
            this.moMapControl1.Layers = moLayers1;
            this.moMapControl1.Location = new System.Drawing.Point(314, 123);
            this.moMapControl1.Margin = new System.Windows.Forms.Padding(4);
            this.moMapControl1.Name = "moMapControl1";
            this.moMapControl1.SelectionColor = System.Drawing.Color.Cyan;
            this.moMapControl1.Size = new System.Drawing.Size(1178, 868);
            this.moMapControl1.TabIndex = 6;
            this.moMapControl1.MapScaleChanged += new MyMapObjects.moMapControl.MapScaleChangeHandle(this.moMapControl1_MapScaleChanged);
            this.moMapControl1.AfterTrackingLayerDraw += new MyMapObjects.moMapControl.AfterTrackingLayerDrawHandle(this.moMapControl1_AfterTrackingLayerDraw);
            this.moMapControl1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.moMapControl1_MouseClick);
            this.moMapControl1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.moMapControl1_MouseDoubleClick);
            this.moMapControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.moMapControl1_MouseDown);
            this.moMapControl1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.moMapControl1_MouseMove);
            this.moMapControl1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.moMapControl1_MouseUp);
            // 
            // DelFeatureRightMenu
            // 
            this.DelFeatureRightMenu.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.DelFeatureRightMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.删除ToolStripMenuItem});
            this.DelFeatureRightMenu.Name = "DelFeatureRightMenu";
            this.DelFeatureRightMenu.Size = new System.Drawing.Size(137, 42);
            // 
            // 删除ToolStripMenuItem
            // 
            this.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
            this.删除ToolStripMenuItem.Size = new System.Drawing.Size(136, 38);
            this.删除ToolStripMenuItem.Text = "删除";
            this.删除ToolStripMenuItem.Click += new System.EventHandler(this.删除ToolStripMenuItem_Click);
            // 

            // MapCraftForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1492, 1060);
            this.Controls.Add(this.moMapControl1);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.cbxProjectionCS);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tStripFeatureEditor);
            this.Controls.Add(this.tStripMapOperator);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MapCraftForm";
            this.Text = "MapCraft";
            this.Load += new System.EventHandler(this.MapCraftForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tStripMapOperator.ResumeLayout(false);
            this.tStripMapOperator.PerformLayout();
            this.tStripFeatureEditor.ResumeLayout(false);
            this.tStripFeatureEditor.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.LayerRightMenu.ResumeLayout(false);
            this.CreateFeatureRightMenu.ResumeLayout(false);
            this.DelFeatureRightMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 新建地图ToolStripMenuItem;
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
        private System.Windows.Forms.ToolStripButton btnMoveFeature;
        private System.Windows.Forms.ToolStripButton btnCreateFeature;
        private System.Windows.Forms.ToolStripButton btnMoveNode;
        private System.Windows.Forms.ToolStripButton btnAddNode;
        private System.Windows.Forms.ToolStripButton btnDeleteNode;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripSplitButton MapScaleButton;
        private System.Windows.Forms.ToolStripStatusLabel coordinateStatusLabel;
        private System.Windows.Forms.CheckBox cbxProjectionCS;
        private System.Windows.Forms.TreeView treeView1;
        internal MyMapObjects.moMapControl moMapControl1;
        private System.Windows.Forms.ContextMenuStrip LayerRightMenu;
        private System.Windows.Forms.ToolStripMenuItem 另存为;
        private System.Windows.Forms.ToolStripMenuItem 打开属性表;
        private System.Windows.Forms.ToolStripMenuItem 渲染;
        private System.Windows.Forms.ToolStripMenuItem 新建图层ToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip CreateFeatureRightMenu;
        private System.Windows.Forms.ToolStripMenuItem 完成部件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 完成绘制ToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip DelFeatureRightMenu;
        private System.Windows.Forms.ToolStripMenuItem 删除ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 移动图层ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 置顶ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 置底ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 上移ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 下移ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 缩放至图层ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 移除ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 编辑ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 导出ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1000;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem10000000;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3000000;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1000000;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem500000;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem100000;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem10000;
    }
}

