using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using MyMapObjects;

namespace MapCraft.IO
{
    internal class ShapefileWriter
    {

        public static IFeature ConvertToIFeature(moFeature feature, moFields fields)
        {
            // 将自定义的几何转换为 NetTopologySuite 的几何对象
            GeometryFactory geometryFactory = new GeometryFactory();
            Geometry geometry = null;
            switch (feature.ShapeType)
            {
                case moGeometryTypeConstant.Point:
                    {
                        var point = (moPoint)(feature.Geometry);
                        geometry = geometryFactory.CreatePoint(new Coordinate(point.X, point.Y));
                        break;
                    }
                case moGeometryTypeConstant.MultiPolyline:
                    {
                        var polyline = (moMultiPolyline)(feature.Geometry);
                        var lineStrings = new List<LineString>();
                        for (int i = 0; i < polyline.Parts.Count; i++)
                        {
                            var points = polyline.Parts[i];
                            Coordinate[] coordinates = new Coordinate[points.Count];
                            for (int j = 0; j < coordinates.Length; j++)
                            {
                                coordinates[j] = new Coordinate(points[j].X, points[j].Y);
                            }
                            lineStrings.Add(geometryFactory.CreateLineString(coordinates));
                        }
                        geometry = geometryFactory.CreateMultiLineString(lineStrings.ToArray());
                        break;
                    }
                case moGeometryTypeConstant.MultiPolygon:
                    {
                        var polygon = (moMultiPolygon)(feature.Geometry);
                        var polygons = new List<Polygon>();
                        for (int i = 0; i < polygon.Parts.Count; i++)
                        {
                            var points = polygon.Parts[i];
                            Coordinate[] coordinates = new Coordinate[points.Count + 1];
                            for (int j = 0; j < points.Count; j++)
                            {
                                coordinates[j] = new Coordinate(points[j].X, points[j].Y);
                            }
                            coordinates[points.Count] = new Coordinate(points[0].X, points[0].Y);
                            var linearRing = geometryFactory.CreateLinearRing(coordinates);
                            polygons.Add(geometryFactory.CreatePolygon(linearRing));
                        }
                        geometry = geometryFactory.CreateMultiPolygon(polygons.ToArray());
                        break;
                    }
            }


            // 创建 AttributesTable 并填充属性
            AttributesTable attributesTable = new AttributesTable();
            for (int i = 0; i < fields.Count; i++)
            {
                var key = fields[i].Name;
                var val = feature.Attributes[i];
                attributesTable.Add(key, val);
            }

            // 返回 NetTopologySuite 的 Feature 对象
            return new Feature(geometry, attributesTable);
        }


        public static void write(moMapLayer layer, string outputPath)
        {
            moFeatures features = layer.Features;
            moFields fields = layer.AttributeFields;

            List<IFeature> iFeatures = new List<IFeature>();
            for (int i = 0; i < features.Count; i++)
            {
                var feature = features[i];
                IFeature iFeature = ConvertToIFeature(feature, fields);
                iFeatures.Add(iFeature);
            }

            var writer = new ShapefileDataWriter(outputPath)
            {
                Header = ShapefileDataWriter.GetHeader(iFeatures[0], iFeatures.Count)
            };

            // 写入 shapefile
            writer.Write(iFeatures);
        }
    }
}
