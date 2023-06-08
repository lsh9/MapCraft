using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using static System.Text.Json.JsonElement;

namespace MapCraft.FileProcessor
{
    public static class McFile
    {
        private static JsonSerializerOptions sOptions = new JsonSerializerOptions()
        {
            WriteIndented = true,
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
            public Dictionary<string, object> Renderer { get; set; }
            public Dictionary<string, object> LabelRenderer { get; set; }
        }

        public static ProjectInfo Read(string path)
        {
            string project = File.ReadAllText(path);
            ProjectInfo projectInfo = JsonSerializer.Deserialize<ProjectInfo>(project);
            foreach (LayerInfo layerInfo in projectInfo.Layers)
            {
                layerInfo.Renderer = ProcessDictionary(layerInfo.Renderer);
                layerInfo.LabelRenderer = ProcessDictionary(layerInfo.LabelRenderer);
            }
            projectInfo.ProjectName = Path.GetFileNameWithoutExtension(path);
            return projectInfo;
        }

        public static void Write(string path, ProjectInfo projectInfo)
        {
            projectInfo.ProjectName = Path.GetFileNameWithoutExtension(path);
            string project = JsonSerializer.Serialize(projectInfo, sOptions);
            File.WriteAllText(path, project);
        }

        // 格式转换
        static Dictionary<string, object> ProcessDictionary(Dictionary<string, object> dictionary)
        {
            Dictionary<string, object> newDictionary = new Dictionary<string, object>();
            foreach (KeyValuePair<string, object> kvp in dictionary)
            {
                string key = kvp.Key;
                object value = kvp.Value;

                if (value is JsonElement jsonValue)
                {
                    if (jsonValue.ValueKind == JsonValueKind.Number)
                    {
                        // 转换成整数或浮点数并存储
                        if (jsonValue.TryGetInt32(out int intValue))
                        {
                            newDictionary[key] = intValue;
                        }
                        else if (jsonValue.TryGetDouble(out double doubleValue))
                        {
                            newDictionary[key] = doubleValue;
                        }
                    }
                    else if (jsonValue.ValueKind == JsonValueKind.String)
                    {
                        // 转换成字符串并存储
                        newDictionary[key] = jsonValue.GetString();
                    }
                    else if (jsonValue.ValueKind == JsonValueKind.True)
                    {
                        // 转换成布尔值并存储
                        newDictionary[key] = true;
                    }
                    else if (jsonValue.ValueKind == JsonValueKind.False)
                    {
                        // 转换成布尔值并存储
                        newDictionary[key] = false;
                    }
                    else if (jsonValue.ValueKind == JsonValueKind.Null)
                    {
                        // 转换成 null 并存储
                        newDictionary[key] = null;
                    }
                    else if (jsonValue.ValueKind == JsonValueKind.Object)
                    {
                        // 处理嵌套的 JSON 对象
                        Dictionary<string, object> nestedData = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonValue.GetRawText());
                        newDictionary[key] = ProcessDictionary(nestedData);
                    }
                    else if (jsonValue.ValueKind == JsonValueKind.Array)
                    {
                        // 处理嵌套的 JSON 数组
                        newDictionary[key] = ProcessArray(jsonValue.EnumerateArray());
                    }
                    // 可以添加其他需要处理的 JsonValueKind 类型
                }
                else
                {
                    newDictionary[key] = value;
                }
            }
            return newDictionary;
        }

        static List<object> ProcessArray(ArrayEnumerator arrayEnumerator)
        {
            List<object> newArray = new List<object>();
            while (arrayEnumerator.MoveNext())
            {
                JsonElement arrayElement = arrayEnumerator.Current;
                if (arrayElement.ValueKind == JsonValueKind.Number)
                {
                    // 转换成整数或浮点数并存储
                    if (arrayElement.TryGetInt32(out int intValue))
                    {
                        newArray.Add(intValue);
                    }
                    else if (arrayElement.TryGetDouble(out double doubleValue))
                    {
                        newArray.Add(doubleValue);
                    }
                }
                else if (arrayElement.ValueKind == JsonValueKind.String)
                {
                    // 转换成字符串并存储
                    newArray.Add(arrayElement.GetString());
                }
                else if (arrayElement.ValueKind == JsonValueKind.True)
                {
                    // 转换成布尔值并存储
                    newArray.Add(true);
                }
                else if (arrayElement.ValueKind == JsonValueKind.False)
                {
                    // 转换成布尔值并存储
                    newArray.Add(false);
                }
                else if (arrayElement.ValueKind == JsonValueKind.Null)
                {
                    // 转换成 null 并存储
                    newArray.Add(null);
                }
                else if (arrayElement.ValueKind == JsonValueKind.Object)
                {
                    // 处理嵌套的 JSON 对象
                    Dictionary<string, object> nestedData = JsonSerializer.Deserialize<Dictionary<string, object>>(arrayElement.GetRawText());
                    newArray.Add(ProcessDictionary(nestedData));
                }
                else if (arrayElement.ValueKind == JsonValueKind.Array)
                {
                    // 处理嵌套的 JSON 数组
                    newArray.Add(ProcessArray(arrayElement.EnumerateArray()));
                }
                // 可以添加其他需要处理的 JsonValueKind 类型
            }
            return newArray;
        }
    }
}
