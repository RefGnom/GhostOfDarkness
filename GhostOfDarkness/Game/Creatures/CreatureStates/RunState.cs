﻿using Game.Interfaces;

namespace Game.Creatures.CreatureStates;

internal class RunState : CreatureState
{
    public RunState(IStateSwitcher stateSwitcher) : base(stateSwitcher)
    {
        CanMove = true;
        CanAttack = true;
    }

    public override void TakeDamage()
    {
        Switcher.SwitchState<TakeDamageState>();
    }

    public override void Start(IState previousState)
    {
        TimeLeft = OnStarted.Invoke();
    }

    public override void Stop()
    {
    }

    public override void Update(float deltaTime)
    {
        OnUpdate?.Invoke();
    }

    public override void Run()
    {
    }

    public override void Attack()
    {
        Switcher.SwitchState<AttackState>();
    }

    public override void Kill()
    {
        Switcher.SwitchState<DeadState>();
    }

    public override void Idle()
    {
        Switcher.SwitchState<IdleState>();
    }
}