using game.Creatures;
using game.Enums;
using game.Interfaces;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace game.Model;

internal class Location
{
    private Tile[,] tiles;

    public List<IEnemy> Enemies { get; private set; }
    public int Width { get; private set; }
    public int Height { get; private set; }

    public Location(int width, int height)
    {
        tiles = new Tile[Width, Height];
        Width = width;
        Height = height;
        Enemies = new();
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
                Enemies.RemoveAt(i);
                i--;
            }
        }
    }

    public Vector2 GetPositionObjectInBounds(Vector2 position, int objectWidth, int objectHeigth)
    {
        var rightBound = Width - objectWidth / 2;
        var leftBound = objectWidth / 2;
        var bottomBound = Height - objectHeigth / 2;
        var upperBound = objectHeigth / 2;

        if (position.X > rightBound)
            position.X = rightBound;
        else if (position.X < leftBound)
            position.X = leftBound;

        if (position.Y > bottomBound)
            position.Y = bottomBound;
        else if (position.Y < upperBound)
            position.Y = upperBound;
        return position;
    }
}
