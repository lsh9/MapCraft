using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyMapObjects;
using MapCraft.FileProcessor;



namespace MapCraft
{
    public partial class MapCraftForm : Form
    {
        public MapCraftForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 打开shp文件并在mapcontrol中显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenFileItem_Click(object sender, EventArgs e)
        {
            string shpFilePath;
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = @"ShapeFile 文件|*.shp";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                shpFilePath = fileDialog.FileName;
                fileDialog.Dispose();
            }
            else
            {
                fileDialog.Dispose();
                return;
            }

            try
            {
                string layerName = Path.GetFileNameWithoutExtension(shpFilePath);
                string layerPath = shpFilePath.Substring(0, shpFilePath.IndexOf(".shp", StringComparison.Ordinal));

                ShapeFileParser fileProcessor = new ShapeFileParser(layerPath);

                // convert to mapLayer
                moMapLayer mapLayer =
                    new moMapLayer(layerName, fileProcessor.GeometryType, fileProcessor.Fields);

                // construct features, using geometries and attributes list
                moFeatures features = new moFeatures();
                for (int i = 0; i < fileProcessor.Geometries.Count; ++i)
                {
                    moFeature feature = new moFeature(fileProcessor.GeometryType,
                        fileProcessor.Geometries[i], fileProcessor.AttributesList[i]);
                    features.Add(feature);
                }

                mapLayer.Features = features;
                AddLayerToMap(mapLayer);
            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString());
                
            }
        }




    }
}
