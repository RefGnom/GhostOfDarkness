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

    public override void Confirm()
    {
    }

    public override void LoadSave()
    {
    }

    public override void Exit()
    {
    }

    public override void NewGame()
    {
    }

    public override void OpenSettings()
    {
    }

    public override void Play()
    {
    }

    public override void Dead()
    {
        switcher.SwitchState<PlayerDeadState>();
    }

    public override void Restart()
    {
    }

    public override void Save()
    {
    }

    public override void Start(GameState previousState)
    {
    }

    public override void Stop()
    {
    }

    public override void Draw()
    {
    }

    public override void Erase()
    {
    }
}