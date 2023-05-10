using game.CreatureStates;
using game.Interfaces;
using game.View;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace game.Creatures;

internal class MeleeEnemyView : CreatureStatesController,  IDrawable
{
    private static readonly Dictionary<string, int> animations = new Dictionary<string, int>()
    {
        ["idle"] = 0,
        ["run"] = 1,
        ["attack"] = 2,
        ["take damage"] = 3,
        ["dead"] = 4,
    };

    private MeleeEnemy model;

    public int Radius => animator.Radius;

    public MeleeEnemyView(MeleeEnemy model) : base(AnimatorsCreator.GetAnimator("Melee Enemy"), animations)
    {
        this.model = model;
    }

    public void Attack() => SetStateAttack();

    public void TakeDamage() => SetStateTakeDamage();

    public void Kill() => SetStateDead();

    public void Idle() => SetStateIdle();

    public void Run() => SetStateRun();

    public void Draw(SpriteBatch spriteBatch)
    {
        var flip = model.Direction.X < 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
        animator.Draw(model.Position, spriteBatch, flip);
    }
}