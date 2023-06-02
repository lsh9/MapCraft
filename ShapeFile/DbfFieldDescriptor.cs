using System.IO;
using System.Text;
using MyMapObjects;

namespace GISBox.ShapeFile
{
    public class DbfFieldDescriptor
    {
        #region Properity
        public string FieldName { set; get; } //byte 0-10
        public DbfFieldType FieldType { set; get; }
        private byte[] _reserved1;      //byte 12-15: unused
        public byte FieldLength { set; get; } //byte 16
        private byte[] _reserved2;      //byte 17-31: unused
        #endregion

        #region Constructors
        /// <summary>
        /// construct from file binary reader
        /// </summary>
        /// <param name="br"></param>
        public DbfFieldDescriptor(BinaryReader br)
        {
            FieldName = Encoding.UTF8.GetString(br.ReadBytes(11), 0, 11).Replace("\0", "").ToLower();
            FieldType = ReadDbfFileType(br);
            _reserved1 = br.ReadBytes(4);
            FieldLength = br.ReadByte();
            _reserved2 = br.ReadBytes(15);
        }

        /// <summary>
        /// construct from moField
        /// </summary>
        /// <param name="mapField"></param>
        public DbfFieldDescriptor(moField mapField)
        {
            FieldName = mapField.Name;
            switch (mapField.ValueType)
            {
                case moValueTypeConstant.dInt16:
                case moValueTypeConstant.dInt32:
                case moValueTypeConstant.dInt64:
                    FieldType = DbfFieldType.Int;
                    FieldLength = 8;
                    break;
                case moValueTypeConstant.dSingle:
                    FieldType = DbfFieldType.Single;
                    FieldLength = 8;
                    break;
                case moValueTypeConstant.dDouble:
                    FieldType = DbfFieldType.Double;
                    FieldLength = 16;
                    break;
                case moValueTypeConstant.dText:
                default:
                    FieldType = DbfFieldType.Text;
                    FieldLength = 100;
                    break;
            }
            //reserved bytes set to 0
            _reserved1 = new byte[4];
            _reserved2 = new byte[15];
        }
        #endregion

        #region Methods

        /// <summary>
        /// Write one dbf field Descriptor
        /// </summary>
        /// <param name="bw"></param>
        public void WriteToFile(BinaryWriter bw)
        {
            bw.Write(Util.ConvertStringToBytes(FieldName, 11));
            bw.Write((byte)FieldType);
            bw.Write(_reserved1);
            bw.Write(FieldLength);
            bw.Write(_reserved2);
        }
        #endregion

        #region Private functions

        /// <summary>
        /// Read a char 
        /// </summary>
        /// <param name="br"></param>
        /// <returns></returns>
        DbfFieldType ReadDbfFileType(BinaryReader br)
        {
            char fieldTypeChar = br.ReadChar();
            DbfFieldType fieldType;
            switch (fieldTypeChar)
            {
                case 'I':
                    fieldType = DbfFieldType.Int;
                    break;
                case 'F':
                    fieldType = DbfFieldType.Single;
                    break;
                case 'N':
                case 'B':
                    fieldType = DbfFieldType.Double;
                    break;
                default:
                    //other type set to text
                    fieldType = DbfFieldType.Text;
                    break;
            }
            return fieldType;
        }
        #endregion

    }
}
