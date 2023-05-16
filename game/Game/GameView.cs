using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game;

internal class GameView : GameStatesController
{
    private readonly GameModel model;

    private static Camera Camera => GameManager.Instance.Camera;

    public GameView(GameModel model)
    {
        this.model = model;
    }

    public void Draw(SpriteBatch spriteBatch, float scale)
    {
        DrawLocation(spriteBatch, scale);
        DrawHUD(spriteBatch, scale);
        DrawUI(spriteBatch, scale);
    }

    private void DrawLocation(SpriteBatch spriteBatch, float scale)
    {
        spriteBatch.Begin(sortMode: SpriteSortMode.BackToFront, transformMatrix: Camera.Transform);
        GameManager.Instance.Drawer.Draw(spriteBatch, scale);
        spriteBatch.End();
    }

    private void DrawUI(SpriteBatch spriteBatch, float scale)
    {
        spriteBatch.Begin(sortMode: SpriteSortMode.BackToFront);
        Debug.DrawMessages(spriteBatch);
        GameManager.Instance.Drawer.DrawUI(spriteBatch, scale);
        spriteBatch.End();
    }

    private void DrawHUD(SpriteBatch spriteBatch, float scale)
    {
        spriteBatch.Begin(sortMode: SpriteSortMode.BackToFront);
        Debug.DrawMessages(spriteBatch);
        GameManager.Instance.Drawer.DrawHUD(spriteBatch, scale);
        spriteBatch.End();
    }
}