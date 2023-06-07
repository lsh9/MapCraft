using MyMapObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using System.Security.Cryptography;
using MapCraft.Render;

namespace MapCraft.Forms
{
    public partial class LayerDetailForm : Form
    {
        private MapCraftForm Main;
        private moRenderer mRenderer;
        public LayerDetailForm(MapCraftForm main, Int32 layerIndex)
        {
            InitializeComponent();
            Main = main;
            // 将所有图层加入下拉框
            for (Int32 i = 0; i < Main.MapControl.Layers.Count; i++)
            {
                cbBoxLayers.Items.Add(Main.MapControl.Layers.GetItem(i).Name);
            }
            UpdateAll(layerIndex);
            btnLabelColor.BackColor = Color.Black;
            btnFont.Font = new Font("宋体", 9);
            btnFont.Text = "宋体";
            labelSize.Text = "9";
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
            chbVisible.Checked = layer.Visible;
            chbSelectable.Checked = layer.Selectable;
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

        private void tabPageLabel_Enter(object sender, EventArgs e)
        {
            // 获取所有字段
            Int32 index = cbBoxLayers.SelectedIndex;
            moMapLayer layer = Main.MapControl.Layers.GetItem(index);
            // 清空所有字段
            cbFieldsName.Items.Clear();
            moFields fields = layer.AttributeFields;
            for (Int32 i = 0; i < fields.Count; i++)
            {
                cbFieldsName.Items.Add(fields.GetItem(i).Name);
            }
            if (layer.AttributeFields.Count > 0)
            {
                cbFieldsName.SelectedIndex = 0;
            }
            else
            {
                cbFieldsName.SelectedIndex = -1;
            }
            chbMask.Checked = true;
            chbShow.Checked = true;
        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                btnLabelColor.BackColor = colorDialog.Color;
            }
        }

        private void btnFont_Click(object sender, EventArgs e)
        {
            FontDialog fontDialog = new FontDialog();
            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                btnFont.Font = fontDialog.Font;
                btnFont.Text = fontDialog.Font.Name;
                labelSize.Text = Convert.ToString(fontDialog.Font.Size);
            }
        }

        private void btnApplyLabel_Click(object sender, EventArgs e)
        {
            applyLabel();
        }

        private void btnConfirmLabel_Click(object sender, EventArgs e)
        {
            applyLabel();
            Close();
        }

        private void applyLabel()
        {
            moMapLayer layer = Main.MapControl.Layers.GetItem(cbBoxLayers.SelectedIndex);
            moLabelRenderer sLabelRenderer = new moLabelRenderer();
            Int32 FieldIndex = cbFieldsName.SelectedIndex;
            if (FieldIndex >= 0 || FieldIndex < layer.AttributeFields.Count)
            {
                sLabelRenderer.Field = layer.AttributeFields.GetItem(FieldIndex).Name;
            }
            else
            {
                MessageBox.Show("请选择字段");
                return;
            }
            sLabelRenderer.TextSymbol.Font = btnFont.Font;
            sLabelRenderer.TextSymbol.UseMask = chbMask.Checked;
            sLabelRenderer.LabelFeatures = chbShow.Checked;
            layer.LabelRenderer = sLabelRenderer;
            Main.MapControl.RedrawMap();
        }

        private void chbVisible_CheckedChanged(object sender, EventArgs e)
        {
            moMapLayer layer = Main.MapControl.Layers.GetItem(cbBoxLayers.SelectedIndex);
            layer.Visible = chbVisible.Checked;
            Main.MapControl.RedrawMap();
            Main.RefreshLayersTree();
        }

        private void chbSelectable_CheckedChanged(object sender, EventArgs e)
        {
            moMapLayer layer = Main.MapControl.Layers.GetItem(cbBoxLayers.SelectedIndex);
            layer.Selectable = chbVisible.Checked;
        }

        private void btnApplyRender_Click(object sender, EventArgs e)
        {
            apply();
        }

