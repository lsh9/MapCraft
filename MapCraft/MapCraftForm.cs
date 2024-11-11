﻿using MapCraft.FileProcessor;
using MapCraft.Forms;
using MapCraft.IO;
using MyMapObjects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MapCraft
{
    public partial class MapCraftForm : Form
    {
        #region 字段
        // 选项变量
        private Color mZoomBoxColor = Color.DeepPink;       // 缩放盒颜色
        private double mZoomBoxWidth = 0.53;                // 缩放盒的边界宽度，单位毫米
        private Color mSelectBoxColor = Color.DarkGreen;    // 选择盒颜色
        private double mSelectBoxWidth = 0.53;              // 选择盒的边界宽度，单位毫米
        private double mZoomRatioFixed = 2;                 // 固定放大系数
        private double mZoomRatioMouseWheel = 1.2;          // 滑轮放大系数
        private double mSelectingTolerance = 5;             // 选择容限，像素
        private moSimpleFillSymbol mSelectingBoxSymbol;     // 选择盒符号
        private moSimpleFillSymbol mZoomBoxSymbol;          // 缩放盒符号
        private moSimpleFillSymbol mMovingPolygonSymbol;    // 正在移动的多边形的符号
        private moSimpleFillSymbol mEditingPolygonSymbol;   // 正在编辑的多边形的符号
        private moSimpleMarkerSymbol mEditingVertexSymbol;  // 正在编辑的图形的顶点的符号
        private moSimpleLineSymbol mEditingPolylineSymbol;  // 正在编辑的线符号
        private moSimpleLineSymbol mElasticSymbol;          // 橡皮筋符号
        private bool mShowLngLat = false;                   // 是否显示经纬度

        private int SelectedLayerIndex = -1;  //选中的图层索引
        private int mEditingLayerIndex = -1;   //编辑的图层索引
        private List<ShapeFileParser> mShapefiles = new List<ShapeFileParser>();// 图层路径记录
        private List<AttributeTable> AttributeTables = new List<AttributeTable>();
        private static int AttributeTableIndex;
        private string mProjectPath = "";  // 项目路径
        private IdentifyForm mIdentifyForm = null;

        // 与地图操作有关的变量
        private MapOpConstant mMapOpStyle = 0;  // 地图操作方式
        private PointF mStartMouseLocation;     // 鼠标按下时的位置
        private bool mIsInZoom = false;         // 是否在缩放中
        private bool mIsInPan = false;          // 是否在漫游中
        private bool mIsInSelect = false;       // 是否在选择中
        private bool mIsInIdentify = false;     // 是否在查询中
        //private bool mIsInMovingShapes = false; // 是否在移动图形中
        private bool mIsMouseDownOnSelectedFeature = false;//是否在选择要素内
        private bool mIsSelectedFeatureMoved = false;
        private bool mIsLeftMousePressed = false;
        private bool mIsLayerChanged = false;   //是否对图层修改
        private bool mIsNodeChanged = false;    //是否对节点修改
        private bool mIsMouseDownNearNode = false;  //是否接近结点
        private List<moGeometry> mMovingGeometries = new List<moGeometry>(); // 正在移动的图形集合
        private moGeometry mEditingGeometry;   // 正在编辑的图形
        private List<moPoint> mSketchingPoint;    //正在描绘的点
        private List<moPoints> mSketchingShape;   // 正在描绘的图形，用多点集合存储
        private int mMouseOnPartIndex = -1;
        private int mMouseOnPointIndex = -1;


        #endregion

        #region 属性
        /// <summary>
        /// 地图控件，子窗体可使用
        /// </summary>
        //public moMapControl MapControl
        //{
        //    get { return MapControl; }
        //}

        /// <summary>
        /// 正在编辑的图层要素类型
        /// </summary>
        public moGeometryTypeConstant EditingLayerShape
        {
            get
            {
                if (mEditingLayerIndex == -1)
                    return moGeometryTypeConstant.None;
                else
                    return MapControl.Layers.GetItem(mEditingLayerIndex).ShapeType;
            }
        }

        public List<ShapeFileParser> Shapefiles
        {
            get { return mShapefiles; }
        }

        #endregion

        #region 构造函数
        public MapCraftForm()
        {
            InitializeComponent();
            MapControl.MouseWheel += moMapControl1_MouseWheel;
        }
        #endregion

        #region 窗体与控件的事件处理
        // 窗体加载事件
        private void MapCraftForm_Load(object sender, EventArgs e)
        {
            // （1）初始化符号
            InitializeSymbols();
            // （2）初始化描绘图形
            InitializeSketchingShape();
            // （3）描绘图形比例尺
            ShowMapScale();
        }

        #region 菜单栏控件事件

        // 点击新建地图菜单项
        private void 新建地图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 如果至少有一个图层,提示保存
            if (MapControl.Layers.Count != 0)
            {
                DialogResult result = MessageBox.Show("是否保存当前地图？", "提示", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    保存ToolStripMenuItem_Click(sender, e);
                }
                else if (result == DialogResult.Cancel)
                {
                    return;
                }
                // 关闭所有shp
                foreach (ShapeFileParser shapefile in mShapefiles)
                {
                    //shapefile.Close();
                }
            }

            mProjectPath = "";
            ReLoad();
        }

        // 点击新建图层菜单项
        private void 新建图层ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateLayerForm createLayerForm = new CreateLayerForm(this);
            createLayerForm.Show();
        }

        // 点击打开地图菜单项
        private void 打开地图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 提示保存
            if (MapControl.Layers.Count != 0)
            {
                DialogResult result = MessageBox.Show("是否保存当前地图？", "提示", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    保存ToolStripMenuItem_Click(sender, e);
                }
                else if (result == DialogResult.Cancel)
                {
                    return;
                }
            }
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "打开文件";
            openFileDialog.Filter = "MapCraft文件(*.mc)|*.mc";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;
            openFileDialog.ShowDialog();
            if (openFileDialog.FileName == string.Empty)
            {
                return;
            }
            OpenProject(openFileDialog.FileName);
        }

        // 点击保存（地图）菜单项
        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mProjectPath == string.Empty)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Title = "保存文件",
                    Filter = "MapCraft文件(*.mc)|*.mc",
                    FilterIndex = 1,
                    RestoreDirectory = true
                };
                saveFileDialog.ShowDialog();
                if (saveFileDialog.FileName == string.Empty)
                {
                    return;
                }
                mProjectPath = saveFileDialog.FileName;
            }
            SaveProject(mProjectPath);
        }

        // 点击另存为（地图）菜单项
        private void 另存为ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Title = "另存为文件",
                Filter = "MapCraft文件(*.mc)|*.mc",
                FilterIndex = 1,
                RestoreDirectory = true,
            };
            saveFileDialog.ShowDialog();
            if (saveFileDialog.FileName == string.Empty)
            {
                return;
            }
            SaveProject(saveFileDialog.FileName);
        }

        // 点击导出（导出为图片）菜单项
        private void 导出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 将MapControl的BufferMap1导出为图片
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Title = "导出为图片",
                Filter = "BMP文件(*.bmp)|*.bmp|JPG文件(*.jpg)|*.jpg|PNG文件(*.png)|*.png",
                FilterIndex = 1,
                RestoreDirectory = true
            };
            saveFileDialog.ShowDialog();
            if (saveFileDialog.FileName == string.Empty)
            {
                return;
            }
            string fileName = saveFileDialog.FileName;
            MapControl.SaveImage(fileName);
        }

        private void 按属性选择ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnSelectByAttribute_Click(sender, e);
        }

        private void 按位置选择ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnSelectByLocation_Click(sender, e);
        }

        #endregion


        /// <summary>
        /// 显示经纬度控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChkShowLngLat_CheckedChanged(object sender, EventArgs e)
        {
            mShowLngLat = !cbxProjectionCS.Checked;
        }

        #region 图层TreeView控件操作
        private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            SelectedLayerIndex = e.Node.Index;
        }


        private void TreeView1_DragDrop(object sender, DragEventArgs e)
        {
            if (treeViewLayers.Nodes.Count <= 1)
            {
                return;
            }
            TreeNode moveNode = (TreeNode)e.Data.GetData("System.Windows.Forms.TreeNode");
            //根据鼠标坐标确定要移动到的目标节点
            int fromIndex = moveNode.Index;
            int toIndex = 0;
            Point pt = ((TreeView)(sender)).PointToClient(new Point(e.X, e.Y));
            int sNodesNum = treeViewLayers.Nodes.Count;
            Rectangle sFirstRect = treeViewLayers.Nodes[0].Bounds;
            Rectangle sLastRect = treeViewLayers.Nodes[sNodesNum - 1].Bounds;
            if (pt.Y <= sFirstRect.Y)
            {
                toIndex = 0;
            }
            else if (pt.Y >= (sLastRect.Y + sLastRect.Height))
            {
                toIndex = sNodesNum - 1;
            }
            else
            {
                for (int i = 0; i < sNodesNum; ++i)
                {
                    Rectangle sCurRect = treeViewLayers.Nodes[i].Bounds;
                    if ((pt.Y > sCurRect.Y) && (pt.Y <= (sCurRect.Y + sCurRect.Height)))
                    {
                        toIndex = i;
                        break;
                    }
                }
            }
            MoveLayer(fromIndex, toIndex);
            RefreshLayersTree();    //刷新图层列表
            MapControl.RedrawMap();  //刷新地图
        }

        private void TreeView1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.Data.GetDataPresent("System.Windows.Forms.TreeNode") ? DragDropEffects.Move : DragDropEffects.None;
        }

        private void TreeView1_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                DoDragDrop(e.Item, DragDropEffects.Move);
            }
        }

        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            int sIndex = e.Node.Index;
            moMapLayer sMapLayer = MapControl.Layers.GetItem(sIndex);
            sMapLayer.Visible = e.Node.Checked;
            MapControl.RedrawMap();
        }

        private void 另存为_Click(object sender, EventArgs e)
        {
            string shpFilePath;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "保存文件";
            saveFileDialog.Filter = "ShapeFile文件(*.shp)|*.shp|所有文件(*.*)|*.*";
            saveFileDialog.FilterIndex = 1;

            saveFileDialog.RestoreDirectory = true;


            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                shpFilePath = saveFileDialog.FileName;
                saveFileDialog.Dispose();
            }
            else
            {
                saveFileDialog.Dispose();
                return;
            }
            try
            {
                string layerPath = shpFilePath.Substring(0, shpFilePath.IndexOf(".shp", StringComparison.Ordinal));
                // mShapefiles[SelectedLayerIndex].Write_ShapeFile(layerPath);    // 原始代码
                // 下面是新添加的代码
                var selectedLayer = MapControl.Layers.GetItem(SelectedLayerIndex);
                // var outputPath = Shapefiles[SelectedLayerIndex].FilePath;
                ShapefileWriter.write(selectedLayer, layerPath);
            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString());

            }
        }

        private void 保存到数据库ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShapeFileParser curShpFileParser = mShapefiles[SelectedLayerIndex];
            if (curShpFileParser.isDBLayer == false)
            {
                // 原来的代码
                //MessageBox.Show("不是数据库图层，暂未实现[保存到数据库]功能！");
                //return;

                // 新添加的代码
                AddDataFromDB addDataToDB = new AddDataFromDB(true, MapControl.Layers.GetItem(SelectedLayerIndex));
                addDataToDB.ShowDialog();
            }
            else
            {
                ConnDBParser connDBParser = curShpFileParser.connDBParser;
                moFeatures curFeatures = MapControl.Layers.GetItem(SelectedLayerIndex).Features;
                connDBParser.Write_DB(curFeatures);
            }
        }

        private void 打开属性表_Click(object sender, EventArgs e)
        {
            AttributeTable attributeTable = new AttributeTable(this, SelectedLayerIndex);
            attributeTable.Owner = this;
            attributeTable.Name = MapControl.Layers.GetItem(SelectedLayerIndex).Name;
            attributeTable.Show();
            attributeTable.SetDesktopLocation(Location.X + (Width - attributeTable.Width) / 2,
                Location.Y + (Height - attributeTable.Height) / 2);
            attributeTable.RefreshDataFormByMainForm();
            AttributeTables.Add(attributeTable);//将新打开的添加进去
            attributeTable.FormIndex = AttributeTableIndex;
            AttributeTableIndex++;
        }

        private void 渲染ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayerDetailForm layerDetailForm = new LayerDetailForm(this, SelectedLayerIndex);
            layerDetailForm.ShowRenderer();
        }

        private void 注记ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayerDetailForm layerDetailForm = new LayerDetailForm(this, SelectedLayerIndex);
            layerDetailForm.ShowLabelRenderer();
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            foreach (TreeNode n in treeViewLayers.Nodes)
            {
                n.BackColor = Color.Empty;
            }
            SelectedLayerIndex = e.Node.Index;
            e.Node.BackColor = Color.LightGray;
        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            // 如果是图层节点被点击
            if (true)
            {
                LayerDetailForm layerDetailForm = new LayerDetailForm(this, SelectedLayerIndex);
                layerDetailForm.Show();
            }
        }
        #endregion

        #region 按钮控件点击事件
        // 某个操作按钮被点击
        private void tStripMapOperator_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            // 清除所有按钮的选中状态
            foreach (ToolStripItem sItem in tStripMapOperator.Items)
            {
                if (sItem.GetType() == typeof(ToolStripButton))
                {
                    ToolStripButton button = (ToolStripButton)sItem;
                    button.BackColor = SystemColors.Control;
                }
            }
            // 选中当前按钮
            ToolStripButton sButton = (ToolStripButton)e.ClickedItem;
            //// 使用按钮图标修改鼠标图标,如果按钮是放大\缩小\平移\按位置选择\识别
            //if (sButton.Name == "btnZoomIn" || sButton.Name == "btnZoomOut" || sButton.Name == "btnSelectByLocation" || sButton.Name == "btnIdentify" || sButton.Name == "btnPan")
            //{
            //    Bitmap sBitmap = (Bitmap)sButton.Image;
            //    IntPtr sCursorHandle = sBitmap.GetHicon();
            //    Cursor sCursor = new Cursor(sCursorHandle);
            //    moMapControl1.Cursor = sCursor;
            //}
            //else
            //{
            //    moMapControl1.Cursor = Cursors.Default;
            //}
            sButton.BackColor = Color.LightBlue;
        }

        // 点击添加图层按钮
        private void btnAddData_Click(object sender, EventArgs e)
        {
            string shpFilePath;
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = @"ShapeFile 文件|*.shp";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                shpFilePath = fileDialog.FileName;
                fileDialog.Dispose();
            }
            else
            {
                btnAddData.BackColor = SystemColors.Control;
                mMapOpStyle = MapOpConstant.None;
                fileDialog.Dispose();
                return;
            }

            try
            {
                string layerName = Path.GetFileNameWithoutExtension(shpFilePath);
                string layerPath = Path.Combine(Path.GetDirectoryName(shpFilePath), layerName);
                ShapeFileParser fileProcessor = new ShapeFileParser(layerPath);
                AddLayer(fileProcessor);
            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString());

            }
            btnAddData.BackColor = SystemColors.Control;
            mMapOpStyle = MapOpConstant.None;
        }

        // 点击从数据库添加图层按钮
        private void btnAddDataFromDB_Click(object sender, EventArgs e)
        {
            AddDataFromDB adfdb = new AddDataFromDB(false, null);
            ConnDBParser connDBParser;
            if (adfdb.ShowDialog() == DialogResult.OK)
            {
                connDBParser = adfdb.connDBParserRtn;
                AddLayerFromDB(connDBParser);
                MessageBox.Show("添加成功！");
            }
            else
            {
                MessageBox.Show("取消从数据库添加图层！");
            }
        }

        // 点击放大按钮
        private void btnZoomIn_Click(object sender, EventArgs e)
        {
            mMapOpStyle = MapOpConstant.ZoomIn;
        }

        // 点击缩小按钮
        private void btnZoomOut_Click(object sender, EventArgs e)
        {
            mMapOpStyle = MapOpConstant.ZoomOut;
        }

        // 点击漫游按钮
        private void btnPan_Click(object sender, EventArgs e)
        {
            mMapOpStyle = MapOpConstant.Pan;
        }

        // 点击缩放至全图按钮
        private void btnFullExtent_Click(object sender, EventArgs e)
        {
            MapControl.FullExtent();
            btnFullExtent.BackColor = SystemColors.Control;
            mMapOpStyle = MapOpConstant.None;
        }

        // 点击固定比例放大按钮
        private void btnFixedZoomIn_Click(object sender, EventArgs e)
        {
            //计算地图空间中心点的地图坐标
            double sY = MapControl.ClientRectangle.Width / 2;
            double sX = MapControl.ClientRectangle.Height / 2;
            moPoint sPoint = MapControl.ToMapPoint(sX, sY);
            MapControl.ZoomByCenter(sPoint, mZoomRatioFixed);
            btnFixedZoomIn.BackColor = SystemColors.Control;
            mMapOpStyle = MapOpConstant.None;
        }

        // 点击固定比例缩小按钮
        private void btnFixedZoomOut_Click(object sender, EventArgs e)
        {
            //计算地图空间中心点的地图坐标
            double sY = MapControl.ClientRectangle.Width / 2;
            double sX = MapControl.ClientRectangle.Height / 2;
            Console.WriteLine(sX + " " + sY);
            moPoint sPoint = MapControl.ToMapPoint(sX, sY);
            Console.WriteLine(sPoint.X + " " + sPoint.Y);
            MapControl.ZoomByCenter(sPoint, 1 / mZoomRatioFixed);
            btnFixedZoomOut.BackColor = SystemColors.Control;
            mMapOpStyle = MapOpConstant.None;
        }

        // 点击按位置选择按钮
        private void btnSelectByLocation_Click(object sender, EventArgs e)
        {
            mMapOpStyle = MapOpConstant.SelectByLocation;
        }

        // 点击按属性选择按钮
        private void btnSelectByAttribute_Click(object sender, EventArgs e)
        {

            mMapOpStyle = MapOpConstant.SelectByAttribute;
            SelectByAttributeForm sSelectByAttributeForm = new SelectByAttributeForm(this);
            sSelectByAttributeForm.Show();
            btnSelectByAttribute.BackColor = SystemColors.Control;
            mMapOpStyle = MapOpConstant.None;
        }

        // 点击清除选择按钮
        private void btnClearSelection_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < MapControl.Layers.Count; i++)
            {
                moMapLayer sLayer = MapControl.Layers.GetItem(i);
                sLayer.SelectedFeatures.Clear();
            }
            MapControl.RedrawMap();
            btnClearSelection.BackColor = SystemColors.Control;
            mMapOpStyle = MapOpConstant.None;
        }

        // 点击查询按钮
        private void btnIdentify_Click(object sender, EventArgs e)
        {
            mMapOpStyle = MapOpConstant.Identify;
        }

        #endregion

        #region 编辑器控件事件

        /// <summary>
        /// 点击编辑器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ddbtnEditor_Click(object sender, EventArgs e)
        {
            if (SelectedLayerIndex != -1)
                开始编辑ToolStripMenuItem.Enabled = true;
        }

        /// <summary>
        /// 点击开始编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 开始编辑ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mEditingLayerIndex = SelectedLayerIndex;
            开始编辑ToolStripMenuItem.Enabled = false;
            结束编辑ToolStripMenuItem.Enabled = true;
            保存编辑内容ToolStripMenuItem.Enabled = true;
            btnMoveFeature.Enabled = true;
            btnCreateFeature.Enabled = true;
            btnMoveNode.Enabled = true;
            if (EditingLayerShape != moGeometryTypeConstant.Point)
            {
                btnAddNode.Enabled = true;
                btnDeleteNode.Enabled = true;
            }
            btnMoveFeature_Click(sender, e);
        }

        /// <summary>
        /// 点击结束编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 结束编辑ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveNodeEdit();
            if (mIsLayerChanged)
            {
                DialogResult dr = MessageBox.Show("是否保存编辑内容？", "保存", MessageBoxButtons.YesNoCancel);
                // DialogResult dr = DialogResult.No;
                if (dr == DialogResult.Yes)
                {
                    保存编辑内容ToolStripMenuItem_Click(sender, e);
                }
                else if (dr == DialogResult.No)
                {
                    //根据路径重读此文件
                    MapControl.RedrawMap();
                }
                else
                {
                    return;
                }
            }
            开始编辑ToolStripMenuItem.Enabled = true;
            结束编辑ToolStripMenuItem.Enabled = false;
            保存编辑内容ToolStripMenuItem.Enabled = false;
            mEditingLayerIndex = -1;
            btnMoveFeature.Enabled = false;
            btnCreateFeature.Enabled = false;
            btnMoveNode.Enabled = false;
            btnMoveNode.Enabled = false;
            btnAddNode.Enabled = false;
            btnDeleteNode.Enabled = false;
            FinishEditLayer();
            //清除选择要素
            for (int i = 0; i < MapControl.Layers.Count; i++)
            {
                MapControl.Layers.GetItem(i).SelectedFeatures.Clear();
            }
            MapControl.RedrawMap();
        }

        /// <summary>
        /// 点击保存编辑内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 保存编辑内容ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShapeFileParser shapefile = Shapefiles[mEditingLayerIndex];
            shapefile.Write_ShapeFile(shapefile.FilePath);
            保存编辑内容ToolStripMenuItem.Enabled = false;
            return;
        }

        /// <summary>
        /// 点击移动要素
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMoveFeature_Click(object sender, EventArgs e)
        {
            SaveNodeEdit();
            mMapOpStyle = MapOpConstant.MoveFeature;
            btnMoveFeature.Checked = true;
            btnCreateFeature.Checked = false;
            btnMoveNode.Checked = false;
            btnAddNode.Checked = false;
            btnDeleteNode.Checked = false;
            MapControl.ContextMenuStrip = DelFeatureRightMenu;
        }

        /// <summary>
        /// 点击创建要素
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreateFeature_Click(object sender, EventArgs e)
        {
            SaveNodeEdit();
            btnMoveFeature.Checked = false;
            btnCreateFeature.Checked = true;
            btnMoveNode.Checked = false;
            btnAddNode.Checked = false;
            btnDeleteNode.Checked = false;
            mMapOpStyle = MapOpConstant.CreateFeature;
            MapControl.ContextMenuStrip = CreateFeatureRightMenu;
        }

        /// <summary>
        /// 点击删除节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMoveNode_Click(object sender, EventArgs e)
        {
            if (!GotEditGeometry())
                return;
            btnMoveFeature.Checked = false;
            btnCreateFeature.Checked = false;
            btnMoveNode.Checked = true;
            btnAddNode.Checked = false;
            btnDeleteNode.Checked = false;
            mMapOpStyle = MapOpConstant.MoveFeature;
            MapControl.ContextMenuStrip = null;
        }

        /// <summary>
        /// 点击添加节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddNode_Click(object sender, EventArgs e)
        {
            if (!GotEditGeometry())
                return;
            btnMoveFeature.Checked = false;
            btnCreateFeature.Checked = false;
            btnMoveNode.Checked = false;
            btnAddNode.Checked = true;
            btnDeleteNode.Checked = false;
            mMapOpStyle = MapOpConstant.AddNode;
            MapControl.ContextMenuStrip = null;
        }

        /// <summary>
        /// 点击删除节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteNode_Click(object sender, EventArgs e)
        {
            if (!GotEditGeometry())
                return;
            btnMoveFeature.Checked = false;
            btnCreateFeature.Checked = false;
            btnMoveNode.Checked = false;
            btnAddNode.Checked = false;
            btnDeleteNode.Checked = true;
            mMapOpStyle = MapOpConstant.DeleteNode;
            MapControl.ContextMenuStrip = null;
        }
        #endregion

        #region MapControl控件事件


        #region MapControl鼠标按下事件

        // 鼠标在MapControl中被按下
        private void moMapControl1_MouseDown(object sender, MouseEventArgs e)
        {
            switch (mMapOpStyle)
            {
                case MapOpConstant.ZoomIn:
                    OnZoom_MouseDown(e);
                    break;
                case MapOpConstant.ZoomOut:
                    OnZoom_MouseDown(e);
                    break;
                case MapOpConstant.Pan:
                    OnPan_MouseDown(e);
                    break;
                case MapOpConstant.SelectByLocation:
                    OnSelectByLocation_MouseDown(e);
                    break;
                case MapOpConstant.Identify:
                    OnIdentify_MouseDown(e);
                    break;
                case MapOpConstant.MoveFeature:
                    OnMoveFeature_MouseDown(e);
                    break;
                case MapOpConstant.MoveNode:
                    OnMoveNode_MouseDown(e);
                    break;
                default:
                    break;
            }
        }

        // 缩放操作-鼠标按下
        private void OnZoom_MouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mStartMouseLocation = e.Location;
                mIsInZoom = true;
            }
        }

        //漫游操作-鼠标按下
        private void OnPan_MouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mStartMouseLocation = e.Location;
                mIsInPan = true;
            }
        }

        //按位置选择操作-鼠标按下
        private void OnSelectByLocation_MouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mStartMouseLocation = e.Location;
                mIsInSelect = true;
            }
        }

        //查询操作-鼠标按下
        private void OnIdentify_MouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mStartMouseLocation = e.Location;
                mIsInIdentify = true;
            }
        }

        //移动要素-鼠标按下
        private void OnMoveFeature_MouseDown(MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;
            //if (mEditingLayerIndex == -1)
            //    return;
            mStartMouseLocation = e.Location;
            mIsLeftMousePressed = true;
            // 选择是进行选择，还是进行移动
            mIsMouseDownOnSelectedFeature = false;
            moPoint mousePoint = MapControl.ToMapPoint(e.Location.X, e.Location.Y);
            moMapLayer sLayer = MapControl.Layers.GetItem(mEditingLayerIndex);
            moFeatures editedSelectedFeatures = sLayer.SelectedFeatures;
            double mapTolerance = MapControl.ToMapDistance(mSelectingTolerance);

            switch (EditingLayerShape)
            {
                case moGeometryTypeConstant.Point:
                    {
                        for (int i = 0; i < editedSelectedFeatures.Count; i++)
                        {
                            moPoint sPoint = (moPoint)editedSelectedFeatures.GetItem(i).Geometry;
                            if (!moMapTools.IsPointOnPoint(mousePoint, sPoint, mapTolerance)) continue;
                            mIsMouseDownOnSelectedFeature = true;
                            break;
                        }
                        break;
                    }
                case moGeometryTypeConstant.MultiPolyline:
                    {
                        for (int i = 0; i < editedSelectedFeatures.Count; i++)
                        {
                            moMultiPolyline sMultiPolygon = (moMultiPolyline)editedSelectedFeatures.GetItem(i).Geometry;
                            if (!moMapTools.IsPointOnMultiPolyline(mousePoint, sMultiPolygon, mapTolerance)) continue;
                            mIsMouseDownOnSelectedFeature = true;
                            break;
                        }
                        break;
                    }
                case moGeometryTypeConstant.MultiPolygon:
                    {
                        for (int i = 0; i < editedSelectedFeatures.Count; i++)
                        {
                            moMultiPolygon sMultiPolygon = (moMultiPolygon)editedSelectedFeatures.GetItem(i).Geometry;
                            if (!moMapTools.IsPointWithinMultiPolygon(mousePoint, sMultiPolygon)) continue;
                            mIsMouseDownOnSelectedFeature = true;
                        }
                        break;
                    }
            }
            if (mIsMouseDownOnSelectedFeature)
            {
                GetMovingGeometries();
            }
        }

        //获取正在移动的图形
        private void GetMovingGeometries()
        {
            mMovingGeometries.Clear();
            moMapLayer sLayer = MapControl.Layers.GetItem(mEditingLayerIndex);
            moFeatures editedSelectedFeatures = sLayer.SelectedFeatures;
            int sSelFeatureCount = editedSelectedFeatures.Count;

            switch (EditingLayerShape)
            {
                case moGeometryTypeConstant.Point:
                    {
                        for (int i = 0; i < sSelFeatureCount; ++i)
                        {
                            moPoint point = (moPoint)editedSelectedFeatures.GetItem(i).Geometry;
                            mMovingGeometries.Add(point);
                        }
                        break;
                    }

                case moGeometryTypeConstant.MultiPolyline:
                    {
                        for (int i = 0; i < sSelFeatureCount; ++i)
                        {
                            moMultiPolyline multiPolyline = (moMultiPolyline)editedSelectedFeatures.GetItem(i).Geometry;
                            mMovingGeometries.Add(multiPolyline);
                        }
                        break;
                    }

                case moGeometryTypeConstant.MultiPolygon:
                    {
                        for (int i = 0; i < sSelFeatureCount; ++i)
                        {
                            moMultiPolygon multiPolygon = (moMultiPolygon)editedSelectedFeatures.GetItem(i).Geometry;
                            mMovingGeometries.Add(multiPolygon);
                        }
                        break;
                    }
            }
        }

        //移动结点-鼠标按下
        private void OnMoveNode_MouseDown(MouseEventArgs e)
        {
            if (mMouseOnPartIndex == -1 || mMouseOnPointIndex == -1)
                return;
            mIsMouseDownNearNode = true;
        }
        #endregion

        #region MapControl鼠标移动事件
        // 鼠标在MapControl中移动
        private void moMapControl1_MouseMove(object sender, MouseEventArgs e)
        {
            ShowCoordinates(e.Location);
            switch (mMapOpStyle)
            {
                case MapOpConstant.ZoomIn:
                    OnZoom_MouseMove(e);
                    break;
                case MapOpConstant.ZoomOut:
                    OnZoom_MouseMove(e);
                    break;
                case MapOpConstant.Pan:
                    OnPan_MouseMove(e);
                    break;
                case MapOpConstant.SelectByLocation:
                    OnSelectByLocation_MouseMove(e);
                    break;
                case MapOpConstant.Identify:
                    OnIdentify_MouseMove(e);
                    break;
                case MapOpConstant.MoveFeature:
                    OnMoveFeature_MouseMove(e);
                    break;
                case MapOpConstant.CreateFeature:
                    OnCreateFeature_MouseMove(e);
                    break;
                case MapOpConstant.MoveNode:
                    OnMoveNode_MouseMove(e);
                    break;
                case MapOpConstant.AddNode:
                    OnAddNode_MouseMove(e);
                    break;
                case MapOpConstant.DeleteNode:
                    OnDeleteNode_MouseMove(e);
                    break;
                default:
                    break;
            }
        }

        //缩放操作-鼠标移动
        private void OnZoom_MouseMove(MouseEventArgs e)
        {
            if (mIsInZoom == false)
                return;
            MapControl.Refresh();
            moRectangle sRect = GetMapRectByTwoPoints(mStartMouseLocation, e.Location);
            moUserDrawingTool sDrawingtool = MapControl.GetDrawingTool();
            sDrawingtool.DrawRectangle(sRect, mZoomBoxSymbol);
        }

        //漫游操作-鼠标移动
        private void OnPan_MouseMove(MouseEventArgs e)
        {
            if (mIsInPan == false)
                return;
            MapControl.PanMapImageTo(e.Location.X - mStartMouseLocation.X, e.Location.Y - mStartMouseLocation.Y);
        }

        //按位置选择操作-鼠标移动
        private void OnSelectByLocation_MouseMove(MouseEventArgs e)
        {
            if (mIsInSelect == false)
                return;
            MapControl.Refresh();
            moRectangle sRect = GetMapRectByTwoPoints(mStartMouseLocation, e.Location);
            moUserDrawingTool sDrawingtool = MapControl.GetDrawingTool();
            sDrawingtool.DrawRectangle(sRect, mSelectingBoxSymbol);
        }

        //查询操作-鼠标移动
        private void OnIdentify_MouseMove(MouseEventArgs e)
        {
            if (mIsInIdentify == false) return;
            MapControl.Refresh();
            moRectangle sRect = GetMapRectByTwoPoints(mStartMouseLocation, e.Location);
            moUserDrawingTool sDrawingTool = MapControl.GetDrawingTool();
            sDrawingTool.DrawRectangle(sRect, mSelectingBoxSymbol);
        }

        //移动要素-鼠标移动
        private void OnMoveFeature_MouseMove(MouseEventArgs e)
        {
            if (mIsMouseDownOnSelectedFeature)
            {
                mIsSelectedFeatureMoved = true;
                mIsLayerChanged = true;
                //修改移动图形的坐标
                double deltaX = MapControl.ToMapDistance(e.Location.X - mStartMouseLocation.X);
                double deltaY = MapControl.ToMapDistance(mStartMouseLocation.Y - e.Location.Y);
                ModifyMovingGeometries(deltaX, deltaY);
                //刷新地图并绘制移动图形
                MapControl.Refresh();
                DrawMovingShapes();
                //重新设置鼠标位置
                mStartMouseLocation = e.Location;
            }
            else
            {
                OnSelectByLocation_MouseMove(e);
            }
        }

        //创建要素-鼠标移动
        private void OnCreateFeature_MouseMove(MouseEventArgs e)
        {
            //获取当前位置地图坐标
            moPoint mapPoint = MapControl.ToMapPoint(e.Location.X, e.Location.Y);
            switch (EditingLayerShape)
            {
                case moGeometryTypeConstant.MultiPolyline:
                    {
                        //获取最后一个部件
                        moPoints sLastPart = mSketchingShape.Last();
                        int sPointCount = sLastPart.Count;
                        //绘制橡皮筋
                        if (sPointCount == 0)
                        { }
                        else
                        {
                            MapControl.Refresh();
                            //只需绘制一条橡皮筋
                            moPoint sLastPoint = sLastPart.GetItem(sPointCount - 1);
                            moUserDrawingTool sDrawingTool = MapControl.GetDrawingTool();
                            sDrawingTool.DrawLine(sLastPoint, mapPoint, mElasticSymbol);
                        }
                        break;
                    }
                case moGeometryTypeConstant.MultiPolygon:
                    {
                        //获取最后一个部件
                        moPoints sLastPart = mSketchingShape.Last();
                        int sPointCount = sLastPart.Count;
                        //绘制橡皮筋
                        if (sPointCount == 0)
                        { }
                        else if (sPointCount == 1)
                        {
                            MapControl.Refresh();
                            //只有一个顶点，绘制一条橡皮筋
                            moPoint sFirstPoint = sLastPart.GetItem(0);
                            moUserDrawingTool sDrawingTool = MapControl.GetDrawingTool();
                            sDrawingTool.DrawLine(sFirstPoint, mapPoint, mElasticSymbol);
                        }
                        else
                        {
                            MapControl.Refresh();
                            //两个以上顶点，绘制两条橡皮筋
                            moPoint sFirstPoint = sLastPart.GetItem(0);
                            moPoint sLastPoint = sLastPart.GetItem(sPointCount - 1);
                            moUserDrawingTool sDrawingTool = MapControl.GetDrawingTool();
                            sDrawingTool.DrawLine(sFirstPoint, mapPoint, mElasticSymbol);
                            sDrawingTool.DrawLine(sLastPoint, mapPoint, mElasticSymbol);
                        }
                        break;
                    }
                default: break;
            }
        }

        //移动结点-鼠标移动
        private void OnMoveNode_MouseMove(MouseEventArgs e)
        {
            if (!mIsMouseDownNearNode)
            {
                SelectNodeToMove_MouseMove(e);
            }
            else
            {
                MoveSelectedNode_MouseMove(e);
            }
        }



        //添加结点-鼠标移动
        private void OnAddNode_MouseMove(MouseEventArgs e)
        {
            moPoint sPoint = MapControl.ToMapPoint(e.Location.X, e.Location.Y);
            double mapTolerance = MapControl.ToMapDistance(mSelectingTolerance);

            switch (EditingLayerShape)
            {
                case moGeometryTypeConstant.Point:
                    {
                        moPoint sEditingPoint = (moPoint)mEditingGeometry;

                        if (moMapTools.IsPointOnPoint(sPoint, sEditingPoint, mapTolerance))
                        {
                            mMouseOnPartIndex = 0;
                            mMouseOnPointIndex = 0;
                            //Cursor = new Cursor(Resources.add.Handle);
                        }
                        else
                        {
                            Cursor = Cursors.Default;
                            mMouseOnPartIndex = -1;
                            mMouseOnPointIndex = -1;
                        }
                        break;
                    }

                case moGeometryTypeConstant.MultiPolyline:
                    {
                        moMultiPolyline sMultiPolyline = (moMultiPolyline)mEditingGeometry;
                        if (PointCloseToMultiPolylinePoint(sPoint, sMultiPolyline, mapTolerance))
                        {
                            //Cursor = new Cursor(Resources.add.Handle);
                        }
                        else
                        {
                            Cursor = Cursors.Default;
                            mMouseOnPartIndex = -1;
                            mMouseOnPointIndex = -1;
                        }
                        break;
                    }
                case moGeometryTypeConstant.MultiPolygon:
                    {
                        moMultiPolygon sMultiPolygon = (moMultiPolygon)mEditingGeometry;

                        if (PointCloseToMultiPolygonPoint(sPoint, sMultiPolygon, mapTolerance))
                        {

                        }
                        else
                        {
                            Cursor = Cursors.Default;
                            mMouseOnPartIndex = -1;
                            mMouseOnPointIndex = -1;
                        }
                        break;
                    }
            }
        }

        //删除结点-鼠标移动
        private void OnDeleteNode_MouseMove(MouseEventArgs e)
        {
            moPoint sPoint = MapControl.ToMapPoint(e.Location.X, e.Location.Y);
            double mapTolerance = MapControl.ToMapDistance(mSelectingTolerance);

            switch (EditingLayerShape)
            {
                case moGeometryTypeConstant.Point:
                    {
                        moPoint sEditingPoint = (moPoint)mEditingGeometry;

                        if (moMapTools.IsPointOnPoint(sPoint, sEditingPoint, mapTolerance))
                        {
                            mMouseOnPartIndex = 0;
                            mMouseOnPointIndex = 0;
                            //Cursor = new Cursor(Resources.del.Handle);
                        }
                        else
                        {
                            Cursor = Cursors.Default;
                            mMouseOnPartIndex = -1;
                            mMouseOnPointIndex = -1;
                        }
                        break;
                    }

                case moGeometryTypeConstant.MultiPolyline:
                    {
                        moMultiPolyline sMultiPolyline = (moMultiPolyline)mEditingGeometry;
                        if (PointCloseToMultiPolylinePoint(sPoint, sMultiPolyline, mapTolerance))
                        {
                            //Cursor = new Cursor(Resources.del.Handle);
                        }
                        else
                        {
                            Cursor = Cursors.Default;
                            mMouseOnPartIndex = -1;
                            mMouseOnPointIndex = -1;
                        }
                        break;
                    }
                case moGeometryTypeConstant.MultiPolygon:
                    {
                        moMultiPolygon sMultiPolygon = (moMultiPolygon)mEditingGeometry;

                        if (PointCloseToMultiPolygonPoint(sPoint, sMultiPolygon, mapTolerance))
                        {

                        }
                        else
                        {
                            Cursor = Cursors.Default;
                            mMouseOnPartIndex = -1;
                            mMouseOnPointIndex = -1;
                        }
                        break;
                    }
            }
        }

        #endregion

        #region MapControl鼠标松开事件

        // 鼠标在MapControl中被松开
        private void moMapControl1_MouseUp(object sender, MouseEventArgs e)
        {
            switch (mMapOpStyle)
            {
                case MapOpConstant.ZoomIn:
                    OnZoomIn_MouseUp(e);
                    break;
                case MapOpConstant.ZoomOut:
                    OnZoomOut_MouseUp(e);
                    break;
                case MapOpConstant.Pan:
                    OnPan_MouseUp(e);
                    break;
                case MapOpConstant.SelectByLocation:
                    OnSelectByLocation_MouseUp(e);
                    break;
                case MapOpConstant.Identify:
                    OnIdentify_MouseUp(e);
                    break;
                case MapOpConstant.MoveFeature:
                    OnMoveFeature_MouseUp(e);
                    break;
                case MapOpConstant.MoveNode:
                    OnMoveNode_MouseUp(e);
                    break;
                default:
                    break;
            }
        }

        //放大操作-鼠标松开
        private void OnZoomIn_MouseUp(MouseEventArgs e)
        {
            if (mIsInZoom == false)
                return;
            mIsInZoom = false;
            if (mStartMouseLocation.X == e.Location.X && mStartMouseLocation.Y == e.Location.Y)
            {
                //单点放大
                moPoint sPoint = MapControl.ToMapPoint(mStartMouseLocation.X, mStartMouseLocation.Y);
                MapControl.ZoomByCenter(sPoint, mZoomRatioFixed);
            }
            else
            {
                //拉框放大
                moRectangle sBox = GetMapRectByTwoPoints(mStartMouseLocation, e.Location);
                MapControl.ZoomToExtent(sBox);
            }
        }

        //缩小操作-鼠标松开
        private void OnZoomOut_MouseUp(MouseEventArgs e)
        {
            if (mIsInZoom == false)
                return;
            mIsInZoom = false;
            if (mStartMouseLocation.X == e.Location.X && mStartMouseLocation.Y == e.Location.Y)
            {
                //单点缩小
                moPoint sPoint = MapControl.ToMapPoint(mStartMouseLocation.X, mStartMouseLocation.Y);
                MapControl.ZoomByCenter(sPoint, 1 / mZoomRatioFixed);
            }
            else
            {
                //拉框缩小
                //moRectangle sBox = GetMapRectByTwoPoints(mStartMouseLocation, e.Location);
                //moMapControl1.ZoomOutToExtent(sBox);
                moPoint sPoint = MapControl.ToMapPoint(mStartMouseLocation.X, mStartMouseLocation.Y);
                MapControl.ZoomByCenter(sPoint, 1 / mZoomRatioFixed);
            }
        }

        //漫游操作-鼠标松开
        private void OnPan_MouseUp(MouseEventArgs e)
        {
            if (mIsInPan == false)
                return;
            mIsInPan = false;
            double sDeltaX = MapControl.ToMapDistance(e.Location.X - mStartMouseLocation.X);
            double sDeltaY = MapControl.ToMapDistance(mStartMouseLocation.Y - e.Location.Y);
            MapControl.PanDelta(sDeltaX, sDeltaY);
        }

        //选择操作-鼠标松开
        private void OnSelectByLocation_MouseUp(MouseEventArgs e)
        {
            if (mIsInSelect == false)
                return;
            mIsInSelect = false;
            moRectangle sBox = GetMapRectByTwoPoints(mStartMouseLocation, e.Location);
            double tolerance = MapControl.ToMapDistance(mSelectingTolerance);
            MapControl.SelectByBox(sBox, tolerance, 0);
            MapControl.RedrawTrackingShapes();
        }

        //查询操作-鼠标松开
        private void OnIdentify_MouseUp(MouseEventArgs e)
        {
            if (mIsInIdentify == false) return;
            mIsInIdentify = false;
            MapControl.Refresh();
            moRectangle sBox = GetMapRectByTwoPoints(mStartMouseLocation, e.Location);
            double tolerance = MapControl.ToMapDistance(mSelectingTolerance);
            if (MapControl.Layers.Count == 0)
                return;
            else
            {
                Int32 index;
                // 窗口关闭，重新新建，SelectedLayerIndex
                if (mIdentifyForm == null || mIdentifyForm.IsDisposed)
                {
                    mIdentifyForm = new IdentifyForm(this);
                    if (SelectedLayerIndex >= 0)
                    {
                        index = SelectedLayerIndex;
                    }
                    else if (MapControl.Layers.Count > 0)
                    {
                        index = 0;
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    // 否则使用内部IdentifyIndex
                    index = mIdentifyForm.IdentifyIndex;
                }
                moMapLayer sLayer = MapControl.Layers.GetItem(index);
                moFeatures sFeatures = sLayer.SearchByBox(sBox, tolerance);
                int sSelFeatureCount = sFeatures.Count;
                if (sSelFeatureCount > 0)
                {
                    moGeometry[] sGeometries = new moGeometry[sSelFeatureCount];
                    for (int i = 0; i <= sSelFeatureCount - 1; i++)
                        sGeometries[i] = sFeatures.GetItem(i).Geometry;
                    MapControl.FlashShapes(sGeometries, 3, 800);
                }
                // 显示识别到的要素属性
                mIdentifyForm.Show(sFeatures, index);
            }
        }

        //移动要素-鼠标松开
        private void OnMoveFeature_MouseUp(MouseEventArgs e)
        {
            if (!mIsLeftMousePressed) return;
            if (mIsMouseDownOnSelectedFeature)
            {
                MoveSelectedFeature_MouseUp(e);
            }
            else
            {
                OnSelectByLocation_MouseMove(e);
            }
            mIsLeftMousePressed = false;
        }

        //移动选中要素-鼠标松开
        private void MoveSelectedFeature_MouseUp(MouseEventArgs e)
        {
            if (mIsSelectedFeatureMoved)
            {
                mIsSelectedFeatureMoved = false;
                moMapLayer sLayer = MapControl.Layers.GetItem(mEditingLayerIndex);
                for (int i = 0; i < sLayer.SelectedFeatures.Count; i++)
                {
                    moFeature sFeature = sLayer.SelectedFeatures.GetItem(i);
                    sFeature.Geometry = mMovingGeometries[i];
                }
                sLayer.UpdateExtent();
                MapControl.RedrawMap();
            }
            else
            {
                moRectangle sBox = GetMapRectByTwoPoints(e.Location, e.Location);
                double sTolerance = MapControl.ToMapDistance(mSelectingTolerance);
                MapControl.SelectLayerByBox(sBox, sTolerance, mEditingLayerIndex);
                MapControl.RedrawTrackingShapes();
            }
            //清除移动图形列表
            mMovingGeometries.Clear();
        }

        //移动节点-鼠标松开
        private void OnMoveNode_MouseUp(MouseEventArgs e)
        {
            mIsMouseDownNearNode = false;
            mMouseOnPointIndex = -1;
            mMouseOnPointIndex = -1;
        }
        #endregion

        #region MapControl鼠标单击事件
        // 鼠标在MapControl中单击
        private void moMapControl1_MouseClick(object sender, MouseEventArgs e)
        {
            switch (mMapOpStyle)
            {
                case MapOpConstant.ZoomIn:
                    OnZoomIn_MouseClick(e);
                    break;
                case MapOpConstant.ZoomOut:
                    OnZoomOut_MouseClick(e);
                    break;
                case MapOpConstant.Pan:
                    break;
                case MapOpConstant.SelectByLocation:
                    //OnSelectByLocation_MouseClick(e);
                    break;
                case MapOpConstant.Identify:
                    //OnIdentify_MouseClick(e);
                    break;
                case MapOpConstant.CreateFeature:
                    OnCreateFeature_MouseClick(e);
                    break;
                case MapOpConstant.AddNode:
                    OnAddNode_MouseClick(e);
                    break;
                case MapOpConstant.DeleteNode:
                    OnDeleteNode_MouseClick(e);
                    break;
                default:
                    break;
            }
        }

        //放大操作-鼠标单击
        private void OnZoomIn_MouseClick(MouseEventArgs e)
        {
            //单点放大
            moPoint sPoint = MapControl.ToMapPoint(e.Location.X, e.Location.Y);
            MapControl.ZoomByCenter(sPoint, mZoomRatioFixed);
        }

        //缩小操作-鼠标单击
        private void OnZoomOut_MouseClick(MouseEventArgs e)
        {
            //单点缩小
            moPoint sPoint = MapControl.ToMapPoint(e.Location.X, e.Location.Y);
            MapControl.ZoomByCenter(sPoint, 1 / mZoomRatioFixed);
        }

        //创建要素-鼠标单击
        private void OnCreateFeature_MouseClick(MouseEventArgs e)
        {
            switch (EditingLayerShape)
            {
                case moGeometryTypeConstant.Point:
                    {
                        //屏幕坐标转为地理坐标加入描绘图形
                        moPoint sPoint = MapControl.ToMapPoint(e.Location.X, e.Location.Y);
                        mSketchingPoint.Add(sPoint);
                        FinishCreateFeature();
                        //重绘跟踪图形
                        MapControl.RedrawTrackingShapes();
                        break;
                    }
                case moGeometryTypeConstant.MultiPolyline:
                    {
                        //屏幕坐标转为地理坐标加入描绘图形
                        moPoint sPoint = MapControl.ToMapPoint(e.Location.X, e.Location.Y);
                        mSketchingShape.Last().Add(sPoint);
                        //重绘跟踪图形
                        MapControl.RedrawTrackingShapes();
                        break;
                    }
                case moGeometryTypeConstant.MultiPolygon:
                    {
                        //屏幕坐标转为地理坐标加入描绘图形
                        moPoint sPoint = MapControl.ToMapPoint(e.Location.X, e.Location.Y);
                        mSketchingShape.Last().Add(sPoint);
                        //重绘跟踪图形
                        MapControl.RedrawTrackingShapes();
                        break;
                    }
            }
        }

        //添加结点-鼠标单击
        private void OnAddNode_MouseClick(MouseEventArgs e)
        {
            if (mMouseOnPartIndex == -1 || mMouseOnPointIndex == -1) return;
            mIsNodeChanged = true;
            moPoint addedPoint = MapControl.ToMapPoint(e.Location.X, e.Location.Y);
            switch (EditingLayerShape)
            {
                case moGeometryTypeConstant.MultiPolyline:
                    {
                        moMultiPolyline multiPolyline = (moMultiPolyline)mEditingGeometry;
                        moPoints points = multiPolyline.Parts.GetItem(mMouseOnPartIndex);
                        points.Insert(mMouseOnPointIndex + 1, addedPoint);
                        multiPolyline.UpdateExtent();
                        mEditingGeometry = multiPolyline;
                        break;
                    }
                case moGeometryTypeConstant.MultiPolygon:
                    {
                        moMultiPolygon multiPolygon = (moMultiPolygon)mEditingGeometry;
                        moPoints sPoints = multiPolygon.Parts.GetItem(mMouseOnPartIndex);
                        sPoints.Insert(mMouseOnPointIndex + 1, addedPoint);
                        multiPolygon.UpdateExtent();
                        mEditingGeometry = multiPolygon;
                        break;
                    }
            }
            MapControl.RedrawTrackingShapes();
            mMouseOnPartIndex = -1;
            mMouseOnPointIndex = -1;
        }

        //删除结点-鼠标单击
        private void OnDeleteNode_MouseClick(MouseEventArgs e)
        {
            if (mMouseOnPartIndex == -1 || mMouseOnPointIndex == -1) return;
            mIsNodeChanged = true;
            switch (EditingLayerShape)
            {
                case moGeometryTypeConstant.MultiPolyline:
                    {
                        moMultiPolyline multiPolyline = (moMultiPolyline)mEditingGeometry;
                        moPoints points = multiPolyline.Parts.GetItem(mMouseOnPartIndex);
                        if (points.Count > 2)
                        {
                            points.RemoveAt(mMouseOnPointIndex);
                        }
                        else
                        {
                            MessageBox.Show(@"要素节点数量不足，无法删减");
                            // return to move node state
                            btnMoveNode_Click(new object(), e);
                            return;
                        }
                        multiPolyline.UpdateExtent();
                        mEditingGeometry = multiPolyline;
                        break;
                    }
                case moGeometryTypeConstant.MultiPolygon:
                    {
                        moMultiPolygon multiPolygon = (moMultiPolygon)mEditingGeometry;
                        moPoints points = multiPolygon.Parts.GetItem(mMouseOnPartIndex);
                        if (points.Count > 3)
                        {
                            points.RemoveAt(mMouseOnPointIndex);
                        }
                        else
                        {
                            MessageBox.Show(@"要素节点数量不足，无法删减");
                            // trans to move node state
                            btnMoveNode_Click(new object(), e);
                            return;
                        }
                        multiPolygon.UpdateExtent();
                        mEditingGeometry = multiPolygon;
                        break;
                    }
            }
            MapControl.RedrawTrackingShapes();
            mMouseOnPartIndex = -1;
            mMouseOnPointIndex = -1;
        }
        #endregion

        #region MapControl鼠标双击事件
        // 鼠标在MapControl中双击
        private void moMapControl1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (mMapOpStyle == MapOpConstant.CreateFeature)
                OnCreateFeature_MouseDoubleClick(e);
        }

        //创建要素-鼠标双击
        private void OnCreateFeature_MouseDoubleClick(MouseEventArgs e)
        {
            FinishCreatePart();
            FinishCreateFeature();
        }
        #endregion

        #region MapControl其他事件处理（含自定义事件）
        // MapControl鼠标滑轮
        private void moMapControl1_MouseWheel(object sender, MouseEventArgs e)
        {
            // 计算地图空间中心点的地图坐标
            //double sY = moMapControl1.ClientRectangle.Width / 2;
            //double sX = moMapControl1.ClientRectangle.Height / 2;
            // 使用鼠标位置为中心进行缩放
            double sX = e.Location.X;
            double sY = e.Location.Y;
            moPoint sPoint = MapControl.ToMapPoint(sX, sY);
            if (e.Delta > 0)
            {
                MapControl.ZoomByCenter(sPoint, mZoomRatioMouseWheel);
            }
            else
            {
                MapControl.ZoomByCenter(sPoint, 1 / mZoomRatioMouseWheel);
            }
        }

        // 地图比例尺发生了变化
        private void moMapControl1_MapScaleChanged(object sender)
        {
            ShowMapScale();
        }


        private void toolStripMenuItem1000_Click(object sender, EventArgs e)
        {
            moPoint point = MapControl.ToMapPoint(MapControl.Width / 2, MapControl.Height / 2);
            double zoomRatio = MapControl.MapScale / 1000;
            MapControl.ZoomByCenter(point, zoomRatio);
        }

        private void toolStripMenuItem10000_Click(object sender, EventArgs e)
        {
            moPoint point = MapControl.ToMapPoint(MapControl.Width / 2, MapControl.Height / 2);
            double zoomRatio = MapControl.MapScale / 10000;
            MapControl.ZoomByCenter(point, zoomRatio);
        }

        private void toolStripMenuItem100000_Click(object sender, EventArgs e)
        {
            moPoint point = MapControl.ToMapPoint(MapControl.Width / 2, MapControl.Height / 2);
            double zoomRatio = MapControl.MapScale / 100000;
            MapControl.ZoomByCenter(point, zoomRatio);
        }

        private void toolStripMenuItem500000_Click(object sender, EventArgs e)
        {
            moPoint point = MapControl.ToMapPoint(MapControl.Width / 2, MapControl.Height / 2);
            double zoomRatio = MapControl.MapScale / 500000;
            MapControl.ZoomByCenter(point, zoomRatio);
        }

        private void toolStripMenuItem1000000_Click(object sender, EventArgs e)
        {
            moPoint point = MapControl.ToMapPoint(MapControl.Width / 2, MapControl.Height / 2);
            double zoomRatio = MapControl.MapScale / 1000000;
            MapControl.ZoomByCenter(point, zoomRatio);
        }

        private void toolStripMenuItem3000000_Click(object sender, EventArgs e)
        {
            moPoint point = MapControl.ToMapPoint(MapControl.Width / 2, MapControl.Height / 2);
            double zoomRatio = MapControl.MapScale / 3000000;
            MapControl.ZoomByCenter(point, zoomRatio);
        }

        private void toolStripMenuItem10000000_Click(object sender, EventArgs e)
        {
            moPoint point = MapControl.ToMapPoint(MapControl.Width / 2, MapControl.Height / 2);
            double zoomRatio = MapControl.MapScale / 10000000;
            MapControl.ZoomByCenter(point, zoomRatio);
        }

        // 地图控件绘制完毕
        private void moMapControl1_AfterTrackingLayerDraw(object sender, moUserDrawingTool drawTool)
        {
            DrawSketchingShapes(drawTool);
            DrawEditingShapes(drawTool);
        }

        ///完成部件绘制
        private void 完成部件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FinishCreatePart();
        }

        //完成要素绘制
        private void 完成绘制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FinishCreateFeature();
        }

        //删除要素
        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mIsLayerChanged = true;
            MapControl.Layers.GetItem(SelectedLayerIndex).RemoveSelection();
            MapControl.RedrawMap();
        }
        #endregion


        #endregion

        #endregion

        #region 方法

        #region 图层操作

        private void 置顶ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MoveLayer(SelectedLayerIndex, 0);
            // 重新渲染TreeView控件
            RefreshLayersTree();
        }

        private void 置底ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MoveLayer(SelectedLayerIndex, MapControl.Layers.Count - 1);
            RefreshLayersTree();
        }

        private void 上移ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedLayerIndex == 0) return;
            MoveLayer(SelectedLayerIndex, SelectedLayerIndex - 1);
            RefreshLayersTree();
        }

        private void 下移ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedLayerIndex == MapControl.Layers.Count - 1) return;
            MoveLayer(SelectedLayerIndex, SelectedLayerIndex + 1);
            RefreshLayersTree();
        }

        private void 缩放至图层ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            moMapLayer layer = MapControl.Layers.GetItem(SelectedLayerIndex);
            moRectangle rect = layer.Extent;
            MapControl.ZoomToExtent(rect);
        }

        private void 移除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MapControl.Layers.RemoveAt(SelectedLayerIndex);
            if (SelectedLayerIndex == mEditingLayerIndex)
            {
                mEditingLayerIndex = -1;
            }
            mShapefiles.RemoveAt(SelectedLayerIndex);
            RefreshLayersTree();
            MapControl.RedrawMap();
        }

        private void MoveLayer(Int32 from, Int32 to)
        {
            if (from == to) return;
            if (from < 0 || from >= MapControl.Layers.Count) return;
            if (to < 0 || to >= MapControl.Layers.Count) return;
            // 移动MapControl中的图层
            MapControl.Layers.MoveTo(from, to);
            // 移动mShapefiles的顺序
            ShapeFileParser shapefile = mShapefiles[from];
            mShapefiles.RemoveAt(from);
            mShapefiles.Insert(to, shapefile);
        }

        // 添加图层到当前地图
        public void AddLayer(ShapeFileParser shapefile)
        {
            moFeatures features = shapefile.Read_ShapeFile();
            moMapLayer mapLayer = new moMapLayer(Path.GetFileNameWithoutExtension(shapefile.FilePath), shapefile.GeometryType, shapefile.Fields);
            mapLayer.Features = features;
            // 将图层添加到第一个
            MapControl.Layers.Add(mapLayer);
            mShapefiles.Add(shapefile);
            MoveLayer(MapControl.Layers.Count - 1, 0);
            MapControl.FullExtent();
            RefreshLayersTree();
        }

        public void AddLayerFromDB(ConnDBParser connDBParser)
        {
            // features
            moFeatures features = connDBParser.Read_DB();
            // layer
            moMapLayer mapLayer = new moMapLayer(connDBParser.Table, connDBParser.GeometryType, connDBParser.Fields);
            // layer.Features = features
            mapLayer.Features = features;
            // 添加layer到mapcontrol.layers
            MapControl.Layers.Add(mapLayer);

            // !!!!!!!!!!!!!!!!!!!!!!! 将DB图层添加到mShapefiles
            ShapeFileParser shpFileParser = new ShapeFileParser(connDBParser);
            mShapefiles.Add(shpFileParser);     

            MoveLayer(MapControl.Layers.Count - 1, 0);
            MapControl.FullExtent();
            RefreshLayersTree();
        }
        #endregion

        #endregion



        #region 私有函数
        //初始化符号
        private void InitializeSymbols()
        {
            mSelectingBoxSymbol = new moSimpleFillSymbol();
            mSelectingBoxSymbol.Color = Color.Transparent;
            mSelectingBoxSymbol.Outline.Color = mSelectBoxColor;
            mSelectingBoxSymbol.Outline.Size = mSelectBoxWidth;
            mZoomBoxSymbol = new moSimpleFillSymbol();
            mZoomBoxSymbol.Color = Color.Transparent;
            mZoomBoxSymbol.Outline.Color = mZoomBoxColor;
            mZoomBoxSymbol.Outline.Size = mZoomBoxWidth;
            mMovingPolygonSymbol = new moSimpleFillSymbol();
            mMovingPolygonSymbol.Color = Color.Transparent;
            mMovingPolygonSymbol.Outline.Color = Color.Black;
            mEditingPolygonSymbol = new moSimpleFillSymbol();
            mEditingPolygonSymbol.Color = Color.Transparent;
            mEditingPolygonSymbol.Outline.Color = Color.DarkGreen;
            mEditingPolygonSymbol.Outline.Size = 0.53;
            mEditingVertexSymbol = new moSimpleMarkerSymbol();
            mEditingVertexSymbol.Color = Color.DarkGreen;
            mEditingVertexSymbol.Style = moSimpleMarkerSymbolStyleConstant.SolidSquare;
            mEditingVertexSymbol.Size = 2;
            mEditingPolylineSymbol = new moSimpleLineSymbol();
            mEditingPolylineSymbol.Color = Color.Green;
            mElasticSymbol = new moSimpleLineSymbol();
            mElasticSymbol.Color = Color.DarkGreen;
            mElasticSymbol.Size = 0.52;
            mElasticSymbol.Style = moSimpleLineSymbolStyleConstant.Dash;
        }

        // 初始化描绘图形
        private void InitializeSketchingShape()
        {
            mSketchingShape = new List<moPoints>();
            moPoints sPoints = new moPoints();
            mSketchingShape.Add(sPoints);
            mSketchingPoint = new List<moPoint>();
        }

        // 根据屏幕坐标显示地图坐标
        private void ShowCoordinates(PointF point)
        {
            moPoint sPoint = MapControl.ToMapPoint(point.X, point.Y);
            if (mShowLngLat == false)
            {
                double sX = Math.Round(sPoint.X, 2);
                double sY = Math.Round(sPoint.Y, 2);
                coordinateStatusLabel.Text = "X=" + sX.ToString() + ", Y=" + sY.ToString();
            }
            else
            {
                moPoint sLngLat = MapControl.ProjectionCS.TransferToLngLat(sPoint);
                double sX = Math.Round(sLngLat.X, 6);
                double sY = Math.Round(sLngLat.Y, 6);
                coordinateStatusLabel.Text = "X=" + sX.ToString() + ", Y=" + sY.ToString();
            }
        }

        // 显示地图比例尺
        private void ShowMapScale()
        {
            MapScaleButton.Text = "1:" + MapControl.MapScale.ToString("0.00");
        }

        private moRectangle GetMapRectByTwoPoints(PointF point1, PointF point2)
        {
            moPoint sPoint1 = MapControl.ToMapPoint(point1.X, point1.Y);
            moPoint sPoint2 = MapControl.ToMapPoint(point2.X, point2.Y);
            double sMinX = Math.Min(sPoint1.X, sPoint2.X);
            double sMaxX = Math.Max(sPoint1.X, sPoint2.X);
            double sMinY = Math.Min(sPoint1.Y, sPoint2.Y);
            double sMaxY = Math.Max(sPoint1.Y, sPoint2.Y);
            moRectangle sRect = new moRectangle(sMinX, sMaxX, sMinY, sMaxY);
            return sRect;
        }

        //修改移动图形的坐标
        private void ModifyMovingGeometries(double deltaX, double deltaY)
        {
            //课堂实践代码只有多边形，需要补充
            Int32 sCount = mMovingGeometries.Count;
            switch (EditingLayerShape)
            {
                case moGeometryTypeConstant.Point:
                    {
                        for (int i = 0; i < sCount; i++)
                        {
                            moPoint sPoint = (moPoint)mMovingGeometries[i];
                            sPoint.X += deltaX;
                            sPoint.Y += deltaY;
                        }
                        break;
                    }

                case moGeometryTypeConstant.MultiPolyline:
                    {
                        for (int i = 0; i < sCount; i++)
                        {
                            moMultiPolyline sMultiPolyline =
                                (moMultiPolyline)mMovingGeometries[i];
                            int sPartCount = sMultiPolyline.Parts.Count;
                            for (int j = 0; j <= sPartCount - 1; j++)
                            {
                                moPoints sPoints = sMultiPolyline.Parts.GetItem(j);
                                int sPointCount = sPoints.Count;
                                for (int k = 0; k <= sPointCount - 1; k++)
                                {
                                    moPoint sPoint = sPoints.GetItem(k);
                                    sPoint.X += deltaX;
                                    sPoint.Y += deltaY;
                                }
                            }

                            sMultiPolyline.UpdateExtent();
                        }
                        break;
                    }
                case moGeometryTypeConstant.MultiPolygon:
                    {
                        for (int i = 0; i < sCount; i++)
                        {
                            moMultiPolygon sMultiPolygon = (moMultiPolygon)mMovingGeometries[i];
                            int sPartCount = sMultiPolygon.Parts.Count;
                            for (int j = 0; j <= sPartCount - 1; j++)
                            {
                                moPoints sPoints = sMultiPolygon.Parts.GetItem(j);
                                int sPointCount = sPoints.Count;
                                for (int k = 0; k <= sPointCount - 1; k++)
                                {
                                    moPoint sPoint = sPoints.GetItem(k);
                                    sPoint.X += deltaX;
                                    sPoint.Y += deltaY;
                                }
                            }
                            sMultiPolygon.UpdateExtent();
                        }
                        break;
                    }
            }
        }

        //绘制移动图形
        private void DrawMovingShapes()
        {
            moUserDrawingTool sDrawingTool = MapControl.GetDrawingTool();
            Int32 sCount = mMovingGeometries.Count;
            for (Int32 i = 0; i <= sCount - 1; i++)
            {
                if (mMovingGeometries[i].GetType() == typeof(moMultiPolygon))
                {
                    moMultiPolygon sMultiPolygon = (moMultiPolygon)mMovingGeometries[i];
                    sDrawingTool.DrawMultiPolygon(sMultiPolygon, mMovingPolygonSymbol);
                }
            }
        }

        //选择移动节点
        private void SelectNodeToMove_MouseMove(MouseEventArgs e)
        {
            moPoint sPoint = MapControl.ToMapPoint(e.Location.X, e.Location.Y);
            double mapTolerance = MapControl.ToMapDistance(mSelectingTolerance);

            switch (EditingLayerShape)
            {
                case moGeometryTypeConstant.Point:
                    {
                        moPoint sEditingPoint = (moPoint)mEditingGeometry;

                        if (moMapTools.IsPointOnPoint(sPoint, sEditingPoint, mapTolerance))
                        {
                            mMouseOnPartIndex = 0;
                            mMouseOnPointIndex = 0;
                        }
                        else
                        {
                            Cursor = Cursors.Default;
                            mMouseOnPartIndex = -1;
                            mMouseOnPointIndex = -1;
                        }
                        break;
                    }

                case moGeometryTypeConstant.MultiPolyline:
                    {
                        moMultiPolyline sMultiPolyline = (moMultiPolyline)mEditingGeometry;
                        if (PointCloseToMultiPolylinePoint(sPoint, sMultiPolyline, mapTolerance))
                        {

                        }
                        else
                        {
                            Cursor = Cursors.Default;
                            mMouseOnPartIndex = -1;
                            mMouseOnPointIndex = -1;
                        }
                        break;
                    }
                case moGeometryTypeConstant.MultiPolygon:
                    {
                        moMultiPolygon sMultiPolygon = (moMultiPolygon)mEditingGeometry;

                        if (PointCloseToMultiPolygonPoint(sPoint, sMultiPolygon, mapTolerance))
                        {

                        }
                        else
                        {
                            Cursor = Cursors.Default;
                            mMouseOnPartIndex = -1;
                            mMouseOnPointIndex = -1;
                        }
                        break;
                    }
            }
        }

        //移动选中节点
        private void MoveSelectedNode_MouseMove(MouseEventArgs e)
        {
            moPoint sPoint = MapControl.ToMapPoint(e.Location.X, e.Location.Y);
            switch (EditingLayerShape)
            {
                case moGeometryTypeConstant.Point:
                    {

                        mIsNodeChanged = true;
                        mEditingGeometry = sPoint;
                        MapControl.RedrawTrackingShapes();
                        break;
                    }

                case moGeometryTypeConstant.MultiPolyline:
                    {
                        mIsNodeChanged = true;
                        moMultiPolyline sMultiPolyline = (moMultiPolyline)mEditingGeometry;

                        moPoint newPoint = sMultiPolyline.Parts.GetItem(mMouseOnPartIndex).GetItem(mMouseOnPointIndex);
                        newPoint.X = sPoint.X; newPoint.Y = sPoint.Y;
                        sMultiPolyline.UpdateExtent();
                        mEditingGeometry = sMultiPolyline;
                        MapControl.RedrawTrackingShapes();

                        break;
                    }
                case moGeometryTypeConstant.MultiPolygon:
                    {
                        mIsNodeChanged = true;
                        moMultiPolygon sMultiPolygon = (moMultiPolygon)mEditingGeometry;
                        moPoint newPoint = sMultiPolygon.Parts.GetItem(mMouseOnPartIndex).GetItem(mMouseOnPointIndex);
                        newPoint.X = sPoint.X; newPoint.Y = sPoint.Y;
                        sMultiPolygon.UpdateExtent();
                        mEditingGeometry = sMultiPolygon;
                        MapControl.RedrawTrackingShapes();
                        break;
                    }
            }
        }

        //判断选点是否靠近线
        private bool PointCloseToMultiPolylinePoint(moPoint sPoint, moMultiPolyline sMultiPolyline, double sTolerance)
        {
            switch (mMapOpStyle)
            {
                case MapOpConstant.MoveNode:
                case MapOpConstant.DeleteNode:
                    {
                        for (int i = 0; i < sMultiPolyline.Parts.Count; i++)
                        {
                            moPoints sPoints = sMultiPolyline.Parts.GetItem(i);
                            int j = 0;
                            for (; j < sPoints.Count; j++)
                            {
                                if (moMapTools.IsPointOnPoint(sPoint, sPoints.GetItem(j), sTolerance))
                                {
                                    mMouseOnPartIndex = i;
                                    mMouseOnPointIndex = j;
                                    return true;
                                }
                            }
                        }
                        break;
                    }
                case MapOpConstant.AddNode:
                    {
                        for (int i = 0; i < sMultiPolyline.Parts.Count; i++)
                        {
                            moPoints sPoints = sMultiPolyline.Parts.GetItem(i);
                            for (int j = 0; j < sPoints.Count - 1; j++)
                            {
                                moPoints tempPoints = new moPoints();
                                tempPoints.Add(sPoints.GetItem(j));
                                tempPoints.Add(sPoints.GetItem(j + 1));
                                tempPoints.UpdateExtent();
                                if (moMapTools.IsPointOnPolyline(sPoint, tempPoints, sTolerance))
                                {
                                    mMouseOnPartIndex = i;
                                    mMouseOnPointIndex = j;
                                    return true;
                                }
                            }
                        }
                        break;
                    }
            }
            return false;
        }

        //判断选点是否靠近多边形
        private bool PointCloseToMultiPolygonPoint(moPoint point, moMultiPolygon multiPolygon, double tolerance)
        {
            switch (mMapOpStyle)
            {
                case MapOpConstant.MoveNode:
                case MapOpConstant.DeleteNode:
                    {
                        for (int i = 0; i < multiPolygon.Parts.Count; i++)
                        {
                            moPoints sPoints = multiPolygon.Parts.GetItem(i);
                            for (int j = 0; j < sPoints.Count; j++)
                            {
                                if (moMapTools.IsPointOnPoint(point, sPoints.GetItem(j), tolerance))
                                {
                                    mMouseOnPartIndex = i;
                                    mMouseOnPointIndex = j;
                                    return true;
                                }
                            }
                        }
                        break;
                    }
                case MapOpConstant.AddNode:
                    {
                        for (int i = 0; i < multiPolygon.Parts.Count; i++)
                        {
                            moPoints points = multiPolygon.Parts.GetItem(i);
                            for (int j = 0; j < points.Count; j++)
                            {
                                moPoints tempPoints = new moPoints();
                                tempPoints.Add(points.GetItem(j));
                                tempPoints.Add(j < points.Count - 1 ? points.GetItem(j + 1) : points.GetItem(0));
                                tempPoints.UpdateExtent();
                                if (moMapTools.IsPointOnPolyline(point, tempPoints, tolerance))
                                {
                                    mMouseOnPartIndex = i;
                                    mMouseOnPointIndex = j;
                                    return true;
                                }
                            }
                        }
                        break;
                    }
            }
            return false;
        }

        //绘制正在描绘的图形
        private void DrawSketchingShapes(moUserDrawingTool drawingTool)
        {
            if (mEditingLayerIndex == -1) return;

            switch (EditingLayerShape)
            {
                case moGeometryTypeConstant.Point:
                    {
                        if (mSketchingPoint.Count == 0) return;
                        drawingTool.DrawPoint(mSketchingPoint[0], mEditingVertexSymbol);
                        break;
                    }
                case moGeometryTypeConstant.MultiPolyline:
                    {
                        if (mSketchingShape == null)
                            return;
                        int partCount = mSketchingShape.Count;

                        // draw finished parts
                        for (int i = 0; i < partCount - 1; i++)
                        {
                            drawingTool.DrawPolyline(mSketchingShape[i], mEditingPolylineSymbol);
                        }

                        // draw creating part
                        moPoints lastPart = mSketchingShape.Last();
                        if (lastPart.Count > 1)
                        {
                            drawingTool.DrawPolyline(lastPart, mEditingPolylineSymbol);
                        }

                        // drawing all nodes
                        for (int i = 0; i < partCount; i++)
                        {
                            moPoints points = mSketchingShape[i];
                            drawingTool.DrawPoints(points, mEditingVertexSymbol);
                        }
                        break;
                    }
                case moGeometryTypeConstant.MultiPolygon:
                    {
                        if (mSketchingShape == null)
                            return;
                        int partCount = mSketchingShape.Count;

                        // draw finished pars
                        for (int i = 0; i < partCount - 1; i++)
                        {
                            drawingTool.DrawPolygon(mSketchingShape[i], mEditingPolygonSymbol);
                        }

                        // draw creating part
                        // use polyline to express polygon
                        moPoints lastPart = mSketchingShape.Last();
                        if (lastPart.Count > 1)
                            drawingTool.DrawPolyline(lastPart, mEditingPolygonSymbol.Outline);

                        // draw all nodes
                        for (int i = 0; i <= partCount - 1; i++)
                        {
                            moPoints points = mSketchingShape[i];
                            drawingTool.DrawPoints(points, mEditingVertexSymbol);
                        }
                        break;
                    }
            }
        }

        //绘制正在编辑的图形
        private void DrawEditingShapes(moUserDrawingTool drawingTool)
        {
            if (mEditingGeometry == null)
                return;
            switch (EditingLayerShape)
            {
                case moGeometryTypeConstant.Point:
                    {
                        moPoint point = (moPoint)mEditingGeometry;
                        drawingTool.DrawPoint(point, mEditingVertexSymbol);
                        break;
                    }
                case moGeometryTypeConstant.MultiPolyline:
                    {
                        moMultiPolyline multiPolyline = (moMultiPolyline)mEditingGeometry;
                        drawingTool.DrawMultiPolyline(multiPolyline, mEditingPolylineSymbol);

                        int partCount = multiPolyline.Parts.Count;
                        for (int i = 0; i < partCount; i++)
                        {
                            moPoints points = multiPolyline.Parts.GetItem(i);
                            drawingTool.DrawPoints(points, mEditingVertexSymbol);
                        }

                        break;
                    }
                case moGeometryTypeConstant.MultiPolygon:
                    {
                        moMultiPolygon sMultiPolygon = (moMultiPolygon)mEditingGeometry;

                        // draw border (transparent fill)
                        drawingTool.DrawMultiPolygon(sMultiPolygon, mEditingPolygonSymbol);

                        // draw nodes
                        int partCount = sMultiPolygon.Parts.Count;
                        for (int i = 0; i <= partCount - 1; i++)
                        {
                            moPoints points = sMultiPolygon.Parts.GetItem(i);
                            drawingTool.DrawPoints(points, mEditingVertexSymbol);
                        }

                        break;
                    }
            }
        }

        //结束部件
        private void FinishCreatePart()
        {
            mIsLayerChanged = true;
            switch (EditingLayerShape)
            {
                case moGeometryTypeConstant.MultiPolyline:
                    {
                        //判断是否少于2个点
                        if (mSketchingShape.Last().Count < 2)
                        {
                            MessageBox.Show("至少需要2个点才能构成一个部件。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        //描绘图形添加多点对象
                        moPoints sPoints = new moPoints();
                        mSketchingShape.Add(sPoints);
                        MapControl.RedrawTrackingShapes();
                        break;
                    }
                case moGeometryTypeConstant.MultiPolygon:
                    {
                        //判断是否少于2个点
                        if (mSketchingShape.Last().Count < 3)
                        {
                            MessageBox.Show("至少需要3个点才能构成一个部件。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        //描绘图形添加多点对象
                        moPoints sPoints = new moPoints();
                        mSketchingShape.Add(sPoints);
                        MapControl.RedrawTrackingShapes();
                        break;
                    }
                default:
                    break;
            }
        }

        //结束绘制
        private void FinishCreateFeature()
        {
            mIsLayerChanged = true;
            switch (EditingLayerShape)
            {
                case moGeometryTypeConstant.Point:
                    {
                        if (mSketchingPoint.Count == 1)
                        {
                            moMapLayer sLayer = MapControl.Layers.GetItem(mEditingLayerIndex);
                            moFeature sFeature = sLayer.GetNewFeature();
                            sFeature.Geometry = mSketchingPoint[0];
                            sLayer.Features.Add(sFeature);
                            sLayer.UpdateExtent();
                            sLayer.SelectedFeatures.Clear();
                            sLayer.SelectedFeatures.Add(sFeature);
                        }
                        break;
                    }
                case moGeometryTypeConstant.MultiPolyline:
                    {
                        if (mSketchingShape.Last().Count == 1)
                        {
                            MessageBox.Show("部件至少需要两个点！");
                            return;
                        }
                        //最后一个部件点数为0则删除
                        if (mSketchingShape.Last().Count == 0)
                            mSketchingShape.Remove(mSketchingShape.Last());
                        //用户的确输入则加入线图层
                        if (mSketchingShape.Count > 0)
                        {
                            //查找编辑图层
                            moMapLayer sLayer = MapControl.Layers.GetItem(mEditingLayerIndex);
                            //新建复合线要素
                            moMultiPolyline sMultiPolyline = new moMultiPolyline();
                            sMultiPolyline.Parts.AddRange(mSketchingShape.ToArray());
                            sMultiPolyline.UpdateExtent();
                            //新要素加入图层
                            moFeature sFeature = sLayer.GetNewFeature();
                            sFeature.Geometry = sMultiPolyline;
                            sLayer.Features.Add(sFeature);
                            sLayer.UpdateExtent();
                            sLayer.SelectedFeatures.Clear();
                            sLayer.SelectedFeatures.Add(sFeature);
                        }
                        break;
                    }
                case moGeometryTypeConstant.MultiPolygon:
                    {
                        if (mSketchingShape.Last().Count < 3 && mSketchingShape.Last().Count >= 1)
                        {
                            MessageBox.Show("部件至少需要三个点！");
                            return;
                        }
                        //最后一个部件点数为0则删除
                        if (mSketchingShape.Last().Count == 0)
                            mSketchingShape.Remove(mSketchingShape.Last());
                        //用户的确输入则加入线图层
                        if (mSketchingShape.Count > 0)
                        {
                            //查找编辑图层
                            moMapLayer sLayer = MapControl.Layers.GetItem(mEditingLayerIndex);
                            //新建复合多边形
                            moMultiPolygon sMultiPolygon = new moMultiPolygon();
                            sMultiPolygon.Parts.AddRange(mSketchingShape.ToArray());
                            sMultiPolygon.UpdateExtent();
                            //新要素加入图层
                            moFeature sFeature = sLayer.GetNewFeature();
                            sFeature.Geometry = sMultiPolygon;
                            sLayer.Features.Add(sFeature);
                            sLayer.UpdateExtent();
                            sLayer.SelectedFeatures.Clear();
                            sLayer.SelectedFeatures.Add(sFeature);
                        }
                        break;
                    }
                default:
                    break;
            }
            mSketchingPoint.Clear();
            mSketchingShape.Clear();
            moPoints sPoints = new moPoints();
            mSketchingShape.Add(sPoints);
            MapControl.RedrawMap();
            btnMoveFeature_Click(new object(), EventArgs.Empty);
        }


        public void RefreshLayersTree()
        {
            treeViewLayers.Nodes.Clear();
            for (int i = 0; i < MapControl.Layers.Count; i++)
            {
                TreeNode layerNode = new TreeNode
                {
                    Text = MapControl.Layers.GetItem(i).Name,
                    Checked = MapControl.Layers.GetItem(i).Visible,
                    ContextMenuStrip = LayerRightMenu
                };
                treeViewLayers.Nodes.Add(layerNode);
            }
            treeViewLayers.Refresh();
        }

        //编辑要素是否成功获取
        private bool GotEditGeometry()
        {
            if (mEditingLayerIndex == -1) return false;
            moMapLayer sLayer = MapControl.Layers.GetItem(mEditingLayerIndex);
            if (sLayer.SelectedFeatures.Count != 1)
            {
                MessageBox.Show("请选择单个要素");
                return false;
            }
            mEditingGeometry = sLayer.SelectedFeatures.GetItem(0).Geometry;
            MapControl.RedrawTrackingShapes();
            return true;
        }

        //保存编辑节点
        private void SaveNodeEdit()
        {
            if (mIsNodeChanged)
            {
                moMapLayer sLayer = MapControl.Layers.GetItem(mEditingLayerIndex);
                sLayer.SelectedFeatures.GetItem(0).Geometry = mEditingGeometry;
                sLayer.UpdateExtent();
            }
            FinishEditNode();
            MapControl.RedrawMap();
        }


        //结束编辑节点，初始化变量
        private void FinishEditNode()
        {
            mIsNodeChanged = false;
            mEditingGeometry = null;
            mMouseOnPointIndex = -1;
            mMouseOnPartIndex = -1;
        }

        //结束编辑图层
        private void FinishEditLayer()
        {
            mMapOpStyle = MapOpConstant.None;
            mEditingLayerIndex = -1;
            mIsLayerChanged = false;
            mMovingGeometries.Clear();
        }

        private void ReLoad()
        {
            Controls.Clear();
            InitializeComponent();
            MapControl.MouseWheel += moMapControl1_MouseWheel;
            InitializeSymbols();
            InitializeSketchingShape();
            ShowMapScale();
            RefreshLayersTree();
            MapControl.RedrawMap();
        }

        #region 项目文件管理

        // 打开项目
        private void OpenProject(string path)
        {
            try
            {
                mProjectPath = path;
                McFile.ProjectInfo projectInfo = McFile.Read(path);
                for (Int32 i = 0; i < projectInfo.Layers.Count; i++)
                {
                    McFile.LayerInfo layerInfo = projectInfo.Layers[i];
                    string basePath = System.IO.Directory.GetCurrentDirectory();
                    string relativePath = layerInfo.Path;
                    ShapeFileParser shapeFile = new ShapeFileParser(GetFullPath(relativePath, basePath));
                    moFeatures features = shapeFile.Read_ShapeFile();
                    moMapLayer layer = new moMapLayer(layerInfo.Name, shapeFile.GeometryType, shapeFile.Fields)
                    {
                        Features = features,
                        Description = layerInfo.Description
                    };
                    layer.Renderer = moRenderer.FromDictionary(layerInfo.Renderer);
                    layer.LabelRenderer = moLabelRenderer.FromDictionary(layerInfo.LabelRenderer);
                    mShapefiles.Add(shapeFile);
                    MapControl.Layers.Add(layer);
                }
                Text = projectInfo.ProjectName;
                RefreshLayersTree();
                MapControl.RedrawMap();
                MapControl.FullExtent();
            }
            catch (Exception ex)
            {
                MessageBox.Show("可能是文件路径问题\n", ex.Message);
            }
        }

        // 保存项目
        private void SaveProject(string path)
        {
            try
            {
                // 新地图没保存过,设置项目路径
                if (mProjectPath == string.Empty)
                {
                    mProjectPath = path;
                }
                McFile.ProjectInfo projectInfo = new McFile.ProjectInfo();
                projectInfo.Layers.Clear();
                for (Int32 i = 0; i < MapControl.Layers.Count; i++)
                {
                    moMapLayer layer = MapControl.Layers.GetItem(i);
                    McFile.LayerInfo layerInfo = new McFile.LayerInfo();
                    string basePath = System.IO.Directory.GetCurrentDirectory();
                    string fullPath = mShapefiles[i].FilePath;
                    layerInfo.Path = GetRelativePath(fullPath, basePath);
                    layerInfo.Name = layer.Name;
                    layerInfo.Description = layer.Description;
                    layerInfo.Renderer = layer.Renderer.ToDictionary();
                    layerInfo.LabelRenderer = layer.LabelRenderer.ToDictionary();
                    projectInfo.Layers.Add(layerInfo);
                }
                McFile.Write(path, projectInfo);
            }
            catch (Exception ex)
            {
                MessageBox.Show("可能是文件路径问题\n", ex.Message);
            }
        }

        private string GetRelativePath(string fullPath, string basePath)
        {
            Uri fullPathUri = new Uri(fullPath);
            Uri basePathUri = new Uri(basePath);

            // 使用 MakeRelativeUri 方法来获取相对路径
            Uri relativeUri = basePathUri.MakeRelativeUri(fullPathUri);

            // Uri 类返回的相对路径是以斜杠分隔的，将其转换为反斜杠分隔的路径
            string relativePath = relativeUri.ToString().Replace('/', Path.DirectorySeparatorChar);

            return relativePath;
        }

        private string GetFullPath(string relativePath, string basePath)
        {
            Uri basePathUri = new Uri(basePath);
            Uri relativePathUri = new Uri(relativePath, UriKind.RelativeOrAbsolute);
            Uri fullPathUri = new Uri(basePathUri, relativePathUri);
            return fullPathUri.LocalPath;
        }


        #endregion

        #endregion

        private void 拓扑查错ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TopoCheck topocheck = new TopoCheck(this, SelectedLayerIndex);
            topocheck.ShowDialog();
        }
    }
}
