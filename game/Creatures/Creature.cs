using game.Interfaces;
using Microsoft.Xna.Framework;

namespace game.Creatures
{
    public class Creature : IEntity
    {
        public Vector2 Position { get; protected set; }
        public float Damage { get; protected set; }
        public float Health { get; protected set; }
        public bool IsDead => Health <= 0;
        public float Speed { get; protected set; }

        public Creature(Vector2 position, float speed)
        {
            Position = position;
            Speed = speed;
            Health = 100;
            Damage = 10;
        }

        public virtual void Attack(Creature target)
        {
            target.TakeDamage(Damage);
        }

        public void TakeDamage(float damage)
        {
            Health -= damage;
        }
    }
}
