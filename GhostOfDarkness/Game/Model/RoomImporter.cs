using System;
using System.Collections.Generic;
using Core.Extensions;
using game;
using Game.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using IDrawable = Game.Interfaces.IDrawable;

namespace Game.Model;

internal static class RoomImporter
{
    private static readonly Dictionary<int, Func<Vector2, bool, IDrawable>> colorToEntity;

    static RoomImporter()
    {
        var wall = GetColorValue(Textures.WallColor);
        var door = GetColorValue(Textures.DoorColor);
        var roomFloor = GetColorValue(Textures.RoomFloorColor);
        var hallwayFloor = GetColorValue(Textures.HallwayFloorColor);

        colorToEntity = new()
        {
            [wall] = (position, _) => new Wall(position),
            [door] = (position, rotate90) => new Door(position, rotate90),
            [roomFloor] = (position, _) => new Floor(position),
            [hallwayFloor] = (position, _) => new Floor(position),
        };
    }

    public static Room Import(Texture2D texture, int tileSize, Vector2 position, bool swapTriggers)
    {
        var tiles = new Tile[texture.Width, texture.Height];
        var data = texture.TransformToColorsArray();
        var doors = new List<Door>();
        for (var x = 0; x < texture.Width; x++)
        {
            for (var y = 0; y < texture.Height; y++)
            {
                var localPosition = new Vector2(x, y) * tileSize;
                if (colorToEntity.ContainsKey(data[x, y]))
                {
                    var rotateDoor = y > 0 && tiles[x, y - 1]?.Entity is not (null or Floor);

                    var entity = colorToEntity[data[x, y]](localPosition + position, rotateDoor);
                    if (entity is Door door)
                    {
                        doors.Add(door);
                    }

                    tiles[x, y] = new Tile(entity, localPosition + position, tileSize);
                }
                else
                {
                    tiles[x, y] = new Tile(localPosition + position, tileSize);
                }

            }
        }
        var room = new Room(tiles, position, tileSize);
        if (doors.Count == 2)
        {
            GameModel.AddInteractable(new InteractableDoor(doors[0], doors[1]));
            FillDoorTriggers(room, tileSize, doors[0], swapTriggers);
        }
        return room;
    }

    private static void FillDoorTriggers(Room room, int tileSize, Door door, bool swap)
    {
        Rectangle firstHitbox;
        Rectangle secondHitbox;
        var width = tileSize / 4;
        var height = tileSize * 2;
        if (door.Vertical)
        {
            firstHitbox.X = (int)door.Position.X - (tileSize / 2 + width / 2);
            firstHitbox.Y = (int)door.Position.Y;
            firstHitbox.Width = width;
            firstHitbox.Height = height;
            secondHitbox = firstHitbox;
            secondHitbox.X += 2 * tileSize;
        }
        else
        {
            firstHitbox.X = (int)door.Position.X;
            firstHitbox.Y = (int)door.Position.Y - (tileSize / 2 + width / 2);
            firstHitbox.Width = height;
            firstHitbox.Height = width;
            secondHitbox = firstHitbox;
            secondHitbox.Y += 2 * tileSize;
        }
        room.InputTrigger = new DoorTrigger(firstHitbox);
        room.OutputTrigger = new DoorTrigger(secondHitbox);
        if (swap)
        {
            (room.OutputTrigger, room.InputTrigger) = (room.InputTrigger, room.OutputTrigger);
        }
    }

    private static int GetColorValue(Texture2D texture)
    {
        var data = new int[1];
        texture.GetData(data);
        return data[0];
    }
}