using game.Interfaces;
using game.View;
using System.Collections.Generic;

namespace game.CreatureStates;

internal class AttackState : CreatureState
{
    private float timeLeft;

    public AttackState(IStateSwitcher stateSwitcher, Animator animator, Dictionary<string, int> animations)
        : base(stateSwitcher, animator, animations)
    {
        CanMove = false;
        CanAttack = true;
    }

    public override void Attack()
    {
    }

    public override void Run()
    {
    }

    public override void TakeDamage()
    {
    }

    public override void Start()
    {
        Animator.SetAnimation(animations["attack"]);
        timeLeft = Animator.GetAnimationTime(animations["attack"]);
    }

    public override void Stop()
    {

    }

    public override void Update(float deltaTime)
    {
        timeLeft -= deltaTime;
        if (timeLeft <= 0)
            stateSwitcher.SwitchState<FightState>();
    }

    public override void Kill()
    {
    }

    public override void Idle()
    {
    }
}