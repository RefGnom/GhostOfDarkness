using System.Collections.Generic;
using System.Linq;

namespace game.CreatureStates;

internal class StationController : IStationStateSwitcher
{
    private CreatureState currentState;
    private readonly List<CreatureState> states;

    public StationController()
    {
        states = new List<CreatureState>()
        {
            new IdleState(this),
            new RunState(this),
            new FightState(this),
            new AttackState(this),
            new TakeDamageState(this),
            new DeadState(this)
        };
    }

    public void SwitchState<T>() where T : CreatureState
    {
        var state = states.FirstOrDefault(s => s is T);
        currentState.Stop();
        state.Start();
        currentState = state;
    }

    public void Attack() => currentState.Attack();

    public void Dead() => currentState.Dead();

    public void Run() => currentState.Run();

    public void Start() => currentState.Start();

    public void StartFight() => currentState.StartFight();

    public void Stop() => currentState.Stop();

    public void TakeDamage(float damage) => currentState.TakeDamage(damage);
}