using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMapObjects
{
    /// <summary>
    /// 属性值集合
    /// </summary>
    public class moAttributes
    {
        #region 字段
        private List<object> _Attributes;
        #endregion

        #region 构造函数
        public moAttributes()
        {
            _Attributes = new List<object>();
        }
        #endregion

        #region 方法
        public object this[Int32 index]
        {
            get { return _Attributes[index]; }
            set { _Attributes[index] = value; }
        }

        /// <summary>
        /// 获取指定索引的元素
        /// </summary>
        /// <param name="index">索引</param>
        /// <returns>元素</returns>
        public object GetItem(Int32 index)
        {
            return _Attributes[index];
        }

        /// <summary>
        /// 设置指定索引的元素
        /// </summary>
        /// <param name="index">索引</param>
        /// <param name="value">新值</param>
        public void SetItem(Int32 index, object value)
        {
            _Attributes[index] = value;
        }

        /// <summary>
        /// 将所有元素复制到一个新数组中
        /// </summary>
        /// <returns></returns>
        public object[] ToArray()
        {
            return _Attributes.ToArray();
        }

        /// <summary>
        /// 从指定数组中获取元素
        /// </summary>
        /// <param name="values">数组</param>
        public void FromArray(object[] values)
        {
            _Attributes.Clear();
            _Attributes.AddRange(values);
        }

        /// <summary>
        /// 在末尾增加一个元素
        /// </summary>
        /// <param name="value">元素</param>
        public void Append(object value)
        {
            _Attributes.Add(value);
        }

        /// <summary>
        /// 删除指定索引的元素
        /// </summary>
        /// <param name="index">索引</param>
        public void RemoveAt(Int32 index)
        {
            _Attributes.RemoveAt(index);
        }

        /// <summary>
        /// 复制
        /// </summary>
        /// <returns>新对象</returns>
        public moAttributes Clone()
        {
            moAttributes attributes = new moAttributes();
            attributes._Attributes.AddRange(_Attributes);
            return attributes;
        }
        #endregion
    }
}
