using game.Interfaces;
using game.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game
{
    internal class Bullet : IEntity, Interfaces.IDrawable
    {
        private Vector2 direction;
        private float speed = 600;
        private float lifetime = 1.2f;

        public Vector2 Position { get; private set; }
        public bool IsDead => lifetime <= 0;

        public Bullet(Vector2 position, Vector2 direction)
        {
            Position = position;
            this.direction = direction;
        }

        public void Update(float deltaTime)
        {
            Position += direction * speed * deltaTime;
            lifetime -= deltaTime;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TexturesManager.Bullet, Position, Color.White);
        }
    }
}