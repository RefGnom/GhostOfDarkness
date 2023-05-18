using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace game;

internal static class TexturesManager
{
    private static ContentManager contentManager;

    public static Texture2D Hitbox { get; private set; }
    public static Texture2D Player { get; private set; }
    public static Texture2D Bullet { get; private set; }
    public static Texture2D MeleeEnemy { get; private set; }
    public static Texture2D PauseMenu { get; private set; }
    public static Texture2D HealthBarForeground { get; private set; }
    public static Texture2D HealthBarBackground { get; private set; }
    public static Texture2D Wall { get; private set; }
    public static Texture2D Door { get; private set; }
    public static Texture2D Floor { get; private set; }
    public static Texture2D ButtonBackground { get; private set; }
    public static Texture2D FieldForText { get; private set; }
    public static Texture2D SavesWindow { get; private set; }
    public static Texture2D PauseBackground { get; private set; }
    public static Texture2D Background { get; private set; }

    #region TileColors
    public static Texture2D WallColor { get; private set; }
    public static Texture2D DoorColor { get; private set; }
    public static Texture2D HallwayFloorColor { get; private set; }
    public static Texture2D RoomFloorColor { get; private set; }
    #endregion

    public static List<(string Name, Texture2D Texture)> Rooms { get; private set; }

    public static void Load(ContentManager content)
    {
        contentManager = content;
        Hitbox = Load("Textures\\Creatures\\Hitbox");
        Player = Load("Textures\\Creatures\\Player");
        Bullet = Load("Textures\\Creatures\\Bullet");
        MeleeEnemy = Load("Textures\\Creatures\\Melee Enemy");
        PauseMenu = Load("Textures\\UI\\Pause Menu");
        HealthBarForeground = Load("Textures\\HUD\\HealthBarForeground");
        HealthBarBackground = Load("Textures\\HUD\\HealthBarBackground");
        Wall = Load("Textures\\Tiles\\Wall");
        Door = Load("Textures\\Tiles\\Door");
        Floor = Load("Textures\\Tiles\\Floors\\Metal");
        ButtonBackground = Load("Textures\\UI\\ButtonBackground");
        FieldForText = Load("Textures\\UI\\FieldForGameName");
        SavesWindow = Load("Textures\\UI\\Saves window");
        PauseBackground = Load("Textures\\UI\\Pause background");
        Background = Load("Textures\\UI\\Background");
        LoadTileColors();
        LoadRooms();
    }

    public static Texture2D GetFloorTexture(string name)
    {
        Texture2D texture;
        try
        {
            texture = contentManager.Load<Texture2D>($"Textures\\Tiles\\Floors\\{name}");
            return texture;
        }
        catch
        {
            Debug.Log($"Floor {name} not found");
            return Floor;
        }
    }

    private static void LoadTileColors()
    {

        var previousDirectory = contentManager.RootDirectory;
        contentManager.RootDirectory += "\\Service";

        WallColor = Load("Wall color");
        DoorColor = Load("Door color");
        HallwayFloorColor = Load("Hallway floor color");
        RoomFloorColor = Load("Room floor color");

        contentManager.RootDirectory = previousDirectory;
    }

        private static void LoadRooms()
    {
        var previousDirectory = contentManager.RootDirectory;
        contentManager.RootDirectory += "\\Textures\\Rooms";

        var names = new List<string>()
        {
            "Education room",
            "Boss room",
            "Hallway",
            "Room1",
            "Room2",
            "Room3",
            "Room4",
            "Room5",
            "Room6",
            "Room7",
            "Room8",
            "Room9",
        };

        Rooms = names.Select(x => (x, Load(x))).ToList();
        contentManager.RootDirectory = previousDirectory;
    }

    private static Texture2D Load(string name) => contentManager.Load<Texture2D>(name);
}