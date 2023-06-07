using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MyMapObjects
{
    public class moTextSymbol
    {
        #region 字段
        
        private Font _Font = new Font("微软雅黑", 8);   // 字体
        private Color _FontColor = Color.Black;      // 字体颜色
        private moTextSymbolAlignmentConstant _Alignment = moTextSymbolAlignmentConstant.CenterCenter; // 布局
        private double _OffsetX, _OffsetY;  // X,Y偏移量，单位为像素mm，向右为正，向上为正
        private bool _UseMask = false;      // 是否描边
        private double _MaskWidth = 1;      // 描边宽度，mm
        private Color _MaskColor = Color.White;  // 描边颜色

        #endregion

        #region 属性
        /// <summary>
        /// 获取或设置字体
        /// </summary>
        public Font Font
        {
            get { return _Font; }
            set { _Font = value; }
        }

        /// <summary>
        /// 获取或设置字体颜色
        /// </summary>
        public Color FontColor
        {
            get { return _FontColor; }
            set { _FontColor = value; }
        }

        /// <summary>
        /// 获取或设置布局
        /// </summary>
        public moTextSymbolAlignmentConstant Alignment
        {
            get { return _Alignment; }
            set { _Alignment = value; }
        }

        /// <summary>
        /// 获取或设置X方向偏移量
        /// </summary>
        public double OffsetX
        {
            get { return _OffsetX; }
            set { _OffsetX = value; }
        }

        /// <summary>
        /// 获取或设置Y方向偏移量
        /// </summary>
        public double OffsetY
        {
            get { return _OffsetY; }
            set { _OffsetY = value; }
        }

        /// <summary>
        /// 获取或设置是否描边
        /// </summary>
        public bool UseMask
        {
            get { return _UseMask; }
            set { _UseMask = value; }
        }

        /// <summary>
        /// 获取或设置描边宽度
        /// </summary>
        public double MaskWidth
        {
            get { return _MaskWidth; }
            set { _MaskWidth = value; }
        }

        /// <summary>
        /// 获取或设置描边颜色
        /// </summary>
        public Color MaskColor
        {
            get { return _MaskColor; }
            set { _MaskColor = value; }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 克隆
        /// </summary>
        /// <returns></returns>
        public moTextSymbol Clone()
        {
            moTextSymbol symbol = new moTextSymbol();
            symbol._Font = (Font)_Font.Clone();
            symbol._FontColor = _FontColor;
            symbol._Alignment = _Alignment;
            symbol._OffsetX = _OffsetX;
            symbol._OffsetY = _OffsetY;
            symbol._UseMask = _UseMask;
            symbol._MaskWidth = _MaskWidth;
            symbol._MaskColor = _MaskColor;
            return symbol;
        }

        public Dictionary<string, object> ToDictionary()
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            // 将Font对象的Family和Size属性存入字典
            Dictionary<string, object> fontDict = new Dictionary<string, object>();
            fontDict.Add("Family", _Font.FontFamily.Name);
            fontDict.Add("Size", _Font.Size);
            dict.Add("Font", fontDict);
            dict.Add("FontColor", _FontColor.ToArgb());
            dict.Add("Alignment", _Alignment);
            dict.Add("OffsetX", _OffsetX);
            dict.Add("OffsetY", _OffsetY);
            dict.Add("UseMask", _UseMask);
            dict.Add("MaskWidth", _MaskWidth);
            dict.Add("MaskColor", _MaskColor.ToArgb());
            return dict;
        }

        public static moTextSymbol FromDictionary(Dictionary<string, object> dict)
        {
            moTextSymbol symbol = new moTextSymbol();
            // 根据Family和Size构造Font对象
            string family = Convert.ToString(((Dictionary<string, object>)dict["Font"])["Family"]);
            float size = Convert.ToSingle(((Dictionary<string, object>)dict["Font"])["Size"]);
            symbol._Font = new Font(family, size);
            symbol._FontColor = Color.FromArgb(Convert.ToInt32(dict["FontColor"]));
            symbol._Alignment = (moTextSymbolAlignmentConstant)dict["Alignment"];
            symbol._OffsetX = Convert.ToDouble(dict["OffsetX"]);
            symbol._OffsetY = Convert.ToDouble(dict["OffsetY"]);
            symbol._UseMask = Convert.ToBoolean(dict["UseMask"]);
            symbol._MaskWidth = Convert.ToDouble(dict["MaskWidth"]);
            symbol._MaskColor = Color.FromArgb(Convert.ToInt32(dict["MaskColor"]));
            return symbol;
        }

        #endregion
    }
}
