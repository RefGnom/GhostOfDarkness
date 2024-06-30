using game;
using Game.Controllers.Buttons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Color = Microsoft.Xna.Framework.Color;

namespace Game.Controllers;

public class SaveButton : Button
{
    private static readonly SpriteFont nameFont = Fonts.Common16;
    private static readonly SpriteFont infoFont = Fonts.Common14;

    private readonly string saveName;
    private readonly string difficulty;
    private readonly string playTime;

    public SaveButton(
        Texture2D texture,
        Vector2 position,
        string saveName,
        string difficulty,
        string playTime
    ) : base(texture, position, "stub")
    {
        this.saveName = saveName;
        this.difficulty = $"Difficulty {difficulty}";
        this.playTime = $"Play time {playTime}";
    }

    protected override void DrawText(SpriteBatch spriteBatch, float scale)
    {
        const int deltaX = 30;
        var namePosition = GetTextPosition(deltaX, 20);
        spriteBatch.DrawString(nameFont, saveName, namePosition, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, TextLayer);

        var deltaY = Texture.Height - infoFont.MeasureString(difficulty).Y - 15;
        var difficultyPosition = GetTextPosition(deltaX, deltaY);
        spriteBatch.DrawString(infoFont, difficulty, difficultyPosition, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, TextLayer);

        var playTimeSize = infoFont.MeasureString(playTime);
        var timePosition = GetTextPosition(Texture.Width - playTimeSize.X - deltaX, deltaY);
        spriteBatch.DrawString(infoFont, playTime, timePosition, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, TextLayer);

        return;
        Vector2 GetTextPosition(float dx, float dy) => (Position + new Vector2(dx, dy)) * scale;
    }
}