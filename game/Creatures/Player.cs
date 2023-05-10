using game.Enums;
using game.Managers;
using game.View;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace game.Creatures;

internal class Player : Creature
{
    private int width;
    private int height;
    protected PlayerView view;

    public List<Bullet> Bullets { get; private set; }

    public readonly Dictionary<Directions, bool> EnableDirections = new()
    {
        [Directions.Up] = false,
        [Directions.Down] = false,
        [Directions.Left] = false,
        [Directions.Right] = false
    };

    public Player(Vector2 position, float speed, float cooldown) : base(position, speed, 100, 20, 100, cooldown)
    {
        view = new(this);
        Bullets = new();
        Drawer.Register(view);
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

    protected void Shoot()
    {
        if (currentColdown <= 0)
        {
            var bullet = new Bullet(Position, Direction);
            Bullets.Add(bullet);
            Drawer.Register(bullet);
            currentColdown = cooldown;
            view.Attack();
        }
    }

    public void Update(float deltaTime, int locationWidth, int locationHeight)
    {
        if (view.Killed)
        {
            Drawer.Unregister(view);
            return;
        }
        UpdateDirection();
        if (view.CanAttack && MouseManager.LeftButtomClicked())
            Shoot();
        if (view.CanMove)
            Move(deltaTime);
        CheckOnOutBounds(locationWidth, locationHeight);
        UpdateBullets(deltaTime);
        currentColdown -= deltaTime;
        view.Update(deltaTime);
    }

    protected void UpdateBullets(float deltaTime)
    {
        for (int i = 0; i < Bullets.Count; i++)
        {
            Bullets[i].Update(deltaTime);
            if (Bullets[i].IsDead)
            {
                Drawer.Unregister(Bullets[i]);
                Bullets.RemoveAt(i);
                i--;
            }
        }
    }

    private void UpdateDirection()
    {
        var direction = Mouse.GetState().Position.ToVector2() - Position;
        direction.Normalize();
        Direction = direction;
    }

    private void Move(float deltaTime)
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
            view.Run();
        }
        else
        {
            view.Idle();
        }
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

    public override void TakeDamage(float damage)
    {
        Health -= damage;
        if (Health > 0)
            view.TakeDamage();
        else
            view.Kill();
    }
}