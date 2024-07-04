using Game.ContentLoaders;
using Game.Graphics;
using Game.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using IDrawable = Game.Interfaces.IDrawable;

namespace Game.Service;

internal class Fps : IDrawable
{
    private float frames;
    private float elapsed;
    private float previousDeltaTime;
    private string msg = "";
    private readonly float frequencyUpdate;

    public Fps(float frequencyUpdate)
    {
        this.frequencyUpdate = frequencyUpdate;
        GameManager.Instance.Drawer.RegisterUi(this);
    }

    public void Update(GameTime gameTime)
    {
        var now = (float)gameTime.TotalGameTime.TotalSeconds;
        elapsed = now - previousDeltaTime;
        if (elapsed > frequencyUpdate)
        {
            msg = $"Fps: {frames / elapsed}";
            elapsed = 0;
            frames = 0;
            previousDeltaTime = now;
        }

        frames++;
    }

    public void Draw(ISpriteBatch spriteBatch, float scale)
    {
        if (Settings.ShowFps)
        {
            spriteBatch.DrawString(Fonts.Debug, msg, new Vector2(5, 5), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, Layers.Text);
        }
    }
}