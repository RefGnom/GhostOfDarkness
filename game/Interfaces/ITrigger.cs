using Microsoft.Xna.Framework;

namespace game;

internal interface ITrigger
{
    public Rectangle Hitbox { get; }

    public bool Triggered(ICollisionable collisionable);

    public bool Triggered(Vector2 position);
}