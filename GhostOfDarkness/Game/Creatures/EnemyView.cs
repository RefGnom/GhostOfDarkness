namespace game;

internal abstract class EnemyView : CreatureStatesController
{
    protected float scaleFactor = 1;
    protected Enemy model;

    public void SetModel(Enemy model)
    {
        this.model = model;
    }
}