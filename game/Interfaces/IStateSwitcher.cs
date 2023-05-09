using game.CreatureStates;

namespace game.Interfaces;

internal interface IStateSwitcher
{
    void SwitchState<T>() where T : CreatureState;
}