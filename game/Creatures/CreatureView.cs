using game.View;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using game.Creatures.CreatureStates;

namespace game.Creatures;

internal abstract class CreatureView : CreatureStatesController, Interfaces.IDrawable
{
    public CreatureView(Animator animator, Dictionary<string, int> animations) : base(animator, animations)
    {
    }

    public void Attack() => SetStateAttack();

    public void TakeDamage() => SetStateTakeDamage();

    public void Kill() => SetStateDead();

    public void Idle() => SetStateIdle();

    public void Run() => SetStateRun();

    public abstract void Draw(SpriteBatch spriteBatch);

    public virtual void Update(float deltaTime)
    {
        UpdateState(deltaTime);
    }
}