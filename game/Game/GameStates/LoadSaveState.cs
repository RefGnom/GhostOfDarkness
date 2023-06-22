using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game;

internal class LoadSaveState : GameState
{
    private Sprite savesStorage;
    private Sprite background;

    public LoadSaveState(IGameStateSwitcher stateSwitcher) : base(stateSwitcher)
    {
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
        savesStorage = new Sprite(Textures.SavesWindow, new Vector2(40, 70), Layers.UIBackground);
        background = new Sprite(Textures.Background, Vector2.Zero, Layers.Background);

        var load = new Button(Textures.ButtonBackground, new Vector2(40, 960), "Load");
        load.OnClicked += Play;
        var createNew = new Button(Textures.ButtonBackground, new Vector2(538, 960), "Create New Game");
        createNew.OnClicked += NewGame;
        var back = new Button(Textures.ButtonBackground, new Vector2(1432, 960), "Back");
        back.OnClicked += Back;

        buttons = new()
        {
            load,
            createNew,
            back,
        };

        MouseController.LeftButtonOnClicked += ClickedButtons;
    }

    public override void Stop()
    {
        MouseController.LeftButtonOnClicked -= ClickedButtons;
    }

    public override void Draw(SpriteBatch spriteBatch, float scale)
    {
        savesStorage.Draw(spriteBatch, scale);
        background.Draw(spriteBatch, scale);
        base.Draw(spriteBatch, scale);
    }
}