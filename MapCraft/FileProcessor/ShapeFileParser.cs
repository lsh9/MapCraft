using MyMapObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapCraft.FileProcessor
{
    public class ShapeFileParser
    {
        #region Properties

        private ShxParser _shxFile;
        private ShpParser _shpFile;
        private DbfFileParser _dbfFile; 
        private string _filePath;
        public moFields Fields => _dbfFile.Fields;
        public List<moGeometry> Geometries => _shpFile.Geometries;
        public List<moAttributes> AttributesList => _dbfFile.AttributesList;
        public moGeometryTypeConstant GeometryType => _shpFile.GetMoGeometryType();

        #endregion

        public string FilePath
        {
            get { return _filePath; }
            set { _filePath = value; }
        }


        #region Constructors

        /// <summary>
        /// read ShapeFile and manage data 
        /// </summary>
        /// <param name="layerFilePath"></param>
        public ShapeFileParser(string FilePath)
        {
            _filePath = FilePath;
            _shxFile = new ShxParser(_filePath + ".shx");
            _shpFile = new ShpParser(_filePath + ".shp");
            _dbfFile = new DbfFileParser(_filePath + ".dbf");
        }

        #endregion



        //读取shapefile
        public moFeatures Read_ShapeFile()
        {
            _shxFile.Read();
            _dbfFile.Read();
            _shpFile.Read();
            moFeatures features = new moFeatures();
            for (int i = 0; i < _shpFile.Geometries.Count; ++i)
            {
                moFeature feature = new moFeature(_shpFile.GetMoGeometryType(),
                    _shpFile.Geometries[i], _dbfFile.AttributesList[i]);
                features.Add(feature);
            }
            return features;
        }

        public void Write_ShapeFile(string ShapeFilePath)
        {
            _shxFile.SaveToFile(ShapeFilePath + ".shx");
            _shpFile.SaveToFile(ShapeFilePath + ".shp");
            _dbfFile.SaveToFile(ShapeFilePath + ".dbf");
        }

        public void Read_shx()
        {
            _shxFile.Read();
        }

        public void Write_shx(string ShapeFilePath)
        {
            _shxFile.SaveToFile(ShapeFilePath + ".shx");
        }

        public void Write_shp(string ShapeFilePath)
        {
            _shpFile.SaveToFile(ShapeFilePath + ".shp");
        }

        public void Write_dbf(string ShapeFilePath)
        {
            _dbfFile.SaveToFile(ShapeFilePath + ".dbf");
        }

        public void Read_shp()
        {
            _shpFile.Read();
        }

        public void Read_dbf()
        {
            _dbfFile.Read();
        }
    }
}
