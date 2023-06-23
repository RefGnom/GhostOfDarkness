using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game;

internal class LoadSaveState : GameState
{
    public LoadSaveState(IGameStateSwitcher stateSwitcher) : base(stateSwitcher)
    {
        drawables.Add(new Sprite(Textures.SavesWindow, new Vector2(40, 70), Layers.UIBackground));
        drawables.Add(new Sprite(Textures.Background, Vector2.Zero, Layers.Background));

        var load = new Button(Textures.ButtonBackground, new Vector2(40, 960), "Load");
        load.OnClicked += Play;
        components.Add(load);

        var createNew = new Button(Textures.ButtonBackground, new Vector2(538, 960), "Create New Game");
        createNew.OnClicked += NewGame;
        components.Add(createNew);

        var back = new Button(Textures.ButtonBackground, new Vector2(1432, 960), "Back");
        back.OnClicked += Back;
        components.Add(back);
    }

    public override void Back()
    {
        switcher.SwitchState<MainMenuState>();
    }

    public override void NewGame()
    {
        switcher.SwitchState<CreateGameState>();
    }

    public override void Play()
    {
        switcher.SwitchState<PlayState>();
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