using System.Collections.Generic;
using game;
using Game.Creatures;
using Microsoft.Xna.Framework;

namespace Game.Model;

internal class World
{
    private static readonly Dictionary<string, (Vector2, bool)> roomInfo = new Dictionary<string, (Vector2, bool)>
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
    private bool difficultySelected;

    public Room CurrentRoom { get; private set; }
    private Boss Boss { get; set; }
    public Player Player { get; set; }
    public bool BossIsDead { get; private set; }

    public World()
    {
        rooms = new();
        GameManager.Instance.CollisionDetecter.CreateQuadTree(width, height);
        for (var i = 0; i < Textures.Rooms.Count; i++)
        {
            var (name, texture) = Textures.Rooms[i];
            var info = roomInfo[name];
            var position = info.Item1 * tileSize;
            var room = RoomImporter.Import(texture, tileSize, position, info.Item2);
            room.OnCleared += RoomOnCleared;
            room.Name = name;
            rooms.Add(room);
            GameManager.Instance.Drawer.Register(room);
            if (name == "Hallway")
            {
                hallwayIndex = i;
            }

            if (name == "Education room")
            {
                SetCurrentRoom(room);
                EducationRoomUI.Create(position);
                var note = new Note(position + new Vector2(256, 272), Story.DungeonDescription, Vector2.One);
                GameModel.AddInteractable(note);
                GameManager.Instance.Drawer.RegisterHud(note);
            }
        }
    }

    public void Generate(int difficulty)
    {
        foreach (var room in rooms)
        {
            if (room.Name != "Education room" && room.Name != "Hallway")
            {
                room.Generate(difficulty);
            }

            if (room.Name == "Boss room")
            {
                var speed = 400 + 20 * difficulty;
                var health = 2500 + 300 * difficulty;
                var damage = 100 + 10 * difficulty;
                Boss = new Boss(room.Center, speed, health, damage);
                Boss.Tag = "Boss";
                room.CreateEnemy(Boss);
            }
        }
    }

    public void Regenerate(int difficulty)
    {
        foreach (var room in rooms)
        {
            room.Delete();
        }
        Generate(difficulty);
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
            if (!difficultySelected && CurrentRoom.Name == "Education room")
            {
                SelectDifficulty();
            }

            SetCurrentRoom(rooms[hallwayIndex]);
        }
        var currentRoom = GetCurrentRoom(player);
        if (currentRoom is not null)
        {
            SetCurrentRoom(currentRoom);
        }
    }

    private void SelectDifficulty()
    {
        GameManager.Instance.DialogManager.Enable(Story.GetChoiceDifficulty(
            () => Regenerate(1),
            () => Regenerate(2),
            () => Regenerate(3),
            () => Regenerate(4)));
        difficultySelected = true;
    }

    private void RoomOnCleared(Creature player)
    {
        player.Heal(50);
        Boss.Health -= 200;
        Boss.Damage -= 10;
        Boss.Speed -= 10;
    }

    private void SetCurrentRoom(Room room)
    {
        if (CurrentRoom == room)
        {
            return;
        }

        if (CurrentRoom is not null)
        {
            CurrentRoom.Active = false;
        }

        CurrentRoom = room;
        if (room.Name == "Hallway" || room.Name == "Education room")
        {
            SongsManager.StartPlaylist(Sounds.HallwaySongs);
        }
        else if (room.Name == "Boss room")
        {
            SongsManager.StartPlaylist(Sounds.BossSongs);
        }
        else
        {
            SongsManager.StartPlaylist(Sounds.RoomSongs);
        }

        CurrentRoom.Active = true;
    }

    private void UpdateRooms(float deltaTime, Creature player)
    {
        foreach (var room in rooms)
        {
            room.Update(deltaTime, player);
        }
        if (CurrentRoom.BossIsDead)
        {
            BossIsDead = true;
        }
    }

    private Room GetCurrentRoom(Creature player)
    {
        foreach (var room in rooms)
        {
            if (room.InputTrigger is not null && room.InputTrigger.Triggered(player))
            {
                return room;
            }
        }
        return null;
    }
}