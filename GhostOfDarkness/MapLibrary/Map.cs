using Microsoft.Xna.Framework;
using Newtonsoft.Json;

namespace Core;

public class Map
{
    [JsonProperty]
    private MapItem[,] items;
    [JsonProperty]
    private Vector2 sizeInTiles;
    [JsonIgnore]
    public readonly int TileSize = 32;

    [JsonIgnore]
    public Vector2 SizeInTiles {
        get => sizeInTiles;
        set
        {
            if (value.X < 0 || value.Y < 0)
                throw new ArgumentException("Size cannot be a negative value");
            var newItems = new MapItem[(int)value.X, (int)value.Y];
            for (int i = 0; i < sizeInTiles.X; i++)
                for (int j = 0; j < sizeInTiles.Y; j++)
                    newItems[i, j] = items[i, j];
            items = newItems;
            sizeInTiles = value;
            Size = value * TileSize;
        }
    }

    public Vector2 Size { get; private set; }

    public MapItem this[int x, int y]
    {
        get => items[x, y];
        set => items[x, y] = value;
    }

    public Map(int widthInTiles, int heightInTiles)
    {
        items = new MapItem[widthInTiles, heightInTiles];
        Size = new Vector2(widthInTiles, heightInTiles) * TileSize;
    }
}