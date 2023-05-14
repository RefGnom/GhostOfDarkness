namespace game;

internal interface IState
{
    public void Start(IState previousState);

    public void Stop();
}