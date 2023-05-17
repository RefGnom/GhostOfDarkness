using System;
using System.Collections.Generic;

namespace game;

internal static class AnimatorsCreator
{
    private static readonly Dictionary<string, Func<Animator>> animators = new()
    {
        ["Melee Enemy"] = () => new(TexturesManager.MeleeEnemy, 32, 32, new int[] { 5, 8, 7, 3, 7 }, 5),
        ["Player"] = () => new(TexturesManager.Player, 26, 38, new int[] { 1 }, 1),
    };

    public static Animator GetAnimator(string spriteName)
    {
        return animators[spriteName]();
    }
}