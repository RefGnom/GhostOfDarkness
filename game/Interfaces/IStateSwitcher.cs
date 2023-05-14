namespace game;

internal interface IStateSwitcher
{
    public void SwitchState<T>() where T : IState;

    public void SwitchState<T>(T state) where T : IState
    {
        SwitchState<T>();
    }
}