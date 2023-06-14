using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game;

internal class Bullet : IDrawable, ICollisionable
{
    private readonly Vector2 direction;
    private readonly float speed = 800;
    private float lifetime = 1.4f;

    public Vector2 Position { get; private set; }
    public Rectangle Hitbox => HitboxManager.Bullet;
    public bool CanCollide => true;
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

    public void Draw(SpriteBatch spriteBatch, float scale)
    {
        spriteBatch.Draw(Textures.Bullet, Position, Color.Orange);
        if (Settings.ShowHitboxes)
            HitboxManager.DrawHitbox(spriteBatch, Position, HitboxManager.Bullet, Vector2.Zero);
    }
}