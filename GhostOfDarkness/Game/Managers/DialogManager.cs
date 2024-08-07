﻿using Game.ContentLoaders;
using Game.Controllers.InputServices;
using Game.Graphics;
using Game.Service;
using Game.View;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using IDrawable = Game.Interfaces.IDrawable;

namespace Game.Managers;

internal class DialogManager : IDrawable
{
    private Message message;
    private bool enabledDialog;

    public DialogManager()
    {
        GameManager.Instance.Drawer.RegisterHud(this);
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
        {
            return;
        }

        if (Input.KeyboardService.IsSingleKeyDown(Keys.Down))
        {
            message.MoveNextChoice();
        }

        if (Input.KeyboardService.IsSingleKeyDown(Keys.Up))
        {
            message.MoveBackChoice();
        }

        if (Input.KeyboardService.IsSingleKeyDown(Keys.Enter))
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

    public void Draw(ISpriteBatch spriteBatch, float scale)
    {
        if (enabledDialog)
        {
            message.Draw(spriteBatch, scale);
            var hintPosition = new Vector2(1800, 1000);
            spriteBatch.Draw(Textures.DialogHelpUi, hintPosition * scale, null, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, Layers.HudBackground);
        }
    }
}