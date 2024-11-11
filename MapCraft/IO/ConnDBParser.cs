using MapCraft.FileProcessor;
using MyMapObjects;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MapCraft.IO
{
    public class ConnDBParser
    {
        #region Field
        private string _port;
        private string _host;
        private string _id;
        private string _password;
        private string _database;
        private string _table;

        private string _geometryColumnName = "geom";

        private moGeometryTypeConstant _geometryType;
        private moFields _fields;
        #endregion

        #region Properties
        public string Table { get { return _table; } }
        public moGeometryTypeConstant GeometryType { get { return _geometryType; } }
        public moFields Fields { get { return _fields; } set { _fields = value; } }
        #endregion

        #region Constructor
        public ConnDBParser(string port, string host, string id, string password, string database, string table)
        {
            _port = port;
            _host = host;
            _id = id;
            _password = password;
            _database = database;
            _table = table;
        }
        #endregion

        #region Methods

        #region Read DB
        // 获取geometry类型
        private moGeometryTypeConstant GetGeometryType(string geometryTypeStr)
        {
            MyMapObjects.moGeometryTypeConstant moGeometryType;
            switch (geometryTypeStr.ToUpper())
            {
                case "POINT":
                    moGeometryType = MyMapObjects.moGeometryTypeConstant.Point;
                    break;
                case "LINESTRING":
                    moGeometryType = MyMapObjects.moGeometryTypeConstant.MultiPolyline;
                    break;
                case "POLYGON":
                    moGeometryType = MyMapObjects.moGeometryTypeConstant.MultiPolygon;
                    break;
                default:
                    {
                        const string error = "不支持该 ShapeFile 类型数据";
                        throw new NotSupportedException(error);
                    }
            }
            return moGeometryType;
        }
        // 获取geometry
        private moGeometry GetGeometry(string geometryTypeStr, string geometryWKT,
            string MinX, string MinY, string MaxX, string MaxY)
        {
            MyMapObjects.moGeometryTypeConstant moGeometryType = GetGeometryType(geometryTypeStr);
            switch (moGeometryType)
            {
                case moGeometryTypeConstant.Point:
                    {
                        moPoint point = new moPoint(); 

                        string coordinatesPart = geometryWKT.Replace("POINT", "").Replace("(", "").Replace(")", "").Trim();
                        string[] coordinates = coordinatesPart.Split(' ');
                        point.X = double.Parse(coordinates[0]);
                        point.Y = double.Parse(coordinates[1]);
                        return point;
                    }
                case moGeometryTypeConstant.MultiPolyline:
                    {
                        // 存储多线
                        moMultiPolyline multiPolyline = new moMultiPolyline();
                        multiPolyline.MinX = double.Parse(MinX);
                        multiPolyline.MinY = double.Parse(MinY);
                        multiPolyline.MaxX = double.Parse(MaxX);
                        multiPolyline.MaxY = double.Parse(MaxY);

                        string linesPart = geometryWKT.Replace("LINESTRING", "").Trim().Trim('(').Trim(')');
                        string[] lineStrings = linesPart.Split(new string[] { "), (" }, StringSplitOptions.RemoveEmptyEntries);

                        // 对每条线
                        foreach (string lineString in lineStrings)
                        {
                            moPoints points = new moPoints();
                            string[] pointsStr = lineString.Split(',');

                            // 对每个点
                            foreach (string pointStr in pointsStr)
                            {
                                moPoint point = new moPoint();
                                string[] coordinates = pointStr.Trim().Split(' ');
                                point.X = double.Parse(coordinates[0]);
                                point.Y = double.Parse(coordinates[1]);
                                points.Add(point);
                            }
                            multiPolyline.Parts.Add(points);
                        }
                        return multiPolyline;
                    }
                case moGeometryTypeConstant.MultiPolygon:
                    {
                        // 存储多面
                        moMultiPolygon multiPolygon = new moMultiPolygon();
                        multiPolygon.MinX = double.Parse(MinX);
                        multiPolygon.MinY = double.Parse(MinY);
                        multiPolygon.MaxX = double.Parse(MaxX);
                        multiPolygon.MaxY = double.Parse(MaxY);

                        string polygonsPart = geometryWKT.Replace("POLYGON", "").Trim().Trim('(').Trim(')');
                        string[] polygonsStrings = polygonsPart.Split(new string[] { ")), ((" }, StringSplitOptions.RemoveEmptyEntries);

                        // 对每一个面
                        foreach (string polygonString in polygonsStrings)
                        {
                            moPoints points = new moPoints();
                            string[] pointsStr = polygonString.Split(',');
                            // 对每个点
                            foreach (string pointStr in pointsStr)
                            {
                                moPoint point = new moPoint();
                                string[] coordinates = pointStr.Trim().Split(' ');
                                point.X = double.Parse(coordinates[0]);
                                point.Y = double.Parse(coordinates[1]);
                                points.Add(point);
                            }
                            points.RemoveAt(points.Count - 1);  // wkt重复了第一个点，删除
                            multiPolygon.Parts.Add(points);
                        }
                        return multiPolygon;
                    }
                /*
                 普通多边形：
                 MULTIPOLYGON (((30 20, 45 40, 10 40, 30 20)),
                 ((15 5, 40 10, 10 20, 5 10, 15 5)))
                 带洞多边形：
                 MULTIPOLYGON (((40 40, 20 45, 45 30, 40 40)),
                 ((20 35, 10 30, 10 10, 30 5, 45 20, 20 35), (30 20, 20 15, 20 25, 30 20)))
                 */
                default:
                    {
                        MessageBox.Show("不支持的shapefile类型!");
                        return null;
                    }
            }
        }
        // 获取字段值
        private moAttributes GetAttributes(List<object> attributesValues)
        {
            moAttributes moAttributes = new moAttributes();
            foreach (object attributeValue in attributesValues)
                moAttributes.Append(attributeValue);
            return moAttributes;
        }
        // 转换为moFields
        private moFields Convert2Fields(List<string> ColumnNames, List<string> ColumnTypes, string GeometryColumnName)
        {
            moFields Fields = new moFields();
            for (int i = 0; i < ColumnNames.Count; i++)
            {
                if (ColumnNames[i].Trim() !=  GeometryColumnName.Trim())
                {
                    moValueTypeConstant curValueType;
                    switch (ColumnTypes[i].Trim())
                    {
                        case "integer":
                            curValueType = moValueTypeConstant.dInt32;
                            break;
                        case "smallint":
                            curValueType = moValueTypeConstant.dInt16;
                            break;
                        case "bigint":
                            curValueType = moValueTypeConstant.dInt64;
                            break;
                        case "double precision":
                            curValueType = moValueTypeConstant.dDouble;
                            break;
                        case "real":
                            curValueType = moValueTypeConstant.dSingle;
                            break;
                        case "text":
                            curValueType = moValueTypeConstant.dText;
                            break;
                        case "character varying":
                            curValueType = moValueTypeConstant.dText;
                            break;
                        default:
                            curValueType = moValueTypeConstant.dText;
                            break;
                    }
                    moField curField = new moField(ColumnNames[i].Trim(), curValueType);
                    Fields.Append(curField);
                }
            }
            return Fields;
        }

        // 读取DB
        public moFeatures Read_DB()
        {
            // 连接数据库 读取table的所有数据
            string ConStr =
                @"PORT=" + _port +
                ";DATABASE=" + _database +
                ";HOST=" + _host +
                ";PASSWORD=" + _password +
                ";USER ID=" + _id;
            NpgsqlConnection SqlConn = new NpgsqlConnection(ConStr);
            SqlConn.Open();

            if (SqlConn.State != ConnectionState.Open)
            {
                MessageBox.Show("错误！", "系统提示", MessageBoxButtons.OK);
            }

            // 获取geometry字段名、非geometry字段名称和类型
            // 获取table的属性信息（不包括空间数据列），并存到moFields实例中
            string queryGeometryField = "SELECT column_name, data_type FROM information_schema.columns " +
                "WHERE table_name = '" + _table + "' AND udt_name = 'geometry'";
            string queryNonGeometryField = "SELECT column_name, data_type FROM information_schema.columns " +
                "WHERE table_name = '" + _table + "' AND udt_name != 'geometry'";
            string queryFields = "SELECT column_name, data_type, udt_name FROM information_schema.columns WHERE table_name = '" + _table + "'";
            string GeometryColumnName = "geom";     // default name
            List<string> ColumnNames = new List<string>();
            List<string> ColumnTypes = new List<string>();
            
            using (var cmd = new NpgsqlCommand(queryGeometryField, SqlConn))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        GeometryColumnName = reader["column_name"].ToString();
                        break;
                    }
                }
            }
            _geometryColumnName = GeometryColumnName;
            using (var cmd = new NpgsqlCommand(queryNonGeometryField, SqlConn))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string columnName = reader["column_name"].ToString();
                        string dataType = reader["data_type"].ToString();
                        ColumnNames.Add(columnName);
                        ColumnTypes.Add(dataType);
                        
                    }
                }
            }
            _fields = Convert2Fields(ColumnNames, ColumnTypes, GeometryColumnName);
            // 获取table的所有数据，并转为feature(s)
            moFeatures features = new moFeatures();
            string query = "SELECT GeometryType(" + GeometryColumnName +
                ") as geom_type, ST_AsText(" + GeometryColumnName +
                ") as geom_wkt, ST_XMax(" + GeometryColumnName +
                ") as maxX, ST_YMax(" + GeometryColumnName +
                ") as maxY, ST_XMin(" + GeometryColumnName +
                ") as minX, ST_YMin(" + GeometryColumnName +
                ") as minY, * FROM " + _table;
            using (var cmd = new NpgsqlCommand(query, SqlConn))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // 获取geometry的类型
                        string geomType = reader["geom_type"].ToString();
                        // 获取WKT形式的geometry
                        string geomWkt = reader["geom_wkt"].ToString();
                        // MessageBox.Show($"Geometry Type: {geomType}, WKT: {geomWkt}");
                        // 获取最大最小x, y
                        string maxX = reader.GetDouble(reader.GetOrdinal("maxX")).ToString();
                        string maxY = reader.GetDouble(reader.GetOrdinal("maxY")).ToString();
                        string minX = reader.GetDouble(reader.GetOrdinal("minX")).ToString();
                        string minY = reader.GetDouble(reader.GetOrdinal("minY")).ToString();

                        // 获取其他非几何属性的值
                        // Dictionary<string, Type, object> attrivutes = new Dictionary<string, Type, object>();
                        /*List<string> attributesNames = new List<string>();
                        List<Type> attributesTypes = new List<Type>();*/
                        List<object> attributesValues = new List<object>();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            if (reader.GetName(i) != "geom" && reader.GetName(i) != "geom_type" && reader.GetName(i) != "geom_wkt" && 
                                reader.GetName(i) != "maxx" && reader.GetName(i) != "maxy" && reader.GetName(i) != "minx" && reader.GetName(i) != "miny")
                            {
                                /*attributesNames.Add(reader.GetName(i));         // !!!!!!!!!!!如果没用就删掉
                                attributesTypes.Add(reader.GetFieldType(i));*/
                                attributesValues.Add(reader.GetValue(i));
                            }
                        }
                        _geometryType = GetGeometryType(geomType);
                        moFeature feature = new moFeature(
                            _geometryType,
                            GetGeometry(geomType, geomWkt, minX, minY, maxX, maxY),
                            GetAttributes(attributesValues));
                        features.Add(feature);
                    }
                }
            }

            SqlConn.Close();


            return features;
        }
        #endregion

        #region Write DB
        // 从Geometry获取WKT
        private string GetWKT(moGeometry geometry)
        {
            string wkt = "";
            if (geometry is moPoint point)
            {
                string x = Convert.ToString(point.X);
                string y = Convert.ToString(point.Y);
                wkt = "POINT (" + x + " " + y + ")";
                return wkt;
            }
            else if (geometry is moMultiPolyline multiPolyline)
            {
                wkt = "LINESTRING(";
                // 对每一条线
                for (int i = 0; i < multiPolyline.Parts.Count; i++)
                {
                    // wkt += "(";  // 软件暂不支持multilinestring
                    moPoints curPoints = multiPolyline.Parts[i];
                    // 对线上的每一个点
                    for (int j = 0; j < curPoints.Count; j++)
                    {
                        moPoint curPoint = curPoints[j];
                        string x = Convert.ToString(curPoint.X);
                        string y = Convert.ToString(curPoint.Y);
                        wkt += x + " " + y + ", ";
                    }
                    wkt = wkt.Trim().TrimEnd(',');
                    wkt += "), ";
                }
                wkt = wkt.Trim().TrimEnd(',');
                // wkt += ")";
                return wkt;
            }
            else if (geometry is moMultiPolygon multiPolygon)
            {
                wkt = "POLYGON(";
                // 对每一个多边形
                for (int i = 0; i < multiPolygon.Parts.Count; i++)
                {
                    wkt += "(";
                    moPoints curPoints = multiPolygon.Parts[i];
                    // 对多边形上的每一个点
                    for (int j = 0; j < curPoints.Count; j++)
                    {
                        moPoint curPoint = curPoints[j];
                        string x = Convert.ToString(curPoint.X);
                        string y = Convert.ToString(curPoint.Y);
                        wkt += x + " " + y + ", ";
                    }
                    moPoint firstPoint = curPoints[0];
                    string firstX = Convert.ToString(firstPoint.X);
                    string firstY = Convert.ToString(firstPoint.Y);
                    wkt += firstX + " " + firstY + ", ";
                    wkt = wkt.Trim().TrimEnd(',');
                    wkt += "), ";
                }
                wkt = wkt.Trim().TrimEnd(',');
                wkt += ")";
                return wkt;
            }
            else
            {
                MessageBox.Show("不支持的geometry类型！");
                return wkt;
            }
        }
        // 从Attributes | Fields 构造SQL子语句（"1"或"3.14"或"'string'"）
        private string[] GetFieldsSubSQL(moAttributes attributes)
        {
            string[] subSQLs = new string[_fields.Count];
            for ( int i = 0; i < _fields.Count; i ++)
            {
                moField curField = _fields.GetItem(i);
                
                switch (curField.ValueType)
                {
                    case moValueTypeConstant.dInt16:
                    case moValueTypeConstant.dInt32:
                    case moValueTypeConstant.dInt64:
                    case moValueTypeConstant.dDouble:
                    case moValueTypeConstant.dSingle:
                        subSQLs[i] = attributes.GetItem(i).ToString().Trim();
                        break;
                    case moValueTypeConstant.dText:
                    default:
                        subSQLs[i] = "'" + attributes.GetItem(i).ToString().Trim() + "'";
                        break;
                }
            }
            return subSQLs;
        }
        // 写入DB
        public void Write_DB(moFeatures features)
        {
            string[] wkts = new string[features.Count];
            string[] valuesSQLs = new string[features.Count];
            // 获取Geometry和Attributes，方便后续构造WKT和SQL
            for (int i = 0; i < features.Count; i++)
            {
                moFeature feature = features.GetItem(i);
                // geometry
                wkts[i] = GetWKT(feature.Geometry);
                // attributes 获取除了geom之外其他所有属性值；构造成(value1, value2, 'string3')的字符串
                string[] subSQL = GetFieldsSubSQL(feature.Attributes);
                string valuesSQL = "(";
                foreach (string s in subSQL)
                {
                    valuesSQL += s;
                    valuesSQL += ", ";
                }
                // "(VALUE1, VALUE2, VALUEn, "
                // valuesSQL += ")";
                valuesSQLs[i] = valuesSQL;
                
            }
            // fields 获取非geometry字段名
            string[] fielsNames = new string[_fields.Count];
            for (int i = 0; i < _fields.Count; i++)
                fielsNames[i] = _fields.GetItem(i).Name;

            // 构造INSERT SQL
            /*string insertSQL0 = "INSERT INTO " + _table + " (" + "字段" + ") VALUES " +
                "(VALUE1, VALUE2, ST_GeomFromText('POINT(30 10)', 4326))" + "," +
                "(VALUE1, VALUE2, ST_GeomFromText('POINT(30 10)', 4326))" + "," +
                "(VALUE1, VALUE2, ST_GeomFromText('POINT(30 10)', 4326))";*/
            string insertSQL = "INSERT INTO " + _table + " (";
            foreach (string s in fielsNames)
                insertSQL += s + ", ";
            insertSQL += _geometryColumnName + ")";
            insertSQL += " VALUES ";
            for (int i = 0; i < features.Count; i++)
                insertSQL += valuesSQLs[i] + "ST_GeomFromText('" + wkts[i] + "', 4326)), ";
            insertSQL = insertSQL.Trim().TrimEnd(',');

            // 连接DB；删除数据；插入数据
            string ConStr =
                @"PORT=" + _port +
                ";DATABASE=" + _database +
                ";HOST=" + _host +
                ";PASSWORD=" + _password +
                ";USER ID=" + _id;
            NpgsqlConnection SqlConn = new NpgsqlConnection(ConStr);
            SqlConn.Open();
            // Execute
            string deleteInsertQuery = "DELETE FROM " + _table + ";\r\n" + insertSQL;
            using (var deleteInsertCmd = new NpgsqlCommand(deleteInsertQuery, SqlConn))
            {
                try
                {
                    deleteInsertCmd.ExecuteNonQuery();
                    MessageBox.Show("保存成功！");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("出现错误：" + ex.Message);
                    SqlConn.Close();
                    return;
                }
            }
            SqlConn.Close();
        }
        #endregion

        #region Check if Table exists & Create New Table
        
        #endregion
        public bool CheckIfTableExists()
        {
            string ConStr =
                @"PORT=" + _port +
                ";DATABASE=" + _database +
                ";HOST=" + _host +
                ";PASSWORD=" + _password +
                ";USER ID=" + _id;
            try
            {
                using (var conn = new NpgsqlConnection(ConStr))
                {
                    conn.Open();

                    // 构建SQL查询，使用LIKE操作符来匹配表名
                    string query = $"SELECT EXISTS(SELECT FROM information_schema.tables WHERE table_schema = 'public' AND table_name = '" +
                        _table + "')";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        // 执行查询
                        var result = cmd.ExecuteScalar();

                        // 检查结果并输出
                        if (result != null && result is bool && (bool)result)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                return true;
            }
            
        }

        public bool CreateNewGeometryTable(string CreateTableSQL)
        {
            // 连接DB；删除数据；插入数据
            string ConStr =
                @"PORT=" + _port +
                ";DATABASE=" + _database +
                ";HOST=" + _host +
                ";PASSWORD=" + _password +
                ";USER ID=" + _id;
            NpgsqlConnection SqlConn = new NpgsqlConnection(ConStr);
            SqlConn.Open();
            // Execute
            using (var deleteInsertCmd = new NpgsqlCommand(CreateTableSQL, SqlConn))
            {
                try
                {
                    deleteInsertCmd.ExecuteNonQuery();
                    // MessageBox.Show("保存成功！");
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("创建Table时出现错误：" + ex.Message);
                    SqlConn.Close();
                    return false;
                }
            }
            SqlConn.Close();
        }
        #endregion

        /*
         备忘
        !!!!!!!!!!!如果没用就删掉

         总结

        - 读取
        - 保存
        - 获取ST_XMax()，存入XMax
        - point, multipolyline, multipolygon
        - polygon可带空洞

        - geometry -> wkt: 需要闭合
        - wkt -> geometry: 无需再输入多一个点（？）

        - 主窗体中 AddLayer() override（？）了一下，以适应从DB中读取数据
         
         */


        //读取shapefile
        //public moFeatures Read_ShapeFile()
        //{
        //    _shxFile.Read();
        //    _dbfFile.Read();
        //    _shpFile.Read();
        //    moFeatures features = new moFeatures();
        //    for (int i = 0; i < _shpFile.Geometries.Count; ++i)
        //    {
        //        moFeature feature = new moFeature(_shpFile.GetMoGeometryType(),
        //            _shpFile.Geometries[i], _dbfFile.AttributesList[i]);
        //        features.Add(feature);
        //    }
        //    return features;
        //}
    }
}
