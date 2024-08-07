﻿using System;
using Game.ContentLoaders;
using Game.Graphics;
using Game.Interfaces;
using Game.Managers;
using Game.Service;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using IDrawable = Game.Interfaces.IDrawable;

namespace Game.Model.Tiles;

internal class Door : IDrawable, ICollisionable
{
    private readonly Texture2D closedTexture;
    private readonly Texture2D openedTexture;
    private Texture2D currentTexture;
    private readonly Floor floor;
    private readonly Vector2 origin;
    private readonly float scaleFactor = 0.5f;
    private readonly float rotation;

    public Vector2 Position { get; private set; }
    public Vector2 Center => Position + origin * scaleFactor;
    public Rectangle Hitbox => HitboxManager.Wall;
    public bool CanCollide { get; private set; }
    public bool Vertical { get; private set; }

    public Door(Vector2 position, bool vertical = false)
    {
        closedTexture = Textures.Door;
        openedTexture = Textures.OpenedDoor;
        floor = new Floor(position);
        origin = new Vector2(closedTexture.Width / 2, closedTexture.Height / 2);
            Position = position;
            Vertical = vertical;
        if (vertical)
            rotation = (float)Math.PI / 2;
        Close();
    }

    public void Draw(ISpriteBatch spriteBatch, float scale)
    {
        spriteBatch.Draw(currentTexture, Center, null, Color.White, rotation, origin, scaleFactor, SpriteEffects.None, Layers.Tiles);
        floor.Draw(spriteBatch, scale);
        if (Settings.ShowHitboxes)
            HitboxManager.DrawHitbox(spriteBatch, Position, Hitbox, Vector2.Zero);
    }

    public void Close()
    {
        currentTexture = closedTexture;
        CanCollide = true;
    }

    public void Open()
    {
        currentTexture = openedTexture;
        CanCollide = false;
    }
}