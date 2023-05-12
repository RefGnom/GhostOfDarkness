﻿using game.Enums;
using game.Managers;
using game.Objects;
using game.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace game.Creatures;

internal class Player : Creature
{
    private HealthBar healthBar;

    public List<Bullet> Bullets { get; private set; }
    public float MaxHealth { get; private set; }

    public readonly Dictionary<Directions, int> EnableDirections = new()
    {
        [Directions.Up] = 0,
        [Directions.Down] = 0,
        [Directions.Left] = 0,
        [Directions.Right] = 0
    };

    public Player(Vector2 position, float speed, float health, float cooldown) : base(position, speed, health, 20, 100, cooldown)
    {
        MaxHealth = health;
        View = new PlayerView(this);
        Bullets = new();
        healthBar = new HealthBar(this);
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
        if (View.CanAttack && MouseManager.LeftButtomClicked())
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
        var speed = Speed * deltaTime;
        var moveVector = Vector2.Zero;
        moveVector.X = EnableDirections[Directions.Left] + EnableDirections[Directions.Right];
        if (GameManager.Instance.CollisionDetecter.CollisionWithbjects(this, moveVector * speed))
            moveVector.X = 0;
        moveVector.Y = EnableDirections[Directions.Up] + EnableDirections[Directions.Down];
        if (GameManager.Instance.CollisionDetecter.CollisionWithbjects(this, moveVector * speed))
            moveVector.Y = 0;

        if (moveVector != Vector2.Zero)
        {
            moveVector.Normalize();
            Position += moveVector * speed;
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
        if (Health > 0)
            View.TakeDamage();
        else
            View.Kill();
    }
}