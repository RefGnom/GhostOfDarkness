using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace game.View
{
    internal static class AnimatorsCreator
    {
        private static readonly Dictionary<string, Func<Texture2D, Animator>> animators = new()
        {
            ["Melee Enemy"] = texture => new(texture, 32, 32, new int[] { 5, 8, 7, 3, 7 }, 3),
        };

        public static Animator GetAnimator(string spriteName, Texture2D texture)
        {
            return animators[spriteName](texture);
        }
    }
}
