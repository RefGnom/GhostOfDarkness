using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace game;

internal class Player : Creature
{
    private readonly HealthBar healthBar;

    public List<Bullet> Bullets { get; private set; }

    public readonly Dictionary<Directions, int> EnableDirections = new()
    {
        [Directions.Up] = 0,
        [Directions.Down] = 0,
        [Directions.Left] = 0,
        [Directions.Right] = 0
    };

    private CollisionDetecter CollisionDetecter => GameManager.Instance.CollisionDetecter;

    public Player(Vector2 position, float speed, float health, float cooldown) : base(position, speed, health, 20, 100, cooldown)
    {
        View = new PlayerView(this);
        Bullets = new();
        healthBar = new HealthBar(health);
        Hitbox = HitboxManager.Player;
        Create(this);
        //Speed = 1000;
    }

    public void SetPosition(Vector2 position)
    {
        Position = position;
    }

    protected void Shoot()
    {
        if (currentColdown <= 0)
        {
            // 20?? (Чтобы пули вылетали вне хитбокса)
            var bullet = new Bullet(Position + Direction * 20, Direction);
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
        if (View.CanAttack && MouseController.LeftButtonClicked())
            Shoot();
        if (View.CanMove)
            Move(deltaTime);
        UpdateBullets(deltaTime);
        currentColdown -= deltaTime;
        View.Update(deltaTime);
        DisableDirections();
    }

    // Переместить в класс Gun
    protected void UpdateBullets(float deltaTime)
    {
        for (int i = 0; i < Bullets.Count; i++)
        {
            Bullets[i].Update(deltaTime);
            var obj = CollisionDetecter.CollisionWithbjects(Bullets[i]);
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
        var direction = MouseController.WorldPosition - Position;
        direction.Normalize();
        Direction = direction;
    }

    private void Move(float deltaTime)
    {
        var deltaX = EnableDirections[Directions.Left] + EnableDirections[Directions.Right];
        var deltaY = EnableDirections[Directions.Up] + EnableDirections[Directions.Down];
        var movementVector = CollisionDetecter.GetMovementVectorWithoutCollision(this, deltaX, deltaY, Speed, deltaTime);

        if (movementVector != Vector2.Zero)
        {
            movementVector.Normalize();
            Position += movementVector * Speed * deltaTime;
            View.Idle();
        }
        else
        {
            View.Idle();
        }
    }

    private void DisableDirections()
    {
        foreach (var direction in EnableDirections.Keys)
        {
            EnableDirections[direction] = 0;
        }
    }

    public override void TakeDamage(float damage)
    {
        Health -= damage;
        healthBar.SetHealth(Health);
        if (Health > 0)
            View.TakeDamage();
        else
            View.Kill();
    }
}