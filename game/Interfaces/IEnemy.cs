using Microsoft.Xna.Framework;

namespace game.Interfaces
{
    public interface IEnemy
    {
        public bool IsDead { get; }
        public abstract Vector2 Position { get; }
        public abstract Vector2 Direction { get; }

        public abstract void Update(float deltaTime, Vector2 target);
    }
}
