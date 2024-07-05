namespace Game.Interfaces;

public interface IState
{
    public void Start(IState previousState);

    public void Stop();
}