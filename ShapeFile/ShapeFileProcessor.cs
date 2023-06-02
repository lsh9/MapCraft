using System.Collections.Generic;
using MyMapObjects;

namespace GISBox.ShapeFile
{
    /// <summary>
    /// manage a ShapeFile
    /// including .shx .shp .dbf three file
    /// </summary>
    public class ShapeFileProcessor
    {
        #region Properties

        private ShxFileProcessor _shxFile;
        private ShpFileProcessor _shpFile;
        private DbfFileProcessor _dbfFile;

        public moGeometryTypeConstant GeometryType => _shpFile.GeometryType;
        public moFields Fields => _dbfFile.Fields;
        public List<moGeometry> Geometries => _shpFile.Geometries;
        public List<moAttributes> AttributesList => _dbfFile.AttributesList;
        #endregion

        #region Constructors

        /// <summary>
        /// read ShapeFile and manage data 
        /// </summary>
        /// <param name="layerFilePath"></param>
        public ShapeFileProcessor(string layerFilePath)
        {
            _shxFile = new ShxFileProcessor(layerFilePath + ".shx");
            _shpFile = new ShpFileProcessor(_shxFile, layerFilePath + ".shp");
            _dbfFile = new DbfFileProcessor(layerFilePath + ".dbf");
        }

        /// <summary>
        /// construct from a moMapLayer
        /// </summary>
        /// <param name="layer"></param>
        public ShapeFileProcessor(moMapLayer layer)
        {

        }

        #endregion
    }
}