        private void btnConfirmRender_Click(object sender, EventArgs e)
        {
            apply();
            Close();
        }

        private void apply()
        {
            moMapLayer layer = Main.MapControl.Layers.GetItem(cbBoxLayers.SelectedIndex);
            moGeometryTypeConstant shapeType = layer.ShapeType;
            // 判断当前在哪一页
            if (tabControlInner.SelectedIndex == 0)
            {
                // 单一值渲染
                moSimpleRenderer simpleRenderer = new moSimpleRenderer();
                layer.Renderer = simpleRenderer;
                switch (shapeType)
                {
                    case moGeometryTypeConstant.Point:
                        moSimpleMarkerSymbol markerSymbol = new moSimpleMarkerSymbol();
                        markerSymbol.Style = (moSimpleMarkerSymbolStyleConstant)cbSymble.SelectedIndex;
                        markerSymbol.Color = btnRenderColor.BackColor;
                        markerSymbol.Size = Convert.ToInt32(numericUpDownSize.Value);
                        simpleRenderer.Symbol = markerSymbol;
                        break;
                    case moGeometryTypeConstant.MultiPolyline:
                        moSimpleLineSymbol lineSymbol = new moSimpleLineSymbol();
                        lineSymbol.Style = (moSimpleLineSymbolStyleConstant)cbSymble.SelectedIndex;
                        lineSymbol.Color = btnRenderColor.BackColor;
                        lineSymbol.Size = Convert.ToInt32(numericUpDownSize.Value);
                        simpleRenderer.Symbol = lineSymbol;
                        break;
                    case moGeometryTypeConstant.MultiPolygon:

                        moSimpleFillSymbol fillSymbol = new moSimpleFillSymbol();
                        moSimpleLineSymbol outlineSymbol = new moSimpleLineSymbol();
                        fillSymbol.Outline = outlineSymbol;
                        outlineSymbol.Style = (moSimpleLineSymbolStyleConstant)cbSymble.SelectedIndex;
                        outlineSymbol.Color = btnRenderColor.BackColor;
                        outlineSymbol.Size = Convert.ToInt32(numericUpDownSize.Value);
                        simpleRenderer.Symbol = fillSymbol;
                        break;
                }

            }
            else if (tabControlInner.SelectedIndex == 1)
            {
                // 唯一值渲染
                moUniqueValueRenderer uniqueValueRenderer = new moUniqueValueRenderer();
                layer.Renderer = uniqueValueRenderer;
                Int32 FieldIndex = cbFieldsName.SelectedIndex;
                string sFieldName = cbUniqueValueField.SelectedItem.ToString();
                int sFieldIndex = layer.AttributeFields.FindField(sFieldName);
                if (sFieldIndex < 0)
                {
                    return;
                }
                uniqueValueRenderer.Field = sFieldName;
                // 获取唯一值
                List<string> uniqueValues = new List<string>();
                //for (int i = 0; i < dataGridViewUnique.Rows.Count; i++)
                //{
                //    uniqueValues.Add(dataGridViewUnique.Rows[i].Cells[1].Value.ToString());
                //}
                //switch (shapeType)
                //{
                //    case moGeometryTypeConstant.Point:
                //        for (int i = 0; i < uniqueValues.Count; i++)
                //        {
                //            moSimpleMarkerSymbol sSymbol = new moSimpleMarkerSymbol();
                //            sSymbol.Style = (moSimpleMarkerSymbolStyleConstant)cbSymble.SelectedIndex;
                //            sSymbol.Color = dataGridViewUnique.Rows[i].Cells[0].Style.BackColor;
                //            sSymbol.Size = Convert.ToInt32(numericUpDownSize.Value);
                //            uniqueValueRenderer.AddUniqueValue(dataGridViewUnique.Rows[i].Cells[1].Value.ToString(), sSymbol);
                //        }
                //        break;
                //    case moGeometryTypeConstant.MultiPolyline:
                //        for (int i = 0; i < uniqueValues.Count; i++)
                //        {
                //            moSimpleLineSymbol sSymbol = new moSimpleLineSymbol();
                //            sSymbol.Style = (moSimpleLineSymbolStyleConstant)cbSymble.SelectedIndex;
                //            sSymbol.Color = dataGridViewUnique.Rows[i].Cells[0].Style.BackColor;
                //            sSymbol.Size = Convert.ToInt32(numericUpDownSize.Value);
                //            uniqueValueRenderer.AddUniqueValue(dataGridViewUnique.Rows[i].Cells[1].Value.ToString(), sSymbol);
                //        }
                //        break;
                //    case moGeometryTypeConstant.MultiPolygon:
                //        for (int i = 0; i < uniqueValues.Count; i++)
                //        {
                //            moSimpleFillSymbol sSymbol = new moSimpleFillSymbol();
                //            sSymbol.Color = dataGridViewUnique.Rows[i].Cells[0].Style.BackColor;
                //            string s = dataGridViewUnique.Rows[i].Cells[1].ToString();
                //            uniqueValueRenderer.AddUniqueValue(dataGridViewUnique.Rows[i].Cells[1].Value.ToString(), sSymbol);

                //        }
                //        break;
                //}


                //uniqueValueRenderer.DefaultSymbol=new MyMapObjects.moSimpleFillSymbol();
                //layer.Renderer = uniqueValueRenderer;
                layer.Renderer = mRenderer;
                Main.MapControl.RedrawMap();
            }
            else if (tabControlInner.SelectedIndex == 2)
            {
                // 唯一值渲染
                moUniqueValueRenderer sUniqueValueRenderer = new moUniqueValueRenderer();
                Int32 FieldIndex = cbFieldsName.SelectedIndex;
                if (FieldIndex >= 0 || FieldIndex < layer.AttributeFields.Count)
                {
                    sUniqueValueRenderer.Field = layer.AttributeFields.GetItem(FieldIndex).Name;
                }
                else
                {
                    MessageBox.Show("请选择字段");
                    return;
                }
                //sUniqueValueRenderer.DefaultSymbol.FillColor = btnColor.BackColor;
                layer.Renderer = sUniqueValueRenderer;
            }
            Main.MapControl.RedrawMap();

        }


