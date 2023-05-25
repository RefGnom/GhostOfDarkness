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

    public GameModel()
    {
    }

    public void Start()
    {
        World = new();
        var x = 400;
        var y = 320;
        for (int j = 0; j < 2; j++)
        {
            for (int i = 0; i < 2; i++)
            {
                World.CurrentRoom.CreateEnemy(new MeleeEnemy(World.CurrentRoom.Center - new Vector2(x, y), 80));
                x -= 30;
            }
            x = 400;
            y -= 40;
        }
        Player = new(World.CurrentRoom.Center, 230f, 100, 0.1f);
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
