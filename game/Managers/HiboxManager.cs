using Microsoft.Xna.Framework;

namespace game.Managers;

internal static class HiboxManager
{
    public static readonly Rectangle MeleeEnemy = new(6, 6, 21, 27);
    public static readonly Rectangle Bullet = new(1, 1, 8, 8);
    public static readonly Rectangle Wall = new(0, 0, 32, 32);
}