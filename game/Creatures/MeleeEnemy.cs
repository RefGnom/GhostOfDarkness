using game.Creatures;
using Microsoft.Xna.Framework;

namespace game.Enemies
{
    internal class MeleeEnemy : Creature
    {
        public float AttackDistance;

        public MeleeEnemy(Vector2 position, float speed) : base(position, speed) { }

        public override void Attack(Creature target)
        {
            var distance = Vector2.Distance(Position, target.Position);
            if (distance <= AttackDistance)
                target.TakeDamage(Damage);
        }
    }
}