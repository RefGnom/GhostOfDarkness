﻿using Microsoft.Xna.Framework;
using NUnit.Framework;
using System.Collections.Generic;

namespace game.Tests;

[TestFixture]
internal class AStartTests
{
    private static readonly int size = 32;

    private static Tile[,] CreateTiles(int width, int height)
    {
        var tiles = new Tile[width, height];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                tiles[i, j] = new Tile(new Vector2(size * i, size * j), size);
            }
        }
        return tiles;
    }

    [Test]
    public void DirectionDownTest()
    {
        var tiles = CreateTiles(1, 2);
        var expected = new List<Point>()
        {
            new Point(0, 0),
            new Point(0, 1)
        };
        var actual = AStar.FindPath(tiles, new Point(0, 0), new Point(0, 1));
        TestPath(expected, actual);
    }

    [Test]
    public void DirectionLeftTest()
    {
        var tiles = CreateTiles(2, 1);
        var expected = new List<Point>()
        {
            new Point(1, 0),
            new Point(0, 0)
        };
        var actual = AStar.FindPath(tiles, new Point(1, 0), new Point(0, 0));
        TestPath(expected, actual);
    }

    [Test]
    public void DiagonalPathBetterThanOrthogonal()
    {
        var size = 10;
        var tiles = CreateTiles(size, size);
        var expected = new List<Point>();
        for (int i = 0; i < size; i++)
            expected.Add(new Point(i, i));
        var actual = AStar.FindPath(tiles, new Point(0, 0), new Point(size - 1, size - 1));
        TestPath(expected, actual);
    }

    [Test]
    public void PathNotFound()
    {
        var tiles = CreateTiles(4, 4);
        var actual = AStar.FindPath(tiles, new Point(0, 0), new Point(-1, -1));
        Assert.IsNull(actual);
    }

    private static void TestPath(List<Point> expected, List<Point> actual)
    {
        Assert.IsNotNull(actual);
        Assert.AreEqual(expected.Count, actual.Count);
        for (int i = 0; i < expected.Count; i++)
        {
            Assert.AreEqual(expected[i], actual[i]);
        }
    }
}