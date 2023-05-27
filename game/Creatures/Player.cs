using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace game;

internal class Player : Creature
{
    private readonly HealthBar healthBar;

    public List<Bullet> Bullets { get; private set; }
    public int DeltaX { get; set; }
    public int DeltaY { get; set; }
    public bool IsCollide { get; set; }
    public Func<bool> Attack { get; set; }

    private static CollisionDetecter CollisionDetecter => GameManager.Instance.CollisionDetecter;

    public Player(Vector2 position, float speed, float health, float damage, float cooldown) : base(position, speed, health, damage, 100, cooldown)
    {
        View = new PlayerView(this);
        Bullets = new();
        healthBar = new HealthBar(health);
        Hitbox = HitboxManager.Player;
        IsCollide = true;
        Create(this);
    }

    public void SetPosition(Vector2 position)
    {
        Position = position;
    }

    protected void Shoot()
    {
        if (currentColdown <= 0)
        {
            // 25?? (Чтобы пули вылетали вне хитбокса)
            var bullet = new Bullet(Position + Direction * 25, Direction);
            Bullets.Add(bullet);
            GameManager.Instance.Drawer.Register(bullet);
            currentColdown = cooldown;
            View.SetStateAttack();
        }
    }

    public void Update(float deltaTime)
    {
        if (View.Killed)
        {
            IsDead = true;
            Delete(this);
            return;
        }
        UpdateDirection();
        if (View.CanAttack && Attack is not null && Attack.Invoke())
            Shoot();
        if (View.CanMove)
            Move(deltaTime);
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
        Vector2 movementVector;
        if (IsCollide)
            movementVector = CollisionDetecter.GetMovementVectorWithoutCollision(this, DeltaX, DeltaY, Speed, deltaTime);
        else
            movementVector = new Vector2(DeltaX, DeltaY);
        DeltaX = 0;
        DeltaY = 0;

        if (movementVector != Vector2.Zero)
        {
            movementVector.Normalize();
            Move(movementVector * Speed * deltaTime);
            View.SetStateIdle();
        }
        else
        {
            View.SetStateIdle();
        }
    }

    public override void TakeDamage(float damage)
    {
        Health -= damage;
        healthBar.SetHealth(Health);
        if (Health > 0)
            View.SetStateTakeDamage();
        else
            View.SetStateDead();
    }
}