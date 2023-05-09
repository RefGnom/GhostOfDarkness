using game.Interfaces;

namespace game.CreatureStates;

internal class DeadState : CreatureState
{
    protected float animationTime = 1;
    private float leftTime;

    public DeadState(IStateSwitcher stateSwitcher) : base(stateSwitcher)
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
        Animator.SetAnimation(animations["dead"]);
        leftTime = animationTime;
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
            stateSwitcher.SwitchState<KilledState>();
    }
}