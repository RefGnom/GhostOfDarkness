using game.Creatures;
using game.Interfaces;
using game.Managers;
using game.View;
using Microsoft.Xna.Framework;

namespace game.Enemies
{
    // Создать Enum States и менять состояние MeleeEnemy,
    // затем обрабатывать в отдельном методе через словарь соответствий State -> номер анимации
    // или просто в методах Attack, Kill, TakeDamage, Update сразу устанавливать номер анимации?

    internal class MeleeEnemy : Creature, IEnemy
    {
        public float AttackDistance;
        public Vector2 Direction { get; private set; }
        public Animator Animator { get; private set; }

        public MeleeEnemy(Vector2 position, float speed) : base(position, speed)
        {
            Animator = AnimatorsCreator.GetAnimator("Melee Enemy", TexturesManager.MeleeEnemy);
        }

        public override void Attack(Creature target)
        {
            // Установить State Attack
            // Animator.SetAnimation(2); Или сделать через Enum?
            var distance = Vector2.Distance(Position, target.Position);
            if (distance <= AttackDistance)
                target.TakeDamage(Damage);
        }

        public void Update(float deltaTime, Vector2 target)
        {
            // Установить State либо Idle, либо Run
            var direction = target - Position;
            direction.Normalize();
            Direction = direction;
            Position += direction * Speed * deltaTime;
        }

        public override void Kill()
        {
            // Установить State Dead
        }
    }
}