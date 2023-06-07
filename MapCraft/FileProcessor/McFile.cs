using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace MapCraft.FileProcessor
{
    public static class McFile
    {
        private static JsonSerializerOptions sOptions = new JsonSerializerOptions()
        {
            WriteIndented = true
        };

        public class ProjectInfo
        {
            private string mProjectName = "Untitled";
            private List<LayerInfo> mLayersInfo = new List<LayerInfo>();

            public string ProjectName
            {
                get { return mProjectName; }
                set { mProjectName = value; }
            }

            public List<LayerInfo> Layers
            {
                get { return mLayersInfo; }
                set { mLayersInfo = value; }
            }

        }

        public class LayerInfo
        {
            public string Path { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string Renderer { get; set; }
            public string LabelRenderer { get; set; }
        }

        public static ProjectInfo Read(string path)
        {
            string project = File.ReadAllText(path);
            ProjectInfo projectInfo = JsonSerializer.Deserialize<ProjectInfo>(project);
            projectInfo.ProjectName = Path.GetFileNameWithoutExtension(path);
            return projectInfo;
        }

        public static void Write(string path, ProjectInfo projectInfo)
        {
            projectInfo.ProjectName = Path.GetFileNameWithoutExtension(path);
            string project = JsonSerializer.Serialize(projectInfo, sOptions);
            File.WriteAllText(path, project);
        }
    }
}
