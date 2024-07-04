using game;
using Game.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Managers;

internal static class HitboxManager
{
    public static readonly Rectangle Player = new Rectangle(0 - 13, 0 - 19, 26, 38);
    public static readonly Rectangle MeleeEnemy = new Rectangle(-11, -16, 22, 33);
    public static readonly Rectangle Boss = new Rectangle(-14, -28, 28, 56);
    public static readonly Rectangle Bullet = new Rectangle(1, 1, 8, 8);
    public static readonly Rectangle Wall = new Rectangle(0, 0, 32, 32);

    public static void DrawHitbox(ISpriteBatch spriteBatch, Vector2 position, Rectangle hitbox, Vector2 origin)
    {
        spriteBatch.Draw(Textures.Hitbox, position, hitbox, Color.White, 0, origin, 1, SpriteEffects.None, 0);
    }
}