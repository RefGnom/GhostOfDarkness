using Core.DependencyInjection;
using Game.Creatures.CreatureStates;

namespace Game.Creatures;

[DiIgnore]
public abstract class EnemyView : CreatureStatesController
{
    protected float ScaleFactor = 1;
    protected Enemy Model;

    public void SetModel(Enemy model)
    {
        Model = model;
    }
}