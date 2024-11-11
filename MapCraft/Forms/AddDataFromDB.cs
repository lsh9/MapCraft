using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MapCraft.IO;
using Microsoft.VisualBasic.Devices;
using MyMapObjects;
using Npgsql;

namespace MapCraft.Forms
{
    public partial class AddDataFromDB : Form
    {
        public ConnDBParser connDBParserRtn { get; private set; }
        public bool IsCreateDBTable;
        moMapLayer Layer;

        public AddDataFromDB(bool isCreateDBTable, moMapLayer layer)
        {
            InitializeComponent();
            IsCreateDBTable = isCreateDBTable;

            if (!IsCreateDBTable)
            {
                GetTableNames();
                comTableName.Visible = true;
                comTableName.Enabled = true;
                txtTableName.Visible = false;
                txtTableName.Enabled = false;
                btnConnect.Text = "连接";
                this.Text = "连接空间数据库";
            }
            else
            {
                Layer = layer;
                comTableName.Visible = false;
                comTableName.Enabled = false;
                txtTableName.Visible = true;
                txtTableName.Enabled = true;
                btnConnect.Text = "创建表";
                this.Text = "在空间数据库中创建表";
            }

        }
        private void AddDataFromDB_Load(object sender, EventArgs e)
        {
            if (!IsCreateDBTable)
            {
                comTableName.Focus();
            }
            else
            {
                txtTableName.Text = Layer.Name;
                txtTableName.Focus();
                txtTableName.SelectAll();
            }
        }

        // 尝试获取Table名
        private void GetTableNames()
        {
            string ConStr =
                @"PORT=" + txtPort.Text.Trim() +
                ";DATABASE=" + txtDBName.Text.Trim() +
                ";HOST=" + txtHost.Text.Trim() +
                ";PASSWORD=" + txtPassword.Text.Trim() +
                ";USER ID=" + txtID.Text.Trim();
            using (NpgsqlConnection SqlConn = new NpgsqlConnection(ConStr))
            {
                try
                {
                    SqlConn.Open();
                    DataTable schema = SqlConn.GetSchema("Tables");
                    foreach (DataRow row in schema.Rows)
                    {
                        string tableName = row["TABLE_NAME"].ToString();
                        // 查询geometry_columns视图来检查表是否包含几何字段
                        string query = $"SELECT count(*) FROM geometry_columns WHERE f_table_name = '{tableName}'";
                        using (NpgsqlCommand cmd = new NpgsqlCommand(query, SqlConn))
                        {
                            int count = Convert.ToInt32(cmd.ExecuteScalar());
                            if (count > 0)
                            {
                                comTableName.Items.Add(tableName);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    SqlConn.Close();
                }
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (!IsCreateDBTable)
            {
                // 返回
                connDBParserRtn = new ConnDBParser(
                    txtPort.Text.Trim(), txtHost.Text.Trim(), txtID.Text.Trim(),
                    txtPassword.Text.Trim(), txtDBName.Text.Trim(), comTableName.Text.Trim());

                this.DialogResult = DialogResult.OK;
            }
            else
            {
                // 创建表
                if (txtTableName.Text.Trim() == "" || txtTableName.Text.Trim() == "请输入数据表名称")
                {
                    MessageBox.Show("请填写完整！");
                    return;
                }    
                
                connDBParserRtn = new ConnDBParser(
                    txtPort.Text.Trim(), txtHost.Text.Trim(), txtID.Text.Trim(),
                    txtPassword.Text.Trim(), txtDBName.Text.Trim(), txtTableName.Text.Trim());
                if (connDBParserRtn.CheckIfTableExists())
                {
                    // 如果已经存在表
                    MessageBox.Show("已经存在表" + txtTableName.Text.Trim() + "！请重新命名！");
                    return;
                }
                else
                {
                    // 创建表
                    string TableNameTypeSQL = "";
                    for (int i  = 0; i < Layer.AttributeFields.Count; i++)
                    {
                        moField curField = Layer.AttributeFields[i];
                        string TypeSQL = "";
                        switch (curField.ValueType)
                        {
                            case moValueTypeConstant.dInt64:
                            case moValueTypeConstant.dInt32:
                            case moValueTypeConstant.dInt16:
                                TypeSQL = "INTEGER";
                                break;
                            case moValueTypeConstant.dSingle:
                                TypeSQL = "FLOAT4";
                                break;
                            case moValueTypeConstant.dDouble:
                                TypeSQL = "DOUBLE PRECISION";
                                break;
                            case moValueTypeConstant.dText:
                            default:
                                TypeSQL = "VARCHAR(255)";
                                break;
                        }
                        TableNameTypeSQL += curField.Name + " " + TypeSQL + ", ";
                    }
                    TableNameTypeSQL += "geom GEOMETRY";
                    // TableNameTypeSQL.TrimEnd(' ');
                    // TableNameTypeSQL.TrimEnd(',');
                    string CreateTableSQL = "CREATE TABLE " + connDBParserRtn.Table + " (" + TableNameTypeSQL + ");";
                    if (!connDBParserRtn.CreateNewGeometryTable(CreateTableSQL))
                        return;
                    

                    // 插入数据
                    connDBParserRtn.Fields = Layer.AttributeFields;
                    connDBParserRtn.Write_DB(Layer.Features);

                    this.DialogResult = DialogResult.OK;
                }
            }
            

            /*string ConStr = 
                @"PORT=" + txtPort.Text.Trim() +
                ";DATABASE=" + txtDBName.Text.Trim() +
                ";HOST=" + txtHost.Text.Trim() +
                ";PASSWORD=" + txtPassword.Text.Trim() +
                ";USER ID=" + txtID.Text.Trim();
            NpgsqlConnection SqlConn = new NpgsqlConnection(ConStr);

            SqlConn.Open();

            if (SqlConn.State != ConnectionState.Open)
            {
                MessageBox.Show("错误！", "系统提示", MessageBoxButtons.OK);
            }

            // string sql = "SELECT * FROM my_point_table";
            string sql = "select id, ST_AsEWKT(geom), name from my_point_table";

            using (var cmd = new NpgsqlCommand(sql, SqlConn))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int fieldCount = reader.FieldCount;

                        int id = reader.GetInt32(0);
                        //string wkt = reader.GetString(1);


                        var wktvar = reader.GetData(1);
                        string wkt = reader.GetData(1).ToString();

                        string name = "";
                        //if (reader.GetString(2) == null)
                        //    name = "";
                        //else 
                        //    name = reader.GetString(2);
                        Console.WriteLine("ID: {0}, wkt: {1}, name: {2}", id, wkt, name);
                        MessageBox.Show(id + ", " + wkt + ", " + name + "\n");
                    }
                }
            }

            SqlConn.Close();*/
        }

        
    }
}
