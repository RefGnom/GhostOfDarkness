namespace game;

internal interface ITrigger
{
    bool IsTriggered(ICollisionable collisionable);
}