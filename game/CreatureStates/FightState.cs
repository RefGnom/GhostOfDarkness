using game.Interfaces;
using game.View;
using System.Collections.Generic;

namespace game.CreatureStates;

internal class FightState : CreatureState
{
    private readonly float activeTime = 3;
    private float leftTime;

    public FightState(IStateSwitcher stateSwitcher, Animator animator, Dictionary<string, int> animations)
        : base(stateSwitcher, animator, animations)
    {
        CanMove = true;
        CanAttack = true;
    }

    public override void Attack()
    {
        stateSwitcher.SwitchState<AttackState>();
    }

    public override void Run()
    {
        stateSwitcher.SwitchState<RunState>();
    }

    public override void TakeDamage()
    {
        stateSwitcher.SwitchState<TakeDamageState>();
    }

    public override void Start()
    {
        Animator.SetAnimation(animations["idle"]);
        leftTime = activeTime;
    }

    public override void Stop()
    {
    }

    public override void Update(float deltaTime)
    {
        leftTime -= deltaTime;
        if (leftTime <= 0)
            stateSwitcher.SwitchState<IdleState>();
    }

    public override void Kill()
    {
        stateSwitcher.SwitchState<DeadState>();
    }

    public override void Idle()
    {
    }
}