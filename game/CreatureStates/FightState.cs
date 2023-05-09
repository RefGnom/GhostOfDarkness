using game.Interfaces;

namespace game.CreatureStates;

internal class FightState : CreatureState
{
    private readonly float activeTime = 3;
    private float leftTime;

    public FightState(IStateSwitcher stateSwitcher) : base(stateSwitcher)
    {
    }

    public override void Attack()
    {
        stateSwitcher.SwitchState<AttackState>();
    }

    public override void Dead()
    {
        stateSwitcher.SwitchState<DeadState>();
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
        leftTime = activeTime;
    }

    public override void StartFight()
    {
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
}