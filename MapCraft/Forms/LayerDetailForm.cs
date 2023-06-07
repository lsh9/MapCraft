using MyMapObjects;
using System;
using System.Windows.Forms;

namespace MapCraft.Forms
{
    public partial class LayerDetailForm : Form
    {
        private MapCraftForm Main;

        public LayerDetailForm(MapCraftForm main, Int32 layerIndex)
        {
            InitializeComponent();
            Main = main;
            // 将所有图层加入下拉框
            for(Int32 i = 0; i < Main.MapControl.Layers.Count; i++)
            {
                cbBoxLayers.Items.Add(Main.MapControl.Layers.GetItem(i).Name);
            }
            UpdateAll(layerIndex);
        }

        private void btnSavePath_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = ".shp文件|*.shp";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = true;
            if (saveFileDialog.ShowDialog() != DialogResult.OK) return;
            textBoxSavePath.Text = saveFileDialog.FileName;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            Int32 index = cbBoxLayers.SelectedIndex;
            moMapLayer layer = Main.MapControl.Layers.GetItem(index);
            layer.Name = textBoxName.Text;
            layer.Description = textBoxDescription.Text;
            if (Main.Shapefiles[index].FilePath != textBoxSavePath.Text)
            {
                Main.Shapefiles[index].FilePath = textBoxSavePath.Text;
                Main.Shapefiles[index].Write_ShapeFile(textBoxSavePath.Text);
            }
            Main.MapControl.Refresh();
            this.Close();
        }

        private void cbBoxLayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateAll(cbBoxLayers.SelectedIndex);
        }

        private void UpdateAll(Int32 index)
        {
            moMapLayer layer = Main.MapControl.Layers.GetItem(index);
            moRectangle rect = layer.Extent;
            // 重新显示所有信息
            cbBoxLayers.SelectedIndex = index;
            textBoxName.Text = layer.Name;
            textBoxDescription.Text = layer.Description;
            switch (layer.ShapeType)
            {
                case moGeometryTypeConstant.Point:
                    textBoxShapeType.Text = "Point";
                    break;
                case moGeometryTypeConstant.MultiPolyline:
                    textBoxShapeType.Text = "MultiPolyline";
                    break;
                case moGeometryTypeConstant.MultiPolygon:
                    textBoxShapeType.Text = "MultiPolygon";
                    break;
            }
            textBoxSavePath.Text = Main.Shapefiles[index].FilePath;
            textBoxMinX.Text = rect.MinX.ToString();
            textBoxMinY.Text = rect.MinY.ToString();
            textBoxMaxX.Text = rect.MaxX.ToString();
            textBoxMaxY.Text = rect.MaxY.ToString();
        }

    }
}
