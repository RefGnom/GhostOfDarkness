using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Core.Maps;

public class MapSerializer
{
    private readonly JsonSerializerSettings settings;

    public MapSerializer()
    {
        settings = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            TypeNameHandling = TypeNameHandling.Auto
        };
        settings.Converters.Add(new BinaryConverter());
    }

    public void Serialize(Maps.Map map, string path)
    {
        var json = JsonConvert.SerializeObject(map, settings);
        File.WriteAllText(path, json);
    }

    public Maps.Map Deserialize(string path)
    {
        var json = File.ReadAllText(path);
        return JsonConvert.DeserializeObject<Maps.Map>(json, settings)!;
    }
}