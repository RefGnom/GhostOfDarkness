namespace game.CreatureStates;

internal interface IStationStateSwitcher
{
    void SwitchState<T>() where T : CreatureState;
}