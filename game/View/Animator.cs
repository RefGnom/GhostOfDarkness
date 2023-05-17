using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game;

internal class Animator
{
    public readonly Vector2 Origin;
    private readonly Texture2D texture;
    private readonly int frameWidth;
    private readonly int frameHeight;
    private readonly int[] countFramesInAnimations;
    private readonly int[] countFrames;
    private readonly int countDrawsForUpdateFrame;
    private bool animationLooped;

    private int currentAnimation;

    public int Radius => (frameHeight + frameWidth) / 4;

    public Animator(Texture2D texture, int frameWidth, int frameHeight, int[] countFramesInAnimations, int countDrawsForUpdateFrame)
    {
        this.texture = texture;
        this.frameWidth = frameWidth;
        this.frameHeight = frameHeight;
        Origin = new Vector2(frameWidth / 2, frameHeight / 2);
        this.countFramesInAnimations = countFramesInAnimations;
        countFrames = new int[countFramesInAnimations.Length];
        this.countDrawsForUpdateFrame = countDrawsForUpdateFrame;
    }

    public float GetAnimationTime(int animation)
    {
        return countFramesInAnimations[animation] * countDrawsForUpdateFrame / 30f;
    }

    public void SetAnimation(int animation, bool looped = true)
    {
        currentAnimation = animation;
        animationLooped = looped;
    }

    public void Draw(Vector2 position, SpriteBatch spriteBatch, SpriteEffects flip, float layerDepth, float scale = 1f)
    {
        var frame = GetFrame();
        spriteBatch.Draw(texture, position, frame, Color.White, 0, Origin, scale, flip, layerDepth);
        IncrementFrame();
    }

    public void Draw(Vector2 position, SpriteBatch spriteBatch, SpriteEffects flip, float rotation, float layerDepth, float scale)
    {
        var frame = GetFrame();
        spriteBatch.Draw(texture, position, frame, Color.White, rotation, Origin, scale, flip, layerDepth);
        IncrementFrame();
    }

    private Rectangle GetFrame()
    {
        return new Rectangle(countFrames[currentAnimation] / countDrawsForUpdateFrame * frameWidth,
            frameHeight * currentAnimation, frameWidth, frameHeight);
    }

    private void IncrementFrame()
    {
        if (GameController.IsPaused)
            return;
        var lastFrame = countFrames[currentAnimation];
        countFrames[currentAnimation] = (countFrames[currentAnimation] + 1)
            % (countFramesInAnimations[currentAnimation] * countDrawsForUpdateFrame);
        if (!animationLooped && countFrames[currentAnimation] == 0)
            countFrames[currentAnimation] = lastFrame;
    }
}