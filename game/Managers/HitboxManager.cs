using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game;

internal static class HitboxManager
{
    public static readonly Rectangle Player = new(0 - 13, 0 - 19, 26, 38);
    public static readonly Rectangle MeleeEnemy = new(6 - 18, 6 - 19, 22, 33);
    public static readonly Rectangle Bullet = new(1, 1, 8, 8);
    public static readonly Rectangle Wall = new(0, 0, 32, 32);

    public static void DrawHitbox(SpriteBatch spriteBatch, Vector2 position, Rectangle hitbox, Vector2 origin)
    {
        spriteBatch.Draw(TexturesManager.Hitbox, position, hitbox, Color.White, 0, origin, 1, SpriteEffects.None, 0);
    }
}