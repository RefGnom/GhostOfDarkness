namespace game;

internal class GameModel
{
    public World World { get; private set; }
    public Player Player { get; private set; }
    
    public GameModel()
    {
        World = new();
        //World.CurrentRoom.CreateEnemy(new MeleeEnemy(World.CurrentRoom.Center, 40));
        Player = new(World.CurrentRoom.Center, 230f, 100, 0.3f);
    }

    public void Update(float deltaTime)
    {
        World.CurrentRoom.Update(deltaTime, Player);
        Player.Update(deltaTime);
    }
}
