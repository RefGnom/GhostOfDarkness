using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace game;

internal class BossView : EnemyView
{
    private readonly Animator animator = AnimatorsCreator.GetAnimator("Boss");
    private static readonly Dictionary<string, int> animations = new()
    {
        ["death"] = 0,
        ["Attack-R"] = 1,
        ["Attack-T"] = 2,
        ["Attack-TR"] = 3,
        ["Attack-TL"] = 4,
        ["Attack-D"] = 5,
        ["Attack-DR"] = 6,
        ["Attack-DL"] = 7,
        ["Attack-L"] = 8,
        ["Idle-R"] = 9,
        ["Idle-T"] = 10,
        ["Idle-TL"] = 11,
        ["Idle-TR"] = 12,
        ["Idle-D"] = 13,
        ["Idle-DR"] = 14,
        ["Idle-DL"] = 15,
        ["Idle-L"] = 16,
    };

    public BossView()
    {
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

    public override void Draw(SpriteBatch spriteBatch, float scale)
    {
        animator.Draw(model.Position, spriteBatch, SpriteEffects.None, Layers.Creatures, scaleFactor);
        var origin = new Vector2(model.Hitbox.Width / 2, model.Hitbox.Height / 2);
        if (Settings.ShowHitboxes)
            HitboxManager.DrawHitbox(spriteBatch, model.Position, model.Hitbox, origin);
    }
}