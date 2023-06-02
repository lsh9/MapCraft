using System;
using System.Collections.Generic;
using System.IO;
using MyMapObjects;

namespace GISBox.ShapeFile
{

    /// <summary>
    /// read shp file and manage features in moGeometry format
    /// </summary>
    public class ShpFileProcessor
    {
        #region Properity
        //(1) header

        public ShpFileHeader FileHeader { get; }

        //(2) MyMapObjects format features
        public List<moGeometry> Geometries { get; set; }

        public moGeometryTypeConstant GeometryType => FileHeader.GetMoGeometryType();
        #endregion

        #region Constructors

        /// <summary>
        /// read shp file, use shx file
        /// </summary>
        /// <param name="shxFileProcessor"></param>
        /// <param name="shpFilePath"></param>
        public ShpFileProcessor(ShxFileProcessor shxFileProcessor, string shpFilePath)
        {
            FileStream fs = new FileStream(shpFilePath, FileMode.Open);
            BinaryReader br = new BinaryReader(fs);

            //header
            FileHeader = new ShpFileHeader(br);

            Geometries = new List<moGeometry>();
            //feature record
            for(int i = 0; i < shxFileProcessor.RecordCount; ++i)
            {
                br.BaseStream.Seek(shxFileProcessor.RecordOffsets[i], SeekOrigin.Begin);
                ReadEachRecordOfShapeFile(br);
            }
            
            br.Dispose();
            fs.Dispose();
        }

        #endregion


        #region 私有函数

        /// <summary>
        /// read a feature from file
        /// including three types: point polyline polygon
        /// </summary>
        /// <param name="br"></param>
        private void ReadEachRecordOfShapeFile(BinaryReader br)
        {
            // record header
            br.ReadBytes(8);
            // feature type
            ShapeFileType shapeType = (ShapeFileType)br.ReadInt32();
            //根据ShapeType读取对应的数据类型
            switch (shapeType)
            {
                case ShapeFileType.Point:
                    ReadShpPoint(br);
                    break;
                case ShapeFileType.PolyLine:
                    ReadShpPolyLine(br);
                    break;
                case ShapeFileType.Polygon:
                    ReadShpPolygon(br);
                    break;
                default:
                {
                    const string error = "不支持该 ShapeFile 类型数据！";
                    throw new NotSupportedException(error);
                }
            }
        }

        /// <summary>
        /// read point from file and convert to moPoint
        /// </summary>
        /// <param name="br"></param>
        private void ReadShpPoint(BinaryReader br)
        {
            double x = br.ReadDouble();
            double y = br.ReadDouble();
            moPoint point = new moPoint(x, y);
            Geometries.Add(point);
        }

        /// <summary>
        /// read polyline from file and convert to moMultiPolyline
        /// </summary>
        /// <param name="br"></param>
        private void ReadShpPolyLine(BinaryReader br)
        {
            moMultiPolyline multiPolyline = new moMultiPolyline
            {
                //(1) mbr[4]
                MinX = br.ReadDouble(),
                MinY = br.ReadDouble(),
                MaxX = br.ReadDouble(),
                MaxY = br.ReadDouble()
            };

            //(2) number of single polyline and total of points
            var numParts = br.ReadInt32();
            var numPoints = br.ReadInt32();

            //(3) the start position of each single polyline
            int[] partIndex = new int[numParts + 1];
            for (int i = 0; i < numParts; ++i)
            {
                partIndex[i] = br.ReadInt32();
            }
            partIndex[numParts] = numPoints;

            //(4) add each polyline
            for(int i = 0; i < numParts; ++i)
            {
                // a part
                moPoints sPoints = new moPoints();
                for(int j = 0; j < partIndex[i + 1] - partIndex[i]; ++j)
                {
                    double x = br.ReadDouble();
                    double y = br.ReadDouble();
                    moPoint sPoint = new moPoint(x, y);
                    sPoints.Add(sPoint);
                }
                multiPolyline.Parts.Add(sPoints);
            }
            multiPolyline.UpdateExtent();
            Geometries.Add(multiPolyline);
        }

        /// <summary>
        /// read polygon from file and convert to moMultiPolygon
        /// </summary>
        /// <param name="br"></param>
        private void ReadShpPolygon(BinaryReader br)
        {
            moMultiPolygon multiPolygon = new moMultiPolygon
            {
                // mbr[4]
                MinX = br.ReadDouble(),
                MinY = br.ReadDouble(),
                MaxX = br.ReadDouble(),
                MaxY = br.ReadDouble()
            };

            // number of rings and total of point
            int numParts = br.ReadInt32();
            int numPoints = br.ReadInt32();

            // the start position of each ring
            int[] partIndex = new int[numParts + 1];
            for (int i = 0; i < numParts; ++i)
            {
                partIndex[i] = br.ReadInt32();
            }
            partIndex[numParts] = numPoints;

            // add rings
            for(int i = 0; i < numParts; ++i)
            {
                moPoints points = new moPoints();
                for (int j = 0; j < partIndex[i + 1] - partIndex[i]; ++j)
                {
                    double x = br.ReadDouble();
                    double y = br.ReadDouble();
                    moPoint point = new moPoint(x, y);
                    points.Add(point);
                }
                multiPolygon.Parts.Add(points);
            }
            Geometries.Add(multiPolygon);
        }

        #endregion
    }
}
