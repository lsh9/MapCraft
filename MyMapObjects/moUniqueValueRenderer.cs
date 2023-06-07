using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMapObjects
{
    public class moUniqueValueRenderer : moRenderer
    {
        #region 
        private string _Field;  // 绑定字段的名称
        private string _HeadTitle;  // 在图层显示控件中的标题
        private bool _ShowHead = true;  // 是否显示标题
        private List<string> _Values = new List<string>();  // 唯一值集合
        private List<moSymbol> _Symbols = new List<moSymbol>();  // 符号集合
        private moSymbol _DefaultSymbol;  // 默认符号
        private bool _ShowDefaultSymbol = true;  // 在图层显示控件中是否显示默认符号
        #endregion

        #region 构造函数
        public moUniqueValueRenderer()
        {
        }
        #endregion

        #region 属性
        /// <summary>
        /// 获取渲染类型
        /// </summary>
        /// <value></value>
        public override moRendererTypeConstant RendererType
        {
            get { return moRendererTypeConstant.UniqueValue; }
        }

        /// <summary>
        /// 获取或设置字段
        /// </summary>
        /// <value></value>
        public string Field
        {
            get { return _Field; }
            set
            {
                _Field = value;
                _HeadTitle = value;
            }
        }

        /// <summary>
        /// 唯一值数目
        /// </summary>
        /// <value></value>
        public Int32 ValueCount
        {
            get { return _Values.Count; }
        }

        /// <summary>
        /// 获取或设置默认符号
        /// </summary>
        /// <value></value>
        public moSymbol DefaultSymbol
        {
            get { return _DefaultSymbol; }
            set { _DefaultSymbol = value; }
        }

        /// <summary>
        /// 是否显示默认标题
        /// </summary>
        /// <value></value>
        public bool ShowHead
        {
            get { return _ShowHead; }
            set { _ShowHead = value; }
        }

        /// <summary>
        /// 是否显示默认符号
        /// </summary>
        /// <value></value>
        public bool ShowDefaultSymbol
        {
            get { return _ShowDefaultSymbol; }
            set { _ShowDefaultSymbol = value; }
        }

        #endregion

        #region 方法

        public string GetValue(Int32 index)
        {
            return _Values[index];
        }

        public void SetValue(Int32 index, string value)
        {
            _Values[index] = value;
        }

        public moSymbol GetSymbol(Int32 index)
        {
            return _Symbols[index];
        }

        public void SetSymbol(Int32 index, moSymbol symbol)
        {
            symbol = _Symbols[index];
        }

        /// <summary>
        /// 增加唯一值及对应的符号
        /// </summary>
        /// <param name="value"></param>
        /// <param name="symbol"></param>
        public void AddUniqueValue(string value, moSymbol symbol)
        {
            _Values.Add(value);
            _Symbols.Add(symbol);
        }

        /// <summary>
        /// 增加唯一值数组与对应的符号数组
        /// </summary>
        /// <param name="values"></param>
        /// <param name="symbols"></param>
        public void AddUniqueValues(string[] values, moSymbol[] symbols)
        {
            if (values.Length != symbols.Length)
                throw new Exception("唯一值数目与符号数目不一致！");
            _Values.AddRange(values);
            _Symbols.AddRange(symbols);
        }

        /// <summary>
        /// 根据指定值获取相应的符号，如果不存在则返回默认符号
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public moSymbol FindSymbol(string value)
        {
            Int32 index = _Values.IndexOf(value);
            if (index >= 0)
                return _Symbols[index];
            else
                return _DefaultSymbol;
        }

        public override moRenderer Clone()
        {
            moUniqueValueRenderer sRenderer = new moUniqueValueRenderer();
            sRenderer._Field = _Field;
            sRenderer._HeadTitle = _HeadTitle;
            sRenderer._ShowHead = _ShowHead;
            Int32 sValueCount = _Values.Count;
            for (Int32 i = 0; i <= sValueCount - 1; i++)
            {
                string sValue = _Values[i];
                moSymbol sSymbol = null;
                if (_Symbols[i] != null)
                    sSymbol = _Symbols[i].Clone();
                sRenderer.AddUniqueValue(sValue, sSymbol);
            }
            if (_DefaultSymbol != null)
                sRenderer.DefaultSymbol = _DefaultSymbol.Clone();
            sRenderer._ShowDefaultSymbol = _ShowDefaultSymbol;
            return sRenderer;
        }

        public override Dictionary<string, object> ToDictionary()
        {
            Dictionary<string, object> sDictionary = new Dictionary<string, object>();
            sDictionary.Add("RendererType", (Int32)RendererType);
            sDictionary.Add("Field", _Field);
            sDictionary.Add("HeadTitle", _HeadTitle);
            sDictionary.Add("ShowHead", _ShowHead);
            Int32 sValueCount = _Values.Count;
            List<string> sValues = new List<string>();
            List<Dictionary<string, object>> sSymbols = new List<Dictionary<string, object>>();
            for (Int32 i = 0; i <= sValueCount - 1; i++)
            {
                sValues.Add(_Values[i]);
                if (_Symbols[i] != null)
                    sSymbols.Add(_Symbols[i].ToDictionary());
                else
                    sSymbols.Add(null);
            }
            sDictionary.Add("Values", sValues);
            sDictionary.Add("Symbols", sSymbols);
            if (_DefaultSymbol != null)
                sDictionary.Add("DefaultSymbol", _DefaultSymbol.ToDictionary());
            sDictionary.Add("ShowDefaultSymbol", _ShowDefaultSymbol);
            return sDictionary;
        }

        public static new moUniqueValueRenderer FromDictionary(Dictionary<string, object> dictionary)
        {
            moUniqueValueRenderer sRenderer = new moUniqueValueRenderer();
            sRenderer._Field = dictionary["Field"].ToString();
            sRenderer._HeadTitle = dictionary["HeadTitle"].ToString();
            sRenderer._ShowHead = Convert.ToBoolean(dictionary["ShowHead"]);
            List<string> sValues = dictionary["Values"] as List<string>;
            List<Dictionary<string, object>> sSymbols = dictionary["Symbols"] as List<Dictionary<string, object>>;
            Int32 sValueCount = sValues.Count;
            for (Int32 i = 0; i <= sValueCount - 1; i++)
            {
                string sValue = sValues[i];
                moSymbol sSymbol = null;
                if (sSymbols[i] != null)
                    sSymbol = moSymbol.FromDictionary(sSymbols[i]);
                sRenderer.AddUniqueValue(sValue, sSymbol);
            }
            if (dictionary.ContainsKey("DefaultSymbol"))
                sRenderer._DefaultSymbol = moSymbol.FromDictionary(dictionary["DefaultSymbol"] as Dictionary<string, object>);
            sRenderer._ShowDefaultSymbol = Convert.ToBoolean(dictionary["ShowDefaultSymbol"]);
            return sRenderer;
        }
        #endregion
    }
}
