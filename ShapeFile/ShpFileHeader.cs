using System;
using System.IO;

namespace GISBox.ShapeFile
{
    /// <summary>
    /// shp header: 100 bytes
    /// </summary>
    public class ShpFileHeader
    {
        #region Properity

        private byte[] _reserved1;    // byte 0-31: not used
        private ShapeFileType _shapeFileType;   // byte 32-35: feature type
        public double MinX { get; } // byte 36-43

        public double MinY { get; } // byte 44-51

        public double MaxX { get; } // byte 52-59

        public double MaxY { get; } // byte 60-67

        private byte[] _reserved2; // byte 68-99: not used

        #endregion

        #region Constructors

        /// <summary>
        /// create a new shp header
        /// </summary>
        /// <param name="shapeType"></param>
        public ShpFileHeader(ShapeFileType shapeType)
        {
            _reserved1 = new byte[32];
            _shapeFileType = shapeType;
            MinX = double.MaxValue;
            MinY = double.MaxValue;
            MaxX = double.MinValue;
            MaxY = double.MinValue;
            _reserved2 = new byte[32];
        }

        /// <summary>
        /// construct from shp file
        /// </summary>
        /// <param name="br"></param>
        public ShpFileHeader(BinaryReader br)
        {
            _reserved1 = br.ReadBytes(32);
            _shapeFileType = (ShapeFileType)br.ReadInt32();
            MinX = br.ReadDouble();
            MinY = br.ReadDouble();
            MaxX = br.ReadDouble();
            MaxY = br.ReadDouble();
            _reserved2 = br.ReadBytes(32);
        }

        #endregion

        #region Methods

        public MyMapObjects.moGeometryTypeConstant GetMoGeometryType()
        {
            MyMapObjects.moGeometryTypeConstant moGeometryType;
            switch (_shapeFileType)
            {
                case ShapeFileType.Point:
                    moGeometryType = MyMapObjects.moGeometryTypeConstant.Point;
                    break;
                case ShapeFileType.PolyLine:
                    moGeometryType = MyMapObjects.moGeometryTypeConstant.MultiPolyline;
                    break;
                case ShapeFileType.Polygon:
                    moGeometryType = MyMapObjects.moGeometryTypeConstant.MultiPolygon;
                    break;
                default:
                {
                    const string error = "不支持该 ShapeFile 类型数据";
                    throw new NotSupportedException(error);
                }
            }
            return moGeometryType;
        }

        /// <summary>
        /// Write shp header to File
        /// </summary>
        /// <param name="bw"></param>
        public void WriteToFile(BinaryWriter bw)
        {
            bw.Write(_reserved1);
            bw.Write((int)_shapeFileType);
            bw.Write(MinX);
            bw.Write(MinY);
            bw.Write(MaxX);
            bw.Write(MaxY);
            bw.Write(_reserved2);
        }
        #endregion
    }
}
