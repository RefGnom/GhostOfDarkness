using System.Collections.Generic;
using game;
using Game.Structures;
using Microsoft.Xna.Framework;
using NUnit.Framework;

namespace Game.Tests;

[TestFixture]
internal class CollisionDetecterTests
{
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

    [Test]
    public void CountElementsTest()
    {
        var count = 10;
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
        Assert.AreEqual(0, quadtree.Count);
    }

    [Test]
    public void DeleteUnusefulQuadrantsTest()
    {
        var count = 10;
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
        Assert.AreEqual(false, quadtree.Divided);
    }

    [Test]
    public void NotCollideEqualsItemTest()
    {
        var boundary = new Rectangle(0, 0, 64, 64);
        var quadtree = new QuadTree(boundary);

        var collisionable = new TestCreature(Vector2.Zero, true);
        quadtree.Insert(collisionable);

        Assert.AreEqual(false, quadtree.IsIntersectedWithItems(collisionable));
    }

    [Test]
    public void CorrectIfEmpty()
    {
        var boundary = new Rectangle(0, 0, 64, 64);
        var quadtree = new QuadTree(boundary);

        var collisionable = new TestCreature(Vector2.Zero, true);
        Assert.AreEqual(false, quadtree.IsIntersectedWithItems(collisionable));
    }

    [Test]
    public void CollideWithItem()
    {
        var boundary = new Rectangle(0, 0, 64, 64);
        var quadtree = new QuadTree(boundary);

        var collisionable1 = new TestCreature(Vector2.Zero, true);
        quadtree.Insert(collisionable1);
        var collisionable2 = new TestCreature(Vector2.Zero, true);
        Assert.AreEqual(true, quadtree.IsIntersectedWithItems(collisionable2));
    }

    [Test]
    public void NotCollideIfCreatureCantCollide()
    {
        var boundary = new Rectangle(0, 0, 64, 64);
        var quadtree = new QuadTree(boundary);

        var collisionable1 = new TestCreature(Vector2.Zero, false);
        quadtree.Insert(collisionable1);
        var collisionable2 = new TestCreature(Vector2.Zero, true);
        Assert.AreEqual(false, quadtree.IsIntersectedWithItems(collisionable2));


        collisionable1 = new TestCreature(Vector2.Zero, true);
        quadtree.Insert(collisionable1);
        collisionable2 = new TestCreature(Vector2.Zero, false);
        Assert.AreEqual(false, quadtree.IsIntersectedWithItems(collisionable2));


        collisionable1 = new TestCreature(Vector2.Zero, false);
        quadtree.Insert(collisionable1);
        collisionable2 = new TestCreature(Vector2.Zero, false);
        Assert.AreEqual(false, quadtree.IsIntersectedWithItems(collisionable2));
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
        Assert.AreEqual(false, quadtree.IsIntersectedWithItems(collisionable2, movement));

        movement.Y = 1;
        Assert.AreEqual(false, quadtree.IsIntersectedWithItems(collisionable2, movement));

        movement.X = 1;
        Assert.AreEqual(true, quadtree.IsIntersectedWithItems(collisionable2, movement));
    }
}