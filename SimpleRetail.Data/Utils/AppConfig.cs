using Newtonsoft.Json;

namespace SimpleRetail.Data.Utils;

public class DataConfig
{
    public DataConfiguration Options { get; set; }

    public static DataConfig Load(string filePath)
    {
        using (StreamReader reader = new StreamReader(filePath))
        {
            string json = reader.ReadToEnd();
            return JsonConvert.DeserializeObject<DataConfig>(json);
        }
    }

    public void Save(string filePath)
    {
        string json = JsonConvert.SerializeObject(this, Formatting.Indented);
        File.WriteAllText(filePath, json);
    }
}

public class DataConfiguration
{
    public string NeedSeeding { get; set; }
}
