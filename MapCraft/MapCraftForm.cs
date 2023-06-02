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
using MapCraft.IO;

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
        private double mSelectingTolerance = 3;             // 选择容限，像素
        private moSimpleFillSymbol mSelectingBoxSymbol;     // 选择盒符号
        private moSimpleFillSymbol mZoomBoxSymbol;          // 缩放盒符号
        private moSimpleFillSymbol mMovingPolygonSymbol;    // 正在移动的多边形的符号
        private moSimpleFillSymbol mEditingPolygonSymbol;   // 正在编辑的多边形的符号
        private moSimpleMarkerSymbol mEditingVertexSymbol;  // 正在编辑的图形的顶点的符号
        private moSimpleLineSymbol mElasticSymbol;          // 橡皮筋符号
        private bool mShowLngLat = false;                   // 是否显示经纬度

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
        private List<Shapefile> mShapefiles = new List<Shapefile>();

        #endregion

        #region 属性
        /// <summary>
        /// 地图控件，子窗体可使用
        /// </summary>
        public moMapControl MapControl
        {
            get { return moMapControl1; }
            set { moMapControl1 = value; }
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

        // 是否显示经纬度（地理坐标）
        private void cbxProjectionCS_CheckedChanged(object sender, EventArgs e)
        {
            mShowLngLat = cbxProjectionCS.Checked;
        }

        #region 按钮控件点击事件
        // 点击添加图层按钮
        private void btnAddData_Click(object sender, EventArgs e)
        {
            CreateLayerForm createLayerForm = new CreateLayerForm();
            createLayerForm.Owner = this;
            createLayerForm.Show();
            //string shpFilePath;
            //OpenFileDialog fileDialog = new OpenFileDialog();
            //fileDialog.Filter = ".shp文件|*.shp";
            //if (fileDialog.ShowDialog() == DialogResult.OK)
            //{
            //    shpFilePath = fileDialog.FileName;
            //    fileDialog.Dispose();
            //}
            //else
            //{
            //    fileDialog.Dispose();
            //    return;
            //}
            //AddLayer(shpFilePath);
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
            SelectByAttributeForm sSelectByAttributeForm = new SelectByAttributeForm(this);
            sSelectByAttributeForm.Show();

        }

        // 点击清除选择按钮
        private void btnClearSelection_Click(object sender, EventArgs e)
        {
            // 清除每个图层选中的要素
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

        #region 图层操作
        // 根据.shp文件的路径添加图层到当前地图
        private void AddLayer(string shpFilePath, string layerName="")
        {
            try
            {
                string shpName = Path.GetFileNameWithoutExtension(shpFilePath);
                shpFilePath.ToLower();
                string layerPath = shpFilePath.Substring(0, shpFilePath.IndexOf(".shp", StringComparison.Ordinal));

                //Shapefile shapefile = new Shapefile(layerPath);
                //shapefile.Read();


                //moMapLayer mapLayer = new moMapLayer(shpName, shapefile.GeometryType, shapefile.Fields);

                //moFeatures features = new moFeatures();
                //for (int i = 0; i < shapefile.Geometries.Count; ++i)
                //{
                //    moFeature feature = new moFeature(shapefile.GeometryType, shapefile.Geometries[i], shapefile.AttributesList[i]);
                //    features.Add(feature);
                //}

                //mapLayer.Features = features;
                //MapControl.Layers.Add(mapLayer);
                //mShapefiles.Add(shapefile);
            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString());
            }
        }
        #endregion

        #endregion

    }
}
