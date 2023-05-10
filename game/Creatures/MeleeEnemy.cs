using Microsoft.Xna.Framework;

namespace game.Creatures;

internal class MeleeEnemy : Enemy
{
    public MeleeEnemy(Vector2 position, float speed)
        : base(position, new Vector2(32, 32), speed, 100, 10, 20, 1.5f)
    {
        Initialize(new MeleeEnemyView(this));
    }
}