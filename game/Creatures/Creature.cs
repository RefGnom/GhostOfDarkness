using game.CreatureStates;
using Microsoft.Xna.Framework;

namespace game.Creatures;

internal abstract class Creature : StatesController
{
    public Vector2 Position { get; protected set; }
    public float Damage { get; protected set; }
    public float Health { get; protected set; }
    public float AttackDistance { get; protected set; }
    public float Speed { get; protected set; }

    public Creature(Vector2 position, float speed, float health, float damage, float attackDistance)
    {
        Position = position;
        Speed = speed;
        Health = health;
        Damage = damage;
        AttackDistance = attackDistance;
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        SetHealth(Health);
    }

    public void Attack(Creature target)
    {
        var distance = Vector2.Distance(Position, target.Position);
        if (distance <= AttackDistance)
        {
            target.TakeDamage(Damage);
        }
        StateAttack();
    }
}