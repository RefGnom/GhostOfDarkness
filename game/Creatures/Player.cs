using game.CreatureStates;
using game.Enums;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace game.Creatures
{
    internal class Player : Creature
    {
        private int width;
        private int height;
        private float cooldown;
        private float currentColdown;

        public List<Bullet> Bullets { get; private set; }

        public readonly Dictionary<Directions, bool> EnableDirections = new()
        {
            [Directions.Up] = false,
            [Directions.Down] = false,
            [Directions.Left] = false,
            [Directions.Right] = false
        };

        public Player(Vector2 position, float speed, float cooldown) : base(position, speed)
        {
            this.cooldown = cooldown;
            Bullets = new();
        }

        public void SetPosition(Vector2 position)
        {
            Position = position;
        }

        public void SetSize(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public void Shoot(Vector2 direction)
        {
            if (currentColdown <= 0)
            {
                Bullets.Add(new Bullet(Position, direction));
                currentColdown = cooldown;
            }
        }

        public void Update(float deltaTime, int locationWidth, int locationHeight)
        {
            var moveVector = Vector2.Zero;
            if (EnableDirections[Directions.Up])
            {
                EnableDirections[Directions.Up] = false;
                moveVector.Y--;
            }
            if (EnableDirections[Directions.Down])
            {
                EnableDirections[Directions.Down] = false;
                moveVector.Y++;
            }
            if (EnableDirections[Directions.Left])
            {
                EnableDirections[Directions.Left] = false;
                moveVector.X--;
            }
            if (EnableDirections[Directions.Right])
            {
                EnableDirections[Directions.Right] = false;
                moveVector.X++;
            }
            if (moveVector != Vector2.Zero)
            {
                moveVector.Normalize();
                Position += moveVector * Speed * deltaTime;
            }
            CheckOnOutBounds(locationWidth, locationHeight);

            for (int i = 0; i < Bullets.Count; i++)
            {
                Bullets[i].Update(deltaTime);
                if (Bullets[i].IsDead)
                {
                    Bullets.RemoveAt(i);
                    i--;
                }
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