using game.Extensions;
using game.Managers;
using Microsoft.Xna.Framework;

namespace game.Creatures;

internal class MeleeEnemy : Enemy
{
    public MeleeEnemy(Vector2 position, float speed)
        : base(new MeleeEnemyView(), position, speed, 100, 10, 30, 1.5f)
    {
        OnUpdate += () => Hitbox = HiboxManager.MeleeEnemy.Shift(Position - new Vector2(16, 16));
    }
}