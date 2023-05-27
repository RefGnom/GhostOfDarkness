namespace game;

internal class TakeDamageState : CreatureState
{
    public TakeDamageState(IStateSwitcher stateSwitcher) : base(stateSwitcher)
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
        timeLeft = OnStarted.Invoke();
    }

    public override void Stop()
    {
    }

    public override void Update(float deltaTime)
    {
        timeLeft -= deltaTime;
        if (timeLeft <= 0)
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