using MyMapObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using System.Security.Cryptography;

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
			applyLabelRender();
		}

		private void btnConfirmLabel_Click(object sender, EventArgs e)
		{
			applyLabelRender();
			Close();
		}

		private void applyLabelRender()
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
			applyRender();
		}

		private void btnConfirmRender_Click(object sender, EventArgs e)
		{
			applyRender();
			Close();
		}

		private void applyRender()
		{
			moMapLayer layer = Main.MapControl.Layers.GetItem(cbBoxLayers.SelectedIndex);
			moGeometryTypeConstant shapeType = layer.ShapeType;
			// 判断当前在哪一页
			if (tabControlInner.SelectedIndex == 0)
			{
				// 单一值渲染
				moSimpleRenderer simpleRenderer = new moSimpleRenderer();
				switch (shapeType)
				{
					case moGeometryTypeConstant.Point:
						moSimpleMarkerSymbol markerSymbol = new moSimpleMarkerSymbol();
						markerSymbol.Style = (moSimpleMarkerSymbolStyleConstant)cbSymble.SelectedIndex;
						markerSymbol.Color = btnRenderColor.BackColor;
						markerSymbol.Size = Convert.ToDouble(numericUpDownSize.Value);
						simpleRenderer.Symbol = markerSymbol;
						break;
					case moGeometryTypeConstant.MultiPolyline:
						moSimpleLineSymbol lineSymbol = new moSimpleLineSymbol();
						lineSymbol.Style = (moSimpleLineSymbolStyleConstant)cbSymble.SelectedIndex;
						lineSymbol.Color = btnRenderColor.BackColor;
						lineSymbol.Size = Convert.ToDouble(numericUpDownSize.Value);
						simpleRenderer.Symbol = lineSymbol;
						break;
					case moGeometryTypeConstant.MultiPolygon:

						moSimpleFillSymbol fillSymbol = new moSimpleFillSymbol();
						moSimpleLineSymbol outlineSymbol = new moSimpleLineSymbol();
						fillSymbol.Outline = outlineSymbol;
						outlineSymbol.Style = (moSimpleLineSymbolStyleConstant)cbSymble.SelectedIndex;
						outlineSymbol.Color = btnRenderColor.BackColor;
						outlineSymbol.Size = Convert.ToDouble(numericUpDownSize.Value);
						simpleRenderer.Symbol = fillSymbol;
						break;
				}
                layer.Renderer = simpleRenderer;
            }
            else if (tabControlInner.SelectedIndex == 1)
			{
				// 唯一值渲染
				moUniqueValueRenderer uniqueValueRenderer = new moUniqueValueRenderer();
				string fieldName = cbUniqueValueField.SelectedItem.ToString();
				int fieldIndex = layer.AttributeFields.FindField(fieldName);
				if (fieldIndex < 0)
				{
					MessageBox.Show("请选择字段");
					return;
				}
				//创建唯一值渲染
				uniqueValueRenderer.Field = fieldName;
				switch (layer.ShapeType)
				{
					case moGeometryTypeConstant.Point:
						// 根据dataGridViewUnique中的值来设置
						for (int i = 0; i < dataGridViewUnique.Rows.Count - 1; i++)
						{
							string sValue = dataGridViewUnique.Rows[i].Cells[0].Value.ToString();
							moSimpleMarkerSymbol markerSymbol = new moSimpleMarkerSymbol();
							markerSymbol.Style = (moSimpleMarkerSymbolStyleConstant)Enum.Parse(typeof(moSimpleMarkerSymbolStyleConstant), dataGridViewUnique.Rows[i].Cells[1].Value.ToString());
							markerSymbol.Color = dataGridViewUnique.Rows[i].Cells[2].Style.BackColor;
							markerSymbol.Size = Convert.ToDouble(dataGridViewUnique.Rows[i].Cells[3].Value);
							uniqueValueRenderer.AddUniqueValue(sValue, markerSymbol);
						}
						break;
					case moGeometryTypeConstant.MultiPolyline:
						for (int i = 0; i < dataGridViewUnique.Rows.Count - 1; i++)
						{
							string sValue = dataGridViewUnique.Rows[i].Cells[0].Value.ToString();
							moSimpleLineSymbol lineSymbol = new moSimpleLineSymbol();
							lineSymbol.Style = (moSimpleLineSymbolStyleConstant)Enum.Parse(typeof(moSimpleLineSymbolStyleConstant), dataGridViewUnique.Rows[i].Cells[1].Value.ToString());
							lineSymbol.Color = dataGridViewUnique.Rows[i].Cells[2].Style.BackColor;
							lineSymbol.Size = Convert.ToDouble(dataGridViewUnique.Rows[i].Cells[3].Value);
							uniqueValueRenderer.AddUniqueValue(sValue, lineSymbol);
						}
						break;
					case moGeometryTypeConstant.MultiPolygon:
						for (int i = 0; i < dataGridViewUnique.Rows.Count - 1; i++)
						{
							string sValue = dataGridViewUnique.Rows[i].Cells[0].Value.ToString();
							moSimpleFillSymbol fillSymbol = new moSimpleFillSymbol();
							moSimpleLineSymbol outlineSymbol = new moSimpleLineSymbol();
							fillSymbol.Outline = outlineSymbol;
							outlineSymbol.Style = (moSimpleLineSymbolStyleConstant)Enum.Parse(typeof(moSimpleLineSymbolStyleConstant), dataGridViewUnique.Rows[i].Cells[1].Value.ToString());
							outlineSymbol.Color = dataGridViewUnique.Rows[i].Cells[2].Style.BackColor;
							outlineSymbol.Size = Convert.ToDouble(dataGridViewUnique.Rows[i].Cells[3].Value);
							fillSymbol.Color = dataGridViewUnique.Rows[i].Cells[4].Style.BackColor;
							uniqueValueRenderer.AddUniqueValue(sValue, fillSymbol);
						}
						break;
                }
				layer.Renderer = uniqueValueRenderer;
            }
            else if (tabControlInner.SelectedIndex == 2)
			{
				// 分级渲染
				moClassBreaksRenderer classBreaksRenderer = new moClassBreaksRenderer();
				string fieldName = cbClassBreaksField.SelectedItem.ToString();
				int fieldIndex = layer.AttributeFields.FindField(fieldName);
				if (fieldIndex < 0)
				{
					MessageBox.Show("请选择字段");
					return;
				}
				if (layer.AttributeFields.GetItem(fieldIndex).ValueType == moValueTypeConstant.dText)
					return;
				//创建分级渲染
				classBreaksRenderer.Field = fieldName;
				//搜索所有值
				List<double> sValues = new List<double>();
				Int32 sFeatureCount = layer.Features.Count;
				for (int i = 0; i < sFeatureCount; i++)
				{
					double sValue = (double)layer.Features.GetItem(i).Attributes.GetItem(fieldIndex);
					sValues.Add(sValue);
				}
				double sMinValue = sValues.Min();
				double sMaxValue = sValues.Max();
				//分级渲染
				int classNum = (int)nupClasses.Value;
				for (int i = 0; i < classNum; i++)
				{
					double sValue = sMinValue + (sMaxValue - sMinValue) * (i + 1) / classNum;
					if (layer.ShapeType == moGeometryTypeConstant.Point)
					{
						moSimpleMarkerSymbol sSymbol = new moSimpleMarkerSymbol();
						classBreaksRenderer.AddBreakValue(sValue, sSymbol);
					}
					else if (layer.ShapeType == moGeometryTypeConstant.MultiPolyline)
					{
						moSimpleLineSymbol sSymbol = new moSimpleLineSymbol();
						classBreaksRenderer.AddBreakValue(sValue, sSymbol);
					}
					else if (layer.ShapeType == moGeometryTypeConstant.MultiPolygon)
					{
						moSimpleFillSymbol sSymbol = new moSimpleFillSymbol();
						moSimpleLineSymbol outLineSymbol = new moSimpleLineSymbol();
						outLineSymbol.Color = btnOutlineColor.BackColor;
						outLineSymbol.Visible = cbShowOutline.Checked;
						sSymbol.Outline = outLineSymbol;
						classBreaksRenderer.AddBreakValue(sValue, sSymbol);
					}
				}
				//生成渐变色
				Color startColor = btnStartColor.BackColor;
				Color endColor = btnEndColor.BackColor;
				classBreaksRenderer.RampColor(startColor, endColor);
				classBreaksRenderer.DefaultSymbol = new moSimpleFillSymbol();
				layer.Renderer = classBreaksRenderer;
			}
			Main.MapControl.RedrawMap();
		}


		private void cbUniqueValueField_SelectedIndexChanged(object sender, EventArgs e)
		{
			moMapLayer layer = Main.MapControl.Layers.GetItem(cbBoxLayers.SelectedIndex);
			moUniqueValueRenderer sUniqueValueRenderer = new moUniqueValueRenderer();
			Int32 FieldIndex = cbUniqueValueField.SelectedIndex;
			// 重新绘制DataGridView
			string field = cbUniqueValueField.SelectedItem.ToString();
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
			cbUniqueValueField.SelectedIndex = 0;
		}

		private void tabPageClass_Enter(object sender, EventArgs e)
		{
			// 根据图层更新字段
			moMapLayer layer = Main.MapControl.Layers.GetItem(cbBoxLayers.SelectedIndex);
			cbClassBreaksField.Items.Clear();
			for (int i = 0; i < layer.AttributeFields.Count; i++)
			{
				cbClassBreaksField.Items.Add(layer.AttributeFields.GetItem(i).Name);
			}
			cbClassBreaksField.SelectedIndex = 0;
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
			dataGridViewUnique.DataSource = dataTable;
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
						uniqueValueRenderer.AddUniqueValue(uniqueValues[i], markerSymbol);
						DataRow dataRow = dataTable.NewRow();
						dataRow[0] = uniqueValues[i];
						dataRow[1] = markerSymbol.Style.ToString();
						dataRow[3] = markerSymbol.Size;
						dataTable.Rows.Add(dataRow);
						// 设置单元格颜色
						dataGridViewUnique.Rows[i].Cells[2].Style.BackColor = markerSymbol.Color;
					}

					break;
				case moGeometryTypeConstant.MultiPolyline:
					uniqueValueRenderer.DefaultSymbol = new moSimpleLineSymbol();
					for (int i = 0; i < uniqueValues.Count; i++)
					{
						moSimpleLineSymbol lineSymbol = new moSimpleLineSymbol();
						uniqueValueRenderer.AddUniqueValue(uniqueValues[i], lineSymbol);
						DataRow dataRow = dataTable.NewRow();
						dataRow[0] = uniqueValues[i];
						dataRow[1] = lineSymbol.Style.ToString();
						dataRow[3] = lineSymbol.Size;
						dataTable.Rows.Add(dataRow);
						// 设置单元格颜色
						dataGridViewUnique.Rows[i].Cells[2].Style.BackColor = lineSymbol.Color;
					}
					break;
				case moGeometryTypeConstant.MultiPolygon:
					dataTable.Columns.Add("填充");
					uniqueValueRenderer.DefaultSymbol = new moSimpleFillSymbol();
					for (int i = 0; i < uniqueValues.Count; i++)
					{
						moSimpleFillSymbol fillSymbol = new moSimpleFillSymbol();
						moSimpleLineSymbol outlineSymbol = new moSimpleLineSymbol();
						fillSymbol.Outline = outlineSymbol;
						uniqueValueRenderer.AddUniqueValue(uniqueValues[i], fillSymbol);
						DataRow dataRow = dataTable.NewRow();
						dataRow[0] = uniqueValues[i];
						dataRow[1] = outlineSymbol.Style.ToString();
						dataRow[3] = outlineSymbol.Size;
						dataTable.Rows.Add(dataRow);
						// 设置单元格颜色
						dataGridViewUnique.Rows[i].Cells[2].Style.BackColor = outlineSymbol.Color;
						dataGridViewUnique.Rows[i].Cells[4].Style.BackColor = fillSymbol.Color;
					}
					break;
			}
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

		private void btnStartColor_Click(object sender, EventArgs e)
		{
			ColorDialog colorDialog = new ColorDialog();
			if (colorDialog.ShowDialog() == DialogResult.OK)
			{
				btnStartColor.BackColor = colorDialog.Color;
			}
		}

		private void btnEndColor_Click(object sender, EventArgs e)
		{
			ColorDialog colorDialog = new ColorDialog();
			if (colorDialog.ShowDialog() == DialogResult.OK)
			{
				btnEndColor.BackColor = colorDialog.Color;
			}
		}

		private void btnOutlineColor_Click(object sender, EventArgs e)
		{
			ColorDialog colorDialog = new ColorDialog();
			if (colorDialog.ShowDialog() == DialogResult.OK)
			{
				btnOutlineColor.BackColor = colorDialog.Color;
			}
		}

		// 外部直接进入渲染
		public void ShowRenderer()
		{
			tabControlOuter.SelectedIndex = 1;
			Show();
		}

		// 外部直接进入注记
		public void ShowLabelRenderer()
		{
			tabControlOuter.SelectedIndex = 2;
			Show();
		}

		private void dataGridViewUnique_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			// // 修改符号
			// if (e.ColumnIndex == 1)
			// {
			// }
			// 修改颜色
			if (e.ColumnIndex == 2)
			{
				ColorDialog colorDialog = new ColorDialog();
				if (colorDialog.ShowDialog() == DialogResult.OK)
				{
					dataGridViewUnique.Rows[e.RowIndex].Cells[2].Style.BackColor = colorDialog.Color;
				}
			}
			// 修改多边形填充颜色
			if (e.ColumnIndex == 4)
			{
				ColorDialog colorDialog = new ColorDialog();
				if (colorDialog.ShowDialog() == DialogResult.OK)
				{
					dataGridViewUnique.Rows[e.RowIndex].Cells[4].Style.BackColor = colorDialog.Color;
				}
			}
			// // 修改符号大小
			// if (e.ColumnIndex == 3)
			// {
			// }

		}
	}
}
