namespace game;

internal interface IEntity : IDrawable, ICollisionable
{
    public bool CanCollided { get; }
}