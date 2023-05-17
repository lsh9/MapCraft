using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMapObjects
{
    public class moField
    {
        #region 字段
        private string _Name = "";  // 字段名称
        private string _AliasName = ""; // 字段别名
        private moValueTypeConstant _ValueType = moValueTypeConstant.dInt32; // 字段值类型
        private Int32 _Length;  // 字段长度，用于文本类型
        #endregion

        #region 构造函数
        public moField(string name)
        {
            _Name = name;
            _AliasName = name;
        }

        public moField(string name, moValueTypeConstant valueType)
        {
            _Name = name;
            _AliasName = name;
            _ValueType = valueType;
        }

        public moField(string name, moValueTypeConstant valueType, Int32 length)
        {
            _Name = name;
            _AliasName = name;
            _ValueType = valueType;
            _Length = length;
        }
        #endregion

        #region 属性
        /// <summary>
        /// 字段名称
        /// </summary>
        public string Name
        {
            get { return _Name; }
        }

        /// <summary>
        /// 字段别名
        /// </summary>
        /// <value>别名</value>
        public string AliasName
        {
            get { return _AliasName; }
            set { _AliasName = value; }
        }

        /// <summary>
        /// 字段值类型
        /// </summary>
        public moValueTypeConstant ValueType
        {
            get { return _ValueType; }
        }

        /// <summary>
        /// 字段长度
        /// </summary>
        public Int32 Width
        {
            get { return _Length; }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 克隆
        /// </summary>
        /// <returns>新对象</returns>
        public moField Clone()
        {
            moField sField = new moField(_Name, _ValueType);
            sField._AliasName = _AliasName;
            sField._Length = _Length;
            return sField;
        }

        #endregion
    }
}
