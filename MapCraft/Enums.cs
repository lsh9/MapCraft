using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapCraft
{
    public enum shpGeometryType
    {
        point = 1,
        polyline = 3,
        polygon = 5,
    }

    public enum DbfFieldType : byte
    {
        Int = (byte)'I',
        Single = (byte)'F',
        Double = (byte)'D',
        Text = (byte)'C',
    }
}
