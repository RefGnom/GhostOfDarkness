﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game;

internal class Bullet : IDrawable, ICollisionable
{
    private Vector2 direction;
    private float speed = 800;
    private float lifetime = 1.2f;

    public Vector2 Position { get; private set; }
    public Rectangle Hitbox => HitboxManager.Bullet;
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
        spriteBatch.Draw(TexturesManager.Bullet, Position, Color.White);
        if (Settings.ShowHitboxes)
            HitboxManager.DrawHitbox(spriteBatch, Position, HitboxManager.Bullet, Vector2.Zero);
    }
}