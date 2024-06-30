using game;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Controllers;

public class SaveButton : Button
{
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
        spriteBatch.DrawString(Fonts.Common16, saveName, namePosition, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, TextLayer);


        var deltaY = Texture.Height - Fonts.Common12.MeasureString(difficulty).Y - 10;
        var difficultyPosition = GetTextPosition(deltaX, deltaY);
        spriteBatch.DrawString(Fonts.Common12, difficulty, difficultyPosition, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, TextLayer);

        var playTimeSize = Fonts.Common12.MeasureString(playTime);
        var timePosition = GetTextPosition(Texture.Width - playTimeSize.X - deltaX, deltaY);
        spriteBatch.DrawString(Fonts.Common12, playTime, timePosition, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, TextLayer);

        return;
        Vector2 GetTextPosition(float dx, float dy) => (Position + new Vector2(dx, dy)) * scale;
    }
}