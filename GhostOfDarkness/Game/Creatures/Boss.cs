using Core.DependencyInjection;
using Game.Managers;
using Microsoft.Xna.Framework;

namespace Game.Creatures;

[DiIgnore]
internal class Boss : Enemy
{
    public Boss(Vector2 position, float speed, float health, float damage) : base(new BossView(), position, speed, health, damage, 80, 2)
    {
        Hitbox = HitboxManager.Boss;
        var view = View as BossView;
        view.SetModel(this);
        view.CreateStates();
    }
}