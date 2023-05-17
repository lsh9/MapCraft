using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMapObjects
{
    public class moPoints
    {
        #region 字段
        private List<moPoint> _Points;
        private double _MinX = double.MaxValue, _MaxX = double.MinValue;
        private double _MinY = double.MaxValue, _MaxY = double.MinValue;
        #endregion

        #region 构造函数
        public moPoints()
        {
            _Points = new List<moPoint>();
        }

        public moPoints(moPoint[] points)
        {
            _Points = new List<moPoint>();
            _Points.AddRange(points);
        }

        #endregion

        #region 属性
        /// <summary>
        /// 获取元素个数
        /// </summary>
        public Int32 Count
        {
            get { return _Points.Count; }
        }

        public double MinX
        {
            get { return _MinX; }
        }

        public double MaxX
        {
            get { return _MaxX; }
        }

        public double MinY
        {
            get { return _MinY; }
        }

        public double MaxY
        {
            get { return _MaxY; }
        }
        #endregion


        #region 方法
        public moPoint this[Int32 index]
        {
            get { return _Points[index]; }
            set { _Points[index] = value; }
        }

        /// <summary>
        /// 获取指定索引号的元素
        /// <param name="index">索引号</param>
        /// <returns>指定索引号的元素</returns>
        /// </summary>
        public moPoint GetItem(Int32 index)
        {
            return _Points[index];
        }

        /// <summary>
        /// 在末尾增加一个元素
        /// <param name="point">元素</param>
        /// </summary>
        public void Add(moPoint point)
        {
            _Points.Add(point);
            if (point.X < _MinX) _MinX = point.X;
            if (point.X > _MaxX) _MaxX = point.X;
            if (point.Y < _MinY) _MinY = point.Y;
            if (point.Y > _MaxY) _MaxY = point.Y;
        }

        /// <summary>
        /// 将指定数组中的元素添加到末尾
        /// <param name="points">元素数组</param>
        /// </summary>
        public void AddRange(moPoint[] points)
        {
            _Points.AddRange(points);
            foreach (moPoint point in points)
            {
                if (point.X < _MinX) _MinX = point.X;
                if (point.X > _MaxX) _MaxX = point.X;
                if (point.Y < _MinY) _MinY = point.Y;
                if (point.Y > _MaxY) _MaxY = point.Y;
            }
        }

        /// <summary>
        /// 插入一个元素到指定位置
        /// <param name="index">指定位置</param>
        /// <param name="point">元素</param>
        /// </summary>
        public void Insert(Int32 index, moPoint point)
        {
            _Points.Insert(index, point);
            if (point.X < _MinX) _MinX = point.X;
            if (point.X > _MaxX) _MaxX = point.X;
            if (point.Y < _MinY) _MinY = point.Y;
            if (point.Y > _MaxY) _MaxY = point.Y;
        }

        /// <summary>
        /// 插入一个数组中的元素到指定位置
        /// <param name="index">指定位置</param>
        /// <param name="points">元素数组</param>
        /// </summary>
        public void InsertRange(Int32 index, moPoint[] points)
        {
            _Points.InsertRange(index, points);
            foreach (moPoint point in points)
            {
                if (point.X < _MinX) _MinX = point.X;
                if (point.X > _MaxX) _MaxX = point.X;
                if (point.Y < _MinY) _MinY = point.Y;
                if (point.Y > _MaxY) _MaxY = point.Y;
            }
        }

        /// <summary>
        /// 删除指定索引号的元素
        /// <param name="index">索引号</param>
        /// </summary>
        public void RemoveAt(Int32 index)
        {
            _Points.RemoveAt(index);
        }

        /// <summary>
        /// 删除所有元素
        /// </summary>
        public void Clear()
        {
            _Points.Clear();
        }

        /// <summary>
        /// 获取元素数组
        /// </summary>
        /// <returns>元素数组</returns>
        public moPoint[] ToArray()
        {
            return _Points.ToArray();
        }

        /// <summary>
        /// 获取外包矩形
        /// </summary>
        /// <returns>外包矩形</returns>
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

        // 计算范围
        private void CalExtent()
        {
            _MinX = double.MaxValue;
            _MaxX = double.MinValue;
            _MinY = double.MaxValue;
            _MaxY = double.MinValue;
            foreach (moPoint point in _Points)
            {
                if (point.X < _MinX) _MinX = point.X;
                if (point.X > _MaxX) _MaxX = point.X;
                if (point.Y < _MinY) _MinY = point.Y;
                if (point.Y > _MaxY) _MaxY = point.Y;
            }
        }

        /// <summary>
        /// 克隆
        /// </summary>
        /// <returns>新的moPoints对象</returns>
        public moPoints Clone()
        {
            moPoints points = new moPoints();
            foreach (moPoint point in _Points)
            {
                points.Add(point.Clone());
            }
            points._MinX = _MinX;
            points._MaxX = _MaxX;
            points._MinY = _MinY;
            points._MaxY = _MaxY;
            return points;
        }

        #endregion

    }
}
