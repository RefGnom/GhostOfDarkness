using Game.Game.GameStates;
using Game.Interfaces;
using Game.View;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game;

internal class PauseState : GameState
{
    public PauseState(IGameStateSwitcher stateSwitcher) : base(stateSwitcher)
    {
        Drawables.Add(new Sprite(Textures.PauseBackground, new Vector2(564, 312), Layers.UIBackground));

        var position = new Vector2(736, 370);
        var continueGame = new Button(Textures.ButtonBackground, position, "Resume");
        continueGame.OnClicked += Play;
        Components.Add(continueGame);

        position.Y += 130;
        var settings = new Button(Textures.ButtonBackground, position, "Settings");
        settings.OnClicked += OpenSettings;
        Components.Add(settings);

        position.Y += 130;
        var mainMenu = new Button(Textures.ButtonBackground, position, "To Main Menu");
        mainMenu.OnClicked += Exit;
        Components.Add(mainMenu);
    }

    public override void Back()
    {
        Switcher.SwitchState<PlayState>();
    }

    public override void Exit()
    {
        Switcher.SwitchState<MainMenuState>();
    }

    public override void OpenSettings()
    {
        Switcher.SwitchState<SettingsState>();
    }

    public override void Play()
    {
        Switcher.SwitchState<PlayState>();
    }

    public override void Start(GameState previousState)
    {
    }

    public override void Stop()
    {
    }

    public override void Draw(SpriteBatch spriteBatch, float scale)
    {
        base.Draw(spriteBatch, scale);
    }
}