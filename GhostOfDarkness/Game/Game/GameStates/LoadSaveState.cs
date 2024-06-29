using Game.Game.GameStates;
using Game.Interfaces;
using Game.View;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game;

internal class LoadSaveState : GameState
{
    public LoadSaveState(IGameStateSwitcher stateSwitcher) : base(stateSwitcher)
    {
        Drawables.Add(new Sprite(Textures.SavesWindow, new Vector2(40, 70), Layers.UIBackground));
        Drawables.Add(new Sprite(Textures.Background, Vector2.Zero, Layers.Background));

        var load = new Button(Textures.ButtonBackground, new Vector2(40, 960), "Load");
        load.OnClicked += Play;
        Components.Add(load);

        var createNew = new Button(Textures.ButtonBackground, new Vector2(538, 960), "Create New Game");
        createNew.OnClicked += NewGame;
        Components.Add(createNew);

        var back = new Button(Textures.ButtonBackground, new Vector2(1432, 960), "Back");
        back.OnClicked += Back;
        Components.Add(back);
    }

    public override void Back()
    {
        Switcher.SwitchState<MainMenuState>();
    }

    public override void NewGame()
    {
        Switcher.SwitchState<CreateGameState>();
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