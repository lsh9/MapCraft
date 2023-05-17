using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMapObjects
{
    public class moSimpleRenderer : moRenderer
    {
        #region 
        private moSymbol _symbol;   // 符号
        #endregion

        #region 构造函数
        public moSimpleRenderer()
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
            get { return moRendererTypeConstant.Simple; }
        }

        /// <summary>
        /// 获取或设置符号
        /// </summary>
        /// <value></value>
        public moSymbol Symbol
        {
            get { return _symbol; }
            set { _symbol = value; }
        }
        #endregion

        #region 方法
        public override moRenderer Clone()
        {
            moSimpleRenderer sRenderer = new moSimpleRenderer();
            sRenderer.Symbol = this.Symbol.Clone();
            return sRenderer;
        }
        #endregion
    }
}
