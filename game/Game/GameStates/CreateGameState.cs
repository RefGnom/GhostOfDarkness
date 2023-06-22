using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game;

internal class CreateGameState : GameState
{
    private TextInput textInput;
    private Sprite background;

    public CreateGameState(IGameStateSwitcher stateSwitcher) : base(stateSwitcher)
    {
    }

    public override void Back()
    {
        switcher.SwitchState(previousState);
    }

    public override void Play()
    {
        switcher.SwitchState<PlayState>();
    }

    private void CreateGame()
    {
        switcher.StartGame();
        Play();
    }

    public override void Start(GameState previousState)
    {
        background = new Sprite(Textures.Background, Vector2.Zero, Layers.Background);

        var back = new Button(Textures.ButtonBackground, new Vector2(1432, 960), "Back");
        back.OnClicked += Back;

        var create = new Button(Textures.ButtonBackground, new Vector2(380, 600), "Create");
        create.OnClicked += CreateGame;

        textInput = new TextInput(Textures.FieldForText, new Vector2(380, 466));

        buttons = new()
        {
            back,
            create
        };

        MouseController.LeftButtonOnClicked += ClickedButtons;
    }

    public override void Stop()
    {
        textInput.Delete();
        MouseController.LeftButtonOnClicked -= ClickedButtons;
    }

    public override void Draw(SpriteBatch spriteBatch, float scale)
    {
        textInput.Draw(spriteBatch, scale);
        background.Draw(spriteBatch, scale);
        base.Draw(spriteBatch, scale);
    }
}