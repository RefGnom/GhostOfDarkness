using game;
using NUnitLite;

namespace game.Program;

public class Program
{
    static void Main(string[] args)
    {
        new AutoRun().Execute(args);
        var game = new GameView();
        //var game = new TestGame();
        game.Run();
    }
}