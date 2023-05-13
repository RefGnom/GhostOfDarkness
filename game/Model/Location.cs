﻿using game.Algorithms;
using game.Creatures;
using game.Extensions;
using game.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace game.Model;

internal class Location : Interfaces.IDrawable
{
    private Tile[,] tiles;
    private readonly int tileSize = 32;

    public List<Enemy> Enemies { get; private set; }
    public int Width { get; private set; }
    public int Height { get; private set; }

    public Location(int width, int height)
    {
        Width = (int)Math.Ceiling(width / (double)tileSize);
        Height = (int)Math.Ceiling(height / (double)tileSize);
        tiles = new Tile[Width, Height];
        Enemies = new();
        Initialize();
        GameManager.Instance.Drawer.Register(this);
    }

    private void Initialize()
    {
        for (int column = 0; column < Width; column++)
        {
            for (int row = 0; row < Height; row++)
            {
                tiles[column, row] = new Tile(tileSize * column, tileSize * row, tileSize);
                if (row == 0 || row == Height - 1 || column == 0 || column == Width - 1)
                    tiles[column, row].SetWall();
            }
        }
        for (int i = 0; i < 10; i++)
        {
            tiles[6 + i, 5].SetWall();
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
            var movementVector = PathsFinder.GetPathToPlayer(tiles, Enemies[i].Position, player.Position)
                .ToMovementVetors()
                .FirstOrDefault(Vector2.Zero);
            movementVector.Normalize();
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