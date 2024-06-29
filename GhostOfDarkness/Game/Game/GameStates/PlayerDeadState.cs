using Game.Game.GameStates;
using Game.View;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game;

internal class PlayerDeadState : GameState
{
    public PlayerDeadState(IGameStateSwitcher stateSwitcher) : base(stateSwitcher)
    {
        Drawables.Add(new Sprite(Textures.PauseBackground, new Vector2(564, 312), Layers.UIBackground));

        var position = new Vector2(736, 430);
        var restart = new Button(Textures.ButtonBackground, position, "Restart");
        restart.OnClicked += Restart;
        Components.Add(restart);

        position.Y += 140;
        var mainMenu = new Button(Textures.ButtonBackground, position, "To Main Menu");
        mainMenu.OnClicked += Exit;
        Components.Add(mainMenu);
    }

    public override void Exit()
    {
        Switcher.SwitchState<MainMenuState>();
    }

    public override void Restart()
    {
        Switcher.StartGame();
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