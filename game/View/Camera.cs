using Microsoft.Xna.Framework;

namespace game.View;

internal class Camera
{
    public Matrix Transform { get; private set; }

    public void Follow(Vector2 playerPosition, int width, int height)
    {
        var position = Matrix.CreateTranslation(-playerPosition.X, -playerPosition.Y, 0);
        var offset = Matrix.CreateTranslation(width / 2, height / 2, 0);

        Transform = position * offset;
    }
}