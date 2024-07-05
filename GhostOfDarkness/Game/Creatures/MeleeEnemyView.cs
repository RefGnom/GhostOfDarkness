using System.Collections.Generic;
using Game.Creatures.CreatureStates;
using Game.Graphics;
using Game.Managers;
using Game.Service;
using Game.View;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Creatures;

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
        ScaleFactor = scale;
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
        var flip = Model.Direction.X < 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
        animator.Draw(Model.Position, spriteBatch, flip, Layers.Creatures, ScaleFactor);
        var origin = new Vector2(Model.Hitbox.Width / 2, Model.Hitbox.Height / 2);
        if (Settings.ShowHitboxes)
            HitboxManager.DrawHitbox(spriteBatch, Model.Position, Model.Hitbox, origin);
    }
}