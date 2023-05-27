using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace game;

internal class PlayerView : CreatureStatesController
{
    private readonly Animator animator = AnimatorsCreator.GetAnimator("Player");
    private static readonly Dictionary<string, int> animations = new()
    {
        ["idle"] = 0,
        ["run"] = 0,
        ["attack"] = 0,
        ["take damage"] = 0,
        ["dead"] = 0,
    };

    private readonly Player model;
    private float rotation;

    public PlayerView(Player model)
    {
        this.model = model;
        SetStates(CreateStates());
    }

    public List<CreatureState> CreateStates()
    {
        var idle = new IdleState(this);
        idle.OnStarted = () =>
        {
            animator.SetAnimation(animations["idle"]);
            return animator.GetAnimationTime(animations["idle"]);
        };
        var run = new RunState(this);
        run.OnStarted = () =>
        {
            animator.SetAnimation(animations["run"]);
            return animator.GetAnimationTime(animations["run"]);
        };
        var fight = new FightState(this);
        fight.OnStarted = idle.OnStarted;
        var attack = new AttackState(this);
        attack.OnStarted = () =>
        {
            animator.SetAnimation(animations["attack"]);
            return animator.GetAnimationTime(animations["attack"]);
        };
        var takeDamage = new TakeDamageState(this);
        takeDamage.OnStarted = () =>
        {
            animator.SetAnimation(animations["take damage"]);
            return animator.GetAnimationTime(animations["take damage"]);
        };
        var dead = new DeadState(this);
        dead.OnStarted = () =>
        {
            animator.SetAnimation(animations["dead"], false);
            return animator.GetAnimationTime(animations["dead"]) * 20;
        };

        return new List<CreatureState>()
        {
            idle,
            run,
            fight,
            attack,
            takeDamage,
            dead
        };
    }

    public override void Update(float deltaTime)
    {
        var angle = -Math.Atan(model.Direction.X / model.Direction.Y);
        if (model.Direction.Y > 0)
            angle += Math.PI;
        rotation = (float)angle;
        base.Update(deltaTime);
    }

    public override void Draw(SpriteBatch spriteBatch, float scale)
    {
        var flip = SpriteEffects.None;
        animator.Draw(model.Position, spriteBatch, flip, rotation, Layers.Creatures, 1);
        if (Settings.ShowHitboxes)
            HitboxManager.DrawHitbox(spriteBatch, model.Position, HitboxManager.Player, animator.Origin);
    }
}