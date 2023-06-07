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
        private byte[] HeaderByte;
        private byte[] DataByte;
        private int FileBanben = 1000;
        public shpGeometryType shpGeometryType;
        public double MinX { get; set; }
        public double MinY { get; set; }
        public double MaxX { get; set; }
        public double MaxY { get; set; }

        // data
        public List<moGeometry> Geometries { get; set; }
        public string ShpFilePath;

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
            HeaderByte = new byte[24];
            for (int i = 0; i < 24; i++)
                HeaderByte[i] = 0;
            HeaderByte[2] = 39;
            HeaderByte[3] = 10;
            DataByte = new byte[32];
            for (int i = 0; i < 32; i++)
                DataByte[i] = 0;
        }

        public ShpParser(string shpFilePath)
        {
            ShpFilePath = shpFilePath;
            shpGeometryType = shpGeometryType.point;
            MinX = 0;
            MinY = 0;
            MaxX = 0;
            MaxY = 0;
            Geometries = new List<moGeometry>();
            HeaderByte = new byte[24];
            for (int i = 0; i < 24; i++)
                HeaderByte[i] = 0;
            HeaderByte[2] = 39;
            HeaderByte[3] = 10;
            DataByte = new byte[32];
            for(int i=0;i<32;i++)
                DataByte[i] = 0;
        }

        #endregion

        #region Methods

        public void Read()
        {
            FileStream fs = new FileStream(ShpFilePath, FileMode.Open);
            BinaryReader br = new BinaryReader(fs);
            Geometries = new List<moGeometry>();

            //读取文件过程
            HeaderByte = br.ReadBytes(24);
            int FileLength = br.ReadInt32();
            Console.WriteLine("文件长度:" + ChangeByteOrder(FileLength));
            FileBanben = br.ReadInt32();
            int ShapeType = br.ReadInt32();
            shpGeometryType = (shpGeometryType)ShapeType;
            MinX = br.ReadDouble();
            MinY = br.ReadDouble();
            MaxX = br.ReadDouble();
            MaxY = br.ReadDouble();
            DataByte = br.ReadBytes(32);
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
                        Console.WriteLine("文件记录号为:" + ChangeByteOrder((int)RecordNum));
                        int DataLength = br.ReadInt32();
                        Console.WriteLine("坐标长度为:" + ChangeByteOrder(DataLength));
                        //读取第i个记录
                        int m = br.ReadInt32();
                        moMultiPolygon multiPolygon = new moMultiPolygon();
                        multiPolygon.MinX = br.ReadDouble();
                        Console.WriteLine("最小X坐标为:" + multiPolygon.MinX);
                        multiPolygon.MinY = br.ReadDouble();
                        Console.WriteLine("最小Y坐标为:" + multiPolygon.MinY);
                        multiPolygon.MaxX = br.ReadDouble();
                        Console.WriteLine("最大X坐标为:" + multiPolygon.MaxX);
                        multiPolygon.MaxY = br.ReadDouble();
                        Console.WriteLine("最大Y坐标为:" + multiPolygon.MaxY);
                        
                        int NumParts = br.ReadInt32();
                        int NumPoints = br.ReadInt32();
                        Console.WriteLine("部件个数："+NumParts);
                        Console.WriteLine("点总数：" + NumPoints);

                        int[] begin = new int[NumParts];
                        int[] end = new int[NumParts];
                        for (int i = 0; i < NumParts; i++)
                        {
                            begin[i] = br.ReadInt32();
                        }
                        for (int i = 1; i < NumParts; i++)
                        {
                            end[i - 1] = begin[i];
                            Console.WriteLine("第" + i + "个部分的终点为:" + end[i-1]);
                        }
                        end[NumParts - 1] = NumPoints;
                        //for (int i = 0; i < NumParts; i++)
                        //{
                        //    moPoints points = new moPoints();
                        //    for (int j = begin[i]; j < end[i]; j++)
                        //    {
                        //        moPoint spoint = new moPoint();
                        //        spoint.X = br.ReadDouble();
                        //        spoint.Y = br.ReadDouble();
                        //        points.Add(spoint);
                        //    }
                        //    multiPolygon.Parts.Add(points);
                        //}

                        moPoints moPoints = new moPoints();
                        int flag = 0;
                        for(int j = 0; j < NumPoints; j++)
                        {
                            moPoint moPoint = new moPoint();
                            moPoint.X = br.ReadDouble();
                            moPoint.Y = br.ReadDouble();
                            moPoints.Add(moPoint);
                            if(j == end[flag]-1)
                            {
                                multiPolygon.Parts.Add(moPoints);
                                moPoints = new moPoints();
                                flag++;
                            }
                        }
                        Geometries.Add(multiPolygon);
                    }
                    break;
                default:
                    MessageBox.Show("不支持的shapefile类型!");
                    break;
            }

        }
    
        public void SaveToFile(string shpFilePath)
        {
            FileStream fs = new FileStream(shpFilePath, FileMode.Create, FileAccess.Write);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(HeaderByte);
            int filelength = 100;
            switch (shpGeometryType)
            {
                case shpGeometryType.point:
                    for(int i=0;i<Geometries.Count;i++ )
                    {
                        filelength += 28;
                    }
                    break;
                case shpGeometryType.polyline:
                    for (int i = 0; i < Geometries.Count; i++)
                    {
                        filelength += 52;
                        moMultiPolyline sMultiPolyline = (moMultiPolyline)Geometries[i];
                        filelength += sMultiPolyline.Parts.Count * 4;
                        for (int j = 0; j < sMultiPolyline.Parts.Count;j++)
                            filelength += sMultiPolyline.Parts[j].Count*16;
                    }
                    break;
                case shpGeometryType.polygon:
                    for (int i = 0; i < Geometries.Count; i++)
                    {
                        filelength += 52;
                        moMultiPolygon sMultiPolygon = (moMultiPolygon)Geometries[i];
                        filelength += sMultiPolygon.Parts.Count * 4;
                        for (int j = 0; j < sMultiPolygon.Parts.Count; j++)
                            filelength += sMultiPolygon.Parts[j].Count * 16;
                    }
                    break;
            }
            //写入文件总长度
            FileTools.WriteInt32InBigEndian(filelength, bw);
            bw.Write(FileBanben);
            int shapetype = 0;
            switch (shpGeometryType)
            {
                case shpGeometryType.point:
                    shapetype = 1; break;
                case shpGeometryType.polygon:
                    shapetype = 5; break;
                case shpGeometryType.polyline:
                    shapetype = 3; break;
            }
            bw.Write(shapetype);
            bw.Write(MinX);
            bw.Write(MinY);
            bw.Write(MaxX); 
            bw.Write(MaxY);
            bw.Write(DataByte);
            //开始写入几何数据
            switch (shapetype)
            {
                case 1:
                    for(int i=0;i<Geometries.Count;i++)
                    {
                        FileTools.WriteInt32InBigEndian(i+1, bw);
                        int datalength = 20;
                        FileTools.WriteInt32InBigEndian(datalength, bw);
                        bw.Write(shapetype);
                        moPoint spoint = (moPoint)Geometries[i];
                        bw.Write(spoint.X);
                        bw.Write(spoint.Y);
                    }
                    break;
                case 3:
                    for(int i = 0; i < Geometries.Count; i++)
                    {
                        FileTools.WriteInt32InBigEndian(i + 1, bw);
                        int datalength = 44;
                        int NumPoints = 0;
                        moMultiPolyline sMultiPolyline = (moMultiPolyline)Geometries[i];
                        datalength += sMultiPolyline.Parts.Count * 4;
                        for(int j = 0; j < sMultiPolyline.Parts.Count; j++)
                        {
                            NumPoints += sMultiPolyline.Parts[j].Count;
                            datalength += sMultiPolyline.Parts[j].Count * 16;
                        }
                        FileTools.WriteInt32InBigEndian(datalength, bw);
                        bw.Write(shapetype);
                        bw.Write(sMultiPolyline.MinX);
                        bw.Write(sMultiPolyline.MinY);
                        bw.Write(sMultiPolyline.MaxX);
                        bw.Write(sMultiPolyline.MaxY);
                        bw.Write(sMultiPolyline.Parts.Count);
                        bw.Write(NumPoints);
                        //写入分区
                        int StartNum = 0;
                        for(int j = 0; j < sMultiPolyline.Parts.Count; j++)
                        {
                            bw.Write(StartNum);
                            StartNum += sMultiPolyline.Parts[j].Count;
                        }
                        for(int j = 0; j < sMultiPolyline.Parts.Count; j++)
                        {
                            for(int n = 0; n < sMultiPolyline.Parts[j].Count;n++)
                            {
                                bw.Write(sMultiPolyline.Parts[j][n].X);
                                bw.Write(sMultiPolyline.Parts[j][n].Y);
                            }
                        }
                    }
                    break;
                case 5:
                    for (int i = 0; i < Geometries.Count; i++)
                    {
                        FileTools.WriteInt32InBigEndian(i + 1, bw);
                        int datalength = 44;
                        int NumPoints = 0;
                        moMultiPolygon sMultiPolygon = (moMultiPolygon)Geometries[i];
                        datalength += sMultiPolygon.Parts.Count * 4;
                        for (int j = 0; j < sMultiPolygon.Parts.Count; j++)
                        {
                            NumPoints += sMultiPolygon.Parts[j].Count;
                            datalength += sMultiPolygon.Parts[j].Count * 16;
                        }
                        FileTools.WriteInt32InBigEndian(datalength, bw);
                        bw.Write(shapetype);
                        bw.Write(sMultiPolygon.MinX);
                        bw.Write(sMultiPolygon.MinY);
                        bw.Write(sMultiPolygon.MaxX);
                        bw.Write(sMultiPolygon.MaxY);
                        bw.Write(sMultiPolygon.Parts.Count);
                        bw.Write(NumPoints);
                        //写入分区
                        int StartNum = 0;
                        for (int j = 0; j < sMultiPolygon.Parts.Count; j++)
                        {
                            bw.Write(StartNum);
                            StartNum += sMultiPolygon.Parts[j].Count;
                        }
                        for (int j = 0; j < sMultiPolygon.Parts.Count; j++)
                        {
                            for (int n = 0; n < sMultiPolygon.Parts[j].Count; n++)
                            {
                                bw.Write(sMultiPolygon.Parts[j][n].X);
                                bw.Write(sMultiPolygon.Parts[j][n].Y);
                            }
                        }
                    }
                    break;
            }
            bw.Dispose();
            fs.Dispose();
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
            return value*2;
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
