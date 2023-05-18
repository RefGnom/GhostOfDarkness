using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace game;

internal class Room : IDrawable
{
    public readonly Tile[,] Tiles;
    public readonly Vector2 Position;
    public readonly Vector2 Size;

    public Vector2 Center => Position + Size / 2;
    public List<Enemy> Enemies { get; private set; }

    public Room(Tile[,] tiles, Vector2 position, int tileSize)
    {
        Tiles = tiles;
        Position = position;
        Size = new Vector2(tiles.GetLength(0), tiles.GetLength(1)) * tileSize;
        Enemies = new();
    }

    public void Update(float deltaTime, Creature player)
    {
        for (int i = 0; i < Enemies.Count; i++)
        {
            Enemies[i].Update(deltaTime, player);
            if (Enemies[i].IsDead)
            {
                DeleteEnemy(i);
                i--;
            }
        }
    }

    public void Draw(SpriteBatch spriteBatch, float scale)
    {
        for (int column = 0; column < Tiles.GetLength(0); column++)
        {
            for (int row = 0; row < Tiles.GetLength(1); row++)
            {
                Tiles[column, row]?.Entity?.Draw(spriteBatch, scale);
            }
        }
    }

    public void CreateEnemy(Enemy enemy)
    {
        Creature.Create(enemy);
        Enemies.Add(enemy);
    }

    private void DeleteEnemy(int index)
    {
        Creature.DeleteFromLocation(Enemies[index]);
        Enemies.RemoveAt(index);
    }
}