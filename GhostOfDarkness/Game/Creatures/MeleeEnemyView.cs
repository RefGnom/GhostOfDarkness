using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Game.Graphics;
using Game.Managers;
using Game.View;

namespace game;

internal class MeleeEnemyView : EnemyView
{
    private readonly Animator animator = AnimatorsCreator.GetAnimator("Melee Enemy");
    private static readonly Dictionary<string, int> animations = new()
    {
        ["idle"] = 0,
        ["run"] = 1,
        ["attack"] = 2,
        ["take damage"] = 3,
        ["dead"] = 4,
    };

    public MeleeEnemyView(float scale)
    {
        scaleFactor = scale;
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

    public override void Draw(ISpriteBatch spriteBatch, float scale)
    {
        var flip = model.Direction.X < 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
        animator.Draw(model.Position, spriteBatch, flip, Layers.Creatures, scaleFactor);
        var origin = new Vector2(model.Hitbox.Width / 2, model.Hitbox.Height / 2);
        if (Settings.ShowHitboxes)
            HitboxManager.DrawHitbox(spriteBatch, model.Position, model.Hitbox, origin);
    }
}