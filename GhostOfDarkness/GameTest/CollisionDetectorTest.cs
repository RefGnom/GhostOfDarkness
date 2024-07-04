using FluentAssertions;
using Game.Interfaces;
using Game.Structures;
using Microsoft.Xna.Framework;

namespace GameTest;

public class CollisionDetectorTest
{
    [Test]
    public void CountElementsTest()
    {
        const int count = 10;
        var boundary = new Rectangle(0, 0, 64, 64);
        var quadtree = new QuadTree(boundary);
        var collisionables = new List<TestCreature>();

        for (var i = 0; i < count; i++)
        {
            var collisionable = new TestCreature(Vector2.Zero, true);
            quadtree.Insert(collisionable);
            collisionables.Add(collisionable);
        }

        for (var i = 0; i < count; i++)
        {
            quadtree.Remove(collisionables[i]);
        }

        quadtree.Count.Should().Be(0);
    }

    [Test]
    public void DeleteUnusefulQuadrantsTest()
    {
        const int count = 10;
        var boundary = new Rectangle(0, 0, 64, 64);
        var quadtree = new QuadTree(boundary);
        var collisionables = new List<TestCreature>();

        for (var i = 0; i < count; i++)
        {
            var collisionable = new TestCreature(Vector2.Zero, true);
            quadtree.Insert(collisionable);
            collisionables.Add(collisionable);
        }

        for (var i = 0; i < count; i++)
        {
            quadtree.Remove(collisionables[i]);
        }

        quadtree.Divided.Should().BeFalse();
    }

    [Test]
    public void NotCollideEqualsItemTest()
    {
        var boundary = new Rectangle(0, 0, 64, 64);
        var quadtree = new QuadTree(boundary);

        var collisionable = new TestCreature(Vector2.Zero, true);
        quadtree.Insert(collisionable);

        quadtree.IsIntersectedWithItems(collisionable).Should().BeFalse();
    }

    [Test]
    public void CorrectIfEmpty()
    {
        var boundary = new Rectangle(0, 0, 64, 64);
        var quadtree = new QuadTree(boundary);

        var collisionable = new TestCreature(Vector2.Zero, true);
        quadtree.IsIntersectedWithItems(collisionable).Should().BeFalse();
    }

    [Test]
    public void CollideWithItem()
    {
        var boundary = new Rectangle(0, 0, 64, 64);
        var quadtree = new QuadTree(boundary);

        var collisionable1 = new TestCreature(Vector2.Zero, true);
        quadtree.Insert(collisionable1);
        var collisionable2 = new TestCreature(Vector2.Zero, true);

        quadtree.IsIntersectedWithItems(collisionable2).Should().BeTrue();
    }

    [Test]
    public void NotCollideIfCreatureCantCollide()
    {
        var boundary = new Rectangle(0, 0, 64, 64);
        var quadtree = new QuadTree(boundary);

        var collisionable1 = new TestCreature(Vector2.Zero, false);
        quadtree.Insert(collisionable1);
        var collisionable2 = new TestCreature(Vector2.Zero, true);
        quadtree.IsIntersectedWithItems(collisionable2).Should().BeFalse();


        collisionable1 = new TestCreature(Vector2.Zero, true);
        quadtree.Insert(collisionable1);
        collisionable2 = new TestCreature(Vector2.Zero, false);
        quadtree.IsIntersectedWithItems(collisionable2).Should().BeFalse();


        collisionable1 = new TestCreature(Vector2.Zero, false);
        quadtree.Insert(collisionable1);
        collisionable2 = new TestCreature(Vector2.Zero, false);
        quadtree.IsIntersectedWithItems(collisionable2).Should().BeFalse();
    }

    [Test]
    public void CollideWithMovementVector()
    {
        var boundary = new Rectangle(0, 0, 64, 64);
        var quadtree = new QuadTree(boundary);

        var collisionable1 = new TestCreature(Vector2.UnitX * 4, true);
        quadtree.Insert(collisionable1);

        var collisionable2 = new TestCreature(Vector2.Zero, true);
        var movement = Vector2.Zero;
        quadtree.IsIntersectedWithItems(collisionable2, movement).Should().BeFalse();

        movement.Y = 1;
        quadtree.IsIntersectedWithItems(collisionable2, movement).Should().BeFalse();

        movement.X = 1;
        quadtree.IsIntersectedWithItems(collisionable2, movement).Should().BeTrue();
    }

    private class TestCreature : ICollisionable
    {
        public Rectangle Hitbox => new Rectangle(0, 0, 4, 4);
        public Vector2 Position { get; init; }
        public bool CanCollide { get; init; }

        public TestCreature(Vector2 position, bool canCollide)
        {
            Position = position;
            CanCollide = canCollide;
        }
    }
}