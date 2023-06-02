using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapCraft.FileProcessor
{
    public class ShxParser
    {
        #region 属性

        public long RecordCount { get; }
        private byte[] _reservedHeader;
        public List<int> RecordOffsets { get; }
        public List<int> RecordLengths { get; }

        #endregion

        #region 构造函数

        public ShxParser()
        {
            _reservedHeader = new byte[100];
            RecordCount = 0;
            RecordOffsets = new List<int>();
            RecordLengths = new List<int>();
        }

        public ShxParser(string shxFilePath)
        {
            FileStream fs = new FileStream(shxFilePath, FileMode.Open);
            BinaryReader br = new BinaryReader(fs);

            _reservedHeader = br.ReadBytes(100);
            RecordCount = (br.BaseStream.Length - 100) / 8;
            RecordOffsets = new List<int>();
            RecordLengths = new List<int>();
            while (true)
            {
                try
                {
                    RecordOffsets.Add(FileTools.ReadInt32InBigEndian(br) * 2);
                    RecordLengths.Add(FileTools.ReadInt32InBigEndian(br) * 2);
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

        #region 方法

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shxFilePath"></param>
        public void WriteToFile(string shxFilePath)
        {
            FileStream fs = new FileStream(shxFilePath, FileMode.Create, FileAccess.Write);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(_reservedHeader);
            for (int i = 0; i < RecordCount; i++)
            {
                //写入数据
                FileTools.WriteInt32InBigEndian(RecordOffsets[i] / 2, bw);
                FileTools.WriteInt32InBigEndian(RecordLengths[i] / 2, bw);
            }
        }

        #endregion
    }
}
