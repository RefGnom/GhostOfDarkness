﻿using Microsoft.Xna.Framework;

namespace game;

internal interface ICollisionable
{
    public Rectangle Hitbox { get; }
    public Vector2 Position { get; }
    public bool CanCollide { get; }

    public bool Collision(ICollisionable collisionable)
    {
        var hitbox1 = Hitbox.Shift(Position);
        var hitbox2 = collisionable.Hitbox.Shift(collisionable.Position);
        return hitbox1.Intersects(hitbox2);
    }

    public bool Collision(ICollisionable collisionable, Vector2 movementVector)
    {
        var hitbox1 = Hitbox.Shift(Position + movementVector);
        var hitbox2 = collisionable.Hitbox.Shift(collisionable.Position);
        return hitbox1.Intersects(hitbox2);
    }
}