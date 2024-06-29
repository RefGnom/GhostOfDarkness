using Core.Maps;
using Newtonsoft.Json;

namespace Core.Saves;

public class Save
{
    [JsonIgnore]
    public SaveInfo Info { get; set; }
    [JsonProperty]
    public Map Map { get; set; }
}