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

        public Dictionary<string, object> ToDictionary()
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("LabelFeatures", _LabelFeatures);
            dict.Add("TextSymbol", _TextSymbol.ToDictionary());
            dict.Add("Field", _Field);
            return dict;
        }

        public static moLabelRenderer FromDictionary(Dictionary<string, object> dict)
        {
            moLabelRenderer labelRenderer = new moLabelRenderer();
            labelRenderer.LabelFeatures = Convert.ToBoolean(dict["LabelFeatures"]);
            labelRenderer.TextSymbol = moTextSymbol.FromDictionary((Dictionary<string, object>)dict["TextSymbol"]);
            labelRenderer.Field = Convert.ToString(dict["Field"]);
            return labelRenderer;
        }
    }
}