        private void cbUniqueValueField_SelectedIndexChanged(object sender, EventArgs e)
        {
            moMapLayer layer = Main.MapControl.Layers.GetItem(cbBoxLayers.SelectedIndex);
            moUniqueValueRenderer sUniqueValueRenderer = new moUniqueValueRenderer();
            Int32 FieldIndex = cbFieldsName.SelectedIndex;
            // 重新绘制DataGridView
            string field =  cbUniqueValueField.SelectedItem.ToString();
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(field);
            dataTable.Columns.Add("符号");
            dataTable.Columns.Add("颜色");
            dataTable.Columns.Add("大小");
            dataTable.Columns.Add("透明度");

            
            dataGridViewUnique.DataSource = dataTable;
        }

        private void tabControlInner_Enter(object sender, EventArgs e)
        {
            
        }

        private void tabPageSingle_Enter(object sender, EventArgs e)
        {
            // 根据几何类型设置样式
            moMapLayer layer = Main.MapControl.Layers.GetItem(cbBoxLayers.SelectedIndex);
            moGeometryTypeConstant shapeType = layer.ShapeType;
            switch (shapeType)
            {
                case moGeometryTypeConstant.Point:
                    cbSymble.Items.Clear();
                    cbSymble.Items.Add("Circle");
                    cbSymble.Items.Add("SolidCircle");
                    cbSymble.Items.Add("Triangle");
                    cbSymble.Items.Add("SolidTriangle");
                    cbSymble.Items.Add("Square");
                    cbSymble.Items.Add("SolidSquare");
                    cbSymble.Items.Add("CircleDot");
                    cbSymble.Items.Add("CircleCircle");
                    labelFill.Hide();
                    btnFillColor.Hide();
                    break;
                case moGeometryTypeConstant.MultiPolyline:
                    cbSymble.Items.Clear();
                    cbSymble.Items.Add("Solid");
                    cbSymble.Items.Add("Dash");
                    cbSymble.Items.Add("Dot");
                    cbSymble.Items.Add("DashDot");
                    cbSymble.Items.Add("DashDotDot");
                    labelFill.Hide();
                    btnFillColor.Hide();
                    break;
                case moGeometryTypeConstant.MultiPolygon:
                    cbSymble.Items.Clear();
                    cbSymble.Items.Add("Solid");
                    cbSymble.Items.Add("Dash");
                    cbSymble.Items.Add("Dot");
                    cbSymble.Items.Add("DashDot");
                    cbSymble.Items.Add("DashDotDot");
                    labelFill.Show();
                    btnFillColor.Show();
                    break;
            }
            cbSymble.SelectedIndex = 0;
        }

