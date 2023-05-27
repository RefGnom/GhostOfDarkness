namespace game;

internal class RunState : CreatureState
{
    public RunState(IStateSwitcher stateSwitcher) : base(stateSwitcher)
    {
        CanMove = true;
        CanAttack = true;
    }

    public override void TakeDamage()
    {
        switcher.SwitchState<TakeDamageState>();
    }

    public override void Start(IState previousState)
    {
        timeLeft = OnStarted.Invoke();
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
        switcher.SwitchState<AttackState>();
    }

    public override void Kill()
    {
        switcher.SwitchState<DeadState>();
    }

    public override void Idle()
    {
        switcher.SwitchState<IdleState>();
    }
}