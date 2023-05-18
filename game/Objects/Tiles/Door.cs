using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game;

internal class Door : IDrawable, ICollisionable
{
    public Vector2 Position { get; private set; }
    public Rectangle Hitbox => HitboxManager.Wall;

    public Door(Vector2 position)
    {
        Position = position;
    }

    public void Draw(SpriteBatch spriteBatch, float scale)
    {

    }
}