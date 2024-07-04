using System;
using Core.DependencyInjection;
using Game.Creatures.CreatureStates;
using Game.Interfaces;
using Game.Managers;
using Microsoft.Xna.Framework;

namespace Game.Creatures;

[DiIgnore]
public abstract class Creature : ICollisionable
{
    public Vector2 Position { get; protected set; }
    public Vector2 Direction { get; protected set; }
    public float Damage { get; set; }
    public float Health { get; set; }
    public float AttackDistance { get; protected set; }
    public float Speed { get; set; }
    public Rectangle Hitbox { get; protected set; }
    public bool CanCollide => true;
    public CreatureStatesController View { get; protected set; }
    public bool IsDead => Health <= 0;
    public bool CanDelete { get; protected set; }
    public string Tag { get; set; }

    private readonly float maxHealth;
    protected readonly float Cooldown;
    protected float CurrentCooldown;

    public Creature(Vector2 position, float speed, float health, float damage, float attackDistance, float cooldown)
    {
        Position = position;
        Direction = new Vector2(-1, 0.0000001f);
        Speed = speed;
        Health = health;
        maxHealth = health;
        Damage = damage;
        AttackDistance = attackDistance;
        this.Cooldown = cooldown;
    }

    public void Heal(int percent)
    {
        if (percent > 100 || percent < 0)
        {
            throw new ArgumentException("percent it should be between 0 and 100");
        }

        var takenDamage = maxHealth - Health;
        Health += takenDamage / 100 * percent;
    }

    public abstract void TakeDamage(float damage);

    public void Move(Vector2 movementVector)
    {
        GameManager.Instance.CollisionDetector.Unregister(this);
        Position += movementVector;
        GameManager.Instance.CollisionDetector.Register(this);
    }

    public static void Create(Creature creature)
    {
        GameManager.Instance.CollisionDetector.Register(creature);
        GameManager.Instance.Drawer.Register(creature.View);
    }

    public static void Delete(Creature creature)
    {
        DeleteHitbox(creature);
        DeleteFromLocation(creature);
    }

    public static void DeleteHitbox(Creature creature)
    {
        GameManager.Instance.CollisionDetector.Unregister(creature);
    }

    public static void DeleteFromLocation(Creature creature)
    {
        GameManager.Instance.Drawer.Unregister(creature.View);
        GameManager.Instance.CollisionDetector.Unregister(creature);
    }
}