        private void tabPageUnique_Enter(object sender, EventArgs e)
        {
            // 根据图层更新字段
            moMapLayer layer = Main.MapControl.Layers.GetItem(cbBoxLayers.SelectedIndex);
            cbUniqueValueField.Items.Clear();
            for (int i = 0; i < layer.AttributeFields.Count; i++)
            {
                cbUniqueValueField.Items.Add(layer.AttributeFields.GetItem(i).Name);
            }
            cbUniqueValueField.SelectedItem = 0;
        }

        private void btnGetUniqueValue_Click(object sender, EventArgs e)
        {
            // 获取唯一值
            moMapLayer layer = Main.MapControl.Layers.GetItem(cbBoxLayers.SelectedIndex);
            moUniqueValueRenderer uniqueValueRenderer = new moUniqueValueRenderer();
            moGeometryTypeConstant shapeType = layer.ShapeType;
            string sFieldName = cbUniqueValueField.SelectedItem.ToString();
            int sFieldIndex = layer.AttributeFields.FindField(sFieldName);
            if (sFieldIndex < 0)
            {
                return;
            }
            uniqueValueRenderer.Field = sFieldName;
            // 获取唯一值
            List<string> uniqueValues = new List<string>();
            for (int i = 0; i < layer.Features.Count; i++)
            {
                string value = layer.Features.GetItem(i).Attributes.GetItem(sFieldIndex).ToString();
                uniqueValues.Add(value);
            }
            uniqueValues = uniqueValues.Distinct().ToList();
            // 填充DataGridView
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(sFieldName);
            dataTable.Columns.Add("符号");
            dataTable.Columns.Add("颜色");
            dataTable.Columns.Add("大小");
            switch (shapeType)
            {
                // 将结果填入dataGridView
                case moGeometryTypeConstant.Point:
                    uniqueValueRenderer.DefaultSymbol = new moSimpleMarkerSymbol();
                    for (int i = 0; i < uniqueValues.Count; i++)
                    {
                        // 生成随机符号
                        moSimpleMarkerSymbol markerSymbol = new moSimpleMarkerSymbol();
                        markerSymbol.Style = (moSimpleMarkerSymbolStyleConstant)(i % 8);
                        markerSymbol.Color = CreateRandomColor();
                        uniqueValueRenderer.AddUniqueValue(uniqueValues[i], markerSymbol);
                        DataRow dataRow = dataTable.NewRow();
                        dataRow[0] = uniqueValues[i];
                        dataRow[1] = cbSymble.Items[i % 8].ToString();
                        dataRow[2] = markerSymbol.Color;
                        dataRow[3] = markerSymbol.Size;
                        dataTable.Rows.Add(dataRow);
                    }
                    break;
                case moGeometryTypeConstant.MultiPolyline:
                    uniqueValueRenderer.DefaultSymbol = new moSimpleLineSymbol();
                    for (int i = 0; i < uniqueValues.Count; i++)
                    {
                        moSimpleLineSymbol lineSymbol = new moSimpleLineSymbol();
                        lineSymbol.Style = (moSimpleLineSymbolStyleConstant)(i % 5);
                        lineSymbol.Color = CreateRandomColor();
                        uniqueValueRenderer.AddUniqueValue(uniqueValues[i], lineSymbol);
                        DataRow dataRow = dataTable.NewRow();
                        dataRow[0] = uniqueValues[i];
                        dataRow[1] = cbSymble.Items[i % 5].ToString();
                        dataRow[2] = lineSymbol.Color;
                        dataRow[3] = lineSymbol.Size;
                        dataTable.Rows.Add(dataRow);
                        // 设置单元格颜色
                        //dataGridViewUnique.Rows[i].Cells[2].Style.BackColor = lineSymbol.Color;
                    }
                    break;
                case moGeometryTypeConstant.MultiPolygon:
                    dataTable.Columns.Add("填充");
                    uniqueValueRenderer.DefaultSymbol = new moSimpleFillSymbol();
                    for (int i = 0; i < uniqueValues.Count; i++)
                    {
                        moSimpleFillSymbol fillSymbol = new moSimpleFillSymbol();
                        fillSymbol.Color = CreateRandomColor();
                        moSimpleLineSymbol outlineSymbol = new moSimpleLineSymbol();
                        outlineSymbol.Style = (moSimpleLineSymbolStyleConstant)(i % 5);
                        outlineSymbol.Color = CreateRandomColor();
                        fillSymbol.Outline = outlineSymbol;
                        uniqueValueRenderer.AddUniqueValue(uniqueValues[i], fillSymbol);
                        DataRow dataRow = dataTable.NewRow();
                        dataRow[0] = uniqueValues[i];
                        dataRow[1] = cbSymble.Items[i % 5].ToString();
                        // dataRow[2] = outlineSymbol.Color;
                        dataRow[3] = outlineSymbol.Size;
                        // dataRow[4] = fillSymbol.Color;
                        dataTable.Rows.Add(dataRow);
                        //dataGridViewUnique.Rows[i].Cells[2].Style.BackColor = outlineSymbol.Color;
                        //dataGridViewUnique.Rows[i].Cells[4].Style.BackColor = fillSymbol.Color;
                    }
                    break;
            }
            dataGridViewUnique.DataSource = dataTable;
            mRenderer = uniqueValueRenderer;
        }

