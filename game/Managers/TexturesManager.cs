using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace game;

internal static class TexturesManager
{
    public static Texture2D Hitbox { get; private set; }
    public static Texture2D Player { get; private set; }
    public static Texture2D Bullet { get; private set; }
    public static Texture2D MeleeEnemy { get; private set; }
    public static Texture2D PauseMenu { get; private set; }
    public static Texture2D HealthBarForeground { get; private set; }
    public static Texture2D HealthBarBackground { get; private set; }
    public static Texture2D Wall { get; private set; }
    public static Texture2D ButtonBackground { get; private set; }
    public static Texture2D FieldForText { get; private set; }

    public static void Load(ContentManager content)
    {
        Hitbox = content.Load<Texture2D>("Textures\\Creatures\\Hitbox");
        Player = content.Load<Texture2D>("Textures\\Creatures\\Player");
        Bullet = content.Load<Texture2D>("Textures\\Creatures\\Bullet");
        MeleeEnemy = content.Load<Texture2D>("Textures\\Creatures\\Melee Enemy");
        PauseMenu = content.Load<Texture2D>("Textures\\UI\\Pause Menu");
        HealthBarForeground = content.Load<Texture2D>("Textures\\HUD\\HealthBarForeground");
        HealthBarBackground = content.Load<Texture2D>("Textures\\HUD\\HealthBarBackground");
        Wall = content.Load<Texture2D>("Textures\\Tiles\\Wall");
        ButtonBackground = content.Load<Texture2D>("Textures\\UI\\ButtonBackground");
        FieldForText = content.Load<Texture2D>("Textures\\UI\\FieldForGameName");
    }
}