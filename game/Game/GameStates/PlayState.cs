namespace game;

internal class PlayState : GameState
{
    public PlayState(IGameStateSwitcher stateSwitcher) : base(stateSwitcher)
    {
    }

    public override void Back()
    {
        switcher.SwitchState<PauseState>();
    }

    public override void Dead()
    {
        switcher.SwitchState<PlayerDeadState>();
    }

    public override void Start(GameState previousState)
    {
        SongsManager.Resume();
    }

    public override void Stop()
    {
        SongsManager.Pause();
    }
}