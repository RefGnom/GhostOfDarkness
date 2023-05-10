using game.Interfaces;
using game.View;
using System.Collections.Generic;

namespace game.CreatureStates;

internal class TakeDamageState : CreatureState
{
    private float leftTime;

    public TakeDamageState(IStateSwitcher stateSwitcher, Animator animator, Dictionary<string, int> animations)
        : base(stateSwitcher, animator, animations)
    {
        CanAttack = false;
        CanMove = false;
    }

    public override void Attack()
    {
    }

    public override void Run()
    {
    }

    public override void TakeDamage()
    {
        stateSwitcher.SwitchState<TakeDamageState>();
    }

    public override void Start()
    {
        Animator.SetAnimation(animations["take damage"]);
        leftTime = Animator.GetAnimationTime(animations["take damage"]);
    }

    public override void Stop()
    {
    }

    public override void Update(float deltaTime)
    {
        leftTime -= deltaTime;
        if (leftTime <= 0)
        {
            stateSwitcher.SwitchState<FightState>();
        }
    }

    public override void Kill()
    {
        stateSwitcher.SwitchState<DeadState>();
    }
}