using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMapObjects
{
    public class moFields
    {
        #region 字段
        private List<moField> _Fields = new List<moField>(); // 字段列表
        private string _PrimaryField = ""; // 主键字段
        private bool _ShowAlias = false; // 是否显示别名
        #endregion

        #region 构造函数
        public moFields()
        {
        }
        #endregion

        #region 属性
        /// <summary>
        /// 字段个数
        /// </summary>
        public Int32 Count
        {
            get { return _Fields.Count; }
        }

        /// <summary>
        /// 获取或修改主键名
        /// </summary>
        /// <value>主键名</value>
        public string PrimaryField
        {
            get { return _PrimaryField; }
            set { _PrimaryField = value; }
        }

        /// <summary>
        /// 是否显示别名
        /// </summary>
        /// <value>别名</value>
        public bool ShowAlias
        {
            get { return _ShowAlias; }
            set { _ShowAlias = value; }
        }
        #endregion


        #region 方法
        public moField this[Int32 index]
        {
            get { return _Fields[index]; }
        }

        public moField this[string name]
        {
            get
            {
                Int32 sIndex = FindField(name);
                if (sIndex >= 0)
                {
                    return _Fields[sIndex];
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 根据索引获取字段
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public moField GetItem(Int32 index)
        {
            return _Fields[index];
        }

        /// <summary>
        /// 跟据名称获取字段
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public moField GetItem(string name)
        {
            Int32 sIndex = FindField(name);
            if (sIndex >= 0)
            {
                return _Fields[sIndex];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 添加字段
        /// </summary>
        /// <param name="field">字段</param>
        public void Append(moField field)
        {
            if (FindField(field.Name) >= 0)
            {
                throw new Exception("字段已存在");
            }
            _Fields.Add(field);
            // 触发事件
            if (FieldAppended != null)
            {
                FieldAppended(this, field);
            }
        }

        /// <summary>
        /// 删除指定索引号的字段
        /// </summary>
        /// <param name="index">字段索引</param>
        public void RemoveAt(Int32 index)
        {
            moField sField = _Fields[index];
            _Fields.RemoveAt(index);
            // 触发事件
            if (FieldRemoved != null)
            {
                FieldRemoved(this, index, sField);
            }
        }

        #endregion

        #region 事件
        /// <summary>
        /// 有字段被加入时触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="fieldAppended"></param>
        internal delegate void FieldAppendHandler(object sender, moField fieldAppended);
        internal event FieldAppendHandler FieldAppended;

        /// <summary>
        /// 有字段被删除时触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="fieldIndex"></param>
        /// <param name="fieldRemoved"></param>
        internal delegate void FieldRemoveHandler(object sender, Int32 fieldIndex, moField fieldRemoved);
        internal event FieldRemoveHandler FieldRemoved;
        #endregion


        // 根据名称返回索引号，无则返回-1
        public Int32 FindField(string name)
        {
            for (Int32 i = 0; i < _Fields.Count; i++)
            {
                if (_Fields[i].Name.ToLower() == name.ToLower())
                {
                    return i;
                }
            }
            return -1;
        }
        #region 私有方法
        #endregion
    }
}
