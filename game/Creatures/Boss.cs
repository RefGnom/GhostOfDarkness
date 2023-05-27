using Microsoft.Xna.Framework;

namespace game;

internal class Boss : Enemy
{
    public Boss(EnemyView view, Vector2 position) : base(view, position, 300, 500, 40, 80, 2)
    {

    }
}