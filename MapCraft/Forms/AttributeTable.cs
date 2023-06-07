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
    public partial class AttributeTable : Form
    {
        #region 字段

        // (1) 主窗体和图层数据

        public MapCraftForm Main { get; }
        public int FormIndex { get; set; }
        public int LayerIndex { get; }
        public moMapLayer Layer { get; set; }

        public DataTable Table { get; private set; }

        // (2) 操作变量

        public string NewFieldName { get; set; }
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
                for(int m = 0; m < Layer.Features.Count; m++)
                {
                    object[] temp1 = sFeatures.GetItem(m).Attributes.ToArray();
                    for(int n = 0; n < temp.Length; n++)
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
                if(index>-1)
                    dataGridView.Rows[index].Selected = true; //将该序号设置为亮
            }
            RefreshSelectedText();
        }


        public void RefreshSelectedText()
        {
            statusStrip.Text = (Layer.SelectedFeatures.Count.ToString() + @" / " + Layer.Features.Count.ToString() + @" 已选择");
        }


        #endregion


    }
}
