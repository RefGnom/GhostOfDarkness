﻿using Game.Managers;
using Microsoft.Xna.Framework;

namespace Game.Creatures;

internal class MeleeEnemy : Enemy
{
    private const float scale = 1.5f;

    public MeleeEnemy(Vector2 position, float speed, float health = 100, float damage = 10)
        : base(new MeleeEnemyView(scale), position, speed, health, damage, 45, 1.5f)
    {
        Hitbox = HitboxManager.MeleeEnemy;
        var view = View as EnemyView;
        view.SetModel(this);
    }
}