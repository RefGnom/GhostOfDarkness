﻿using game.Managers;
using Microsoft.Xna.Framework;

namespace game.Creatures;

internal class MeleeEnemy : Enemy
{
    public MeleeEnemy(Vector2 position, float speed)
        : base(new MeleeEnemyView(), position, speed, 100, 10, 40, 1.5f)
    {
        Hitbox = HitboxManager.MeleeEnemy;
    }
}