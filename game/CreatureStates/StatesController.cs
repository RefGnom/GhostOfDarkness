using System.Collections.Generic;
using System.Linq;
using game.Interfaces;

namespace game.CreatureStates;

internal class StatesController : IStateSwitcher
{
    private CreatureState currentState;
    private readonly List<CreatureState> states;

    public StatesController()
    {
        states = new List<CreatureState>()
        {
            new IdleState(this),
            new RunState(this),
            new FightState(this),
            new AttackState(this),
            new TakeDamageState(this),
            new DeadState(this),
            new KilledState(this)
        };
    }

    public void SwitchState<T>() where T : CreatureState
    {
        var state = states.FirstOrDefault(s => s is T);
        currentState.Stop();
        state.Start();
        currentState = state;
    }

    public void StateAttack() => currentState.Attack();

    public void SetHealth(float health) => currentState.SetHealth(health);
}