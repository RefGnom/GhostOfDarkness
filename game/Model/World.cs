using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace game;

internal class World
{
    private static readonly Dictionary<string, Vector2> positions = new()
    {
        ["Boss room"] = new Vector2(113, 43),
        ["Education room"] = new Vector2(4, 57),
        ["Hallway"] = new Vector2(13 - 1, 15 - 1),
        ["Room1"] = new Vector2(0, 0),
        ["Room2"] = new Vector2(46, 0),
        ["Room3"] = new Vector2(99, 2),
        ["Room4"] = new Vector2(31, 33),
        ["Room5"] = new Vector2(82, 29),
        ["Room6"] = new Vector2(12, 84),
        ["Room7"] = new Vector2(44, 71),
        ["Room8"] = new Vector2(69, 99),
        ["Room9"] = new Vector2(95, 69),
    };

    private static readonly int tileSize = 32;
    private readonly int width = 64 * 149;
    private readonly int height = 64 * 114;
    private readonly Dictionary<string, Room> rooms;

    public Room CurrentRoom { get; private set; }

    public World()
    {
        rooms = new();
        GameManager.Instance.CollisionDetecter.CreateQuadTree(width, height);
        foreach (var (name, texture) in TexturesManager.Rooms)
        {
            var position = positions[name] * tileSize;
            var tiles = RoomImporter.Import(texture, tileSize, position);
            var room = new Room(tiles, position, tileSize);
            rooms[name] = room;
            GameManager.Instance.Drawer.Register(room);
        }
        CurrentRoom = rooms["Boss room"];
    }

    public void Update(float deltaTime, Creature player)
    {
        CurrentRoom.Update(deltaTime, player);
    }
}