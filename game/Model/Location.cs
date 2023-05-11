using game.Creatures;
using game.Interfaces;
using game.Managers;
using game.Objects;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace game.Model;

internal class Location : IDrawable
{
    private Tile[,] tiles;
    private int tileSize = 32;

    public List<Enemy> Enemies { get; private set; }
    public int Width { get; private set; }
    public int Height { get; private set; }

    public Location(int width, int height)
    {
        tiles = new Tile[width / tileSize, height / tileSize];
        Width = width;
        Height = height;
        Enemies = new();
        Initialize();
        GameManager.Instance.Drawer.Register(this);
    }

    private void Initialize()
    {
        for (int column = 0; column < tiles.GetLength(0); column++)
        {
            for (int row = 0; row < tiles.GetLength(1); row++)
            {
                tiles[column, row] = new Tile(tileSize * column, tileSize * row, tileSize);
            }
        }
    }

    public static Location GetStartLocation()
    {
        throw new NotImplementedException();
    }

    public static Location GetLocation(int width, int height)
    {
        return new Location(width, height);
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

    public void Draw(SpriteBatch spriteBatch)
    {
        for (int column = 0; column < tiles.GetLength(0); column++)
        {
            for (int row = 0; row < tiles.GetLength(1); row++)
            {
                tiles[column, row].Entity?.Draw(spriteBatch);
            }
        }
    }

    public void CreateEnemy(Enemy enemy)
    {
        Creature.Create(enemy);
        Enemies.Add(enemy);
    }

    public void DeleteEnemy(int index)
    {
        Creature.DeleteFromLocation(Enemies[index]);
        Enemies.RemoveAt(index);
    }
}