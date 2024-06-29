using game;
using Game.Controllers;
using Game.Interfaces;
using Game.View;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Game.GameStates;

internal class CreateGameState : GameState
{
    private readonly TextInput textInput;

    public CreateGameState(IGameStateSwitcher stateSwitcher) : base(stateSwitcher)
    {
        Drawables.Add(new Sprite(Textures.Background, Vector2.Zero, Layers.Background));

        var back = new Button(Textures.ButtonBackground, new Vector2(1432, 960), "Back");
        back.OnClicked += Back;
        Components.Add(back);

        var create = new Button(Textures.ButtonBackground, new Vector2(380, 600), "Create");
        create.OnClicked += CreateGame;
        Components.Add(create);

        textInput = new TextInput(Textures.FieldForText, new Vector2(380, 466));
    }

    public override void Back()
    {
        Switcher.SwitchState(PreviousState);
    }

    public override void Play()
    {
        Switcher.SwitchState<PlayState>();
    }

    private void CreateGame()
    {
        Switcher.StartGame();
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