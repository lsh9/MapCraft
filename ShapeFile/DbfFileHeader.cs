using System;
using System.Collections.Generic;
using System.IO;
using MyMapObjects;

namespace GISBox.ShapeFile
{
    /// <summary>
    /// .dbf header, contains two parts
    /// (1) Database Information: 32 bytes
    /// (2) Field Descriptors: n*32 bytes 
    /// </summary>
    public class DbfFileHeader
    {
        #region Properties
        //(1) Database Information: 32 bytes
        private byte _fileType;            //byte 0
        private byte[] _lastModifyDate;     //byte 1-3: Date of last update
        public uint RecordNumber { get; set; }    //byte 4-7: Number of records in the database file
        public ushort HeaderLength { get; set; }     //byte 8-9: Number of bytes in the header
        public ushort RecordLength { get; set; }    //byte 10-11: Number of bytes in a record

        private byte[] _reservedField;      //byte 12-31: unused

        //(2) Field Descriptors: n*32 bytes 
        public List<DbfFieldDescriptor> DbfFieldDescriptors { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// construct from file binary reader
        /// </summary>
        /// <param name="br"></param>
        public DbfFileHeader(BinaryReader br)
        {
            _fileType = br.ReadByte();
            _lastModifyDate = br.ReadBytes(3);
            RecordNumber = br.ReadUInt32();
            HeaderLength = br.ReadUInt16();
            RecordLength = br.ReadUInt16();
            _reservedField = br.ReadBytes(20);
            DbfFieldDescriptors = new List<DbfFieldDescriptor>();
            //0x0D as the field descriptor array terminator
            while (br.PeekChar() != 0x0D)
            {
                DbfFieldDescriptor curDbfFieldDescriptor = new DbfFieldDescriptor(br);
                DbfFieldDescriptors.Add(curDbfFieldDescriptor);
            }
        }

        /// <summary>
        /// construct a new dbf header  when create a new layer
        /// </summary>
        public DbfFileHeader()
        {
            _fileType = 0x02;
            _lastModifyDate = Util.CurDateAsBytes();
            RecordNumber = 0;
            HeaderLength = 33;
            // each record begins with a 1-byte "deletion" flag
            RecordLength = 1;
            _reservedField = new byte[20];
            DbfFieldDescriptors = new List<DbfFieldDescriptor>();
        }

        #endregion


        #region Methods
        /// <summary>
        /// convert DbfFieldDescriptors to moFields
        /// and set primary field
        /// </summary>
        /// <returns></returns>
        public moFields ConvertToMoFields()
        {
            moFields mapFields = new moFields();
            //Add all field description
            foreach (var t in DbfFieldDescriptors)
            {
                string curFieldName = t.FieldName;
                moValueTypeConstant curValueType;
                switch (t.FieldType)
                {
                    case DbfFieldType.Int:
                        curValueType = moValueTypeConstant.dInt32;
                        break;
                    case DbfFieldType.Single:
                        curValueType = moValueTypeConstant.dSingle;
                        break;
                    case DbfFieldType.Double:
                        curValueType = moValueTypeConstant.dDouble;
                        break;
                    case DbfFieldType.Text:
                    default:
                        curValueType = moValueTypeConstant.dText;
                        break;
                }
                moField curMapField = new moField(curFieldName, curValueType);
                mapFields.Append(curMapField);
            }
            //try to find field named id or ID
            for (int i = 0; i < mapFields.Count; i++)
            {
                if (mapFields.GetItem(i).Name != "id" 
                    && mapFields.GetItem(i).Name != "Id") continue;
                mapFields.PrimaryField = mapFields.GetItem(i).Name;
                return mapFields;
            }
            //if not found, set the first field as primary field
            mapFields.PrimaryField = mapFields.GetItem(0).Name;
            return mapFields;
        }

        /// <summary>
        /// change dbf header when add an field descriptor
        /// </summary>
        /// <param name="newField"></param>
        public void AddFieldDescriptor(DbfFieldDescriptor newField)
        {
            HeaderLength += 32;
            RecordLength += newField.FieldLength;
            DbfFieldDescriptors.Add(newField);
        }

        /// <summary>
        /// delete dbf header when delete an field descriptor
        /// </summary>
        /// <param name="index"></param>
        public void DeleteFieldDescriptor(int index)
        {
            if (index < 0 || index >= DbfFieldDescriptors.Count)
            {
                string error = "要删除的字段索引超出范围";
                throw new Exception(error);
            }
            DbfFieldDescriptor deletedDbfFieldDescriptor = DbfFieldDescriptors[index];
            HeaderLength -= 32;
            RecordLength -= deletedDbfFieldDescriptor.FieldLength;
            DbfFieldDescriptors.RemoveAt(index);
        }

        /// <summary>
        /// use binary writer to write dbf header to file
        /// </summary>
        /// <param name="bw"></param>
        public void WriteToFile(BinaryWriter bw)
        {
            //(1) Write database information
            bw.Write(_fileType);
            bw.Write(Util.CurDateAsBytes());
            bw.Write(RecordNumber);
            bw.Write(HeaderLength);
            bw.Write(RecordLength);
            bw.Write(_reservedField);

            //(2) Write fields descriptors
            foreach (var curField in DbfFieldDescriptors)
            {
                curField.WriteToFile(bw);
            }
            //end with 0x0D
            bw.Write((byte)0x0D);
        }
        #endregion
    }
}
