using MyMapObjects;
using System;
using System.Data;
using System.Windows.Forms;

namespace MapCraft.Forms
{
    public partial class SelectByAttributeForm : Form
    {

        #region 字段
        MapCraftForm Main;
        private int mSelectLayerIndex;
        private int mSelectFieldIndex;

        #endregion

        #region 构造函数
        public SelectByAttributeForm(MapCraftForm main)
        {
            InitializeComponent();
            Main = main;
            mSelectLayerIndex = -1;
            mSelectFieldIndex = -1;
            for (int i = 0; i < Main.MapControl.Layers.Count; i++)
            {
                cbLayers.Items.Add(Main.MapControl.Layers.GetItem(i).Name);
            }
        }

        #endregion


        #region 运算符按钮点击事件

        private void btnEq_Click(object sender, EventArgs e)
        {
            TextBoxSQL.AppendText("= ");
        }
        private void btnNeq_Click(object sender, EventArgs e)
        {
            TextBoxSQL.AppendText("<> ");
        }
        private void btnLike_Click(object sender, EventArgs e)
        {
            TextBoxSQL.AppendText("Like ");
        }

        private void btnGt_Click(object sender, EventArgs e)
        {
            TextBoxSQL.AppendText("> ");
        }

        private void btnGe_Click(object sender, EventArgs e)
        {
            TextBoxSQL.AppendText(">= ");
        }

        private void btnAnd_Click(object sender, EventArgs e)
        {
            TextBoxSQL.AppendText("And ");
        }

        private void btnLt_Click(object sender, EventArgs e)
        {
            TextBoxSQL.AppendText("< ");
        }

        private void btnLe_Click(object sender, EventArgs e)
        {
            TextBoxSQL.AppendText("<= ");
        }

        private void btnOr_Click(object sender, EventArgs e)
        {
            TextBoxSQL.AppendText("Or ");
        }

        private void btnIs_Click(object sender, EventArgs e)
        {
            TextBoxSQL.AppendText("Is ");
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            TextBoxSQL.AppendText("In ");
        }

        private void btnNot_Click(object sender, EventArgs e)
        {
            TextBoxSQL.AppendText("Not ");
        }

        private void btnBracket_Click(object sender, EventArgs e)
        {
            TextBoxSQL.AppendText("() ");
            TextBoxSQL.Enabled = true;
            TextBoxSQL.Select(TextBoxSQL.Text.Length - 2, 0);
        }

        private void btnLine_Click(object sender, EventArgs e)
        {
            TextBoxSQL.AppendText("_ ");
        }

        private void btnPercnt_Click(object sender, EventArgs e)
        {
            TextBoxSQL.AppendText("% ");
        }

        private void btnNull_Click(object sender, EventArgs e)
        {
            TextBoxSQL.AppendText("Null ");
        }

        #endregion

        #region 输入与查看事件
        private void cbLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*if (mSelectLayerIndex < 0)
                return;*/
            mSelectLayerIndex = cbLayers.SelectedIndex;
            labelSQL.Text = $"SELECT * FROM {Main.MapControl.Layers.GetItem(mSelectLayerIndex).Name} WHERE ";
            listBoxFields.Items.Clear();
            mSelectFieldIndex = -1;

            for (int i = 0; i < Main.MapControl.Layers.GetItem(mSelectLayerIndex).AttributeFields.Count; i++)
            {
                listBoxFields.Items.Add(Main.MapControl.Layers.GetItem(mSelectLayerIndex).AttributeFields.GetItem(i).Name);
            }
            listBoxUniqueValues.Items.Clear();
        }

        private void ListBoxFields_SelectedIndexChanged(object sender, EventArgs e)
        {
            mSelectFieldIndex = listBoxFields.SelectedIndex;
        }

        private void ListBoxFields_MouseClick(object sender, MouseEventArgs e)
        {
            // 根据点击位置获取index
            int index = listBoxFields.IndexFromPoint(e.Location);
            if (index == ListBox.NoMatches)
                return;
            mSelectFieldIndex = index;
            listBoxFields.SelectedIndex = index;
        }

