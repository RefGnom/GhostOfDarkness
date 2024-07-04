using Game.Interfaces;
using Game.Structures;
using Microsoft.Xna.Framework;

namespace Game.Managers;

internal class CollisionDetector
{
    private QuadTree quadTree;

    public void CreateQuadTree(int width, int height)
    {
        if (quadTree is not null)
        {
            return;
        }

        quadTree = new QuadTree(new Rectangle(0, 0, width, height));
        GameManager.Instance.Drawer.Register(quadTree);
    }

    public void Register(ICollisionable item)
    {
        quadTree.Insert(item);
    }

    public void Unregister(ICollisionable item)
    {
        quadTree.Remove(item);
    }

    public ICollisionable CollisionWithObjects(ICollisionable item) => quadTree.GetIntersectWithItems(item);

    public bool CollisionWithObjects(ICollisionable item, Vector2 movementVector) => quadTree.IsIntersectedWithItems(item, movementVector);

    public Vector2 GetMovementVectorWithoutCollision(ICollisionable item, float deltaX, float deltaY, float speed, float deltaTime)
    {
        var moveVector = Vector2.Zero;
        moveVector.X = deltaX;
        if (CollisionWithObjects(item, moveVector * speed * deltaTime))
        {
            moveVector.X = 0;
        }

        moveVector.Y = deltaY;
        if (CollisionWithObjects(item, moveVector * speed * deltaTime))
        {
            moveVector.Y = 0;
        }

        return moveVector;
    }
}