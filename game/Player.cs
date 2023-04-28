using Microsoft.Xna.Framework;

namespace game
{
    internal class Player
    {
        private Vector2 position;
        private float playerSpeed;

        public Vector2 Position => position;

        public Player(Vector2 position, float speed)
        {
            this.position = position;
            this.playerSpeed = speed;
        }

        public void SetPosition(Vector2 position)
        {
            this.position = position;
        }

        public void SetPositionX(float x)
        {
            position.X = x;
        }

        public void SetPositionY(float y)
        {
            position.Y = y;
        }

        public void Move(Directions direction, float deltaTime)
        {
            if (direction == Directions.Up)
                position.Y -= playerSpeed * deltaTime;

            if (direction == Directions.Down)
                position.Y += playerSpeed * deltaTime;

            if (direction == Directions.Left)
                position.X -= playerSpeed * deltaTime;

            if (direction == Directions.Right)
                position.X += playerSpeed * deltaTime;
        }
    }
}
