using game.CreatureStates;
using game.Interfaces;
using game.View;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace game.Creatures;

internal class MeleeEnemyView : EnemyView
{
    private static readonly Dictionary<string, int> animations = new Dictionary<string, int>()
    {
        ["idle"] = 0,
        ["run"] = 1,
        ["attack"] = 2,
        ["take damage"] = 3,
        ["dead"] = 4,
    };

    public MeleeEnemyView() : base(AnimatorsCreator.GetAnimator("Melee Enemy"), animations)
    {
    }
}