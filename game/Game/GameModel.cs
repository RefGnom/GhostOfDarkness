using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace game;

internal class GameModel
{
    private static readonly List<IInteractable> interactables = new();

    public World World { get; private set; }
    public Player Player { get; private set; }

    public GameModel()
    {
        World = new();
        World.CurrentRoom.CreateEnemy(new MeleeEnemy(World.CurrentRoom.Center + new Vector2(64, 64), 80));
        Player = new(World.CurrentRoom.Center, 230f, 100, 0.3f);
    }

    public void Update(float deltaTime)
    {
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
