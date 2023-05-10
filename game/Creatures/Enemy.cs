using game.Interfaces;
using game.Managers;
using Microsoft.Xna.Framework;
using System;

namespace game.Creatures;

internal abstract class Enemy : Creature, IEnemy
{
    private bool isIdle;
    public bool IsDead { get; private set; }

    private EnemyView view;

    protected event Action OnUpdate;

    public Enemy(Vector2 position, float speed, float health, float damage, float attackDistance, float cooldown)
        : base(position, speed, health, damage, attackDistance, cooldown)
    {
    }

    protected void Initialize(EnemyView view)
    {
        this.view = view;
        GameManager.Instance.Drawer.Register(view);
        GameManager.Instance.EnemiesManager.Add(this);
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
        view.Update(deltaTime);
        if (view.CanDelete)
        {
            GameManager.Instance.Drawer.Unregister(view);
            IsDead = true;
            return;
        }
        if (view.Killed)
        {
            GameManager.Instance.EnemiesManager.Remove(this);
            return;
        }
        UpdateDirection(target);
        if (view.CanAttack)
            Attack(target);

        if (!isIdle)
        {
            if (view.CanMove)
                MoveToPlayer(deltaTime);
            view.Run();
        }
        else
        {
            view.Idle();
        }

        OnUpdate?.Invoke();
        currentColdown -= deltaTime;
    }

    protected virtual void MoveToPlayer(float deltaTime)
    {
        Position += Direction * Speed * deltaTime;
    }

    private void UpdateDirection(Creature target)
    {
        var direction = target.Position - Position;
        isIdle = direction.Length() <= view.Radius;
        direction.Normalize();
        Direction = direction;
    }
}