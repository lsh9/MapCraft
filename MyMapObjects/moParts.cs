using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMapObjects
{
    /// <summary>
    /// 部件集合
    /// </summary>
    public class moParts
    {
        #region 字段
        private List<moPoints> _Parts;
        #endregion

        #region 构造函数
        public moParts()
        {
            _Parts = new List<moPoints>();
        }

        public moParts(moPoints[] parts)
        {
            _Parts = new List<moPoints>();
            _Parts.AddRange(parts);
        }
        #endregion

        #region 属性
        /// <summary>
        /// 获取元素数目
        /// </summary>
        public Int32 Count
        {
            get { return _Parts.Count; }
        }
        #endregion

        #region 方法
        public moPoints this[Int32 index]
        {
            get { return _Parts[index]; }
            set { _Parts[index] = value; }
        }

        /// <summary>
        /// 获取指定索引的元素
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public moPoints GetItem(Int32 index)
        {
            return _Parts[index];
        }

        /// <summary>
        /// 设置指定索引的元素
        /// </summary>
        /// <param name="index">索引号</param>
        /// <param name="part"></param>
        public void SetItem(Int32 index, moPoints part)
        {
            _Parts[index] = part;
        }

        /// <summary>
        /// 添加元素 
        /// </summary>
        /// <param name="part"></param>
        public void Add(moPoints part)
        {
            _Parts.Add(part);
        }

        /// <summary>
        /// 添加元素集合到末尾
        /// </summary>
        /// <param name="parts"></param>
        public void AddRange(moPoints[] parts)
        {
            _Parts.AddRange(parts);
        }

        /// <summary>
        /// 插入元素到指定索引处
        /// </summary>
        /// <param name="index">索引</param>
        /// <param name="part">部件</param>
        public void Insert(Int32 index, moPoints part)
        {
            _Parts.Insert(index, part);
        }

        /// <summary>
        /// 插入元素集合到指定索引处
        /// </summary>
        /// <param name="index">索引</param>
        /// <param name="parts">部件数组</param>
        public void InsertRange(Int32 index, moPoints[] parts)
        {
            _Parts.InsertRange(index, parts);
        }

        /// <summary>
        /// 移除指定索引处的元素 
        /// </summary>
        /// <param name="index">索引</param>
        public void RemoveAt(Int32 index)
        {
            _Parts.RemoveAt(index);
        }

        /// <summary>
        /// 移除指定索引处开始的指定个数的元素
        /// </summary>
        /// <param name="index">索引</param>
        /// <param name="count">元素个数</param>
        public void RemoveRange(Int32 index, Int32 count)
        {
            _Parts.RemoveRange(index, count);
        }

        /// <summary>
        /// 移除所有元素
        /// </summary>
        public void Clear()
        {
            _Parts.Clear();
        }

        /// <summary>
        /// 克隆
        /// </summary>
        /// <returns>新对象</returns>
        public moParts Clone()
        {
            moParts parts = new moParts();
            foreach (moPoints part in _Parts)
            {
                parts.Add(part.Clone());
            }
            return parts;
        }

        #endregion
    }
}
