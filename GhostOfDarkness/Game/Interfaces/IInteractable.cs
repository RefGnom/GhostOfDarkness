using Microsoft.Xna.Framework;

namespace Game.Interfaces;

internal interface IInteractable
{
    public Vector2 Position { get; }
    public bool HasInteract { get; }
    public float InteractionCooldown { get; }

    public bool CanInteract(Vector2 target);

    public void Interact();

    public void Update(float deltaTime);
}