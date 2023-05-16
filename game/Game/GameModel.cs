using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace game;

internal class GameModel
{
    public Location Location { get; private set; }
    public Player Player { get; private set; }
    public List<Bullet> Bullets { get; private set; }
    
    public GameModel(Vector2 playerPosition, int width, int height)
    {
        Location = Location.GetLocation(width, height);
        Location.CreateEnemy(new MeleeEnemy(new Vector2(60, 60), 60));
        Player = new(playerPosition, 130f, 100, 0.3f);
        Bullets = new List<Bullet>();
    }

    public void SetLocation(int width, int height)
    {
        Location = Location.GetLocation(width, height);
    }

    public void Update(float deltaTime)
    {
        Location.Update(deltaTime, Player);
        Player.Update(deltaTime);
    }
}
