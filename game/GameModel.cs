using Microsoft.Xna.Framework;

namespace game
{
    internal class GameModel
    {
        public Location Location { get; private set; }
        public Player Player { get; private set; }

        public GameModel(Vector2 playerPosition)
        {
            Location = Location.GetStartLocation();
            Player = new(playerPosition, 100f);
        }

        public void SetLocation()
        {
            Location = Location.GetLocation();
        }

        public void CheckPlayerOnOutBounds(int playerWidth, int playerHeight)
        {
            var position = Player.Position;
            var rightBound = Location.Width - playerWidth / 2;
            var leftBound = playerWidth / 2;
            var bottomBound = Location.Height - playerHeight / 2;
            var upperBound = playerHeight / 2;

            if (position.X > rightBound)
                Player.SetPositionX(rightBound);
            else if (position.X < leftBound)
                Player.SetPositionX(leftBound);

            if (position.Y > bottomBound)
                Player.SetPositionY(bottomBound);
            else if (position.Y < upperBound)
                Player.SetPositionY(upperBound);
        }
    }
}
