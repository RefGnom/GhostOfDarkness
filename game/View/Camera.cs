using Microsoft.Xna.Framework;

namespace game;

internal class Camera
{
    private readonly float maxScale = 1.4f;
    private readonly float minScale = 0.8f;
    private float scale = 1f;
    private float scaleStepSize = 0.05f;

    public bool FollowPlayer { get; set; }
    public Matrix Transform { get; private set; }

    public Camera(bool followPlayer = false)
    {
        Transform = Matrix.Identity;
        FollowPlayer = followPlayer;
    }

    public void Follow(Vector2 playerPosition, int width, int height, float deltaTime)
    {
        if (!FollowPlayer)
            return;

        var position = Matrix.CreateTranslation(-playerPosition.X, -playerPosition.Y, 0);
        var offset = Matrix.CreateTranslation(width / 2 / scale, height / 2 / scale, 0);
        var scaleMatrix = Matrix.CreateScale(scale);

        Transform = position * offset * scaleMatrix;
    }

    public void ChangeScale(int amount)
    {
        if ((scale <= maxScale || amount < 0)
            && (scale >= minScale || amount > 0))
            scale += amount * scaleStepSize;
    }

    public Vector2 ScreenToWorld(Vector2 value)
    {
        return Vector2.Transform(value, Matrix.Invert(Transform));
    }

    public Vector2 WorldToScreen(Vector2 value)
    {
        return Vector2.Transform(value, Transform);
    }
}