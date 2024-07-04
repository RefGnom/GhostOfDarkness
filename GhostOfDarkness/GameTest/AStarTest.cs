using FluentAssertions;
using Game.Algorithms;
using Game.Model;
using Microsoft.Xna.Framework;

namespace GameTest;

public class AStartTest : TestBase
{
    private const int size = 32;

    private static Room CreateRoom(int width, int height)
    {
        var tiles = new Tile[width, height];
        for (var i = 0; i < width; i++)
        {
            for (var j = 0; j < height; j++)
            {
                tiles[i, j] = new Tile(new Vector2(size * i, size * j), size);
                tiles[i, j].SetFloor();
            }
        }
        return new Room(tiles, Vector2.Zero, size);
    }

    [Test]
    public void DirectionDownTest()
    {
        var room = CreateRoom(1, 2);
        var expected = new List<Point>()
        {
            new Point(0, 0),
            new Point(0, 1)
        };
        var actual = AStarPoint.FindPath(room, new Point(0, 0), new Point(0, 1));
        TestPath(expected, actual);
    }

    [Test]
    public void DirectionLeftTest()
    {
        var room = CreateRoom(2, 1);
        var expected = new List<Point>()
        {
            new Point(1, 0),
            new Point(0, 0)
        };
        var actual = AStarPoint.FindPath(room, new Point(1, 0), new Point(0, 0));
        TestPath(expected, actual);
    }

    [Test]
    public void DiagonalPathBetterThanOrthogonal()
    {
        const int testSize = 10;
        var room = CreateRoom(testSize, testSize);
        var expected = new List<Point>();
        for (var i = 0; i < testSize; i++)
        {
            expected.Add(new Point(i, i));
        }

        var actual = AStarPoint.FindPath(room, new Point(0, 0), new Point(testSize - 1, testSize - 1));
        TestPath(expected, actual);
    }

    [Test]
    public void PathNotFound()
    {
        var room = CreateRoom(4, 4);
        var actual = AStarPoint.FindPath(room, new Point(0, 0), new Point(-1, -1));
        actual.Should().BeNull();
    }

    private static void TestPath(IReadOnlyList<Point> expected, IReadOnlyList<Point> actual)
    {
        actual.Should().NotBeNull();
        actual.Should().HaveCount(expected.Count);

        for (var i = 0; i < expected.Count; i++)
        {
            actual[i].Should().Be(expected[i]);
        }
    }
}