using System.Collections.Generic;

namespace game;

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