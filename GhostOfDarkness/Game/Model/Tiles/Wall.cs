﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game;

internal class Wall : IDrawable, ICollisionable
{
    public Vector2 Position { get; private set; }
    public Rectangle Hitbox => HitboxManager.Wall;
    public bool CanCollide => true;

    public Wall(Vector2 position)
    {
        Position = position;
    }

    public void Draw(SpriteBatch spriteBatch, float scale)
    {
        spriteBatch.Draw(Textures.Wall, Position, null, Color.White, 0, Vector2.Zero, 0.5f, SpriteEffects.None, Layers.Tiles);
        if (Settings.ShowHitboxes)
            HitboxManager.DrawHitbox(spriteBatch, Position, HitboxManager.Wall, Vector2.Zero);
    }
}