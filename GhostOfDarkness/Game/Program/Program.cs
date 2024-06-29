using Game.Game;
using NUnitLite;

namespace game.Program;

public static class Program
{
    private static void Main(string[] args)
    {
        new AutoRun().Execute(args);
        var game = new GameView();
        //var game = new TestGame();
        game.Run();
    }
}