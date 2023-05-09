using game.Interfaces;

namespace game.CreatureStates;

internal class IdleState : CreatureState
{
    public IdleState(IStateSwitcher stateSwitcher) : base(stateSwitcher)
    {
    }

    public override void Attack()
    {
    }

    public override void Dead()
    {
    }

    public override void Run()
    {
        stateSwitcher.SwitchState<RunState>();
    }

    public override void SetHealth(float health)
    {
        if (health < 0)
        {
            stateSwitcher.SwitchState<DeadState>();
        }
        else
        {
            stateSwitcher.SwitchState<TakeDamageState>();
        }
    }

    public override void Start()
    {
        Animator.SetAnimation(animations["idle"]);
    }

    public override void StartFight()
    {
        throw new System.NotImplementedException();
    }

    public override void Stop()
    {
        throw new System.NotImplementedException();
    }

    public override void Update(float deltaTime)
    {
        throw new System.NotImplementedException();
    }
}