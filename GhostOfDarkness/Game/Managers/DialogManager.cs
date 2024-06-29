using Game.Controllers;
using Game.View;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace game;

internal class DialogManager : IDrawable
{
    private Message message;
    private bool enabledDialog;

    public DialogManager()
    {
        GameManager.Instance.Drawer.RegisterHUD(this);
    }

    public void Enable(Message message)
    {
        enabledDialog = true;
        this.message = message;
    }

    public void Disable()
    {
        enabledDialog = false;
    }

    public void Update()
    {
        if (!enabledDialog)
            return;
        if (KeyboardController.IsKeyDown(Keys.Down))
        {
            message.MoveNextChoice();
        }
        if (KeyboardController.IsKeyDown(Keys.Up))
        {
            message.MoveBackChoice();
        }
        if (KeyboardController.IsKeyDown(Keys.Enter))
        {
            message.OnNext?.Invoke();
            if (message.Next is null)
            {
                Disable();
                return;
            }
            message = message.Next;
        }
    }

    public void Draw(SpriteBatch spriteBatch, float scale)
    {
        if (enabledDialog)
        {
            message.Draw(spriteBatch, scale);
            var hintPosition = new Vector2(1800, 1000);
            spriteBatch.Draw(Textures.DialogHelpUI, hintPosition * scale, null, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, Layers.HUDBackground);
        }
    }
}