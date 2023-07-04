using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Core;

public class MapSerializer
{
    private readonly JsonSerializerSettings settings;

    public MapSerializer()
    {
        settings = new JsonSerializerSettings();
        settings.Formatting = Formatting.Indented;
        settings.TypeNameHandling = TypeNameHandling.Auto;
        settings.Converters.Add(new BinaryConverter());
    }

    public void Serialize(Map map, string path)
    {
        var json = JsonConvert.SerializeObject(map, settings);
        File.WriteAllText(path, json);
    }

    public Map Deserialize(string path)
    {
        var json = File.ReadAllText(path);
        return JsonConvert.DeserializeObject<Map>(json, settings)!;
    }
}