using game.Managers;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace game
{
    internal class GameModel
    {
        public Location Location { get; private set; }
        public Player Player { get; private set; }
        public List<Bullet> Bullets { get; private set; }
        private bool IsPaused => GameManager.Instance.PauseManager.IsPaused;

        public GameModel(Vector2 playerPosition)
        {
            Location = Location.GetStartLocation();
            Player = new(playerPosition, 130f);
            Bullets = new List<Bullet>();
        }

        public void SetLocation()
        {
            Location = Location.GetLocation();
        }

        public void Update(float deltaTime)
        {
            if (IsPaused)
                return;
            Player.Update(deltaTime, Location.Width, Location.Height);
        }
    }
}
