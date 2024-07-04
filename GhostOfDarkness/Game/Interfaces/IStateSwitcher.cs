using game;

namespace Game.Interfaces;

public interface IStateSwitcher
{
    public void SwitchState<T>() where T : IState;

    public void SwitchState(IState state);
}