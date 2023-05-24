using Microsoft.Xna.Framework;

namespace game;

internal class MeleeEnemy : Enemy
{
    private static readonly float scale = 1.5f;

    public MeleeEnemy(Vector2 position, float speed)
        : base(new MeleeEnemyView(scale), position, speed, 100, 10, 45, 1.5f)
    {
        Hitbox = HitboxManager.MeleeEnemy;
        var view = View as EnemyView;
        view.SetModel(this);
    }
}