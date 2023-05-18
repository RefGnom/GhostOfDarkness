using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace game;

internal static class RoomImporter
{
    private static readonly Dictionary<int, Func<Vector2, IDrawable>> colorToEntity;

    static RoomImporter()
    {
        var wall = GetColorValue(TexturesManager.WallColor);
        var door = GetColorValue(TexturesManager.DoorColor);
        var roomFloor = GetColorValue(TexturesManager.RoomFloorColor);
        var hallwayFloor = GetColorValue(TexturesManager.HallwayFloorColor);

        colorToEntity = new()
        {
            [wall] = (position) => new Wall(position),
            //[door] = (position) => new Door(position),
            [roomFloor] = (position) => new Floor(position),
            [hallwayFloor] = (position) => new Floor(position),
        };
    }

    public static Tile[,] Import(Texture2D texture, int tileSize, Vector2 position)
    {
        var tiles = new Tile[texture.Width, texture.Height];
        var data = texture.TransformToColorsArray();
        for (int x = 0; x < texture.Width; x++)
        {
            for (int y = 0; y < texture.Height; y++)
            {
                if (colorToEntity.ContainsKey(data[x, y]))
                {
                    var localPosition = new Vector2(x, y) * tileSize;
                    var entity = colorToEntity[data[x, y]](localPosition + position);
                    tiles[x, y] = new Tile(entity, localPosition + position, tileSize);
                }

            }
        }
        return tiles;
    }

    private static int GetColorValue(Texture2D texture)
    {
        var data = new int[1];
        texture.GetData(data);
        return data[0];
    }
}