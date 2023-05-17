using Microsoft.Xna.Framework;

namespace game;

internal class Tile
{
    private IDrawable entity;
    public readonly Vector2 Position;
    public readonly int Size;

    public IDrawable Entity {
        get => entity;
        set
        {
            if (entity is not null && entity is ICollisionable)
                GameManager.Instance.CollisionDetecter.Unregister(entity as ICollisionable);
            if (value is ICollisionable)
                GameManager.Instance.CollisionDetecter.Register(value as ICollisionable);
            entity = value;
        }
    }

    public Tile(int x, int y, int size)
    {
        Position = new Vector2(x, y);
        Size = size;
    }

    public void SetWall()
    {
        Entity = new Wall(Position);
    }

    public void SetFloor(string name = null)
    {
        if (name is null)
            Entity = new Floor(Position);
        else
            Entity = new Floor(Position, name);
    }
}