using game.Interfaces;

namespace game.CreatureStates;

internal class KilledState : CreatureState
{
    public KilledState(IStateSwitcher stateSwitcher) : base(stateSwitcher)
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
    }

    public override void StartFight()
    {
    }

    public override void Stop()
    {
    }

    public override void Update(float deltaTime)
    {
    }
}