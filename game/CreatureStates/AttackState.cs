using game.Interfaces;

namespace game.CreatureStates;

internal class AttackState : CreatureState
{
    protected readonly float stateTime = 1;
    private float timeLeft;

    public AttackState(IStateSwitcher stateSwitcher) : base(stateSwitcher)
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
    }

    public override void SetHealth(float health)
    {
    }

    public override void Start()
    {
        Animator.SetAnimation(animations["attack"]);
        timeLeft = stateTime;
    }

    public override void StartFight()
    {
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
}