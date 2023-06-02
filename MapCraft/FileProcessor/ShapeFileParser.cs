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
        private List<moGeometry> _geometries => _shpFile.Geometries;
        private moFields _fields => _dbfFile.Fields;
        private List<moAttributes> _attributesList => _dbfFile.AttributesList;
        private moGeometryTypeConstant _geometryType => _shpFile.GetMoGeometryType();


        public string Filepath
        {
            get{ return _filePath; }
            set { _filePath = value; }
        }

        public List<moGeometry> Geometries
        {
            get { return _geometries; }
        }

        public moFields Fields
        {
            get { return _fields; }
        }

        public List<moAttributes> AttributesList
        {
            get { return _attributesList; }
        }

        public moGeometryTypeConstant GeometryType
        {
            get { return _geometryType; }
        }


        #endregion




        #region Constructors

        /// <summary>
        /// read ShapeFile and manage data 
        /// </summary>
        /// <param name="layerFilePath"></param>
        public ShapeFileParser(string FilePath)
        {
            _filePath = FilePath;
            _shxFile = new ShxParser(FilePath + ".shx");
            _shpFile = new ShpParser(_shxFile, FilePath + ".shp");
        }

        #endregion
    }
}
