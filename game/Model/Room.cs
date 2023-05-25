using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace game;

internal class Room : IDrawable
{
    private readonly Tile[,] tiles;
    private readonly int tileSize;
    private readonly Vector2 position;
    private readonly Vector2 size;
    private readonly List<Enemy> enemies;

    public Tile[,] Tiles => tiles;
    public int TileSize => tileSize;
    public Vector2 Center => position + size / 2;

    public Room(Tile[,] tiles, Vector2 position, int tileSize)
    {
        this.tiles = tiles;
        this.tileSize = tileSize;
        this.position = position;
        size = new Vector2(tiles.GetLength(0), tiles.GetLength(1)) * tileSize;
        enemies = new();
    }

    public void Delete()
    {
        while (enemies.Count > 0)
            DeleteEnemy(0);
    }

    public void Update(float deltaTime, Creature player)
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].Update(deltaTime, player);
            if (enemies[i].IsDead)
            {
                DeleteEnemy(i);
                i--;
            }
        }
    }

    public void Draw(SpriteBatch spriteBatch, float scale)
    {
        for (int column = 0; column < tiles.GetLength(0); column++)
        {
            for (int row = 0; row < tiles.GetLength(1); row++)
            {
                tiles[column, row]?.Entity?.Draw(spriteBatch, scale);
            }
        }
    }

    public void CreateEnemy(Enemy enemy)
    {
        enemy.GetMovementVector += GetMovementVector;
        Creature.Create(enemy);
        enemies.Add(enemy);
    }

    public Point ConvertToCoordinatePoint(Point point, Func<float, int> round)
    {
        return ConvertToCoordinatePoint(point.ToVector2(), round);
    }

    public Point ConvertToCoordinatePoint(Vector2 vector, Func<float, int> round)
    {
        var result = (vector - position) / tileSize;
        return new Point(round(result.X), round(result.Y));
    }

    public Point ConvertToCoordinatePoint(Rectangle rectanle, Func<float, int> round)
    {
        return ConvertToCoordinatePoint(rectanle.Center, round);
    }

    public bool IsPossiblePosition(Rectangle hitbox)
    {
        var (x1, y1) = ConvertToCoordinatePoint(hitbox.Location.ToVector2(), x => (int)x);
        var (x2, y2) = ConvertToCoordinatePoint(new Vector2(hitbox.Right, hitbox.Bottom), x => (int)x);

        return IsPossiblePosition(x1, y1)
            && IsPossiblePosition(x1, y2)
            && IsPossiblePosition(x2, y1)
            && IsPossiblePosition(x2, y2);
    }

    public bool IsPossiblePosition(Point point)
    {
        return IsPossiblePosition(point.X, point.Y);
    }

    public bool IsPossiblePosition(int x, int y)
    {
        return InBounds(x, y) && tiles[x, y].Entity is not ICollisionable;
    }

    public bool InBounds(Point point)
    {
        return InBounds(point.X, point.Y);
    }

    public bool InBounds(int x, int y)
    {
        return x >= 0 && x < tiles.GetLength(0)
            && y >= 0 && y < tiles.GetLength(1);
    }

    private (Vector2, bool) GetMovementVector(Creature instance, Rectangle target, float deltaTime)
    {
        var hitbox = instance.Hitbox.Shift(instance.Position);
        var path = AStarRectangle.FindPath(this, hitbox, target, 20);

        if (path is null)
            return (Vector2.Zero, false);

        if (path.Count < 2)
            return (Vector2.Zero, true);

        var offset = path[1].GetOffset(hitbox);
        var movementVector = offset;
        movementVector.Normalize();
        return (movementVector, true);
    }

    private void DeleteEnemy(int index)
    {
        Creature.DeleteFromLocation(enemies[index]);
        enemies.RemoveAt(index);
    }
}