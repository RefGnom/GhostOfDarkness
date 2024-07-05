using Microsoft.Xna.Framework;

namespace Game.Interfaces;

public interface ITrigger
{
    public Rectangle Hitbox { get; }

    public bool Triggered(ICollisionable collisionable);

    public bool Triggered(Vector2 position);
}