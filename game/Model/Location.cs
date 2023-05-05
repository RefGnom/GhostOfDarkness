using game.Enums;
using Microsoft.Xna.Framework;

namespace game.Model
{
    internal class Location
    {
        private Tile[,] tiles;

        public int Width { get; set; }
        public int Height { get; set; }

        public static Location GetStartLocation()
        {
            return new Location();
        }

        public static Location GetLocation()
        {
            return new Location();
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
}
