using Microsoft.Xna.Framework;

namespace game;

internal abstract class Enemy : Creature
{
    private bool isIdle;
    private bool hitboxDeleted;
    public new bool IsDead => View.CanDelete;
    private CollisionDetecter CollisionDetecter => GameManager.Instance.CollisionDetecter;

    public Enemy(EnemyView view, Vector2 position, float speed, float health, float damage, float attackDistance, float cooldown)
        : base(position, speed, health, damage, attackDistance, cooldown)
    {
        View = view;
        view.PositionChanged += () => Position;
        view.DirectionChanged += () => Direction;
    }

    private void Attack(Creature target)
    {
        var distance = Vector2.Distance(Position, target.Position);
        if (distance <= AttackDistance && currentColdown <= 0)
        {
            target.TakeDamage(Damage);
            currentColdown = cooldown;
            View.Attack();
        }
    }

    public override void TakeDamage(float damage)
    {
        Health -= damage;
        if (Health > 0)
            View.TakeDamage();
        else
            View.Kill();
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
        if (View.CanAttack)
            Attack(target);

        if (View.CanMove)
            MoveToPlayer(deltaTime, target);

        if (isIdle)
            View.Idle();
        else
            View.Run();
        currentColdown -= deltaTime;
    }

    protected virtual void MoveToPlayer(float deltaTime, Creature target)
    {
        //var path = PathFinder.GetPath(Position, target.Position, 20);
        //if (path == null)
        //{
        //    isIdle = true;
        //    return;
        //}
        //// Работает некорректно
        //var movementVector = path.ToMovementVectors().First();
        //movementVector.Normalize();
        //movementVector = CollisionDetecter.GetMovementVectorWithoutCollision(this, movementVector.X, movementVector.Y, Speed, deltaTime);
        //Position += movementVector * Speed * deltaTime;
    }

    private void UpdateDirection(Creature target)
    {
        var direction = target.Position - Position;
        isIdle = direction.Length() <= AttackDistance * 0.9;
        direction.Normalize();
        Direction = direction;
    }
}