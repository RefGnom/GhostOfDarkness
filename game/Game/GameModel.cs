using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace game;

internal class GameModel
{
    private static readonly List<IInteractable> interactables = new();

    public World World { get; private set; }
    public Player Player { get; private set; }
    public bool Started { get; private set; }

    public void Start()
    {
        World = new();
        World.Generate(2);
        Player = new(World.CurrentRoom.Center + new Vector2(-80, 16), 330f, 100, 25, 0.2f);
        Player.Attack = MouseController.LeftButtonPressed;
        Started = true;
    }

    public void Delete()
    {
        if (Started)
        {
            Creature.DeleteFromLocation(Player);
            World.Delete();
            Started = false;
        }
    }

    public void Update(float deltaTime)
    {
        if (!Started)
            return;
        World.Update(deltaTime, Player);
        Player.Update(deltaTime);
        UpdateInteractables(deltaTime);
    }

    public static void AddInteractable(IInteractable interactable)
    {
        interactables.Add(interactable);
    }

    private void UpdateInteractables(float deltaTime)
    {
        foreach (var item in interactables)
        {
            if (item.CanInteract(Player.Position))
            {
                if (KeyboardController.IsSingleKeyDown(Keys.E))
                    item.Interact();
            }
            item.Update(deltaTime);
        }
    }
}
