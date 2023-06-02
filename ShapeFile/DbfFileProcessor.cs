using System;
using System.Collections.Generic;
using System.IO;
using MyMapObjects;

namespace GISBox.ShapeFile
{
    /// <summary>
    /// Read dbf file, contains two parts
    /// (1) dbf header
    /// (2) moAttributes list
    /// </summary>
    public class DbfFileProcessor
    {
        #region Properity

        // (1) dbf header
        private DbfFileHeader _dbfFileHeader;

        // (2) attributes sheet in MyMapObjects format

        private MoAttributesList _attributesList;

        public moFields Fields => _dbfFileHeader.ConvertToMoFields();
        public List<moAttributes> AttributesList => _attributesList.AttributesList;
        #endregion

        #region Constructor

        public DbfFileProcessor()
        {
            _dbfFileHeader = new DbfFileHeader();
            _attributesList = new MoAttributesList();
        }

        /// <summary>
        /// Construct from dbf file, there are two cases
        /// (1) read existed file
        /// (2) create a new file
        /// </summary>
        /// <param name="dbfFilePath"></param>
        public DbfFileProcessor(string dbfFilePath)
        {
            FileStream fs = new FileStream(dbfFilePath, FileMode.Open);
                BinaryReader br = new BinaryReader(fs);

                _dbfFileHeader = new DbfFileHeader(br);
                _attributesList = new MoAttributesList(_dbfFileHeader,br);

                br.Dispose();
                fs.Dispose();

        }


        #endregion


        #region Methods

        /// <summary>
        /// Add a new field and set initial attributes
        /// </summary>
        /// <param name="newField"></param>
        /// <param name="newAttributes">the initial value of new field</param>
        public void AddField(moField newField, moAttributes newAttributes)
        {
            // the length of new attributes should equal to the count
            object[] newAttributesArray = newAttributes.ToArray();
            if (newAttributesArray.Length != _attributesList.Count)
            {
                const string error = "新增属性值个数不等于要素个数！";
                throw new Exception(error);
            }
            //(1) update header
            _dbfFileHeader.AddFieldDescriptor(new DbfFieldDescriptor(newField));
            //(2) update attributes
            for (int i = 0; i < newAttributesArray.Length; ++i)
            {
                _attributesList[i].Append(newAttributesArray[i]);
            }
        }

        /// <summary>
        /// delete field by index
        /// </summary>
        /// <param name="index">要删除的字段索引</param>
        public void DeleteField(int index)
        {
            if (index < 0 || index >= _dbfFileHeader.DbfFieldDescriptors.Count)
            {
                const string error = "删除的字段索引超出范围！";
                throw new Exception(error);
            }
            //(1) update header
            _dbfFileHeader.DeleteFieldDescriptor(index);
            //(2) update attributes
            for (int i=0;i<_attributesList.Count;i++)
            {
                _attributesList[i].RemoveAt(index);
            }
        }


        /// <summary>
        /// save to the file which is read
        /// </summary>
        public void SaveToFile(string dbfFilePath)
        {
            FileStream fs = new FileStream(dbfFilePath, FileMode.Create, FileAccess.Write);
            BinaryWriter bw = new BinaryWriter(fs);
            _dbfFileHeader.WriteToFile(bw);
            _attributesList.WriteToFile(_dbfFileHeader,bw);
            bw.Dispose();
            fs.Dispose();
        }

        #endregion

    }
}