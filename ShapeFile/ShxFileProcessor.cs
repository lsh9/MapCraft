using System.Collections.Generic;
using System.IO;

namespace GISBox.ShapeFile
{
    /// <summary>
    /// read shx file, contains two parts
    /// (1) header, not used
    /// (2) any number of 8-byte fixed-length records in big endian
    /// IMPORTANT: the unit in shx file is 16-bit
    /// convert the unit to 8-bit in this class
    /// </summary>
    public class ShxFileProcessor
    {
        #region Properties

        public long RecordCount { get; }
        private byte[] _reservedHeader;
        public List<int> RecordOffsets { get; }
        public List<int> RecordLengths { get; }

        #endregion

        #region Constructors

        public ShxFileProcessor()
        {
            _reservedHeader = new byte[100];
            RecordCount = 0;
            RecordOffsets = new List<int>();
            RecordLengths = new List<int>();
        }

        public ShxFileProcessor(string shxFilePath)
        {
            FileStream fs = new FileStream(shxFilePath, FileMode.Open);
            BinaryReader br = new BinaryReader(fs);

            _reservedHeader = br.ReadBytes(100);
            RecordCount = (br.BaseStream.Length - 100) / 8;
            RecordOffsets= new List<int>();
            RecordLengths= new List<int>();
            while (true)
            {
                try
                {
                    // divide 2 to convert unit
                    RecordOffsets.Add(Util.ReadInt32InBigEndian(br) * 2);
                    RecordLengths.Add(Util.ReadInt32InBigEndian(br) * 2);
                }
                catch (IOException)
                {
                    break;  //读到文件尾
                }
            }

            br.Dispose();
            fs.Dispose();
        }

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shxFilePath"></param>
        public void WriteToFile(string shxFilePath)
        {
            FileStream fs = new FileStream(shxFilePath,FileMode.Create,FileAccess.Write);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(_reservedHeader);
            for (int i = 0; i < RecordCount; i++)
            {
                // write to file in big endian
                Util.WriteInt32InBigEndian(RecordOffsets[i]/2,bw);
                Util.WriteInt32InBigEndian(RecordLengths[i]/2,bw);
            }
        }




        #endregion
    }
}
