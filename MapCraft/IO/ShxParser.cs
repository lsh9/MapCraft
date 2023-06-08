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

        public long RecordCount { get; set; }
        private byte[] _reservedHeader;
        public List<int> RecordOffsets { get; set; }
        public List<int> RecordLengths { get; set; }
        public string ShxFilePath { get; set; }

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
            ShxFilePath = shxFilePath;
            _reservedHeader = new byte[100];
            RecordCount = 0;
            RecordOffsets = new List<int>();
            RecordLengths = new List<int>();
        }

        #endregion

        #region 方法

        public void Read()
        {
            FileStream fs = new FileStream(ShxFilePath, FileMode.Open);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shxFilePath"></param>
        public void SaveToFile(string shxFilePath)
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
            fs.Dispose();
            bw.Dispose();
        }

        #endregion
    }
}
