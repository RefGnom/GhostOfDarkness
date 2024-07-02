using Newtonsoft.Json;

namespace Core.Saves;

public class SaveInfo
{
    [JsonProperty]
    public string Name { get; set; }
    [JsonProperty]
    public TimeSpan PlayTime { get; set; }
    [JsonProperty]
    public int Difficulty { get; set; }
}