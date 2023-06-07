using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMapObjects
{
    public abstract class moSymbol
    {
        public abstract moSymbolTypeConstant SymbolType { get; }
        public abstract moSymbol Clone();

        public abstract Dictionary<string, object> ToDictionary();

        public static moSymbol FromDictionary(Dictionary<string, object> dict)
        {
            moSymbolTypeConstant symbolType = (moSymbolTypeConstant)dict["SymbolType"];
            switch (symbolType)
            {
                case moSymbolTypeConstant.SimpleMarkerSymbol:
                    return moSimpleMarkerSymbol.FromDictionary(dict);
                case moSymbolTypeConstant.SimpleLineSymbol:
                    return moSimpleLineSymbol.FromDictionary(dict);
                case moSymbolTypeConstant.SimpleFillSymbol:
                    return moSimpleFillSymbol.FromDictionary(dict);
                default:
                    return null;
            }
        }
    }
}