        private void ListBoxFields_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // 根据点击位置获取index
            int index = listBoxFields.IndexFromPoint(e.Location);
            if (index == ListBox.NoMatches)
                return;
            mSelectFieldIndex = index;
            listBoxFields.SelectedIndex = index;
            TextBoxSQL.AppendText(Main.MapControl.Layers.GetItem(mSelectLayerIndex).AttributeFields.GetItem(mSelectFieldIndex).Name + " ");
        }
        
        private void ListBoxUniqueValues_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // 根据点击位置获取index
            int index = listBoxUniqueValues.IndexFromPoint(e.Location);
            if (index == ListBox.NoMatches)
                return;
            listBoxUniqueValues.SelectedIndex = index;
            TextBoxSQL.AppendText(listBoxUniqueValues.Items[index] + " ");
        }

        private void btnGetUniqueValue_Click(object sender, EventArgs e)
        {
            if (mSelectFieldIndex < 0)
                return;
            listBoxUniqueValues.Items.Clear();
            moMapLayer layer = Main.MapControl.Layers.GetItem(mSelectLayerIndex);
            for (int i = 0; i < layer.Features.Count; i++)
            {
                if (layer.AttributeFields.GetItem(mSelectFieldIndex).ValueType == moValueTypeConstant.dText)
                {
                    listBoxUniqueValues.Items.Add("\'" + layer.Features.GetItem(i).Attributes.GetItem(mSelectFieldIndex) + "\'");
                }
                else
                {
                    listBoxUniqueValues.Items.Add(layer.Features.GetItem(i).Attributes.GetItem(mSelectFieldIndex).ToString());
                }
            }
            for (int i = 0; i < listBoxUniqueValues.Items.Count; i++)
            {
                for (int j = i + 1; j < listBoxUniqueValues.Items.Count; j++)
                {
                    if (listBoxUniqueValues.Items[i].Equals(listBoxUniqueValues.Items[j]))
                    {
                        listBoxUniqueValues.Items.RemoveAt(j);
                        j--;
                    }
                }
            }
        }
        #endregion

        #region 运行SQL相关按钮点击事件
        // 清空
        private void btnClear_Click(object sender, EventArgs e)
        {
            TextBoxSQL.Clear();
        }

        // 验证
        private void btnValidate_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = GetDataTable();
                dt.Select(TextBoxSQL.Text);
                MessageBox.Show("语句合法，验证成功");
            }
            catch
            {
                MessageBox.Show("非法语句，请重新输入");
            }
        }

        // 确定
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                apply();
                Close();
            }
            catch
            {
                MessageBox.Show("非法语句，请重新输入");
            }
        }

        // 应用
        private void btnApply_Click(object sender, EventArgs e)
        {
            try
            {
                apply();
            }
            catch
            {
                MessageBox.Show("非法语句，请重新输入");
            }
        }

        private void apply()
        {
            DataTable dt = GetDataTable();
            DataRow[] dataRows = dt.Select(TextBoxSQL.Text);
            Main.MapControl.Layers.GetItem(mSelectLayerIndex).SelectedFeatures.Clear();
            if (dataRows.Length > 0)
            {
                for (int i = 0; i < dataRows.Length; i++)
                {
                    Main.MapControl.Layers.GetItem(mSelectLayerIndex).SelectedFeatures.Add(Main.MapControl.Layers.GetItem(mSelectLayerIndex).Features.GetItem(dt.Rows.IndexOf(dataRows[i])));
                }
                Main.MapControl.RedrawTrackingShapes();
            }
            else
                MessageBox.Show("未查询到符合条件要素");
        }

        private DataTable GetDataTable()
        {
            // 当前选中图层
            moMapLayer layer = Main.MapControl.Layers.GetItem(mSelectLayerIndex);
            DataTable dt = new DataTable();
            for (int i = 0; i < layer.AttributeFields.Count; i++)
            {
                moValueTypeConstant valueTypeConstant = layer.AttributeFields.GetItem(i).ValueType;
                switch (valueTypeConstant)
                {
                    case moValueTypeConstant.dInt16:
                        dt.Columns.Add(layer.AttributeFields.GetItem(i).Name, typeof(short));
                        break;
                    case moValueTypeConstant.dInt32:
                        dt.Columns.Add(layer.AttributeFields.GetItem(i).Name, typeof(int));
                        break;
                    case moValueTypeConstant.dInt64:
                        dt.Columns.Add(layer.AttributeFields.GetItem(i).Name, typeof(long));
                        break;
                    case moValueTypeConstant.dDouble:
                        dt.Columns.Add(layer.AttributeFields.GetItem(i).Name, typeof(double));
                        break;
                    case moValueTypeConstant.dSingle:
                        dt.Columns.Add(layer.AttributeFields.GetItem(i).Name, typeof(float));
                        break;
                    case moValueTypeConstant.dText:
                        dt.Columns.Add(layer.AttributeFields.GetItem(i).Name, typeof(string));
                        break;
                }
            }
            for (int i = 0; i < layer.Features.Count; i++)
            {
                dt.Rows.Add(layer.Features.GetItem(i).Attributes.ToArray());
            }
            return dt;
        }
        #endregion


    }
}
