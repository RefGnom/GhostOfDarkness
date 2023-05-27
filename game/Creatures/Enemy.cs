using Microsoft.Xna.Framework;
using System;

namespace game;

internal abstract class Enemy : Creature
{
    private bool isIdle;
    private bool hitboxDeleted;

    public new bool IsDead => View.CanDelete;
    private static CollisionDetecter CollisionDetecter => GameManager.Instance.CollisionDetecter;

    public event Func<Creature, Rectangle, float, Vector2> GetMovementVector;

    public Enemy(EnemyView view, Vector2 position, float speed, float health, float damage, float attackDistance, float cooldown)
        : base(position, speed, health, damage, attackDistance, cooldown)
    {
        View = view;
    }

    private bool TryAttack(Creature target)
    {
        var distance = Vector2.Distance(Position, target.Position);
        if (distance <= AttackDistance && currentColdown <= 0)
        {
            target.TakeDamage(Damage);
            currentColdown = cooldown;
            return true;
        }
        return false;
    }

    public override void TakeDamage(float damage)
    {
        Health -= damage;
        if (Health > 0)
            View.SetStateTakeDamage();
        else
            View.SetStateDead();
    }

    public void Update(float deltaTime, Creature target)
    {
        View.Update(deltaTime);
        if (hitboxDeleted)
            return;
        if (View.Killed)
        {
            DeleteHitbox(this);
            hitboxDeleted = true;
            return;
        }
        UpdateDirection(target);
        if (View.CanAttack && TryAttack(target))
            View.SetStateAttack();

        if (View.CanMove)
            isIdle = !TryMoveToPlayer(deltaTime, target);

        if (isIdle)
            View.SetStateIdle();
        else
            View.SetStateRun();
        currentColdown -= deltaTime;
    }

    protected virtual bool TryMoveToPlayer(float deltaTime, Creature target)
    {
        var distance = Vector2.Distance(Position, target.Position);
        if (distance <= AttackDistance)
            return false;
        var movementVector = GetMovementVector.Invoke(this, target.Hitbox.Shift(target.Position), deltaTime);
        if (movementVector == Vector2.Zero)
            return false;
        movementVector = CollisionDetecter.GetMovementVectorWithoutCollision(this, movementVector.X, movementVector.Y, Speed, deltaTime);
        if (movementVector == Vector2.Zero)
            return false;
        Move(movementVector * Speed * deltaTime);
        return true;
    }

    private void UpdateDirection(Creature target)
    {
        var direction = target.Position - Position;
        isIdle = direction.Length() <= AttackDistance * 0.9;
        direction.Normalize();
        Direction = direction;
    }
}