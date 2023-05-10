﻿using Microsoft.Xna.Framework;

namespace game.Interfaces;

internal interface ICollisionable
{
    public Rectangle Hitbox { get; }

    public bool Collision(ICollisionable collisionable)
    {
        return Hitbox.Intersects(collisionable.Hitbox);
    }
}