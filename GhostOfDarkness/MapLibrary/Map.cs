using Microsoft.Xna.Framework;
using Newtonsoft.Json;

namespace MapLibrary;

public class Map
{
    [JsonProperty]
    private readonly MapItem[,] items;

    public Vector2 Size { get; init; }
    public int TileSize { get; init; }

    public MapItem this[int x, int y]
    {
        get => items[x, y];
        set => items[x, y] = value;
    }

    public Map(int widthInTiles, int heightInTiles, int tileSize)
    {
        items = new MapItem[widthInTiles, heightInTiles];
        Size = new Vector2(widthInTiles, heightInTiles) * tileSize;
        TileSize = tileSize;
    }
}