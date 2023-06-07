using MyMapObjects;
using System;
using System.Data;
using System.Windows.Forms;

namespace MapCraft.Forms
{
    public partial class IdentifyForm : Form
    {
        private moMapLayer mLayer;
        private moFeatures mFeatures;
        private MapCraftForm Main;
        public Int32 IdentifyIndex = -1;

        public IdentifyForm(MapCraftForm main)
        {
            InitializeComponent();
            Main = main;
            UpdateLayers();
        }

        private void cbBoxLayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            treeViewFeatures.Nodes.Clear();
            dataGridViewAttributes.DataSource = null;
            int index = cbBoxLayers.SelectedIndex;
            mLayer = Main.MapControl.Layers.GetItem(index);
            IdentifyIndex = index;
        }

        private void treeViewFeatures_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            int index = e.Node.Index;
            UpdateAttributes(index);
            // 地图控件闪烁图形
            moGeometry[] geoms = new moGeometry[1];
            geoms[0] = (Main.MapControl.Layers.GetItem(IdentifyIndex).Features.GetItem(index).Geometry);
            Main.MapControl.FlashShapes(geoms, 3, 800);
        }

        public void Show(moFeatures features, Int32 index = -1)
        {
            UpdateLayers();
            if (index == -1 && cbBoxLayers.SelectedIndex >= 0)
            {
                mLayer = Main.MapControl.Layers.GetItem(IdentifyIndex);
            }
            else if (index >= 0)
            {
                mLayer = Main.MapControl.Layers.GetItem(index);
            }
            else if (features.Count == 0)
            {
                return;
            }
            cbBoxLayers.SelectedIndex = index;
            mFeatures = features;
            for (int i = 0; i < mFeatures.Count; i++)
            {
                moFeature sFeature = mFeatures.GetItem(i);
                treeViewFeatures.Nodes.Add(Convert.ToString(sFeature.Attributes.GetItem(0)));
            }
            UpdateAttributes(0);
            this.Show();
            this.TopMost = true;
        }

        private void UpdateAttributes(int nodeIndex)
        {
            int sFieldCount = mLayer.AttributeFields.Count;
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("字段", typeof(string));
            dataTable.Columns.Add("值", typeof(string));
            if (nodeIndex < mFeatures.Count)
            {
                for (int i = 0; i < sFieldCount; i++)
                {
                    dataTable.Rows.Add(mLayer.AttributeFields.GetItem(i).Name, Convert.ToString(mFeatures.GetItem(nodeIndex).Attributes.GetItem(i)));
                }
            }
            dataGridViewAttributes.DataSource = dataTable;
        }

        private void UpdateLayers()
        {
            // 清楚原来
            cbBoxLayers.Items.Clear();
            // 将所有图层加入列表
            for (int i = 0; i < Main.MapControl.Layers.Count; i++)
            {
                moMapLayer sLayer = Main.MapControl.Layers.GetItem(i);
                cbBoxLayers.Items.Add(sLayer.Name);
            }
        }

    }
}
