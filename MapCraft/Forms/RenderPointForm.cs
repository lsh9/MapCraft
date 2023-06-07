using MyMapObjects;
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
    public partial class RenderPointForm : Form
    {
        private MapCraftForm Main;
        private moMapLayer mLayer;

        public RenderPointForm(MapCraftForm main, moMapLayer layer)
        {
            InitializeComponent();
            Main = main;
            mLayer = layer;
        }

        private void tabControlInner_Enter(object sender, EventArgs e)
        {

        }

        private void tabPageSingle_Enter(object sender, EventArgs e)
        {

        }

        private void btnPointRenderApply_Click(object sender, EventArgs e)
        {
            apply();
        }

        private void btnPointRendererConfirm_Click(object sender, EventArgs e)
        {
            apply();
            Close();
        }

        private void apply()
        {
            // 判断当前在哪一页
            if (tabControlInner.SelectedTab == tabPageSingle) { }
            // {
            //     // 单一符号
            //     // 判断当前选择的是哪种符号
            //     if (radioButtonSingleColor.Checked)
            //     {
            //         // 单一颜色
            //         // 获取颜色
            //         Color color = btnSingleColor.BackColor;
            //         // 获取大小
            //         Int32 size = (Int32)numericUpDownSingleSize.Value;
            //         // 获取符号
            //         moSimplePointSymbol symbol = new moSimplePointSymbol();
            //         symbol.Color = color;
            //         symbol.Size = size;
            //         // 设置符号
            //         mLayer.Renderer = new moSimpleRenderer(symbol);
            //     }
            //     else if (radioButtonSingleImage.Checked)
            //     {
            //         // 单一图片
            //         // 获取图片路径
            //         String path = textBoxSingleImagePath.Text;
            //         // 获取大小
            //         Int32 size = (Int32)numericUpDownSingleSize.Value;
            //         // 获取符号
            //         moPicturePointSymbol symbol = new moPicturePointSymbol();
            //         symbol.PicturePath = path;
            //         symbol.Size = size;
            //         // 设置符号
            //         mLayer.Renderer = new moPictureRenderer(symbol);
            //     }
            //     else if (radioButtonSingleFont.Checked)
            //     {
            //         // 单一字体
            //         // 获取字体
            //         String font = btnFont.Text;
            //         // 获取大小
            //         Int32 size = (Int32)numericUpDownSingleSize.Value;
            //         // 获取符号
            //         moFontPointSymbol symbol = new moFontPointSymbol();
            //         symbol.Font = font;
            //         symbol.Size = size;
            //         // 设置符号
            //         mLayer.Renderer = new moFontRenderer(symbol);
            //     }
            // }
            // else if (tabControlInner.SelectedTab == tabPageUnique)
            // {
            //     // 唯一值符号
            //     // 获取字段
            //     String field = comboBoxUniqueField.Text;
            //     // 获取符号
            //     moUniqueValueRenderer renderer = new moUniqueValueRenderer(field);
            //     // 获取符号
            //     for (Int32 i = 0; i < dataGridViewUnique.Rows.Count; i++)
            //     {
            //         // 获取值
            //         String value = dataGridViewUnique.Rows[i].Cells[0].Value.ToString();
            //         // 获取符号
            //         moSimplePointSymbol symbol = new moSimplePointSymbol();
            //         symbol.Color = (Color)dataGridViewUnique.Rows[i].Cells[1].
        }

        private void btnSingleColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                btnSingleColor.BackColor = colorDialog.Color;
            }
        }
    }
}
