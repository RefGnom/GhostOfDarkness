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
    private readonly List<Room> rooms;
    private readonly int hallwayIndex;

    public Room CurrentRoom { get; private set; }

    public World()
    {
        rooms = new();
        GameManager.Instance.CollisionDetecter.CreateQuadTree(width, height);
        for (int i = 0; i < Textures.Rooms.Count; i++)
        {
            var (name, texture) = Textures.Rooms[i];
            var info = roomInfo[name];
            var position = info.Item1 * tileSize;
            var room = RoomImporter.Import(texture, tileSize, position, info.Item2);
            room.Name = name;
            rooms.Add(room);
            GameManager.Instance.Drawer.Register(room);
            if (name == "Hallway")
                hallwayIndex = i;
            if (name == "Education room")
            {
                SetCurrentRoom(room);
                EducationRoomUI.Create(position);
            }
        }
    }

    public void Generate(int difficulty)
    {
        foreach (var room in rooms)
        {
            if (room.Name != "Education room" && room.Name != "Hallway")
                room.Generate(difficulty);
            if (room.Name == "Boss room")
            {
                room.CreateEnemy(new Boss(room.Center));
            }
        }
    }

    public void Delete()
    {
        foreach (var room in rooms)
        {
            room.Delete();
        }
        GameManager.Delete();
    }

    public void Update(float deltaTime, Creature player)
    {
        UpdateRooms(deltaTime, player);
        if (CurrentRoom.OutputTrigger is not null && CurrentRoom.OutputTrigger.Triggered(player))
        {
            SetCurrentRoom(rooms[hallwayIndex]);
        }
        var (currentRoom, _) = GetCurrentRoom(player);
        if (currentRoom is not null)
        {
            SetCurrentRoom(currentRoom);
        }
    }

    private void SetCurrentRoom(Room room)
    {
        if (CurrentRoom == room)
            return;
        if (CurrentRoom is not null)
            CurrentRoom.Active = false;
        CurrentRoom = room;
        if (room.Name == "Hallway" || room.Name == "Education room")
            SongsManager.StartPlaylist(Sounds.HallwaySongs);
        else if (room.Name == "Boss room")
            SongsManager.StartPlaylist(Sounds.BossSongs);
        else
            SongsManager.StartPlaylist(Sounds.RoomSongs);
        CurrentRoom.Active = true;
    }

    private void UpdateRooms(float deltaTime, Creature player)
    {
        foreach (var room in rooms)
        {
            room.Update(deltaTime, player);
        }
    }

    private (Room, string) GetCurrentRoom(Creature player)
    {
        foreach (var room in rooms)
        {
            if (room.InputTrigger is not null && room.InputTrigger.Triggered(player))
                return (room, room.Name);
        }
        return (null, null);
    }
}