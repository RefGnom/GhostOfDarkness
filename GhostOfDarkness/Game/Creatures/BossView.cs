using System.Collections.Generic;
using Core;
using game;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Creatures;

internal class BossView : EnemyView
{
    private readonly Animator animator = AnimatorsCreator.GetAnimator("Boss");
    private static readonly Dictionary<string, int> animations = new()
    {
        ["death"] = 0,
        ["attack-E"] = 1,
        ["attack-N"] = 2,
        ["attack-NE"] = 3,
        ["attack-NW"] = 4,
        ["attack-S"] = 5,
        ["attack-SE"] = 6,
        ["attack-SW"] = 7,
        ["attack-W"] = 8,
        ["idle-E"] = 9,
        ["idle-N"] = 10,
        ["idle-NW"] = 11,
        ["idle-NE"] = 12,
        ["idle-S"] = 13,
        ["idle-SE"] = 14,
        ["idle-SW"] = 15,
        ["idle-W"] = 16,
    };

    public void CreateStates()
    {
        SetStates(GetStates());
    }

    private List<CreatureState> GetStates()
    {
        var idle = new IdleState(this);
        idle.OnStarted = () =>
        {
            var direction = model.Direction.ToAngle().ToCardinalDirection();
            animator.SetAnimation(animations[$"idle-{direction}"]);
            return animator.GetAnimationTime(animations[$"idle-{direction}"]);
        };
        idle.OnUpdate = idle.OnStarted;
        var run = new RunState(this);
        run.OnStarted = idle.OnStarted;
        run.OnUpdate = run.OnStarted;
        var fight = new FightState(this);
        fight.OnStarted = idle.OnStarted;
        fight.OnUpdate = idle.OnUpdate;
        var attack = new AttackState(this);
        attack.OnStarted = () =>
        {
            var direction = model.Direction.ToAngle().ToCardinalDirection();
            animator.SetAnimation(animations[$"attack-{direction}"]);
            return animator.GetAnimationTime(animations[$"attack-{direction}"]) / 2;
        };
        attack.OnUpdate = attack.OnStarted;
        var takeDamage = new TakeDamageState(this);
        takeDamage.OnStarted = () =>
        {
            var direction = model.Direction.ToAngle().ToCardinalDirection();
            animator.SetAnimation(animations[$"idle-{direction}"]);
            return animator.GetAnimationTime(animations[$"idle-{direction}"]) * 0.2f;
        };
        takeDamage.OnUpdate = takeDamage.OnStarted;
        var dead = new DeadState(this);
        dead.OnStarted = () =>
        {
            model.Direction.ToAngle().ToCardinalDirection();
            animator.SetAnimation(animations["death"], false);
            return animator.GetAnimationTime(animations["death"]) * 20;
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
        {
            HitboxManager.DrawHitbox(spriteBatch, model.Position, model.Hitbox, origin);
        }
    }
}