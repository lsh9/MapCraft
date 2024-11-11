using MyMapObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MapCraft.Forms
{
    public partial class TopoCheck : Form
    {

        #region 字段
        public MapCraftForm Main { get; }
        public int LayerIndex { get; }
        public moMapLayer Layer { get; set; }
        #endregion

        #region 构造函数
        public TopoCheck(MapCraftForm main, int index)
        {
            InitializeComponent();

            Main = main;
            LayerIndex = index;
            Layer = main.MapControl.Layers.GetItem(index);

            lblLayerName.Text = Layer.Name;
            moGeometryTypeConstant typeConstant = Layer.Features[0].ShapeType;
            cbTopoRule.Items.Clear();
            string typeStr = "";
            switch (typeConstant)
            {
                case moGeometryTypeConstant.None:
                    typeStr = "Null";
                    cbTopoRule.Enabled = false;
                    break;
                case moGeometryTypeConstant.Point:
                    typeStr = "Point";
                    cbTopoRule.Items.Add("点不能重合");
                    break;
                case moGeometryTypeConstant.MultiPolyline:
                    typeStr = "PolyLine";
                    cbTopoRule.Items.Add("线之间不能相交");
                    cbTopoRule.Items.Add("线不能自相交");
                    break;
                case moGeometryTypeConstant.MultiPolygon:
                default:
                    typeStr = "Polygon";
                    cbTopoRule.Items.Add("面之间不能相交");
                    cbTopoRule.Items.Add("面不能自相交");
                    break;
            }
            lblLayerType.Text = typeStr;
        }
        #endregion

        private void labelLayers_Click(object sender, EventArgs e)
        {

        }

        private void SelectOverlapPoints(double tolerance)
        {
            Layer.SelectedFeatures.Clear();
            moFeatures features = Layer.Features;
            List<int> overlapPointIDs = new List<int>();
            for (int i = 0; i < features.Count; i++)
            {
                for (int j = i + 1; j < features.Count; j++)
                {
                    if (overlapPointIDs.Contains(i) && overlapPointIDs.Contains(j))
                        continue;
                    moPoint point1 = (moPoint)features[i].Geometry;
                    moPoint point2 = (moPoint)features[j].Geometry;
                    double dist = Math.Sqrt(Math.Pow(point1.X - point2.X, 2) + Math.Pow(point1.Y - point2.Y, 2));
                    if (dist < tolerance)
                    {
                        if (!overlapPointIDs.Contains(i))
                            overlapPointIDs.Add(i);
                        if (!overlapPointIDs.Contains(j))
                            overlapPointIDs.Add(j);
                    }
                }
            }
            foreach (int overlapPointID in overlapPointIDs)
            {
                Layer.SelectedFeatures.Add(Layer.Features.GetItem(overlapPointID));
            }
            Main.MapControl.RedrawTrackingShapes();
        }

        private bool IsLineSegmentIntersect(moPoint pt1, moPoint pt2, moPoint pt3, moPoint pt4, double tolerance)
        {
            // 判断线段是否相交 line1:pt1-pt2   line2:pt3-pt4
            // step1 快速排斥实验 判断投影是否有重合
            if (Math.Max(pt1.X, pt2.X) < Math.Min(pt3.X, pt4.X) ||
                Math.Max(pt3.X, pt4.X) < Math.Min(pt1.X, pt2.X) ||
                Math.Max(pt1.Y, pt2.Y) < Math.Min(pt3.Y, pt4.Y) ||
                Math.Max(pt3.Y, pt4.Y) < Math.Min(pt1.Y, pt2.Y))
                return false;   // 不可能相交

            // step2 跨立实验 通过叉积判断相交
            double cross1 = ((pt1.X - pt3.X) * (pt4.Y - pt3.Y) - (pt1.Y - pt3.Y) * (pt4.X - pt3.X));
            double cross2 = ((pt2.X - pt3.X) * (pt4.Y - pt3.Y) - (pt2.Y - pt3.Y) * (pt4.X - pt3.X));
            double cross3 = ((pt3.X - pt1.X) * (pt2.Y - pt1.Y) - (pt3.Y - pt1.Y) * (pt2.X - pt1.X));
            double cross4 = ((pt4.X - pt1.X) * (pt2.Y - pt1.Y) - (pt4.Y - pt1.Y) * (pt2.X - pt1.X));

            // if (cross1 * cross2 > 0 || cross3 * cross4 > 0) // 未引入tolerance
            if (Math.Sign(cross1) == Math.Sign(cross2) || Math.Sign(cross3) == Math.Sign(cross4))
                return false;   // 未相交
            else if (Math.Abs(cross1) < tolerance || 
                Math.Abs(cross2) < tolerance ||
                Math.Abs(cross3) < tolerance ||
                Math.Abs(cross4) < tolerance)
                return false;   // cross小于容差

            return true;    // 相交且大于容差
        }

        private void SelectOverlapLines(double tolerance)
        {
            Layer.SelectedFeatures.Clear();
            moFeatures features = Layer.Features;
            List<int> overlapIDs = new List<int>();
            for (int i = 0; i < features.Count; i++)
            {
                for (int j = i + 1; j < features.Count; j++)
                {
                    if (overlapIDs.Contains(i) && overlapIDs.Contains(j))
                        continue;
                    moMultiPolyline polyline1 = (moMultiPolyline)features[i].Geometry;
                    moMultiPolyline polyline2 = (moMultiPolyline)features[j].Geometry;
                    bool isIntersecting = false;
                    for (int m = 0; m < polyline1.Parts[0].Count - 1; m++)
                    {
                        for (int n = 0; n < polyline2.Parts[0].Count - 1; n++)
                        {
                            if (IsLineSegmentIntersect(polyline1.Parts[0][m], polyline1.Parts[0][m + 1], 
                                polyline2.Parts[0][n], polyline2.Parts[0][n + 1],
                                tolerance))
                            {
                                isIntersecting = true;
                                continue;
                            }
                        }
                        if (isIntersecting)
                        {
                            if (!overlapIDs.Contains(i))
                                overlapIDs.Add(i);
                            if (!overlapIDs.Contains(j))
                                overlapIDs.Add(j);
                            break;
                        }
                    }
                }
            }
            foreach (int overlapPointID in overlapIDs)
            {
                Layer.SelectedFeatures.Add(Layer.Features.GetItem(overlapPointID));
            }
            Main.MapControl.RedrawTrackingShapes();
        }

        private void SelectSelfOverlapLines(double tolerance)
        {
            Layer.SelectedFeatures.Clear();
            moFeatures features = Layer.Features;
            List<int> selfOverlapIDs = new List<int>();
            for (int i = 0; i < features.Count; i++)
            {
                if (selfOverlapIDs.Contains(i))
                    continue;
                moMultiPolyline polyline1 = (moMultiPolyline)features[i].Geometry;
                bool isIntersecting = false;
                for (int m = 0; m < polyline1.Parts[0].Count - 1; m++)
                {
                    for (int n = m + 2; n < polyline1.Parts[0].Count - 1; n++)
                    {
                        if (IsLineSegmentIntersect(polyline1.Parts[0][m], polyline1.Parts[0][m + 1],
                            polyline1.Parts[0][n], polyline1.Parts[0][n + 1],
                            tolerance))
                        {
                            isIntersecting = true;
                            continue;
                        }
                    }
                    if (isIntersecting)
                    {
                        if (!selfOverlapIDs.Contains(i))
                            selfOverlapIDs.Add(i);
                        break;
                    }
                }
                
            }
            foreach (int overlapPointID in selfOverlapIDs)
            {
                Layer.SelectedFeatures.Add(Layer.Features.GetItem(overlapPointID));
            }
            Main.MapControl.RedrawTrackingShapes();
        }

        private void SelectOverlapPolygons(double tolerance)
        {
            Layer.SelectedFeatures.Clear();
            moFeatures features = Layer.Features;
            List<int> overlapIDs = new List<int>();
            for (int i = 0; i < features.Count; i++)
            {
                for (int j = i + 1; j < features.Count; j++)
                {
                    if (overlapIDs.Contains(i) && overlapIDs.Contains(j))
                        continue;
                    moMultiPolygon polygon1 = (moMultiPolygon)features[i].Geometry;
                    moMultiPolygon polygon2 = (moMultiPolygon)features[j].Geometry;
                    bool isIntersecting = false;
                    for (int m = 0; m < polygon1.Parts[0].Count - 1; m++)
                    {
                        for (int n = 0; n < polygon2.Parts[0].Count - 1; n++)
                        {
                            if (IsLineSegmentIntersect(polygon1.Parts[0][m], polygon1.Parts[0][m + 1], 
                                polygon2.Parts[0][n], polygon2.Parts[0][n + 1],
                                tolerance))
                            {
                                isIntersecting = true;
                                continue;
                            }
                        }
                        if (isIntersecting)
                        {
                            if (!overlapIDs.Contains(i))
                                overlapIDs.Add(i);
                            if (!overlapIDs.Contains(j))
                                overlapIDs.Add(j);
                            break;
                        }
                    }
                }
            }
            foreach (int overlapPointID in overlapIDs)
            {
                Layer.SelectedFeatures.Add(Layer.Features.GetItem(overlapPointID));
            }
            Main.MapControl.RedrawTrackingShapes();
        }

        private void SelectSelfOverlapPolygons(double tolerance)
        {
            Layer.SelectedFeatures.Clear();
            moFeatures features = Layer.Features;
            List<int> selfOverlapIDs = new List<int>();
            for (int i = 0; i < features.Count; i++)
            {
                if (selfOverlapIDs.Contains(i))
                    continue;
                moMultiPolygon polygon0 = (moMultiPolygon)features[i].Geometry;
                bool isIntersecting = false;
                for (int m = 0; m < polygon0.Parts[0].Count - 1; m++)
                {
                    for (int n = m + 2; n < polygon0.Parts[0].Count - 1; n++)
                    {
                        if (IsLineSegmentIntersect(polygon0.Parts[0][m], polygon0.Parts[0][m + 1], 
                            polygon0.Parts[0][n], polygon0.Parts[0][n + 1],
                            tolerance))
                        {
                            isIntersecting = true;
                            continue;
                        }
                    }
                    if (isIntersecting)
                    {
                        if (!selfOverlapIDs.Contains(i))
                            selfOverlapIDs.Add(i);
                        break;
                    }
                }

            }
            foreach (int overlapPointID in selfOverlapIDs)
            {
                Layer.SelectedFeatures.Add(Layer.Features.GetItem(overlapPointID));
            }
            Main.MapControl.RedrawTrackingShapes();
        }
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                string rule = cbTopoRule.Text;
                double tolerance = Convert.ToDouble(txtTolerance.Text.Trim());
                switch (lblLayerType.Text.Trim())
                {
                    case "Point":
                        SelectOverlapPoints(tolerance);
                        break;
                    case "PolyLine":
                        if (cbTopoRule.Text.Trim() == "线之间不能相交")
                            SelectOverlapLines(tolerance);
                        else if (cbTopoRule.Text.Trim() == "线不能自相交")
                            SelectSelfOverlapLines(tolerance);
                        break;
                    case "Polygon":
                        if (cbTopoRule.Text.Trim() == "面之间不能相交")
                            SelectOverlapPolygons(tolerance);
                        else if (cbTopoRule.Text.Trim() == "面不能自相交")
                            SelectSelfOverlapPolygons(tolerance);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void btnGeoDefaultTol_Click(object sender, EventArgs e)
        {
            txtTolerance.Text = 0.000000008983153.ToString();
        }

        private void btnProjDefaultTol_Click(object sender, EventArgs e)
        {
            txtTolerance.Text = 0.0002.ToString();
        }
    }
}
