using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace game
{
    internal class Player
    {
        private Vector2 position;
        private float playerSpeed;
        private int width;
        private int height;
        private float cooldown = 0;
        private float currentColdown;

        public List<Bullet> Bullets { get; private set; }
        public Vector2 Position => position;

        public readonly Dictionary<Directions, bool> EnableMoves = new()
        {
            [Directions.Up] = false,
            [Directions.Down] = false,
            [Directions.Left] = false,
            [Directions.Right] = false
        };

        public Player(Vector2 position, float speed)
        {
            this.position = position;
            playerSpeed = speed;
            Bullets = new();
        }

        public void SetPosition(Vector2 position)
        {
            this.position = position;
        }

        public void SetSize(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public void Shoot(Vector2 direction)
        {
            if (currentColdown < 0)
            {
                Bullets.Add(new Bullet(position, direction));
                currentColdown = cooldown;
            }
        }

        public void Update(float deltaTime, int locationWidth, int locationHeight)
        {
            var moveVector = Vector2.Zero;
            if (EnableMoves[Directions.Up])
            {
                EnableMoves[Directions.Up] = false;
                moveVector.Y--;
            }
            if (EnableMoves[Directions.Down])
            {
                EnableMoves[Directions.Down] = false;
                moveVector.Y++;
            }
            if (EnableMoves[Directions.Left])
            {
                EnableMoves[Directions.Left] = false;
                moveVector.X--;
            }
            if (EnableMoves[Directions.Right])
            {
                EnableMoves[Directions.Right] = false;
                moveVector.X++;
            }
            if (moveVector != Vector2.Zero)
            {
                moveVector.Normalize();
                position += moveVector * playerSpeed * deltaTime;
            }
            CheckOnOutBounds(locationWidth, locationHeight);

            for (int i = 0; i < Bullets.Count; i++)
            {
                Bullets[i].Update(deltaTime);
                if (Bullets[i].IsDead)
                    Bullets.RemoveAt(i);
            }
            currentColdown -= deltaTime;
        }

        public void CheckOnOutBounds(int locationWidth, int locationHeight)
        {
            var position = Position;
            var rightBound = locationWidth - width / 2;
            var leftBound = width / 2;
            var bottomBound = locationHeight - height / 2;
            var upperBound = height / 2;

            if (position.X > rightBound)
                position.X = rightBound;
            else if (position.X < leftBound)
                position.X = leftBound;

            if (position.Y > bottomBound)
                position.Y = bottomBound;
            else if (position.Y < upperBound)
                position.Y = upperBound;
        }
    }
}
