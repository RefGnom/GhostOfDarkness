﻿using System.Collections.Generic;

namespace game;

internal class TakeDamageState : CreatureState
{
    private float leftTime;

    public TakeDamageState(IStateSwitcher stateSwitcher, Animator animator, Dictionary<string, int> animations)
        : base(stateSwitcher, animator, animations)
    {
        CanAttack = false;
        CanMove = false;
    }

    public override void TakeDamage()
    {
        switcher.SwitchState<TakeDamageState>();
    }

    public override void Start(IState previousState)
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
            switcher.SwitchState<FightState>();
        }
    }

    public override void Kill()
    {
        switcher.SwitchState<DeadState>();
    }

    public override void Run()
    {
    }

    public override void Idle()
    {
    }

    public override void Attack()
    {
    }
}