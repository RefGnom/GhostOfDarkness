namespace game;

internal interface IStateSwitcher
{
    public void SwitchState<T>() where T : IState;

    public void SwitchState(IState state);
}