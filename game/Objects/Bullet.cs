using game.Interfaces;
using game.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game.Objects
{
    internal class Bullet : IEntity, Interfaces.IDrawable, ICollisionable
    {
        private Vector2 direction;
        private float speed = 600;
        private float lifetime = 1.2f;

        public Vector2 Position { get; private set; }
        private Point size;
        Rectangle ICollisionable.Hitbox => new Rectangle(Position.ToPoint(), size);
        public bool IsDead => lifetime <= 0;

        public Bullet(Vector2 position, Vector2 direction)
        {
            Position = position;
            this.direction = direction;
            size = new(10, 10);
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