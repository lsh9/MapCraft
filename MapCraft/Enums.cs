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

    /// <summary>
    /// 地图操作类型
    /// </summary>
    internal enum MapOpConstant
    {
        None = 0,
        // 有其他鼠标动作的操作
        ZoomIn = 1,
        ZoomOut = 2,
        Pan = 3,
        SelectByLocation = 7,
        Identify = 9,

        // 无其他鼠标动作的操作
        FullExtent = 4,
        FixedZoomIn = 5,
        FixedZoomOut = 6,
        SelectByAttribute = 8,

        //要素编辑操作
        MoveFeature=10,
        CreateFeature=11,
        MoveNode=12,
        AddNode=13,
        DeleteNode=14,
    }
}
