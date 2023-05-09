namespace game.CreatureStates;

internal abstract class CreatureState : Creature
{
    protected readonly IStationStateSwitcher stateSwitcher;

    public CreatureState(IStationStateSwitcher stateSwitcher)
    {
        this.stateSwitcher = stateSwitcher;
    }

    public abstract void Start();

    public abstract void Stop();

    public abstract void Run();

    public abstract void StartFight();

    public abstract void Attack();

    public abstract void TakeDamage(float damage);

    public abstract void Dead();
}