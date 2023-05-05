using game.Creatures;
using game.Interfaces;
using game.View;
using Microsoft.Xna.Framework;

namespace game.Enemies
{
    internal class MeleeEnemy : Creature, IEnemy
    {
        public float AttackDistance;
        public Vector2 Direction { get; private set; }
        //public Animator Animator { get; private set; }

        public MeleeEnemy(Vector2 position, float speed) : base(position, speed)
        {
            //Animator = AnimatorsCreator.GetAnimator("Melee Enemy", )
        }

        public override void Attack(Creature target)
        {
            var distance = Vector2.Distance(Position, target.Position);
            if (distance <= AttackDistance)
                target.TakeDamage(Damage);
        }

        public void Update(float deltaTime, Vector2 target)
        {
            var direction = target - Position;
            direction.Normalize();
            Direction = direction;
            Position += direction * Speed * deltaTime;
        }
    }
}