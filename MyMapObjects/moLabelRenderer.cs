using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMapObjects
{
    public class moLabelRenderer
    {
        #region 字段
        
        private bool _LabelFeatures = false; // 是否标注要素
        private moTextSymbol _TextSymbol = new moTextSymbol(); // 标注符号
        private string _Field = ""; // 标注字段
        
        #endregion

        #region 属性

        /// <summary>
        /// 获取或设置是否标注要素
        /// </summary>
        public bool LabelFeatures
        {
            get { return _LabelFeatures; }
            set { _LabelFeatures = value; }
        }

        /// <summary>
        /// 获取或设置标注符号
        /// </summary>
        public moTextSymbol TextSymbol
        {
            get { return _TextSymbol; }
            set { _TextSymbol = value; }
        }

        /// <summary>
        /// 获取或设置标注字段
        /// </summary>
        public string Field
        {
            get { return _Field; }
            set { _Field = value; }
        }

        #endregion
    }
}
