using game.CreatureStates;
using game.Interfaces;
using game.View;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace game.Creatures;

internal class PlayerView : CreatureStatesController, IDrawable
{
    private static readonly Dictionary<string, int> animations = new Dictionary<string, int>()
    {
        ["idle"] = 0,
        ["run"] = 0,
        ["attack"] = 0,
        ["take damage"] = 0,
        ["dead"] = 0,
    };

    private Player model;
    private float rotation;

    public PlayerView(Player model) : base(AnimatorsCreator.GetAnimator("Player"), animations)
    {
        this.model = model;
    }

    public void Attack() => SetStateAttack();

    public void TakeDamage() => SetStateTakeDamage();

    public void Kill() => SetStateDead();

    public void Idle() => SetStateIdle();

    public void Run() => SetStateRun();

    public new void Update(float deltaTime)
    {
        var angle = -Math.Atan(model.Direction.X / model.Direction.Y);
        if (model.Direction.Y > 0)
            angle += Math.PI;
        rotation = (float)angle;

        base.Update(deltaTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        var flip = SpriteEffects.None;
        animator.Draw(model.Position, spriteBatch, flip, rotation);
    }
}