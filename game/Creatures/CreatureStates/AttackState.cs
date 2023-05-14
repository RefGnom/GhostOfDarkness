using System.Collections.Generic;

namespace game;

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
        switcher.SwitchState<TakeDamageState>();
    }

    public override void Start(IState previousState)
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
            switcher.SwitchState<FightState>();
    }

    public override void Kill()
    {
    }

    public override void Idle()
    {
    }
}