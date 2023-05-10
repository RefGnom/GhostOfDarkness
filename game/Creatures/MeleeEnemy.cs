using game.Interfaces;
using game.Managers;
using game.View;
using Microsoft.Xna.Framework;

namespace game.Creatures;

internal class MeleeEnemy : Creature, IEnemy
{
    private bool isIdle;
    public bool IsDead => view.Killed;

    private readonly MeleeEnemyView view;

    public MeleeEnemy(Vector2 position, float speed) : base(position, speed, 100, 10, 16, 1)
    {
        view = new MeleeEnemyView(this);
        Drawer.Register(view);
    }

    private void Attack(Creature target)
    {
        var distance = Vector2.Distance(Position, target.Position);
        if (distance <= AttackDistance && currentColdown <= 0)
        {
            target.TakeDamage(Damage);
            currentColdown = cooldown;
            view.Attack();
        }
    }

    public override void TakeDamage(float damage)
    {
        Health -= damage;
        if (Health > 0)
            view.TakeDamage();
        else
            view.Kill();
    }

    public void Update(float deltaTime, Creature target)
    {
        if (view.Killed)
        {
            Drawer.Unregister(view);
            return;
        }
        UpdateDirection(target);
        if (view.CanAttack)
            Attack(target);
        if (view.CanMove)
            MoveToPlayer(deltaTime);
        currentColdown -= deltaTime;
        view.Update(deltaTime);
    }

    private void MoveToPlayer(float deltaTime)
    {
        if (isIdle)
        {
            view.Idle();
        }
        else
        {
            Position += Direction * Speed * deltaTime;
            view.Run();
        }
    }

    private void UpdateDirection(Creature target)
    {
        var direction = target.Position - Position;
        isIdle = direction.Length() <= view.Radius;
        direction.Normalize();
        Direction = direction;
    }
}