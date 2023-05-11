namespace game.Interfaces;

internal interface IEntity : IDrawable, ICollisionable
{
    public bool CanCollided { get; }
}