using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace game.Program;

internal class TestGame : Game
{
    private GraphicsDeviceManager graphics;
    private SpriteBatch spriteBatch;

    public TestGame()
    {
        graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        base.Initialize();
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);
        TexturesManager.Load(Content);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            Exit();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        spriteBatch.Begin();
        var width = 26;
        var height = 38;
        var origin = new Vector2(width / 2, 0);
        var position = new Vector2(0, 0);
        spriteBatch.Draw(TexturesManager.Player, position, null, Color.White, 0, origin, 1, SpriteEffects.None, 0);

        spriteBatch.End();
        base.Draw(gameTime);
    }
}