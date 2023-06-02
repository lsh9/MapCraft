using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MyMapObjects;

namespace GISBox.ShapeFile
{
    public class MoAttributesList
    {
        #region Properity

        public List<moAttributes> AttributesList { get; }
        public int Count => AttributesList.Count;
        public moAttributes this[int index] => AttributesList[index];

        #endregion Properity

        public MoAttributesList()
        {
            AttributesList = new List<moAttributes>();
        }

        /// <summary>
        /// construct from file by binary reader
        /// </summary>
        /// <param name="dbfFileHeader">contains field descriptors</param>
        /// <param name="br"></param>
        public MoAttributesList(DbfFileHeader dbfFileHeader,BinaryReader br)
        {
            AttributesList = new List<moAttributes>();
            // locate to the start of record data
            br.BaseStream.Seek(dbfFileHeader.HeaderLength, SeekOrigin.Begin);
            // iterate all records
            for (int i = 0; i < dbfFileHeader.RecordNumber; ++i)
            {
                byte[] curRecordContent = br.ReadBytes(dbfFileHeader.RecordLength);
                moAttributes curAttributes = new moAttributes();
                // a record contains attributes values of multiply fields
                // each record begins with a 1-byte "deletion" flag
                int sCurIndex = 1;
                foreach (var curDbfFieldDescriptor in dbfFileHeader.DbfFieldDescriptors)
                {
                    // IMPORTANT: all field data is ASCII
                    // so read all field data as string, and convert to difference types
                    string curDbfAttribute = Encoding.UTF8.GetString(curRecordContent, sCurIndex, curDbfFieldDescriptor.FieldLength).Trim((char)0x20).Replace("\0", "");
                    sCurIndex += curDbfFieldDescriptor.FieldLength;

                    switch (curDbfFieldDescriptor.FieldType)
                    {
                        case DbfFieldType.Int:
                            {
                                int curMoAttribute = Convert.ToInt32(curDbfAttribute);
                                curAttributes.Append(curMoAttribute);
                                break;
                            }

                        case DbfFieldType.Single:
                        {
                            float curMoAttribute = 0;
                            if(curDbfAttribute.Length!=0)
                                curMoAttribute = Convert.ToSingle(curDbfAttribute); 
                            curAttributes.Append(curMoAttribute);
                            break;
                            }
                        case DbfFieldType.Double:
                        {
                            double curMoAttribute = 0;
                            if (curDbfAttribute.Length != 0)
                                curMoAttribute = Convert.ToDouble(curDbfAttribute);
                            curAttributes.Append(curMoAttribute);
                            break;
                            }
                        case DbfFieldType.Text:
                            curAttributes.Append(curDbfAttribute);
                            break;
                        default:
                            const string error = "头文件字段说明中包含未定义字段！";
                            throw new Exception(error);
                    }
                }
                AttributesList.Add(curAttributes);
            }
        }



        /// <summary>
        /// write attributes list to file by binary writer
        /// </summary>
        /// <param name="dbfFileHeader"></param>
        /// <param name="bw"></param>
        public void WriteToFile(DbfFileHeader dbfFileHeader, BinaryWriter bw)
        {
            // (1) length of each field 
            int[] fieldLengths = new int[dbfFileHeader.DbfFieldDescriptors.Count];
            for (int i = 0; i < dbfFileHeader.DbfFieldDescriptors.Count; ++i)
            {
                fieldLengths[i] = dbfFileHeader.DbfFieldDescriptors[i].FieldLength;
            }
            //(2) write each record
            foreach (var t in AttributesList)
            {
                // each record begins with a 1-byte "deletion" flag
                bw.Write((byte)0x20);
                object[] curAttributes = t.ToArray();
                for (int j = 0; j < curAttributes.Length; ++j)
                {
                    // for all type of strings, converted to string 
                    // write string to file
                    string curMoAttribute = curAttributes[j].ToString();
                    bw.Write(Util.ConvertStringToBytes(curMoAttribute, fieldLengths[j]));
                }
            }
        }
    }
}