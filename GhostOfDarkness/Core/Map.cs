using Microsoft.Xna.Framework;
using Newtonsoft.Json;

namespace Core;

public class Map
{
    [JsonProperty(Order = 1)]
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
            {
                throw new ArgumentException("Size cannot be a negative value");
            }

            var newItems = new MapItem[(int)value.X, (int)value.Y];
            var width = (int)Math.Min(sizeInTiles.X, value.X);
            var height = (int)Math.Min(sizeInTiles.Y, value.Y);
            for (var i = 0; i < width; i++)
                for (var j = 0; j < height; j++)
            {
                newItems[i, j] = items[i, j];
            }

            items = newItems;
            sizeInTiles = value;
            SizeChanged?.Invoke();
        }
    }

    public event Action? SizeChanged;

    public MapItem this[int x, int y]
    {
        get => items[x, y];
        set => items[x, y] = value;
    }

    public Map(int widthInTiles, int heightInTiles)
    {
        items = new MapItem[widthInTiles, heightInTiles];
        sizeInTiles = new Vector2(widthInTiles, heightInTiles);
    }
}