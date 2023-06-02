using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyMapObjects;

namespace MapCraft.FileProcessor
{
    public class ShpParser
    {
        #region Properties

        // header
        private shpGeometryType shpGeometryType;
        public double MinX { get; set; }
        public double MinY { get; set; }
        public double MaxX { get; set; }
        public double MaxY { get; set; }

        // data
        public List<moGeometry> Geometries { get; set; }

        #endregion

        #region Constructors

        public ShpParser()
        {
            shpGeometryType = shpGeometryType.point;
            MinX = 0;
            MinY = 0;
            MaxX = 0;
            MaxY = 0;
            Geometries = new List<moGeometry>();
        }

        public ShpParser(ShxParser shxParser,string shpFilePath)
        {
            FileStream fs = new FileStream(shpFilePath, FileMode.Open);
            BinaryReader br = new BinaryReader(fs);
            Geometries = new List<moGeometry>();
            ReadShp(br);
        }

        #endregion

        #region Methods

        private void ReadShp(BinaryReader br)
        {
            //读取文件过程
            br.ReadBytes(24);
            int FileLength = br.ReadInt32();
            Console.WriteLine("文件长度:" + ChangeByteOrder(FileLength));
            int FileBanben = br.ReadInt32();
            int ShapeType = br.ReadInt32();
            shpGeometryType = (shpGeometryType)ShapeType;
            MinX = br.ReadDouble();
            MinY = br.ReadDouble();
            MaxX = br.ReadDouble();
            MaxY = br.ReadDouble();
            br.ReadBytes(32);
            switch (ShapeType)
            {
                case 1:
                    while (br.PeekChar() != -1)
                    {
                        moPoint point = new moPoint();
                        uint RecordNum = br.ReadUInt32();
                        int DataLength = br.ReadInt32();
                        //读取第i个记录
                        br.ReadInt32();
                        point.X = br.ReadDouble();
                        point.Y = br.ReadDouble();
                        Geometries.Add(point);
                    }
                    break;
                case 3:
                    while (br.PeekChar() != -1)
                    {
                        uint RecordNum = br.ReadUInt32();
                        int DataLength = br.ReadInt32();
                        //读取第i个记录
                        br.ReadInt32();
                        moMultiPolyline multiPolyline = new moMultiPolyline
                        {
                            MinX = br.ReadDouble(),
                            MinY = br.ReadDouble(),
                            MaxX = br.ReadDouble(),
                            MaxY = br.ReadDouble()
                        };
                        int NumParts = br.ReadInt32();
                        int NumPoints = br.ReadInt32();
                        int[] begin = new int[NumParts];
                        int[] end = new int[NumParts];
                        for (int i = 0; i < NumParts; i++)
                        {
                            begin[i] = br.ReadInt32();
                        }
                        for(int i=1;i< NumParts; i++)
                        {
                            end[i - 1] = begin[i]+1;
                        }
                        end[NumParts-1] = NumPoints;
                        for(int i=0;i<NumParts;i++)
                        {
                            moPoints points = new moPoints();
                            for (int j = begin[i]; j < end[i]; j++)
                            {
                                moPoint spoint = new moPoint();
                                spoint.X = br.ReadDouble();
                                spoint.Y = br.ReadDouble();
                                points.Add(spoint);
                            }
                            multiPolyline.Parts.Add(points);
                        }
                        Geometries.Add(multiPolyline);
                    }
                    break;
                case 5:
                    while (br.PeekChar() != -1)
                    {
                        uint RecordNum = br.ReadUInt32();
                        int DataLength = br.ReadInt32();
                        //读取第i个记录
                        br.ReadInt32();
                        moMultiPolygon multiPolygon = new moMultiPolygon
                        {
                            MinX = br.ReadDouble(),
                            MinY = br.ReadDouble(),
                            MaxX = br.ReadDouble(),
                            MaxY = br.ReadDouble()
                        };
                        int NumParts = br.ReadInt32();
                        int NumPoints = br.ReadInt32();
                        int[] begin = new int[NumParts];
                        int[] end = new int[NumParts];
                        for (int i = 0; i < NumParts; i++)
                        {
                            begin[i] = br.ReadInt32();
                        }
                        for (int i = 1; i < NumParts; i++)
                        {
                            end[i - 1] = begin[i] + 1;
                        }
                        end[NumParts - 1] = NumPoints;
                        for (int i = 0; i < NumParts; i++)
                        {
                            moPoints points = new moPoints();
                            for (int j = begin[i]; j < end[i]; j++)
                            {
                                moPoint spoint = new moPoint();
                                spoint.X = br.ReadDouble();
                                spoint.Y = br.ReadDouble();
                                points.Add(spoint);
                            }
                            multiPolygon.Parts.Add(points);
                        }
                        Geometries.Add(multiPolygon);
                    }
                    break;
                default:
                    MessageBox.Show("不支持的shapefile类型!");
                    break;
            }

        }
    

        private int ChangeByteOrder(int indata)
        {
            byte[] src = new byte[4];
            src[0] = (byte)((indata >> 24) & 0xFF);
            src[1] = (byte)((indata >> 16) & 0xFF);
            src[2] = (byte)((indata >> 8) & 0xFF);
            src[3] = (byte)(indata & 0xFF);


            int value;
            value = (int)((src[0] & 0xFF) | ((src[1] & 0xFF) << 8) | ((src[2] & 0xFF) << 16) | ((src[3] & 0xFF) << 24));
            return value;
        }

        public MyMapObjects.moGeometryTypeConstant GetMoGeometryType()
        {
            MyMapObjects.moGeometryTypeConstant moGeometryType;
            switch (shpGeometryType)
            {
                case shpGeometryType.point:
                    moGeometryType = MyMapObjects.moGeometryTypeConstant.Point;
                    break;
                case shpGeometryType.polyline:
                    moGeometryType = MyMapObjects.moGeometryTypeConstant.MultiPolyline;
                    break;
                case shpGeometryType.polygon:
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

        #endregion
    }
}
