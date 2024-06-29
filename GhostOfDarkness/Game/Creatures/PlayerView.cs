using System.Collections.Generic;
using Core;
using game;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Creatures;

internal class PlayerView : CreatureStatesController
{
    private readonly Animator animator = AnimatorsCreator.GetAnimator("Player");
    private static readonly Dictionary<string, int> animations = new Dictionary<string, int>
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

    private List<CreatureState> CreateStates()
    {
        var idle = new IdleState(this)
        {
            OnStarted = () =>
            {
                animator.SetAnimation(animations["idle"]);
                return animator.GetAnimationTime(animations["idle"]);
            }
        };
        var run = new RunState(this)
        {
            OnStarted = () =>
            {
                animator.SetAnimation(animations["run"]);
                return animator.GetAnimationTime(animations["run"]);
            }
        };
        var fight = new FightState(this)
        {
            OnStarted = idle.OnStarted
        };
        var attack = new AttackState(this)
        {
            OnStarted = () =>
            {
                animator.SetAnimation(animations["attack"]);
                return animator.GetAnimationTime(animations["attack"]);
            }
        };
        var takeDamage = new TakeDamageState(this)
        {
            OnStarted = () =>
            {
                animator.SetAnimation(animations["take damage"]);
                return animator.GetAnimationTime(animations["take damage"]);
            }
        };
        var dead = new DeadState(this)
        {
            OnStarted = () =>
            {
                animator.SetAnimation(animations["dead"], false);
                return animator.GetAnimationTime(animations["dead"]) * 20;
            }
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
        rotation = model.Direction.ToAngle();
        base.Update(deltaTime);
    }

    public override void Draw(SpriteBatch spriteBatch, float scale)
    {
        var flip = SpriteEffects.None;
        animator.Draw(model.Position, spriteBatch, flip, rotation, Layers.Creatures, 1);
        if (Settings.ShowHitboxes)
        {
            HitboxManager.DrawHitbox(spriteBatch, model.Position, HitboxManager.Player, animator.Origin);
        }
    }
}