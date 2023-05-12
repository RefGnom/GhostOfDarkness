using game.Interfaces;
using game.Managers;
using game.Objects;
using Microsoft.Xna.Framework;

namespace game.Model;

internal class Tile
{
    private IEntity entity;
    public readonly Vector2 Position;
    public readonly int Size;
    public IEntity Entity {
        get => entity;
        set
        {
            if (entity is not null)
                GameManager.Instance.CollisionDetecter.Unregister(entity);
            entity = value;
            GameManager.Instance.CollisionDetecter.Register(entity);
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
}