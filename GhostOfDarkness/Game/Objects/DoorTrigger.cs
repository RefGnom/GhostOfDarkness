using Core.DependencyInjection;
using game;
using Game.Interfaces;
using Microsoft.Xna.Framework;

namespace Game.Objects;

[DiIgnore]
internal class DoorTrigger : ITrigger
{
    public Rectangle Hitbox { get; private set; }

    public DoorTrigger(Rectangle hitbox)
    {
        Hitbox = hitbox;
    }

    public bool Triggered(ICollisionable collisionable) => collisionable.Collision(Hitbox);

    public bool Triggered(Vector2 position) => Hitbox.Contains(position);
}