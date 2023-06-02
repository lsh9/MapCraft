using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MapCraft.Forms
{
    public partial class SelectByAttributeForm : Form
    {

        #region 字段
        MapCraftForm MainForm;
        private int mLayerSelectIndex;
        private int mFieldSelectIndex;
        private DataTable mDataTable;   // 数据表

        #endregion

        #region 构造函数
        public SelectByAttributeForm(MapCraftForm main)
        {
            InitializeComponent();
            MainForm = main;
            mLayerSelectIndex = -1;
            mFieldSelectIndex = -1;
            LoadLayers();
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

        #endregion

        #region 输入与查看事件
        // 选中某个图层后
        private void SelectBoxLayer_SelectionChangeCommitted(object sender, EventArgs e)
        {
            mLayerSelectIndex = SelectBoxLayer.SelectedIndex;// 获取选中的图层的索引
            ListBoxUniqueValues.Items.Clear();// 重新选择了图层必然唯一值框要清零
            ListBoxFields.Items.Clear();// 清零字段显示图层
            LoadFields();// 重新加载下拉框
            LoadDataTable();// 重新建立数据表
            mFieldSelectIndex = -1;// 同时重新清零上次选中的字段
        }

        // 选中某个字段后
        private void ListBoxFields_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 暂时无用
        }

        // 单击Fields_List时，单击一下选中，单击第二下将字体投到下方输入框
        private void ListBoxFields_MouseClick(object sender, MouseEventArgs e)
        {
            int index = ListBoxFields.IndexFromPoint(e.Location);// 获取index
            if (index == ListBox.NoMatches)
                return;
            if (index == mFieldSelectIndex)
            {
                // 如果是第二次选中了，就把名字添加到下面的文本框
                TextBoxSQL.AppendText(MainForm.MapControl.Layers.GetItem(mLayerSelectIndex).AttributeFields.GetItem(mFieldSelectIndex).Name + " ");
            }
            else
            {
                // 第一次点就普普通通即可
                mFieldSelectIndex = index;// 选中这个条目
            }
            ListBoxFields.SelectedIndex = index;// 选中这个条目
        }
        // 双击Fields_List时，直接将文本投入下面框
        private void ListBoxFields_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = ListBoxFields.IndexFromPoint(e.Location);// 获取index
            if (index == ListBox.NoMatches)
                return;
            TextBoxSQL.AppendText(MainForm.MapControl.Layers.GetItem(mLayerSelectIndex).AttributeFields.GetItem(mFieldSelectIndex).Name + " ");
            mFieldSelectIndex = index;// 选中这个条目
            ListBoxFields.SelectedIndex = index;// 选中这个条目
        }
        // 双击唯一值时，直接将文本投入下面框
        private void ListBoxUniqueValues_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = ListBoxUniqueValues.IndexFromPoint(e.Location);
            if (index == ListBox.NoMatches)
                return;
            ListBoxUniqueValues.SelectedIndex = index;// 选中这个条目
            TextBoxSQL.AppendText(ListBoxUniqueValues.Items[index] + " ");
        }

        // 唯一值
        private void btnGetUniqueValue_Click(object sender, EventArgs e)
        {
            if (mFieldSelectIndex < 0)
                return;// 如果没有选择字段，就return
            ListBoxUniqueValues.Items.Clear();
            MyMapObjects.moMapLayer layerTemp = MainForm.MapControl.Layers.GetItem(mLayerSelectIndex);
            for (int i = 0; i < layerTemp.Features.Count; i++)
            {
                if (layerTemp.AttributeFields.GetItem(mFieldSelectIndex).ValueType == MyMapObjects.moValueTypeConstant.dText)
                {
                    ListBoxUniqueValues.Items.Add("\'" + layerTemp.Features.GetItem(i).Attributes.GetItem(mFieldSelectIndex) + "\'");
                }
                else
                {
                    ListBoxUniqueValues.Items.Add(layerTemp.Features.GetItem(i).Attributes.GetItem(mFieldSelectIndex).ToString());
                }
            }
            for (int i = 0; i < ListBoxUniqueValues.Items.Count; i++)
            {
                for (int j = i + 1; j < ListBoxUniqueValues.Items.Count; j++)
                {
                    if (ListBoxUniqueValues.Items[i].Equals(ListBoxUniqueValues.Items[j]))
                    {
                        ListBoxUniqueValues.Items.RemoveAt(j);
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
                mDataTable.Select(TextBoxSQL.Text);
                MessageBox.Show(@"语句合法，验证成功");
            }
            catch
            {
                MessageBox.Show(@"非法语句，请重新输入");
            }
        }

        // 确定
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow[] dataRows = mDataTable.Select(TextBoxSQL.Text);
                // 清除被选中数据
                MainForm.MapControl.Layers.GetItem(mLayerSelectIndex).SelectedFeatures.Clear();
                if (dataRows.Length > 0)
                {
                    for (int i = 0; i < dataRows.Length; i++)
                    {
                        // 更新被选中数据
                        MainForm.MapControl.Layers.GetItem(mLayerSelectIndex).SelectedFeatures.Add(MainForm.MapControl.Layers.GetItem(mLayerSelectIndex).Features.GetItem(mDataTable.Rows.IndexOf(dataRows[i])));
                    }
                    // 重新绘制要素图层
                    MainForm.MapControl.RedrawTrackingShapes();
                    // 这里要有一句代码，更新属性表
                    // MainForm.RedrawAttribute();
                }
                else
                    MessageBox.Show(@"未查询到符合条件要素");
                Close();
            }
            catch
            {
                MessageBox.Show(@"非法语句，请重新输入");
            }
        }

        // 应用
        private void btnApply_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow[] dataRows = mDataTable.Select(TextBoxSQL.Text);
                // 清除被选中数据
                MainForm.MapControl.Layers.GetItem(mLayerSelectIndex).SelectedFeatures.Clear();
                if (dataRows.Length > 0)
                {
                    for (int i = 0; i < dataRows.Length; i++)
                    {
                        // 更新被选中数据
                        MainForm.MapControl.Layers.GetItem(mLayerSelectIndex).SelectedFeatures.Add(MainForm.MapControl.Layers.GetItem(mLayerSelectIndex).Features.GetItem(mDataTable.Rows.IndexOf(dataRows[i])));
                    }
                    // 重新绘制要素图层
                    MainForm.MapControl.RedrawTrackingShapes();
                    // MainForm.RedrawAttribute();
                }
                else
                    MessageBox.Show(@"未查询到符合条件要素");
            }
            catch
            {
                MessageBox.Show(@"非法语句，请重新输入");
            }
        }

        #endregion

        #region 私有方法
        // 加载所有图层
        private void LoadLayers()
        {
            for (int i = 0; i < MainForm.MapControl.Layers.Count; i++)
            {
                SelectBoxLayer.Items.Add(MainForm.MapControl.Layers.GetItem(i).Name);
            }
        }

        // 加载当前选中图层的字段
        private void LoadFields()
        {
            for (int i = 0; i < MainForm.MapControl.Layers.GetItem(mLayerSelectIndex).AttributeFields.Count; i++)
            {
                ListBoxFields.Items.Add(MainForm.MapControl.Layers.GetItem(mLayerSelectIndex).AttributeFields.GetItem(i).Name);
            }
        }

        // 加载数据表
        private void LoadDataTable()
        {
            if (mLayerSelectIndex < 0)
                return;
            // 建表
            mDataTable = new DataTable();
            // 做一个中间值便于表示
            MyMapObjects.moMapLayer layerTemp = MainForm.MapControl.Layers.GetItem(mLayerSelectIndex);
            // 建立字段
            for (int i = 0; i < layerTemp.AttributeFields.Count; i++)
            {
                if (layerTemp.AttributeFields.GetItem(i).ValueType == MyMapObjects.moValueTypeConstant.dDouble)
                {
                    mDataTable.Columns.Add(layerTemp.AttributeFields.GetItem(i).Name, typeof(double));
                }
                else if (layerTemp.AttributeFields.GetItem(i).ValueType == MyMapObjects.moValueTypeConstant.dInt16)
                {
                    mDataTable.Columns.Add(layerTemp.AttributeFields.GetItem(i).Name, typeof(short));
                }
                else if (layerTemp.AttributeFields.GetItem(i).ValueType == MyMapObjects.moValueTypeConstant.dInt32)
                {
                    mDataTable.Columns.Add(layerTemp.AttributeFields.GetItem(i).Name, typeof(int));
                }
                else if (layerTemp.AttributeFields.GetItem(i).ValueType == MyMapObjects.moValueTypeConstant.dInt64)
                {
                    mDataTable.Columns.Add(layerTemp.AttributeFields.GetItem(i).Name, typeof(long));
                }
                else if (layerTemp.AttributeFields.GetItem(i).ValueType == MyMapObjects.moValueTypeConstant.dSingle)
                {
                    mDataTable.Columns.Add(layerTemp.AttributeFields.GetItem(i).Name, typeof(float));
                }
                else if (layerTemp.AttributeFields.GetItem(i).ValueType == MyMapObjects.moValueTypeConstant.dText)
                {
                    mDataTable.Columns.Add(layerTemp.AttributeFields.GetItem(i).Name, typeof(string));
                }
            }
            // 读取字段数据,按行读取
            for (int i = 0; i < layerTemp.Features.Count; i++)
            {
                mDataTable.Rows.Add(layerTemp.Features.GetItem(i).Attributes.ToArray());
            }

        }
        #endregion

    }
}
