using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMapObjects
{
    public class moMultiPolygon : moGeometry
    {
        #region 字段
        private moParts _Parts;
        double _MinX = double.MaxValue, _MaxX = double.MinValue;
        double _MinY = double.MaxValue, _MaxY = double.MinValue;
        #endregion

        #region 构造函数
        public moMultiPolygon()
        {
            _Parts = new moParts();
        }

        public moMultiPolygon(moPoints[] parts)
        {
            _Parts = new moParts(parts);
        }

        public moMultiPolygon(moPoints points)
        {
            _Parts = new moParts();
            _Parts.Add(points);
        }

        public moMultiPolygon(moParts parts)
        {
            _Parts = parts;
        }
        #endregion

        #region 属性
        /// <summary>
        /// 获取或设置部件集合
        /// </summary>
        public moParts Parts
        {
            get { return _Parts; }
            set { _Parts = value; }
        }

        /// <summary>
        /// 获取最小X坐标
        /// </summary>
        public double MinX
        {
            get { return _MinX; }
        }

        /// <summary>
        /// 获取最大X坐标
        /// </summary>
        public double MaxX
        {
            get { return _MaxX; }
        }

        /// <summary>
        /// 获取最小y坐标
        /// </summary>
        public double MinY
        {
            get { return _MinY; }
        }

        /// <summary>
        /// 获取最大y坐标
        /// </summary>
        public double MaxY
        {
            get { return _MaxY; }
        }

        #endregion

        #region 方法
        /// <summary>
        /// 获取外包矩形
        /// </summary>
        /// <returns></returns>
        public moRectangle GetEnvelope()
        {
            return new moRectangle(_MinX, _MaxX, _MinY, _MaxY);
        }

        /// <summary>
        /// 更新外包矩形
        /// </summary>
        public void UpdateExtent()
        {
            CalExtent();
        }

        /// <summary>
        /// 克隆
        /// </summary>
        /// <returns>新对象</returns>
        public moMultiPolygon Clone()
        {
            moMultiPolygon multiPolygon = new moMultiPolygon();
            multiPolygon._Parts = _Parts.Clone();
            multiPolygon._MinX = _MinX;
            multiPolygon._MaxX = _MaxX;
            multiPolygon._MinY = _MinY;
            multiPolygon._MaxY = _MaxY;
            return multiPolygon;
        }

        #endregion

        #region 私有方法
        private void CalExtent()
        {
            _MinX = double.MaxValue;
            _MaxX = double.MinValue;
            _MinY = double.MaxValue;
            _MaxY = double.MinValue;
            for (int i = 0; i < _Parts.Count; i++)
            {
                moPoints points = _Parts[i];
                for (int j = 0; j < points.Count; j++)
                {
                    moPoint point = points[j];
                    if (point.X < _MinX)
                        _MinX = point.X;
                    if (point.X > _MaxX)
                        _MaxX = point.X;
                    if (point.Y < _MinY)
                        _MinY = point.Y;
                    if (point.Y > _MaxY)
                        _MaxY = point.Y;
                }
            }
        }

        #endregion


    }
}
