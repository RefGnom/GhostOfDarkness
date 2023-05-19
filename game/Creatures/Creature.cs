using Microsoft.Xna.Framework;

namespace game;

internal abstract class Creature : ICollisionable
{
    public Vector2 Position { get; protected set; }
    public Vector2 Direction { get; protected set; }
    public float Damage { get; protected set; }
    public float Health { get; protected set; }
    public float AttackDistance { get; protected set; }
    public float Speed { get; protected set; }
    public Rectangle Hitbox { get; protected set; }
    public bool CanCollide => true;
    public CreatureView View { get; protected set; }
    public bool IsDead { get; protected set; }

    protected readonly float cooldown;
    protected float currentColdown;

    public Creature(Vector2 position, float speed, float health, float damage, float attackDistance, float cooldown)
    {
        Position = position;
        Speed = speed;
        Health = health;
        Damage = damage;
        AttackDistance = attackDistance;
        this.cooldown = cooldown;
    }

    public abstract void TakeDamage(float damage);

    public static void Create(Creature creature)
    {
        GameManager.Instance.CollisionDetecter.Register(creature);
        GameManager.Instance.Drawer.Register(creature.View);
    }

    public static void Delete(Creature creature)
    {
        DeleteHitbox(creature);
        DeleteFromLocation(creature);
    }

    public static void DeleteHitbox(Creature creature)
    {
        GameManager.Instance.CollisionDetecter.Unregister(creature);
    }

    public static void DeleteFromLocation(Creature creature)
    {
        GameManager.Instance.Drawer.Unregister(creature.View);
    }
}