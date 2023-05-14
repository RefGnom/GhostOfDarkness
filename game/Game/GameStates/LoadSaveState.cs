using Microsoft.Xna.Framework.Graphics;

namespace game;

internal class LoadSaveState : GameState
{
    public LoadSaveState(IStateSwitcher stateSwitcher) : base(stateSwitcher)
    {
    }

    public override void Back()
    {
        switcher.SwitchState<MainMenuState>();
    }

    public override void Confirm()
    {
    }

    public override void LoadSave()
    {
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
    }

    public override void Exit()
    {
    }

    public override void NewGame()
    {
        switcher.SwitchState<CreateGameState>();
    }

    public override void OpenSettings()
    {
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