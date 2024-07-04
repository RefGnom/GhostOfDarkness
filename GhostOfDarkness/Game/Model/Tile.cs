using game;
using Game.Interfaces;
using Game.Managers;
using Microsoft.Xna.Framework;
using IDrawable = Game.Interfaces.IDrawable;

namespace Game.Model;

public class Tile
{
    private IDrawable entity;
    public readonly Vector2 Position;
    public readonly int Size;

    public IDrawable Entity
    {
        get => entity;
        set
        {
            if (entity is ICollisionable oldCollisionable)
            {
                GameManager.Instance.CollisionDetector.Unregister(oldCollisionable);
            }

            if (value is ICollisionable collisionable)
            {
                GameManager.Instance.CollisionDetector.Register(collisionable);
            }

            entity = value;
        }
    }

    public Tile(Vector2 position, int size)
    {
        Position = position;
        Size = size;
    }

    public Tile(IDrawable entity, Vector2 position, int size)
    {
        Entity = entity;
        Position = position;
        Size = size;
    }

    public void SetWall()
    {
        Entity = new Wall(Position);
    }

    public void SetFloor(string name = null)
    {
        Entity = name is null ? new Floor(Position) : new Floor(Position, name);
    }
}