using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game.View
{
    internal class Animator
    {
        private Vector2 origin;
        private readonly Texture2D texture;
        private readonly int frameWidth;
        private readonly int frameHeight;
        private readonly int[] countFramesInAnimations;
        private readonly int[] countFrames;
        private int countDrawsForUpdateFrame;

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

        public void Draw(Vector2 position, int animation, SpriteBatch spriteBatch)
        {
            var frame = GetFrame(animation);
            spriteBatch.Draw(texture, position, frame, Color.White, 0, origin, 1, SpriteEffects.None, 0);
            IncrementFrame(animation);
        }

        private Rectangle GetFrame(int animation)
        {
            return new Rectangle(countFrames[animation] / countDrawsForUpdateFrame * frameWidth, frameHeight * animation, frameWidth, frameHeight);
        }

        private void IncrementFrame(int animation)
        {
            countFrames[animation] = (countFrames[animation] + 1) % (countFramesInAnimations[animation] * countDrawsForUpdateFrame);
        }
    }
}