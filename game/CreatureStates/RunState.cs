using game.Interfaces;
using game.View;
using System.Collections.Generic;

namespace game.CreatureStates;

internal class RunState : CreatureState
{
    public RunState(IStateSwitcher stateSwitcher, Animator animator, Dictionary<string, int> animations) : base(stateSwitcher, animator, animations)
    {
        CanMove = true;
        CanAttack = true;
    }

    public override void TakeDamage()
    {
        stateSwitcher.SwitchState<TakeDamageState>();
    }

    public override void Start()
    {
        Animator.SetAnimation(animations["run"]);
    }

    public override void Stop()
    {
    }

    public override void Update(float deltaTime)
    {
    }

    public override void Run()
    {
    }

    public override void Attack()
    {
        stateSwitcher.SwitchState<AttackState>();
    }

    public override void Kill()
    {
        stateSwitcher.SwitchState<DeadState>();
    }
}