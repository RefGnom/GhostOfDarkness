using Game.Game;

namespace game.Program;

public static class Program
{
    private static void Main(string[] args)
    {
        var game = new GameView();
        //var game = new TestGame();
        game.Run();
    }
}