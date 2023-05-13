using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace game.Managers;

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

    public static void Load(ContentManager content)
    {
        Hitbox = content.Load<Texture2D>("Hitbox");
        Player = content.Load<Texture2D>("Player");
        Bullet = content.Load<Texture2D>("Bullet");
        MeleeEnemy = content.Load<Texture2D>("Melee Enemy");
        PauseMenu = content.Load<Texture2D>("Pause Menu");
        HealthBarForeground = content.Load<Texture2D>("HealthBarForeground");
        HealthBarBackground = content.Load<Texture2D>("HealthBarBackground");
        Wall = content.Load<Texture2D>("Wall");
    }
}