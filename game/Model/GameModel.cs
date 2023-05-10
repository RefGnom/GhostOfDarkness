using game.Creatures;
using game.Managers;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace game.Model;

internal class GameModel
{
    public Location Location { get; private set; }
    public Player Player { get; private set; }
    public List<Bullet> Bullets { get; private set; }
    private bool IsPaused => GameManager.Instance.PauseManager.IsPaused;

    public GameModel(Vector2 playerPosition, int width, int height)
    {
        Location = Location.GetLocation(width, height);
        Location.Enemies.Add(new MeleeEnemy(Vector2.Zero, 60));
        Player = new(playerPosition, 130f, 0.3f);
        Bullets = new List<Bullet>();
    }

    public void SetLocation(int width, int height)
    {
        Location = Location.GetLocation(width, height);
    }

    public void Update(float deltaTime)
    {
        if (IsPaused)
            return;
        Location.Update(deltaTime, Player);
        Player.Update(deltaTime, Location.Width, Location.Height);
    }
}
