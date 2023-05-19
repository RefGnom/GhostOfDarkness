using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace game;

internal static class RoomImporter
{
    private static readonly Dictionary<int, Func<Vector2, bool, IDrawable>> colorToEntity;

    static RoomImporter()
    {
        var wall = GetColorValue(TexturesManager.WallColor);
        var door = GetColorValue(TexturesManager.DoorColor);
        var roomFloor = GetColorValue(TexturesManager.RoomFloorColor);
        var hallwayFloor = GetColorValue(TexturesManager.HallwayFloorColor);

        colorToEntity = new()
        {
            [wall] = (position, rotate90) => new Wall(position),
            [door] = (position, rotate90) => new Door(position, rotate90),
            [roomFloor] = (position, rotate90) => new Floor(position),
            [hallwayFloor] = (position, rotate90) => new Floor(position),
        };
    }

    public static Tile[,] Import(Texture2D texture, int tileSize, Vector2 position)
    {
        var tiles = new Tile[texture.Width, texture.Height];
        var data = texture.TransformToColorsArray();
        var doors = new List<Door>();
        for (int x = 0; x < texture.Width; x++)
        {
            for (int y = 0; y < texture.Height; y++)
            {
                if (colorToEntity.ContainsKey(data[x, y]))
                {
                    var rotateDoor = false;
                    if (y > 0 && tiles[x, y - 1]?.Entity is not (null or Floor))
                        rotateDoor = true;
                    var localPosition = new Vector2(x, y) * tileSize;
                    var entity = colorToEntity[data[x, y]](localPosition + position, rotateDoor);
                    if (entity is Door)
                        doors.Add(entity as Door);
                    tiles[x, y] = new Tile(entity, localPosition + position, tileSize);
                }

            }
        }
        if (doors.Count == 2)
            GameModel.AddInteractable(new InteractableDoor(doors[0], doors[1]));
        return tiles;
    }

    private static int GetColorValue(Texture2D texture)
    {
        var data = new int[1];
        texture.GetData(data);
        return data[0];
    }
}