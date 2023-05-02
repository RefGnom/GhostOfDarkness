using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace game
{
    internal class Sprite
    {
        private readonly Texture2D texture;
        private float rotation;

        public readonly Vector2 Origin;
        public int Scale { get; set; }
        public int Width => texture.Width;
        public int Height => texture.Height;

        public Sprite(Texture2D texture)
        {
            this.texture = texture;
            Origin = new Vector2(Width / 2, Height / 2);
            Scale = 1;
        }

        public void UpdateDirection(Vector2 target, Vector2 position)
        {
            var direction = target - position;
            var angle = -Math.Atan(direction.X / direction.Y);
            if (direction.Y > 0)
                angle += Math.PI;
            rotation = (float)angle;
        }

        public void Rotate(float angle)
        {
            rotation += angle;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            spriteBatch.Draw(texture, position, null, Color.White, rotation, Origin, Scale, SpriteEffects.None, 0);
        }
    }
}
