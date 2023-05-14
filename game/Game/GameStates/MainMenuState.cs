using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game;

internal class MainMenuState : GameState
{
    public MainMenuState(IStateSwitcher stateSwitcher) : base(stateSwitcher)
    {
    }

    public override void Back()
    {
    }

    public override void Confirm()
    {
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        throw new System.NotImplementedException();
    }

    public override void Exit()
    {
        GameIsExit = true;
    }

    public override void NewGame()
    {
        switcher.SwitchState<CreateGameState>();
    }

    public override void LoadSave()
    {
        switcher.SwitchState<LoadSaveState>();
    }

    public override void OpenSettings()
    {
        switcher.SwitchState<SettingsState>();
    }

    public override void Play()
    {
    }

    public override void Restart()
    {
    }

    public override void Save()
    {
    }

    public override void Start(IState previousState)
    {
        var b = new Button(TexturesManager.ButtonBackground, Vector2.One, "Hello world!");
        GameManager.Instance.Drawer.RegisterUI(b);
        b.OnClicked += () => Debug.Log("Click");
        MouseController.LeftButtonOnClicked += () => b.Clicked();
    }

    public override void Stop()
    {
    }
}