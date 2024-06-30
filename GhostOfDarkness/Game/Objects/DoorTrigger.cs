using Game.Interfaces;
using Microsoft.Xna.Framework;

namespace game;

internal class DoorTrigger : ITrigger
{
    public Rectangle Hitbox { get; private set; }

    public DoorTrigger(Rectangle hitbox)
    {
        Hitbox = hitbox;
    }

    public bool Triggered(ICollisionable collisionable)
    {
        return collisionable.Collision(Hitbox);
    }

    public bool Triggered(Vector2 position)
    {
        return Hitbox.Contains(position);
    }
}