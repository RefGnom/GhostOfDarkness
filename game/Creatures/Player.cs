using game.Enums;
using game.Managers;
using game.Objects;
using game.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace game.Creatures;

internal class Player : Creature
{
    private int width;
    private int height;
    private HealthBar healthBar;

    public List<Bullet> Bullets { get; private set; }
    public float MaxHealth { get; private set; }

    public readonly Dictionary<Directions, bool> EnableDirections = new()
    {
        [Directions.Up] = false,
        [Directions.Down] = false,
        [Directions.Left] = false,
        [Directions.Right] = false
    };

    public Player(Vector2 position, float speed, float health, float cooldown) : base(position, speed, health, 20, 100, cooldown)
    {
        MaxHealth = health;
        View = new PlayerView(this);
        Bullets = new();
        healthBar = new HealthBar(this);
        Create(this);
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
            GameManager.Instance.Drawer.Register(bullet);
            currentColdown = cooldown;
            View.Attack();
        }
    }

    public void Update(float deltaTime, int locationWidth, int locationHeight)
    {
        if (View.Killed)
        {
            Delete(this);
            return;
        }
        UpdateDirection();
        if (View.CanAttack && MouseManager.LeftButtomClicked())
            Shoot();
        if (View.CanMove)
            Move(deltaTime);
        CheckOnOutBounds(locationWidth, locationHeight);
        UpdateBullets(deltaTime);
        currentColdown -= deltaTime;
        View.Update(deltaTime);
    }

    // Переместить в класс Gun
    protected void UpdateBullets(float deltaTime)
    {
        for (int i = 0; i < Bullets.Count; i++)
        {
            Bullets[i].Update(deltaTime);
            var obj = GameManager.Instance.CollisionDetecter.CollisionWithbjects(Bullets[i]);
            if (obj is Enemy)
            {
                var enemy = obj as Enemy;
                enemy?.TakeDamage(Damage);
            }
            if (Bullets[i].IsDead || obj is not null)
            {
                GameManager.Instance.Drawer.Unregister(Bullets[i]);
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
            View.Run();
        }
        else
        {
            View.Idle();
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
            View.TakeDamage();
        else
            View.Kill();
    }
}