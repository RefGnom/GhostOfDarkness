using System;
using System.Collections.Generic;
using Core.DependencyInjection;
using game;
using Game.Controllers;
using Game.Managers;
using Game.Objects;
using Microsoft.Xna.Framework;

namespace Game.Creatures;

[DiIgnore]
internal class Player : Creature
{
    private readonly HealthBar healthBar;

    private List<Bullet> Bullets { get; set; }
    public int DeltaX { get; set; }
    public int DeltaY { get; set; }
    public bool IsCollide { get; set; }
    public Func<bool> Attack { get; set; }

    private static CollisionDetector CollisionDetector => GameManager.Instance.CollisionDetector;

    public Player(Vector2 position, float speed, float health, float damage, float cooldown) : base(position, speed, health, damage, 100, cooldown)
    {
        View = new PlayerView(this);
        Bullets = [];
        healthBar = new HealthBar(health);
        Hitbox = HitboxManager.Player;
        IsCollide = true;
        Create(this);
    }

    public void SetPosition(Vector2 position)
    {
        Position = position;
    }

    public void Update(float deltaTime)
    {
        if (View.Killed)
        {
            Delete(this);
            return;
        }

        UpdateDirection();
        if (View.CanAttack && Attack is not null && Attack.Invoke())
        {
            Shoot();
        }

        if (View.CanMove)
        {
            Move(deltaTime);
        }

        UpdateBullets(deltaTime);
        CurrentCooldown -= deltaTime;
        healthBar.SetHealth(Health);
        View.Update(deltaTime);
    }

    private void Shoot()
    {
        if (CurrentCooldown <= 0)
        {
            // 30?? (Чтобы пули вылетали вне хитбокса)
            var bullet = new Bullet(Position + Direction * 30, Direction);
            Bullets.Add(bullet);
            GameManager.Instance.Drawer.Register(bullet);
            CurrentCooldown = Cooldown;
            View.SetStateAttack();
        }
    }

    // Переместить в класс Gun
    private void UpdateBullets(float deltaTime)
    {
        for (var i = 0; i < Bullets.Count; i++)
        {
            Bullets[i].Update(deltaTime);
            var obj = CollisionDetector.CollisionWithObjects(Bullets[i]);
            if (obj is Enemy enemy)
            {
                enemy.TakeDamage(Damage);
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
        var movementVector =
            IsCollide ? CollisionDetector.GetMovementVectorWithoutCollision(this, DeltaX, DeltaY, Speed, deltaTime) : new Vector2(DeltaX, DeltaY);

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
        {
            View.SetStateTakeDamage();
        }
        else
        {
            View.SetStateDead();
        }
    }
}