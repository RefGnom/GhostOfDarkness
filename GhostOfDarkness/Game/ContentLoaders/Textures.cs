using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace game;

internal static class Textures
{
    private static ContentManager contentManager;

    public static Texture2D Hitbox { get; private set; }
    public static Texture2D Player { get; private set; }
    public static Texture2D Bullet { get; private set; }
    public static Texture2D MeleeEnemy { get; private set; }
    public static Texture2D Boss { get; private set; }
    public static Texture2D HealthBarForeground { get; private set; }
    public static Texture2D HealthBarBackground { get; private set; }
    public static Texture2D EducationUI { get; private set; }
    public static Texture2D Paper { get; private set; }
    public static Texture2D Wall { get; private set; }
    public static Texture2D Door { get; private set; }
    public static Texture2D OpenedDoor { get; private set; }
    public static Texture2D Floor { get; private set; }
    public static Texture2D ButtonBackground { get; private set; }
    public static Texture2D SettingsString { get; private set; }
    public static Texture2D FieldForText { get; private set; }
    public static Texture2D SavesWindow { get; private set; }
    public static Texture2D PauseBackground { get; private set; }
    public static Texture2D Background { get; private set; }
    public static Texture2D HorizontalLine { get; private set; }
    public static Texture2D VerticalLine { get; private set; }
    public static Texture2D DialogHelpUI { get; private set; }

    #region ProgressBar
    public static Texture2D ProgressBarBackground { get; private set; }
    public static Texture2D ProgressBarForeground { get; private set; }
    public static Texture2D ProgressBarValue { get; private set; }
    #endregion

    #region Switcher
    public static Texture2D SwitcherLeftArrow { get; private set; }
    public static Texture2D SwitcherRightArrow { get; private set; }
    public static Texture2D SwitcherBackground { get; private set; }
    #endregion

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
        Boss = Load("Textures\\Creatures\\Boss");
        HealthBarForeground = Load("Textures\\HUD\\HealthBarForeground");
        HealthBarBackground = Load("Textures\\HUD\\HealthBarBackground");
        EducationUI = Load("Textures\\UI\\EducationUI");
        Paper = Load("Textures\\UI\\Paper");
        Wall = Load("Textures\\Tiles\\Wall");
        Door = Load("Textures\\Tiles\\Door");
        OpenedDoor = Load("Textures\\Tiles\\Opened door");
        Floor = Load("Textures\\Tiles\\Floors\\Metal");
        ButtonBackground = Load("Textures\\UI\\ButtonBackground");
        SettingsString = Load("Textures\\UI\\Settings string");
        FieldForText = Load("Textures\\UI\\FieldForGameName");
        SavesWindow = Load("Textures\\UI\\Saves window");
        PauseBackground = Load("Textures\\UI\\Pause background");
        Background = Load("Textures\\UI\\Background");
        HorizontalLine = Load("Service\\Horizontal line");
        VerticalLine = Load("Service\\Vertical line");
        DialogHelpUI = Load("Textures\\UI\\Dialog helper");
        ProgressBarBackground = Load("Textures\\UI\\ProgressBar\\ProgressBarBackground");
        ProgressBarForeground = Load("Textures\\UI\\ProgressBar\\ProgressBarForeground");
        ProgressBarValue = Load("Textures\\UI\\ProgressBar\\ProgressBarValue");
        SwitcherLeftArrow = Load("Textures\\UI\\Switcher\\LeftArrow");
        SwitcherRightArrow = Load("Textures\\UI\\Switcher\\RightArrow");
        SwitcherBackground = Load("Textures\\UI\\Switcher\\Background");
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