using Microsoft.Xna.Framework;

namespace Game.View;

public class Camera
{
    private const float maxScale = 1.4f;
    private const float minScale = 0.8f;
    private const float scaleStepSize = 0.05f;
    private float scale;
    private int screenWidth;
    private int screenHeight;

    public bool FollowPlayer { get; set; }
    public Matrix Transform { get; private set; }

    public Camera(bool followPlayer = false)
    {
        Transform = Matrix.Identity;
        FollowPlayer = followPlayer;
        scale = maxScale;
    }

    public void Follow(Vector2 playerPosition, int width, int height, float deltaTime)
    {
        if (!FollowPlayer)
        {
            return;
        }

        screenWidth = width;
        screenHeight = height;

        var position = Matrix.CreateTranslation(-playerPosition.X, -playerPosition.Y, 0);
        var offset = Matrix.CreateTranslation(screenWidth / 2f / scale, screenHeight / 2f / scale, 0);
        var scaleMatrix = Matrix.CreateScale(scale);

        Transform = position * offset * scaleMatrix;
    }

    public void ChangeScale(int amount)
    {
        if ((scale <= maxScale || amount < 0)
            && (scale >= minScale || amount > 0))
        {
            scale += amount * scaleStepSize;
        }
    }

    public Vector2 ScreenToWorld(Vector2 value) => Vector2.Transform(value, Matrix.Invert(Transform));

    public Vector2 WorldToScreen(Vector2 value) => Vector2.Transform(value, Transform);

    public Rectangle GetVisibleArea()
    {
        var inverseViewMatrix = Matrix.Invert(Transform);
        var topLeft = Vector2.Transform(Vector2.Zero, inverseViewMatrix);
        var topRight = Vector2.Transform(new Vector2(screenWidth, 0), inverseViewMatrix);
        var bottomLeft = Vector2.Transform(new Vector2(0, screenHeight), inverseViewMatrix);
        var bottomRight = Vector2.Transform(new Vector2(screenWidth, screenHeight), inverseViewMatrix);
        var min = new Vector2(
            MathHelper.Min(topLeft.X, MathHelper.Min(topRight.X, MathHelper.Min(bottomLeft.X, bottomRight.X))),
            MathHelper.Min(topLeft.Y, MathHelper.Min(topRight.Y, MathHelper.Min(bottomLeft.Y, bottomRight.Y))));
        var max = new Vector2(
            MathHelper.Max(topLeft.X, MathHelper.Max(topRight.X, MathHelper.Max(bottomLeft.X, bottomRight.X))),
            MathHelper.Max(topLeft.Y, MathHelper.Max(topRight.Y, MathHelper.Max(bottomLeft.Y, bottomRight.Y))));
        return new Rectangle((int)min.X, (int)min.Y, (int)(max.X - min.X), (int)(max.Y - min.Y));
    }
}