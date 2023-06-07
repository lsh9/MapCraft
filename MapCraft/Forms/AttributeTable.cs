using MapCraft.FileProcessor;
using MyMapObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MapCraft.Forms
{
    public partial class AttributeTable : Form
    {
        #region 字段


        public MapCraftForm Main { get; }
        public int FormIndex { get; set; }
        public int LayerIndex { get; }
        public moMapLayer Layer { get; set; }

        public DataTable Table { get; private set; }

        public string NewFieldName { get; set; }
        public Int32 NewFieldLength { get; set; }
        public moValueTypeConstant NewFieldType { get; set; }

        public bool HasSelectField { get; private set; }
        public int SelectedFieldIndex { get; private set; }
        public bool IsAttributeChanged { get; private set; }

        #endregion 字段

        #region 构造函数

        public AttributeTable(MapCraftForm main, int index)
        {
            InitializeComponent();
            Main = main;
            LayerIndex = index;
            Layer = main.moMapControl1.Layers.GetItem(index);

            dataGridView.ReadOnly = true;
            HasSelectField = false;
            IsAttributeChanged = false;
            SelectedFieldIndex = -1;
            LoadData();
            Nameshow.Text = Layer.Name;
        }

        #endregion Constructors

        #region 方法

        public void BeginRefresh()
        {
            Thread thread = new Thread(InvokeWork);
            thread.Start();
        }

        private delegate void LoadDataHandler();

        public void InvokeWork()
        {
            LoadDataHandler mission = LoadData;
            BeginInvoke(mission);
        }

        public void LoadData()
        {
            Table = new DataTable();
            dataGridView.DataSource = null;
            dataGridView.DataSource = Table;
            for (int i = 0; i < Layer.AttributeFields.Count; i++)
            {
                if (Layer.AttributeFields.GetItem(i).ValueType == moValueTypeConstant.dDouble)
                {
                    Table.Columns.Add(Layer.AttributeFields.GetItem(i).Name, typeof(double));
                }
                else if (Layer.AttributeFields.GetItem(i).ValueType == moValueTypeConstant.dInt16)
                {
                    Table.Columns.Add(Layer.AttributeFields.GetItem(i).Name, typeof(short));
                }
                else if (Layer.AttributeFields.GetItem(i).ValueType == moValueTypeConstant.dInt32)
                {
                    Table.Columns.Add(Layer.AttributeFields.GetItem(i).Name, typeof(int));
                }
                else if (Layer.AttributeFields.GetItem(i).ValueType == moValueTypeConstant.dInt64)
                {
                    Table.Columns.Add(Layer.AttributeFields.GetItem(i).Name, typeof(long));
                }
                else if (Layer.AttributeFields.GetItem(i).ValueType == moValueTypeConstant.dSingle)
                {
                    Table.Columns.Add(Layer.AttributeFields.GetItem(i).Name, typeof(float));
                }
                else if (Layer.AttributeFields.GetItem(i).ValueType == moValueTypeConstant.dText)
                {
                    Table.Columns.Add(Layer.AttributeFields.GetItem(i).Name, typeof(string));
                }
                dataGridView.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            //读取字段数据,按行读取
            for (int i = 0; i < Layer.Features.Count; i++)
            {
                Table.Rows.Add(Layer.Features.GetItem(i).Attributes.ToArray());
            }
            dataGridView.DefaultCellStyle.BackColor = Color.White;
            dataGridView.DefaultCellStyle.SelectionForeColor = Color.LightBlue;
            RefreshDataFormByMainForm();
        }

        public void RefreshDataFormByMainForm()
        {
            for (int i = 0; i < Layer.SelectedFeatures.Count; i++)
            {
                moFeatures sFeatures = Layer.Features;
                moFeature sFeature = Layer.SelectedFeatures.GetItem(i);
                int index = -1;
                object[] temp = sFeature.Attributes.ToArray();
                bool flag = true;
                for (int m = 0; m < Layer.Features.Count; m++)
                {
                    object[] temp1 = sFeatures.GetItem(m).Attributes.ToArray();
                    for (int n = 0; n < temp.Length; n++)
                    {
                        if (temp1[n] != temp[n])
                        {
                            flag = false;
                            break;
                        }
                    }
                    if (flag)
                    {
                        index = m;
                        break;
                    }
                }

                if (index >= 0)
                    dataGridView.Rows[index].Selected = true; //将该序号设置为亮
            }
            RefreshSelectedText();
        }

        public void AddNewField()
        {
            //moAttributes attributes = new moAttributes();
            //List<object> array = new List<object>();
            //for (int i = 0; i < Layer.Features.Count; i++)
            //    array.Add(0);
            //attributes.FromArray(array.ToArray());
            moField field = new moField(NewFieldName, NewFieldType);
            if (NewFieldType == moValueTypeConstant.dText)
                field = new moField(NewFieldName, NewFieldType, NewFieldLength);
            Layer = Main.moMapControl1.Layers.GetItem(LayerIndex);
            Layer.AttributeFields.Append(field);
            Main.Shapefiles[LayerIndex].Add_Field(field);


            BeginRefresh();//重新加载一下
        }

        public void RefreshSelectedText()
        {
            lblSelectedNum.Text = Layer.SelectedFeatures.Count.ToString() + @" / " + Layer.Features.Count.ToString() + @" 已选择";
        }

        public void RefreshMainFormByDataForm()
        {
            Layer.SelectedFeatures.Clear();
            for (int i = 0; i < dataGridView.SelectedRows.Count; i++)
            {
                Layer.SelectedFeatures.Add(Layer.Features.GetItem(dataGridView.SelectedRows[i].HeaderCell.RowIndex));
            }
            Main.moMapControl1.RedrawTrackingShapes();
            RefreshSelectedText();
        }

        #endregion

        #region 窗体事件

        private void 开始编辑_Click(object sender, EventArgs e)
        {
            dataGridView.ReadOnly = false;
            MessageBox.Show(@"双击单元格编辑数据");
        }

        private void 停止编辑_Click(object sender, EventArgs e)
        {
            dataGridView.ReadOnly = true;
        }

        private void dataGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dataGridView.Rows[e.RowIndex].Selected = true;//表示选中了这一行
            RefreshMainFormByDataForm();
        }

        private void 添加字段_Click(object sender, EventArgs e)
        {
            AddNewField addNewField = new AddNewField(this);
            addNewField.Show();
        }

        private void 删除字段_Click(object sender, EventArgs e)
        {
            if (HasSelectField == false)
            {
                MessageBox.Show(@"请选择需要删除的字段");
                return;
            }
            Layer.AttributeFields.RemoveAt(SelectedFieldIndex);

            HasSelectField = false;
            SelectedFieldIndex = -1;

            MessageBox.Show(@"字段已成功删除");
            BeginRefresh();//重新加载一下
        }

        private void 按属性选择_Click(object sender, EventArgs e)
        {
            SelectByAttributeForm researchSelect = new SelectByAttributeForm(Main);
            researchSelect.Owner = this;
            researchSelect.Show();
        }

        private void 全部选择_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView.Rows.Count; i++)
            {
                dataGridView.Rows[i].Selected = true;
            }
            RefreshMainFormByDataForm();
        }

        private void 清除选择_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView.Rows.Count; i++)
            {
                dataGridView.Rows[i].Selected = false;
            }
            RefreshMainFormByDataForm();
        }

        private void dataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (SelectedFieldIndex == e.ColumnIndex)//初次选择
            {
                return;
            }
            if (SelectedFieldIndex != e.ColumnIndex && SelectedFieldIndex >= 0)
            {
                dataGridView.Columns[SelectedFieldIndex].DefaultCellStyle.BackColor = Color.White;
            }
            dataGridView.Columns[e.ColumnIndex].DefaultCellStyle.BackColor = Color.LightBlue;
            SelectedFieldIndex = e.ColumnIndex;
            HasSelectField = true;
        }

        private void dataGridView_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            int col = e.ColumnIndex;//获取被修改单元格的纵坐标
            int row = e.RowIndex;//获取被修改单元格的横坐标

            Layer.Features.GetItem(row).Attributes.SetItem(col, e.Value);
            IsAttributeChanged = true;
            BeginRefresh(); //重新加载一下
        }

        private void dataGridView_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex < 0)
            {
                RefreshMainFormByDataForm();
            }
            else
            {
                RefreshDataFormByMainForm();
            }
        }

        #endregion


    }
}
