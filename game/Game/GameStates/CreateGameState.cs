using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game;

internal class CreateGameState : GameState
{
    private readonly TextInput textInput;

    public CreateGameState(IGameStateSwitcher stateSwitcher) : base(stateSwitcher)
    {
        drawables.Add(new Sprite(Textures.Background, Vector2.Zero, Layers.Background));

        var back = new Button(Textures.ButtonBackground, new Vector2(1432, 960), "Back");
        back.OnClicked += Back;
        components.Add(back);

        var create = new Button(Textures.ButtonBackground, new Vector2(380, 600), "Create");
        create.OnClicked += CreateGame;
        components.Add(create);

        textInput = new TextInput(Textures.FieldForText, new Vector2(380, 466));
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
        textInput.Enable();
    }

    public override void Stop()
    {
        textInput.Disable();
    }

    public override void Draw(SpriteBatch spriteBatch, float scale)
    {
        textInput.Draw(spriteBatch, scale);
        base.Draw(spriteBatch, scale);
    }
}