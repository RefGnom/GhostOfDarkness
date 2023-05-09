using game.Interfaces;

namespace game.CreatureStates;

internal class TakeDamageState : CreatureState
{
    protected float animationTime = 1;
    private float leftTime;

    public TakeDamageState(IStateSwitcher stateSwitcher) : base(stateSwitcher)
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
        Animator.SetAnimation(animations["take damage"]);
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
        {
            stateSwitcher.SwitchState<FightState>();
        }
    }
}