using MapCraft.FileProcessor;
using Microsoft.VisualBasic;
using MyMapObjects;
using System;
using System.IO;
using System.Windows.Forms;

namespace MapCraft.Forms
{
    public partial class CreateLayerForm : Form
    {

        #region 字段

        MapCraftForm Main = null;
        private moFields mFields = new moFields();
        
        #endregion

        #region 构造函数
        public CreateLayerForm(MapCraftForm main)
        {
            InitializeComponent();
            Owner = main;
            Main = main;
            cbBoxLayerType.Items.Add("Point");
            cbBoxLayerType.Items.Add("MultiPolyline");
            cbBoxLayerType.Items.Add("MultiPolygon");
            cbBoxLayerType.SelectedIndex = 0;

            // 初始化字段列表
            cbBoxValueType.Items.Add("短整型");
            cbBoxValueType.Items.Add("整型");
            cbBoxValueType.Items.Add("长整型");
            cbBoxValueType.Items.Add("单精度浮点型");
            cbBoxValueType.Items.Add("双精度浮点型");
            cbBoxValueType.Items.Add("文本型");
            cbBoxValueType.SelectedIndex = 0;
        }
        #endregion

        #region 窗体控件事件

        // 选择保存路径按钮点击
        private void btnSavePath_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = ".shp文件|*.shp";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = true;
            if (saveFileDialog.ShowDialog() != DialogResult.OK) return;
            textBoxSavePath.Text = saveFileDialog.FileName;
            textBoxFieldName.Text = Path.GetFileNameWithoutExtension("FID");
        }

        // 添加字段按钮点击
        private void btnAddField_Click(object sender, EventArgs e)
        {
            string fieldName = textBoxFieldName.Text;
            if (fieldName == string.Empty)
            {
                MessageBox.Show("请输入字段名称！");
                return;
            }
            moValueTypeConstant valueType = (moValueTypeConstant)(cbBoxValueType.SelectedIndex);
            // 文本型字段需要输入长度
            if (valueType == moValueTypeConstant.dText)
            {
                // 弹出输入框，输入字段长度，添加引用Microsoft.VisualBasic
                string lengthStr = Microsoft.VisualBasic.Interaction.InputBox("请输入文本型字段的长度", "字段长度", "200", -1, -1);
                if (lengthStr == string.Empty) return;
                int length = Convert.ToInt32(lengthStr);
                moField textField = new moField(fieldName, valueType, length);
                mFields.Append(textField);
                listBoxFields.Items.Add(textField.Name + " " + textField.ValueType.ToString());
                return;
            }
            moField field = new moField(fieldName, valueType);
            mFields.Append(field);
            listBoxFields.Items.Add(field.Name + " " + field.ValueType.ToString());
        }

        // 删除字段按钮点击
        private void btnDeleteField_Click(object sender, EventArgs e)
        {
            int index = listBoxFields.SelectedIndex;
            if (index == -1)
            {
                MessageBox.Show("请选择要删除的字段！");
                return;
            }
            mFields.RemoveAt(index);
            listBoxFields.Items.RemoveAt(index);
        }
        
        // 创建按钮点击
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            moGeometryTypeConstant geometryType = (moGeometryTypeConstant)(cbBoxLayerType.SelectedIndex);
            string savePath = textBoxSavePath.Text;

            if (savePath == string.Empty)
            {
                MessageBox.Show("请选择保存路径！");
            }
            else
            {
                string layerName = Path.GetFileNameWithoutExtension(savePath);
                string layerPath = Path.GetDirectoryName(savePath) + layerName;
                ShapeFileParser shapeFileParser = new ShapeFileParser(layerPath);
                shapeFileParser.Write();
                Main.AddLayer(shapeFileParser);
                Close();
            }

        }



        #endregion

    }
}
