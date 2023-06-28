using System;
using System.Collections.Generic;

namespace game;

internal static class AnimatorsCreator
{
    private static readonly Dictionary<string, Func<Animator>> animators = new()
    {
        ["Melee Enemy"] = () => new(Textures.MeleeEnemy, 32, 32, new int[] { 5, 8, 7, 3, 7 }, 5),
        ["Player"] = () => new(Textures.Player, 26, 38, new int[] { 1 }, 1),
        ["Boss"] = () => new(Textures.Boss, 100, 100, new int[] { 24, 24, 24, 24, 24, 24, 24, 24, 24, 12, 12, 12, 12, 12, 12, 12, 12 }, 3),
    };

    public static Animator GetAnimator(string spriteName)
    {
        return animators[spriteName]();
    }
}