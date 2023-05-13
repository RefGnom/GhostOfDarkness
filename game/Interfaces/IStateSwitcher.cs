namespace game;

internal interface IStateSwitcher
{
    void SwitchState<T>() where T : CreatureState;
}