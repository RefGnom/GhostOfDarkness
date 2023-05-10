using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game.View
{
    internal class Animator
    {
        private readonly Vector2 origin;
        private readonly Texture2D texture;
        private readonly int frameWidth;
        private readonly int frameHeight;
        private readonly int[] countFramesInAnimations;
        private readonly int[] countFrames;
        private int countDrawsForUpdateFrame;
        private bool animationLooped;

        private int currentAnimation;

        public int Radius => (frameHeight + frameWidth) / 4;

        public Animator(Texture2D texture, int frameWidth, int frameHeight, int[] countFramesInAnimations, int countDrawsForUpdateFrame)
        {
            this.texture = texture;
            this.frameWidth = frameWidth;
            this.frameHeight = frameHeight;
            origin = new Vector2(frameWidth / 2, frameHeight / 2);
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

        public void Draw(Vector2 position, SpriteBatch spriteBatch, SpriteEffects flip)
        {
            var frame = GetFrame();
            spriteBatch.Draw(texture, position, frame, Color.White, 0, origin, 1, flip, 0);
            IncrementFrame();
        }

        public void Draw(Vector2 position, SpriteBatch spriteBatch, SpriteEffects flip, float rotation)
        {
            var frame = GetFrame();
            spriteBatch.Draw(texture, position, frame, Color.White, rotation, origin, 1, flip, 0);
            IncrementFrame();
        }

        private Rectangle GetFrame()
        {
            return new Rectangle(countFrames[currentAnimation] / countDrawsForUpdateFrame * frameWidth,
                frameHeight * currentAnimation, frameWidth, frameHeight);
        }

        private void IncrementFrame()
        {
            var lastFrame = countFrames[currentAnimation];
            countFrames[currentAnimation] = (countFrames[currentAnimation] + 1)
                % (countFramesInAnimations[currentAnimation] * countDrawsForUpdateFrame);
            if (!animationLooped && countFrames[currentAnimation] == 0)
                countFrames[currentAnimation] = lastFrame;
        }
    }
}