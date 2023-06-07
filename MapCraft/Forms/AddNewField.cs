using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MapCraft.Forms
{
    public partial class AddNewField : Form
    {
        public AttributeTable Table { get; }
        public AddNewField(AttributeTable table)
        {
            InitializeComponent();
            Table = table;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Table.NewFieldName = textBox1.Text;
            Table.AddNewField();
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string typeStr = comboBox1.SelectedItem.ToString();
            switch (typeStr)
            {
                case "Int16":
                    Table.NewFieldType = MyMapObjects.moValueTypeConstant.dInt16;
                    break;
                case "Int32":
                    Table.NewFieldType = MyMapObjects.moValueTypeConstant.dInt32;
                    break;
                case "Int64":
                    Table.NewFieldType = MyMapObjects.moValueTypeConstant.dInt64;
                    break;
                case "Single":
                    Table.NewFieldType = MyMapObjects.moValueTypeConstant.dSingle;
                    break;
                case "Double":
                    Table.NewFieldType = MyMapObjects.moValueTypeConstant.dDouble;
                    break;
                case "Text":
                    Table.NewFieldType = MyMapObjects.moValueTypeConstant.dText;
                    string strlen = Microsoft.VisualBasic.Interaction.InputBox("请输入文本长度", "文本长度", "1000", -1, -1);
                    if (strlen == string.Empty)
                    {
                        MessageBox.Show("文本长度为0，请重新选择文本类型！");
                        return;
                    }
                    Table.NewFieldLength = Convert.ToInt32(strlen);
                    break;
            }
        }
    }
}
