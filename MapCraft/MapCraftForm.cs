using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyMapObjects;
using MapCraft.Forms;
using MapCraft.FileProcessor;
using MapCraft.Render;



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
        private double mSelectingTolerance = 3;             //  选择容限，像素
        private moSimpleFillSymbol mSelectingBoxSymbol;    // 选择盒符号
        private moSimpleFillSymbol mZoomBoxSymbol;         // 缩放盒符号
        private moSimpleFillSymbol mMovingPolygonSymbol;   // 正在移动的多边形的符号
        private moSimpleFillSymbol mEditingPolygonSymbol;  // 正在编辑的多边形的符号
        private moSimpleMarkerSymbol mEditingVertexSymbol; // 正在编辑的图形的顶点的符号
        private moSimpleLineSymbol mElasticSymbol;         // 橡皮筋符号
        private bool mShowLngLat = false;                               // 是否显示经纬度
        public List<ShapeFileParser> mShapefiles = new List<ShapeFileParser>();

        private List<AttributeTable> AttributeTables = new List<AttributeTable>();
        private static int AttributeTableIndex;
        private int SelectedLayerIndex = -1;  //选中的图层索引


        // 与地图操作有关的变量
        private MapOpConstant mMapOpStyle = 0;  // 地图操作方式
        private PointF mStartMouseLocation;     // 鼠标按下时的位置
        private bool mIsInZoom = false;         // 是否在缩放中
        private bool mIsInPan = false;          // 是否在漫游中
        private bool mIsInSelect = false;       // 是否在选择中
        private bool mIsInIdentify = false;     // 是否在查询中
        private bool mIsInMovingShapes = false; // 是否在移动图形中
        private List<moGeometry> mMovingGeometries = new List<moGeometry>(); // 正在移动的图形集合
        private moGeometry mEditingGeometry;   // 正在编辑的图形
        private List<moPoints> mSketchingShape;   // 正在描绘的图形，用多点集合存储

        // 图层路径记录
        //private List<Shapefile> mShapefiles = new List<Shapefile>();
        //图形渲染
        internal Renderer Render = new Renderer();

        #endregion

        #region 属性
        /// <summary>
        /// 地图控件，子窗体可使用
        /// </summary>
        public moMapControl MapControl
        {
            get { return moMapControl1; }
        }
        #endregion

        #region 构造函数
        public MapCraftForm()
        {
            InitializeComponent();
            moMapControl1.MouseWheel += moMapControl1_MouseWheel;
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

        
        
        // 是否显示经纬度
        private void ChkShowLngLat_CheckedChanged(object sender, EventArgs e)
        {
            mShowLngLat = cbxProjectionCS.Checked;
        }

        #region treeview operation

        
        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            int sIndex = e.Node.Index;
            moMapLayer sMapLayer = moMapControl1.Layers.GetItem(sIndex);
            sMapLayer.Visible = e.Node.Checked;
            moMapControl1.RedrawMap();
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
                mShapefiles[SelectedLayerIndex].Write_ShapeFile(layerPath);
            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString());

            }
        }


        private void 打开属性表_Click(object sender, EventArgs e)
        {
            AttributeTable attributeTable = new AttributeTable(this, SelectedLayerIndex);
            attributeTable.Owner = this;
            attributeTable.Name = moMapControl1.Layers.GetItem(SelectedLayerIndex).Name;
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
            Render.mIsInRenderer = false;
            moMapLayer sLayer = moMapControl1.Layers.GetItem(SelectedLayerIndex);//待渲染的图层
            //if (sLayer.ShapeType == moGeometryTypeConstant.Point)
            //{
            //    PointRenderer mPointRenderer = new PointRenderer(mapControl.Layers.GetItem(_operation.SelectedLayerIndex));
            //    mPointRenderer.Owner = this;
            //    mPointRenderer.ShowDialog();
            //    if (Render.mIsInRenderer == false)
            //    {
            //        return;
            //    }
            //    //简单渲染
            //    if (Render.mPointRendererMode == 0)
            //    {
            //        moSimpleRenderer sRenderer = new moSimpleRenderer();
            //        moSimpleMarkerSymbol sSymbol = new moSimpleMarkerSymbol();
            //        sSymbol.Style = (moSimpleMarkerSymbolStyleConstant)Render.mPointSymbolStyle;//修改样式
            //        sSymbol.Color = Render.mPointSimpleRendererColor;//修改颜色
            //        sSymbol.Size = Render.mPointSimpleRendererSize;//修改尺寸
            //        sRenderer.Symbol = sSymbol;
            //        sLayer.Renderer = sRenderer;
            //        mapControl.RedrawMap();
            //    }
            //    //唯一值渲染
            //    else if (Render.mPointRendererMode == 1)
            //    {
            //        moUniqueValueRenderer sRenderer = new moUniqueValueRenderer();
            //        sRenderer.Field = sLayer.AttributeFields.GetItem(Render.mPointUniqueFieldIndex).Name;
            //        List<string> sValues = new List<string>();
            //        Int32 sFeatrueCount = sLayer.Features.Count;
            //        for (Int32 i = 0; i < sFeatrueCount; i++) //加入所有要素的属性值
            //        {
            //            string sValue = Convert.ToString(sLayer.Features.GetItem(i).Attributes.GetItem(Render.mPointUniqueFieldIndex));
            //            sValues.Add(sValue);
            //        }
            //        //去除重复
            //        sValues = sValues.Distinct().ToList();
            //        //生成符号
            //        Int32 sValueCount = sValues.Count;
            //        for (Int32 i = 0; i < sValueCount; i++)
            //        {
            //            moSimpleMarkerSymbol sSymbol = new moSimpleMarkerSymbol();
            //            sSymbol.Style = (moSimpleMarkerSymbolStyleConstant)Render.mPointSymbolStyle;//修改样式
            //            sSymbol.Size = Render.mPointSimpleRendererSize;//修改尺寸
            //            sRenderer.AddUniqueValue(sValues[i], sSymbol);
            //        }
            //        sRenderer.DefaultSymbol = new moSimpleMarkerSymbol();
            //        sLayer.Renderer = sRenderer;
            //        mapControl.RedrawMap();
            //    }
            //    //分级渲染
            //    else if (Render.mPointRendererMode == 2)
            //    {
            //        moClassBreaksRenderer sRenderer = new moClassBreaksRenderer();
            //        sRenderer.Field = sLayer.AttributeFields.GetItem(Render.mPointClassBreaksFieldIndex).Name;
            //        List<double> sValues = new List<double>();
            //        Int32 sFeatrueCount = sLayer.Features.Count;
            //        Int32 sFieldIndex = sLayer.AttributeFields.FindField(sRenderer.Field);
            //        moValueTypeConstant sValueType = sLayer.AttributeFields.GetItem(sFieldIndex).ValueType;
            //        if (sValueType == moValueTypeConstant.dText)
            //        {
            //            MessageBox.Show(@"该字段不是数值字段，不支持分级渲染！");
            //            return;
            //        }
            //        try
            //        {
            //            for (Int32 i = 0; i < sFeatrueCount; i++)
            //            {
            //                double sValue = Convert.ToDouble(sLayer.Features.GetItem(i).Attributes.GetItem(sFieldIndex));
            //                sValues.Add(sValue);
            //            }
            //        }
            //        catch (Exception)
            //        {
            //            MessageBox.Show(@"该字段不是数值字段，不支持分级渲染！");
            //            return;
            //        }
            //        double sMinValue = sValues.Min();
            //        double sMaxValue = sValues.Max();
            //        for (Int32 i = 0; i < Render.mPointClassBreaksNum; i++)
            //        {
            //            double sValue = sMinValue + (sMaxValue - sMinValue) * (i + 1) / Render.mPointClassBreaksNum;
            //            moSimpleMarkerSymbol sSymbol = new moSimpleMarkerSymbol();
            //            sSymbol.Color = Render.mPointClassBreaksRendererColor;
            //            sSymbol.Style = (moSimpleMarkerSymbolStyleConstant)Render.mPointSymbolStyle;
            //            sRenderer.AddBreakValue(sValue, sSymbol);
            //        }
            //        double sMinSize = Render.mPointClassBreaksRendererMinSize;
            //        double sMaxSize = Render.mPointClassBreaksRendererMaxSize;
            //        sRenderer.RampSize(sMinSize, sMaxSize);
            //        sRenderer.DefaultSymbol = new moSimpleMarkerSymbol();
            //        sLayer.Renderer = sRenderer;
            //        mapControl.RedrawMap();
            //    }
            //}
            //else if (sLayer.ShapeType == moGeometryTypeConstant.MultiPolyline)
            //{
            //    PolylineRenderer mPolylineRenderer = new PolylineRenderer(mapControl.Layers.GetItem(_operation.SelectedLayerIndex));
            //    mPolylineRenderer.Owner = this;
            //    mPolylineRenderer.ShowDialog();
            //    if (Render.mIsInRenderer == false)
            //    {
            //        return;
            //    }
            //    //简单渲染
            //    if (Render.mPolylineRendererMode == 0)
            //    {
            //        moSimpleRenderer sRenderer = new moSimpleRenderer();
            //        moSimpleLineSymbol sSymbol = new moSimpleLineSymbol();
            //        sSymbol.Style = (moSimpleLineSymbolStyleConstant)Render.mPolylineSymbolStyle;//传参修改
            //        sSymbol.Color = Render.mPolylineSimpleRendererColor;//修改颜色
            //        sSymbol.Size = Render.mPolylineSimpleRendererSize;//修改尺寸
            //        sRenderer.Symbol = sSymbol;
            //        sLayer.Renderer = sRenderer;
            //        mapControl.RedrawMap();
            //    }
            //    //唯一值渲染
            //    else if (Render.mPolylineRendererMode == 1)
            //    {
            //        moUniqueValueRenderer sRenderer = new moUniqueValueRenderer();
            //        sRenderer.Field = sLayer.AttributeFields.GetItem(Render.mPolylineUniqueFieldIndex).Name;
            //        List<string> sValues = new List<string>();
            //        Int32 sFeatrueCount = sLayer.Features.Count;
            //        for (Int32 i = 0; i < sFeatrueCount; i++)
            //        {
            //            string sValue = Convert.ToString(sLayer.Features.GetItem(i).Attributes.GetItem(Render.mPolylineUniqueFieldIndex));
            //            sValues.Add(sValue);
            //        }
            //        //去除重复
            //        sValues = sValues.Distinct().ToList();
            //        //生成符号
            //        Int32 sValueCount = sValues.Count;
            //        for (Int32 i = 0; i < sValueCount; i++)
            //        {
            //            moSimpleLineSymbol sSymbol = new moSimpleLineSymbol();
            //            sSymbol.Style = (moSimpleLineSymbolStyleConstant)Render.mPolylineSymbolStyle;//修改样式
            //            sSymbol.Size = Render.mPolylineUniqueRendererSize;//修改尺寸
            //            sRenderer.AddUniqueValue(sValues[i], sSymbol);
            //        }
            //        sRenderer.DefaultSymbol = new moSimpleLineSymbol();
            //        sLayer.Renderer = sRenderer;
            //        mapControl.RedrawMap();
            //    }
            //    //分级渲染
            //    else if (Render.mPolylineRendererMode == 2)
            //    {
            //        moClassBreaksRenderer sRenderer = new moClassBreaksRenderer();
            //        sRenderer.Field = sLayer.AttributeFields.GetItem(Render.mPolylineClassBreaksFieldIndex).Name;
            //        List<double> sValues = new List<double>();
            //        Int32 sFeatrueCount = sLayer.Features.Count;
            //        Int32 sFieldIndex = sLayer.AttributeFields.FindField(sRenderer.Field);
            //        moValueTypeConstant sValueType = sLayer.AttributeFields.GetItem(sFieldIndex).ValueType;
            //        if (sValueType == moValueTypeConstant.dText)
            //        {
            //            MessageBox.Show(@"该字段不是数值字段，不支持分级渲染！");
            //            return;
            //        }
            //        try
            //        {
            //            for (Int32 i = 0; i < sFeatrueCount; i++)
            //            {
            //                double sValue = Convert.ToDouble(sLayer.Features.GetItem(i).Attributes.GetItem(sFieldIndex));
            //                sValues.Add(sValue);
            //            }
            //        }
            //        catch (Exception)
            //        {
            //            MessageBox.Show(@"该字段不是数值字段，不支持分级渲染！");
            //            return;
            //        }

            //        double sMinValue = sValues.Min();
            //        double sMaxValue = sValues.Max();
            //        for (Int32 i = 0; i < Render.mPolylineClassBreaksNum; i++)
            //        {
            //            double sValue = sMinValue + (sMaxValue - sMinValue) * (i + 1) / Render.mPolylineClassBreaksNum;
            //            moSimpleLineSymbol sSymbol = new moSimpleLineSymbol();
            //            sSymbol.Color = Render.mPolylineClassBreaksRendererColor;
            //            sSymbol.Style = (moSimpleLineSymbolStyleConstant)Render.mPolylineSymbolStyle;
            //            sRenderer.AddBreakValue(sValue, sSymbol);
            //        }
            //        double sMinSize = Render.mPolylineClassBreaksRendererMinSize;
            //        double sMaxSize = Render.mPolylineClassBreaksRendererMaxSize;
            //        sRenderer.RampSize(sMinSize, sMaxSize);
            //        sRenderer.DefaultSymbol = new moSimpleLineSymbol();
            //        sLayer.Renderer = sRenderer;
            //        mapControl.RedrawMap();
            //    }
            //}
            if (sLayer.ShapeType == moGeometryTypeConstant.MultiPolygon)
            {
                PolygonRenderer mPolygonRenderer = new PolygonRenderer(moMapControl1.Layers.GetItem(SelectedLayerIndex));
                mPolygonRenderer.Owner = this;
                mPolygonRenderer.ShowDialog();
                if (Render.mIsInRenderer == false)
                {
                    return;
                }
                //简单渲染
                if (Render.mPolygonRendererMode == 0)
                {
                    moSimpleRenderer sRenderer = new moSimpleRenderer();
                    moSimpleFillSymbol sSymbol = new moSimpleFillSymbol();
                    sSymbol.Color = Render.mPolygonSimpleRendererColor;
                    sRenderer.Symbol = sSymbol;
                    sLayer.Renderer = sRenderer;
                    moMapControl1.RedrawMap();
                }
                //唯一值渲染
                else if (Render.mPolygonRendererMode == 1)
                {
                    moUniqueValueRenderer sRenderer = new moUniqueValueRenderer();
                    sRenderer.Field = sLayer.AttributeFields.GetItem(Render.mPolygonUniqueFieldIndex).Name;
                    List<string> sValues = new List<string>();
                    Int32 sFeatrueCount = sLayer.Features.Count;
                    for (Int32 i = 0; i < sFeatrueCount; i++)
                    {
                        string sValue = Convert.ToString(sLayer.Features.GetItem(i).Attributes.GetItem(Render.mPolygonUniqueFieldIndex));
                        sValues.Add(sValue);
                    }
                    //去除重复
                    sValues = sValues.Distinct().ToList();
                    //生成符号
                    Int32 sValueCount = sValues.Count;
                    for (Int32 i = 0; i <= sValueCount - 1; i++)
                    {
                        moSimpleFillSymbol sSymbol = new moSimpleFillSymbol();
                        sRenderer.AddUniqueValue(sValues[i], sSymbol);
                    }
                    sRenderer.DefaultSymbol = new moSimpleFillSymbol();
                    sLayer.Renderer = sRenderer;
                    moMapControl1.RedrawMap();
                }
                //分级渲染
                else if (Render.mPolygonRendererMode == 2)
                {
                    moClassBreaksRenderer sRenderer = new moClassBreaksRenderer();
                    sRenderer.Field = sLayer.AttributeFields.GetItem(Render.mPolygonClassBreaksFieldIndex).Name;
                    List<double> sValues = new List<double>();
                    Int32 sFeatrueCount = sLayer.Features.Count;
                    Int32 sFieldIndex = sLayer.AttributeFields.FindField(sRenderer.Field);
                    moValueTypeConstant sValueType = sLayer.AttributeFields.GetItem(sFieldIndex).ValueType;
                    if (sValueType == moValueTypeConstant.dText)
                    {
                        MessageBox.Show(@"该字段不是数值字段，不支持分级渲染！");
                        return;
                    }
                    try
                    {
                        for (Int32 i = 0; i < sFeatrueCount; i++)
                        {
                            double sValue = Convert.ToDouble(sLayer.Features.GetItem(i).Attributes.GetItem(sFieldIndex));
                            sValues.Add(sValue);
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show(@"该字段不是数值字段，不支持分级渲染！");
                        return;
                    }
                    //获取最小最大值并分5级
                    double sMinValue = sValues.Min();
                    double sMaxValue = sValues.Max();
                    for (Int32 i = 0; i < Render.mPolygonClassBreaksNum; i++)
                    {
                        double sValue = sMinValue + (sMaxValue - sMinValue) * (i + 1) / Render.mPolygonClassBreaksNum;
                        moSimpleFillSymbol sSymbol = new moSimpleFillSymbol();
                        sRenderer.AddBreakValue(sValue, sSymbol);
                    }
                    Color sStartColor = Render.mPolygonClassBreaksRendererStartColor;
                    Color sEndColor = Render.mPolygonClassBreaksRendererEndColor;
                    sRenderer.RampColor(sStartColor, sEndColor);
                    sRenderer.DefaultSymbol = new moSimpleFillSymbol();
                    sLayer.Renderer = sRenderer;
                    moMapControl1.RedrawMap();
                }
            }
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            foreach (TreeNode n in treeView1.Nodes)
            {
                n.BackColor = Color.Empty;
            }
            SelectedLayerIndex = e.Node.Index;
            e.Node.BackColor = Color.LightGray;
        }

        #endregion

        #region 按钮控件点击事件
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
                fileDialog.Dispose();
                return;
            }

            try
            {
                string layerName = Path.GetFileNameWithoutExtension(shpFilePath);
                string layerPath = shpFilePath.Substring(0, shpFilePath.IndexOf(".shp", StringComparison.Ordinal));

                ShapeFileParser fileProcessor = new ShapeFileParser(layerPath);
                moFeatures sFeatures =  fileProcessor.Read_ShapeFile();

                // convert to mapLayer
                moMapLayer mapLayer =
                    new moMapLayer(layerName, fileProcessor.GeometryType, fileProcessor.Fields);

                mapLayer.Features = sFeatures;
                AddLayer(mapLayer, fileProcessor);
            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString());

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
            moMapControl1.FullExtent();
        }

        // 点击固定比例放大按钮
        private void btnFixedZoomIn_Click(object sender, EventArgs e)
        {
            //计算地图空间中心点的地图坐标
            double sY = moMapControl1.ClientRectangle.Width / 2;
            double sX = moMapControl1.ClientRectangle.Height / 2;
            moPoint sPoint = moMapControl1.ToMapPoint(sX, sY);
            moMapControl1.ZoomByCenter(sPoint, mZoomRatioFixed);
        }

        // 点击固定比例缩小按钮
        private void btnFixedZoomOut_Click(object sender, EventArgs e)
        {
            //计算地图空间中心点的地图坐标
            double sY = moMapControl1.ClientRectangle.Width / 2;
            double sX = moMapControl1.ClientRectangle.Height / 2;
            moPoint sPoint = moMapControl1.ToMapPoint(sX, sY);
            moMapControl1.ZoomByCenter(sPoint, 1 / mZoomRatioFixed);
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

        }

        // 点击清除选择按钮
        private void btnClearSelection_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < moMapControl1.Layers.Count; i++)
            {
                moMapLayer sLayer = moMapControl1.Layers.GetItem(i);
                sLayer.SelectedFeatures.Clear();
            }
            moMapControl1.RedrawMap();
        }

        // 点击查询按钮
        private void btnIdentify_Click(object sender, EventArgs e)
        {
            mMapOpStyle = MapOpConstant.Identify;
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


        #endregion

        #region MapControl鼠标移动事件
        // 鼠标在MapControl中移动
        private void moMapControl1_MouseMove(object sender, MouseEventArgs e)
        {
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
                default:
                    break;
            }
        }

        //缩放操作-鼠标移动
        private void OnZoom_MouseMove(MouseEventArgs e)
        {
            if (mIsInZoom == false)
                return;
            moMapControl1.Refresh();
            moRectangle sRect = GetMapRectByTwoPoints(mStartMouseLocation, e.Location);
            moUserDrawingTool sDrawingtool = moMapControl1.GetDrawingTool();
            sDrawingtool.DrawRectangle(sRect, mZoomBoxSymbol);
        }

        //漫游操作-鼠标移动
        private void OnPan_MouseMove(MouseEventArgs e)
        {
            if (mIsInPan == false)
                return;
            moMapControl1.PanMapImageTo(e.Location.X - mStartMouseLocation.X, e.Location.Y - mStartMouseLocation.Y);
        }

        //按位置选择操作-鼠标移动
        private void OnSelectByLocation_MouseMove(MouseEventArgs e)
        {
            if (mIsInSelect == false)
                return;
            moMapControl1.Refresh();
            moRectangle sRect = GetMapRectByTwoPoints(mStartMouseLocation, e.Location);
            moUserDrawingTool sDrawingtool = moMapControl1.GetDrawingTool();
            sDrawingtool.DrawRectangle(sRect, mSelectingBoxSymbol);
        }

        //查询操作-鼠标移动
        private void OnIdentify_MouseMove(MouseEventArgs e)
        {
            if (mIsInIdentify == false) return;
            moMapControl1.Refresh();
            moRectangle sRect = GetMapRectByTwoPoints(mStartMouseLocation, e.Location);
            moUserDrawingTool sDrawingTool = moMapControl1.GetDrawingTool();
            sDrawingTool.DrawRectangle(sRect, mSelectingBoxSymbol);
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
                moPoint sPoint = moMapControl1.ToMapPoint(mStartMouseLocation.X, mStartMouseLocation.Y);
                moMapControl1.ZoomByCenter(sPoint, mZoomRatioFixed);
            }
            else
            {
                //拉框放大
                moRectangle sBox = GetMapRectByTwoPoints(mStartMouseLocation, e.Location);
                moMapControl1.ZoomToExtent(sBox);
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
                moPoint sPoint = moMapControl1.ToMapPoint(mStartMouseLocation.X, mStartMouseLocation.Y);
                moMapControl1.ZoomByCenter(sPoint, 1 / mZoomRatioFixed);
            }
            else
            {
                //拉框缩小
                moRectangle sBox = GetMapRectByTwoPoints(mStartMouseLocation, e.Location);
                moMapControl1.ZoomToExtent(sBox);
            }
        }

        //漫游操作-鼠标松开
        private void OnPan_MouseUp(MouseEventArgs e)
        {
            if (mIsInPan == false)
                return;
            mIsInPan = false;
            double sDeltaX = moMapControl1.ToMapDistance(e.Location.X - mStartMouseLocation.X);
            double sDeltaY = moMapControl1.ToMapDistance(mStartMouseLocation.Y - e.Location.Y);
            moMapControl1.PanDelta(sDeltaX, sDeltaY);
        }

        




        //选择操作-鼠标松开
        private void OnSelectByLocation_MouseUp(MouseEventArgs e)
        {
            if (mIsInSelect == false)
                return;
            mIsInSelect = false;
            moRectangle sBox = GetMapRectByTwoPoints(mStartMouseLocation, e.Location);
            double tolerance = moMapControl1.ToMapDistance(mSelectingTolerance);
            moMapControl1.SelectByBox(sBox, tolerance, 0);
            moMapControl1.RedrawTrackingShapes();
        }

        //查询操作-鼠标松开
        private void OnIdentify_MouseUp(MouseEventArgs e)
        {
            if (mIsInIdentify == false) return;
            mIsInIdentify = false;
            moMapControl1.Refresh();
            moRectangle sBox = GetMapRectByTwoPoints(mStartMouseLocation, e.Location);
            double tolerance = moMapControl1.ToMapDistance(mSelectingTolerance);
            if (moMapControl1.Layers.Count == 0)
                return;
            else
            {
                moMapLayer sLayer = moMapControl1.Layers.GetItem(0);
                moFeatures sFeatures = sLayer.SearchByBox(sBox, tolerance);
                int sSelFeatureCount = sFeatures.Count;
                if (sSelFeatureCount > 0)
                {
                    moGeometry[] sGeometries = new moGeometry[sSelFeatureCount];
                    for (int i = 0; i <= sSelFeatureCount - 1; i++)
                        sGeometries[i] = sFeatures.GetItem(i).Geometry;
                    moMapControl1.FlashShapes(sGeometries, 3, 800);
                }
            }
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
                default:
                    break;
            }
        }

        //放大操作-鼠标单击
        private void OnZoomIn_MouseClick(MouseEventArgs e)
        {
            //单点放大
            moPoint sPoint = moMapControl1.ToMapPoint(e.Location.X, e.Location.Y);
            moMapControl1.ZoomByCenter(sPoint, mZoomRatioFixed);
        }

        //缩小操作-鼠标单击
        private void OnZoomOut_MouseClick(MouseEventArgs e)
        {
            //单点缩小
            moPoint sPoint = moMapControl1.ToMapPoint(e.Location.X, e.Location.Y);
            moMapControl1.ZoomByCenter(sPoint, 1 / mZoomRatioFixed);
        }
        #endregion

        #region MapControl其他事件处理（含自定义事件）
        // MapControl鼠标滑轮
        private void moMapControl1_MouseWheel(object sender, MouseEventArgs e)
        {
            //计算地图空间中心点的地图坐标
            double sY = moMapControl1.ClientRectangle.Width / 2;
            double sX = moMapControl1.ClientRectangle.Height / 2;
            moPoint sPoint = moMapControl1.ToMapPoint(sX, sY);
            if (e.Delta > 0)
            {
                moMapControl1.ZoomByCenter(sPoint, mZoomRatioMouseWheel);
            }
            else
            {
                moMapControl1.ZoomByCenter(sPoint, 1 / mZoomRatioMouseWheel);
            }
        }

        // 地图比例尺发生了变化
        private void moMap_MapScaleChanged(object sender)
        {
            ShowMapScale();
        }

        // 地图控件绘制完毕
        private void moMap_AfterTrackingLayerDraw(object sender, moUserDrawingTool drawTool)
        {
            DrawSketchingShapes(drawTool);
            DrawEditingShapes(drawTool);
        }

        #endregion


        #endregion

        #endregion

        #region 方法

        #region 图层操作
        // 根据.shp文件的路径添加图层到当前地图
        public void AddLayer(moMapLayer mapLayer, object shapefile)
        {

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
        }

        // 根据屏幕坐标显示地图坐标
        private void ShowCoordinates(PointF point)
        {
            moPoint sPoint = moMapControl1.ToMapPoint(point.X, point.Y);
            if (mShowLngLat == false)
            {
                double sX = Math.Round(sPoint.X, 2);
                double sY = Math.Round(sPoint.Y, 2);
                coordinateStatusLabel.Text = "X=" + sX.ToString() + ", Y=" + sY.ToString();
            }
            else
            {
                moPoint sLngLat = moMapControl1.ProjectionCS.TransferToLngLat(sPoint);
                double sX = Math.Round(sLngLat.X, 6);
                double sY = Math.Round(sLngLat.Y, 6);
                coordinateStatusLabel.Text = "X=" + sX.ToString() + ", Y=" + sY.ToString();
            }
        }

        // 显示地图比例尺
        private void ShowMapScale()
        {
            MapScaleButton.Text = "1:" + moMapControl1.MapScale.ToString("0.00");
        }

        private moRectangle GetMapRectByTwoPoints(PointF point1, PointF point2)
        {
            moPoint sPoint1 = moMapControl1.ToMapPoint(point1.X, point1.Y);
            moPoint sPoint2 = moMapControl1.ToMapPoint(point2.X, point2.Y);
            double sMinX = Math.Min(sPoint1.X, sPoint2.X);
            double sMaxX = Math.Max(sPoint1.X, sPoint2.X);
            double sMinY = Math.Min(sPoint1.Y, sPoint2.Y);
            double sMaxY = Math.Max(sPoint1.Y, sPoint2.Y);
            moRectangle sRect = new moRectangle(sMinX, sMaxX, sMinY, sMaxY);
            return sRect;
        }

        //查找一个多边形图层
        private moMapLayer GetPolyonLayer()
        {
            Int32 sLayerCount = moMapControl1.Layers.Count;
            moMapLayer sLayer = null;
            for (Int32 i = 0; i <= sLayerCount - 1; i++)
            {
                if (moMapControl1.Layers.GetItem(i).ShapeType == moGeometryTypeConstant.MultiPolygon)
                {
                    sLayer = moMapControl1.Layers.GetItem(i);
                    break;
                }
            }
            return sLayer;
        }

        //修改移动图形的坐标
        private void ModifyMovingGeometries(double deltaX, double deltaY)
        {
            Int32 sCount = mMovingGeometries.Count;
            for (Int32 i = 0; i <= sCount - 1; i++)
            {
                if (mMovingGeometries[i].GetType() == typeof(moMultiPolygon))
                {
                    moMultiPolygon sMultiPolygon = (moMultiPolygon)mMovingGeometries[i];
                    Int32 sPartCount = sMultiPolygon.Parts.Count;
                    for (Int32 j = 0; j <= sPartCount - 1; j++)
                    {
                        moPoints sPoints = sMultiPolygon.Parts.GetItem(j);
                        Int32 sPointCount = sPoints.Count;
                        for (Int32 k = 0; k <= sPointCount - 1; k++)
                        {
                            moPoint sPoint = sPoints.GetItem(k);
                            sPoint.X = sPoint.X + deltaX;
                            sPoint.Y = sPoint.Y + deltaY;
                        }
                    }
                    sMultiPolygon.UpdateExtent();
                }
            }
        }

        //绘制移动图形
        private void DrawMovingShapes()
        {
            moUserDrawingTool sDrawingTool = moMapControl1.GetDrawingTool();
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

        //绘制正在描绘的图形
        private void DrawSketchingShapes(moUserDrawingTool drawingTool)
        {
            if (mSketchingShape == null)
                return;
            Int32 sPartCount = mSketchingShape.Count;
            //绘制已经描绘完成的部分
            for (Int32 i = 0; i <= sPartCount - 2; i++)
            {
                drawingTool.DrawPolygon(mSketchingShape[i], mEditingPolygonSymbol);
            }
            //正在描绘的部分（只有一个Part）
            moPoints sLastPart = mSketchingShape.Last();
            if (sLastPart.Count >= 2)
                drawingTool.DrawPolyline(sLastPart, mEditingPolygonSymbol.Outline);
            //绘制所有顶点手柄
            for (Int32 i = 0; i <= sPartCount - 1; i++)
            {
                moPoints sPoints = mSketchingShape[i];
                drawingTool.DrawPoints(sPoints, mEditingVertexSymbol);
            }
        }

        //绘制正在编辑的图形
        private void DrawEditingShapes(moUserDrawingTool drawingTool)
        {
            if (mEditingGeometry == null)
                return;
            if (mEditingGeometry.GetType() == typeof(moMultiPolygon))
            {
                moMultiPolygon sMultiPolygon = (moMultiPolygon)mEditingGeometry;
                //绘制边界
                drawingTool.DrawMultiPolygon(sMultiPolygon, mEditingPolygonSymbol);
                //绘制顶点手柄
                Int32 sPartCount = sMultiPolygon.Parts.Count;
                for (Int32 i = 0; i <= sPartCount - 1; i++)
                {
                    moPoints sPoints = sMultiPolygon.Parts.GetItem(i);
                    drawingTool.DrawPoints(sPoints, mEditingVertexSymbol);
                }
            }
        }
        public void AddLayer(moMapLayer mapLayer, ShapeFileParser shapefile)
        {
            moMapControl1.Layers.Add(mapLayer);
            mShapefiles.Add(shapefile);
            //treeView1.Nodes.Add(mapLayer.Name);
            RefreshLayersTree();
            if(moMapControl1.Layers.Count ==1)
                moMapControl1.FullExtent();
            else
                moMapControl1.RedrawMap();
        }


        private void RefreshLayersTree()
        {
            treeView1.Nodes.Clear();
            for (int i = 0; i < moMapControl1.Layers.Count; i++)
            {
                TreeNode layerNode = new TreeNode
                {
                    Text = moMapControl1.Layers.GetItem(i).Name,
                    Checked = moMapControl1.Layers.GetItem(i).Visible,
                    ContextMenuStrip = LayerRightMenu
                };
                treeView1.Nodes.Add(layerNode);
            }
            treeView1.Refresh();
        }





        #endregion


    }
}
