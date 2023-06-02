using MyMapObjects;
using System;
using System.IO;
using System.Windows.Forms;

namespace MapCraft.Forms
{
    public partial class CreateLayerForm : Form
    {
        #region 构造函数
        public CreateLayerForm()
        {
            InitializeComponent();

            chbLayerType.Items.Add("Point");
            chbLayerType.Items.Add("MultiPolyline");
            chbLayerType.Items.Add("MultiPolygon");
            chbLayerType.SelectedIndex = 0;
        }
        #endregion

        #region 窗体控件事件

        // 选择保存路径
        private void btnSavePath_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = ".shp文件|*.shp";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = true;
            if (saveFileDialog.ShowDialog() != DialogResult.OK) return;
            // 如果文件存在，询问是否覆盖
            textBoxSavePath.Text = saveFileDialog.FileName;
            textBoxLayrName.Text = Path.GetFileNameWithoutExtension(saveFileDialog.FileName);
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            moGeometryTypeConstant geometryType;
            string layerName;
            string savePath;
            if (chbLayerType.SelectedIndex == 0)
            {
                geometryType = moGeometryTypeConstant.Point;
            }
            else if (chbLayerType.SelectedIndex == 1)
            {
                geometryType = moGeometryTypeConstant.MultiPolyline;
            }
            else if (chbLayerType.SelectedIndex == 2)
            {
                geometryType = moGeometryTypeConstant.MultiPolygon;
            }

            savePath = textBoxSavePath.Text;
            if (savePath == string.Empty)
            {
                MessageBox.Show(@"请选择保存路径");
            }
            else
            {
                MapCraftForm main = (MapCraftForm)Owner;
                layerName = textBoxLayrName.Text;
                //main.GetCreateLayerInfo(layerName, geometryType, savePath);
                Close();
            }

        }

        #endregion

    }
}
