using Microsoft.Xna.Framework;

namespace game;

internal class InteractableDoor : IInteractable
{
    private readonly Door first;
    private readonly Door second;
    private readonly float interactionDistance = 50;
    private float currentInteractionCooldown;
    private string hint = "Open";
    private bool hintShown;
    private bool isOpen;

    public Vector2 Position => (first.Center + second.Center) / 2;
    public bool HasInteract { get; private set; }
    public float InteractionCooldown => 1;

    public InteractableDoor(Door first, Door second)
    {
        this.first = first;
        this.second = second;
    }

    public bool CanInteract(Vector2 target)
    {
        var distance = Vector2.Distance(Position, target);
        if (distance <= interactionDistance && currentInteractionCooldown <= 0)
        {
            HintManager.Show(hint + " door - E");
            hintShown = true;
            return true;
        }
        if (hintShown)
        {
            HintManager.Hide();
            hintShown = false;
        }
        return false;
    }

    public void Interact()
    {
        if (isOpen)
        {
            isOpen = false;
            hint = "Close";
            first.Close();
            second.Close();
        }
        else
        {
            isOpen = true;
            hint = "Open";
            first.Open();
            second.Open();
        }

        currentInteractionCooldown = InteractionCooldown;
    }

    public void Update(float deltaTime)
    {
        currentInteractionCooldown -= deltaTime;
    }
}