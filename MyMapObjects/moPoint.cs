using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMapObjects
{
    public class moPoint : moGeometry
    {
        #region 字段
        private double _X;
        private double _Y;
        #endregion

        #region 构造函数
        public moPoint() { }
        public moPoint(double x, double y)
        {
            _X = x;
            _Y = y;
        }
        #endregion

        #region 属性
        public double X
        {
            get { return _X; }
            set { _X = value; }
        }
        public double Y
        {
            get { return _Y; }
            set { _Y = value; }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 克隆
        /// </summary>
        /// <returns>新对象</returns>
        public moPoint Clone() 
        {
            return new moPoint(X, Y); 
        }
        #endregion
    }
}
