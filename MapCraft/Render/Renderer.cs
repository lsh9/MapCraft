using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapCraft.Render
{
    public class Renderer
    {
        #region 字段

        internal bool mIsInRenderer = false;

        internal int mPointRendererMode = 0; //渲染方式,0:简单渲染,1:唯一值渲染,2:分级渲染
        internal int mPointSymbolStyle = 0; //样式索引
        internal Color mPointSimpleRendererColor = Color.LightCoral; //符号颜色
        internal double mPointSimpleRendererSize = 5; //符号尺寸
        internal int mPointUniqueFieldIndex = 0; //绑定字段索引
        internal double mPointUniqueRendererSize = 5; //符号尺寸
        internal int mPointClassBreaksFieldIndex = 0; //绑定字段索引
        internal int mPointClassBreaksNum = 5; //分类数
        internal Color mPointClassBreaksRendererColor = Color.LightCoral; //符号颜色
        internal double mPointClassBreaksRendererMinSize = 3; //符号起始尺寸,点图层采用符号尺寸进行分级表示
        internal double mPointClassBreaksRendererMaxSize = 6; //符号终止尺寸

        internal int mPolylineRendererMode = 0; //渲染方式,0:简单渲染,1:唯一值渲染,2:分级渲染
        internal int mPolylineSymbolStyle = 0; //样式索引
        internal Color mPolylineSimpleRendererColor = Color.LightCoral; //符号颜色
        internal double mPolylineSimpleRendererSize = 0.5; //符号尺寸
        internal int mPolylineUniqueFieldIndex = 0; //绑定字段索引
        internal double mPolylineUniqueRendererSize = 0.5; //符号尺寸
        internal int mPolylineClassBreaksFieldIndex = 0; //绑定字段索引
        internal int mPolylineClassBreaksNum = 5; //分类数
        internal Color mPolylineClassBreaksRendererColor = Color.LightCoral; //符号颜色
        internal double mPolylineClassBreaksRendererMinSize = 0.5; //符号起始尺寸,线图层采用符号尺寸进行分级表示
        internal double mPolylineClassBreaksRendererMaxSize = 1.5; //符号终止尺寸

        internal int mPolygonRendererMode = 0; //渲染方式,0:简单渲染,1:唯一值渲染,2:分级渲染
        internal Color mPolygonSimpleRendererColor = Color.LightCoral; //符号颜色
        internal int mPolygonUniqueFieldIndex = 0; //绑定字段索引
        internal int mPolygonClassBreaksFieldIndex = 0; //绑定字段索引
        internal int mPolygonClassBreaksNum = 5; //分类数
        internal Color mPolygonClassBreaksRendererStartColor = Color.LightGreen; //符号起始颜色,面图层采用符号颜色进行分级表示
        internal Color mPolygonClassBreaksRendererEndColor = Color.LightCoral; //符号终止颜色

        #endregion

        #region 构造函数

        public Renderer()
        {

        }



        #endregion
        public void GetPointRenderer(Int32 renderMode, Int32 symbolStyle, Color simpleRendererColor, Double simpleRendererSize,
            Int32 uniqueFieldIndex, Double uniqueRendererSize, Int32 classBreakFieldIndex, Int32 classNum,
            Color classBreakRendererColor, double classBreakRendererMinSize, double classBreakRendererMaxSize)
        {
            mPointRendererMode = renderMode;
            mPointSymbolStyle = symbolStyle;
            mPointSimpleRendererColor = simpleRendererColor;
            mPointSimpleRendererSize = simpleRendererSize;
            mPointUniqueFieldIndex = uniqueFieldIndex;
            mPointUniqueRendererSize = uniqueRendererSize;
            mPointClassBreaksFieldIndex = classBreakFieldIndex;
            mPointClassBreaksNum = classNum;
            mPointClassBreaksRendererColor = classBreakRendererColor;
            mPointClassBreaksRendererMinSize = classBreakRendererMinSize;
            mPointClassBreaksRendererMaxSize = classBreakRendererMaxSize;
            mIsInRenderer = true;
        }

        public void GetPolylineRenderer(Int32 renderMode, Int32 symbolStyle, Color simpleRendererColor, Double simpleRendererSize,
            Int32 uniqueFieldIndex, Double uniqueRendererSize, Int32 classBreakFieldIndex, Int32 classNum,
            Color classBreakRendererColor, double classBreakRendererMinSize, double classBreakRendererMaxSize)
        {
            mPolylineRendererMode = renderMode;
            mPolylineSymbolStyle = symbolStyle;
            mPolylineSimpleRendererColor = simpleRendererColor;
            mPolylineSimpleRendererSize = simpleRendererSize;
            mPolylineUniqueFieldIndex = uniqueFieldIndex;
            mPolylineUniqueRendererSize = uniqueRendererSize;
            mPolylineClassBreaksFieldIndex = classBreakFieldIndex;
            mPolylineClassBreaksNum = classNum;
            mPolylineClassBreaksRendererColor = classBreakRendererColor;
            mPolylineClassBreaksRendererMinSize = classBreakRendererMinSize;
            mPolylineClassBreaksRendererMaxSize = classBreakRendererMaxSize;
            mIsInRenderer = true;
        }

        public void GetPolygonRenderer(Int32 renderMode, Color simpleRendererColor,
            Int32 uniqueFieldIndex, Int32 classBreakFieldIndex, Int32 classNum,
            Color classBreakRendererStartColor, Color classBreakRendererEndColor)
        {
            mPolygonRendererMode = renderMode;
            mPolygonSimpleRendererColor = simpleRendererColor;
            mPolygonUniqueFieldIndex = uniqueFieldIndex;
            mPolygonClassBreaksFieldIndex = classBreakFieldIndex;
            mPolygonClassBreaksNum = classNum;
            mPolygonClassBreaksRendererStartColor = classBreakRendererStartColor;
            mPolygonClassBreaksRendererEndColor = classBreakRendererEndColor;
            mIsInRenderer = true;
        }

    }
}
