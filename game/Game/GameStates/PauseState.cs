using Microsoft.Xna.Framework.Graphics;

namespace game;

internal class PauseState : GameState
{
    public PauseState(IStateSwitcher stateSwitcher) : base(stateSwitcher)
    {
    }

    public override void Back()
    {
        switcher.SwitchState(previousState);
    }

    public override void Confirm()
    {
    }

    public override void LoadSave()
    {
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        throw new System.NotImplementedException();
    }

    public override void Exit()
    {
        switcher.SwitchState<MainMenuState>();
    }

    public override void NewGame()
    {
    }

    public override void OpenSettings()
    {
        switcher.SwitchState<SettingsState>();
    }

    public override void Play()
    {
        switcher.SwitchState<PlayState>();
    }

    public override void Restart()
    {
    }

    public override void Save()
    {
    }

    public override void Start(IState previousState)
    {
        this.previousState = (GameState)previousState;
    }

    public override void Stop()
    {
    }

    public override void Update(float deltaTime)
    {
    }
}