        private void btnRenderColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                btnRenderColor.BackColor = colorDialog.Color;
            }
        }

        private void btnFillColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                btnFillColor.BackColor = colorDialog.Color;
            }
        }

        private Color CreateRandomColor()
        {
            //总体思想：每个随机颜色RGB中总有一个为252，其他两个值的取值范围为179-245，这样取值的目的在于让地图颜色偏浅，美观
            //生成4个元素的字节数组，第一个值决定哪个通道取252，另外三个中的两个值决定另外两个通道的值
            byte[] sBytes = new byte[4];
            RNGCryptoServiceProvider sChanelRng = new RNGCryptoServiceProvider();
            sChanelRng.GetBytes(sBytes);
            Int32 sChanelValue = sBytes[0];
            byte A = 255, R, G, B;
            if (sChanelValue <= 85)
            {
                R = 252;
                G = (byte)(179 + 66 * sBytes[2] / 255);
                B = (byte)(179 + 66 * sBytes[3] / 255);
            }
            else if (sChanelValue <= 170)
            {
                G = 252;
                R = (byte)(179 + 66 * sBytes[1] / 255);
                B = (byte)(179 + 66 * sBytes[3] / 255);
            }
            else
            {
                B = 252;
                R = (byte)(179 + 66 * sBytes[1] / 255);
                G = (byte)(179 + 66 * sBytes[2] / 255);
            }
            return Color.FromArgb(A, R, G, B);
        }
    }
}
