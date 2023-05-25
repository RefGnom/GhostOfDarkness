using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace game;

internal class World
{
    private static readonly Dictionary<string, (Vector2, bool)> roomInfo = new()
    {
        ["Boss room"] = (new Vector2(113, 43), true),
        ["Education room"] = (new Vector2(4, 57), false),
        ["Hallway"] = (new Vector2(13 - 1, 15 - 1), false),
        ["Room1"] = (new Vector2(0, 0), false),
        ["Room2"] = (new Vector2(46, 0), false),
        ["Room3"] = (new Vector2(99, 2), false),
        ["Room4"] = (new Vector2(31, 33), true),
        ["Room5"] = (new Vector2(82, 29), true),
        ["Room6"] = (new Vector2(12, 84), true),
        ["Room7"] = (new Vector2(44, 71), true),
        ["Room8"] = (new Vector2(69, 99), true),
        ["Room9"] = (new Vector2(95, 69), true),
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
            var info = roomInfo[name];
            var position = info.Item1 * tileSize;
            var room = RoomImporter.Import(texture, tileSize, position, info.Item2);
            room.Name = name;
            rooms[name] = room;
            GameManager.Instance.Drawer.Register(room);
        }
        CurrentRoom = rooms["Education room"];
    }

    public void Generate()
    {
        
    }

    public void Delete()
    {
        GameManager.Delete();
        foreach (var room in rooms.Values)
        {
            room.Delete();
        }
    }

    public void Update(float deltaTime, Creature player)
    {
        //Debug.Log(CurrentRoom.Name);
        CurrentRoom.Update(deltaTime, player);
        if (CurrentRoom.OutputTrigger is not null && CurrentRoom.OutputTrigger.Triggered(player))
        {
            CurrentRoom = rooms["Hallway"];
        }
        var (currentRoom, _) = GetCurrentRoom(player);
        if (currentRoom is not null)
        {
            CurrentRoom = currentRoom;
        }
    }

    private (Room, string) GetCurrentRoom(Creature player)
    {
        foreach (var (name, room) in rooms)
        {
            if (room.InputTrigger is not null && room.InputTrigger.Triggered(player))
                return (room, name);
        }
        return (null, null);
    }
}