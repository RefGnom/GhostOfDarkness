using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace game;

internal class Room : IDrawable
{
    private readonly Tile[,] tiles;
    private readonly Vector2 position;
    private readonly Vector2 size;
    private readonly List<Enemy> enemies;

    public Vector2 Center => position + size / 2;

    public Room(Tile[,] tiles, Vector2 position, int tileSize)
    {
        this.tiles = tiles;
        this.position = position;
        size = new Vector2(tiles.GetLength(0), tiles.GetLength(1)) * tileSize;
        enemies = new();
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

    private Vector2 GetMovementVector(Creature instance, Creature target, float deltaTime)
    {
        var myHitbox = instance.Hitbox.Shift(instance.Position);
        var targetHitbox = target.Hitbox.Shift(target.Position);
        var path = PathFinder.GetMovementVector(tiles, myHitbox, targetHitbox, 20)?.ToList();
        if (path is null)
            return Vector2.Zero;
        if (path.Count >= 2)
        {
            var movementVector = path[^2].GetOffset(myHitbox);
            movementVector.Normalize();
            return movementVector;
        }
        return instance.Direction;
    }

    private void DeleteEnemy(int index)
    {
        Creature.DeleteFromLocation(enemies[index]);
        enemies.RemoveAt(index);
    }
}