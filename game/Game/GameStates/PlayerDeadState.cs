using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game;

internal class PlayerDeadState : GameState
{
    private Sprite background;

    public PlayerDeadState(IGameStateSwitcher stateSwitcher) : base(stateSwitcher)
    {
    }

    public override void Exit()
    {
        switcher.SwitchState<MainMenuState>();
    }

    public override void Restart()
    {
        switcher.StartGame();
        switcher.SwitchState<PlayState>();
    }

    public override void Start(GameState previousState)
    {
        background = new Sprite(Textures.PauseBackground, new Vector2(564, 312), Layers.UIBackground);

        var position = new Vector2(736, 430);
        var restart = new Button(Textures.ButtonBackground, position, "Restart");
        restart.OnClicked += Restart;
        position.Y += 140;
        var mainMenu = new Button(Textures.ButtonBackground, position, "To Main Menu");
        mainMenu.OnClicked += Exit;

        buttons = new()
        {
            mainMenu,
            restart
        };

        MouseController.LeftButtonOnClicked += ClickedButtons;
    }

    public override void Stop()
    {
        MouseController.LeftButtonOnClicked -= ClickedButtons;
    }

    public override void Draw(SpriteBatch spriteBatch, float scale)
    {
        background.Draw(spriteBatch, scale);
        base.Draw(spriteBatch, scale);
    }
}