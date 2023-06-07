using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMapObjects
{
    public abstract class moRenderer
    {
        public abstract moRendererTypeConstant RendererType { get; }

        public abstract moRenderer Clone();

        public abstract Dictionary<string, object> ToDictionary();

        public static moRenderer FromDictionary(Dictionary<string, object> dict)
        {
            int type = (int)dict["RendererType"];
            moRendererTypeConstant rendererType = (moRendererTypeConstant)type;
            switch (rendererType)
            {
                case moRendererTypeConstant.Simple:
                    return moSimpleRenderer.FromDictionary(dict);
                case moRendererTypeConstant.UniqueValue:
                    return moUniqueValueRenderer.FromDictionary(dict);
                case moRendererTypeConstant.ClassBreaks:
                    return moClassBreaksRenderer.FromDictionary(dict);
                default:
                    return null;
            }
        }
    }
}
