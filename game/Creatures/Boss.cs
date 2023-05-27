using Microsoft.Xna.Framework;

namespace game;

internal class Boss : Enemy
{
    public Boss(Vector2 position) : base(new BossView(), position, 150, 1000, 40, 80, 2)
    {
        Hitbox = HitboxManager.Boss;
        var view = View as BossView;
        view.SetModel(this);
        view.CreateStates();
    }
}