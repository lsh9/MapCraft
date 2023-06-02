using MyMapObjects;
using System;
using System.Data;
using System.Windows.Forms;

namespace MapCraft.Forms
{
    public partial class IdentifyForm : Form
    {
        private DataTable mDataTableSelect; // 选中数据的数据表
        private moMapLayer mLayer;
        private moFeatures mFeatures;

        public IdentifyForm(moMapLayer layer, moFeatures features)
        {
            InitializeComponent();
            mLayer = layer;
            mFeatures = features;
            lblLayerName.Text = mLayer.Name;
            for (int i = 0; i < mFeatures.Count; i++)
            {
                moFeature sFeature = mFeatures.GetItem(i);
                moAttributes curAttributes = sFeature.Attributes;
                treeView.Nodes.Add(Convert.ToString(curAttributes.GetItem(0)));
            }
            //treeView.Nodes[0].Expand();
            //ShowTable(0);
        }

        private void ShowTable(int nodeIndex)
        {
            int sFieldCount = mLayer.AttributeFields.Count;
            mDataTableSelect = new DataTable();
            table.DataSource = null;
            table.DataSource = mDataTableSelect;
            mDataTableSelect.Columns.Add("字段", typeof(string));
            mDataTableSelect.Columns.Add("值", typeof(string));
            if (nodeIndex < mFeatures.Count)
            {
                for (int i = 0; i < sFieldCount; i++)
                {
                    mDataTableSelect.Rows.Add(mLayer.AttributeFields.GetItem(i).Name, Convert.ToString(mFeatures.GetItem(nodeIndex).Attributes.GetItem(i)));
                }
            }
        }

        private void treeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            int index = e.Node.Index;
            ShowTable(index);
        }
    }
}
