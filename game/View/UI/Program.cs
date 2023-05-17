using game;
using NUnitLite;

public class Program
{
    static void Main(string[] args)
    {
        //new AutoRun().Execute(args); 
        var game = new GameView();
        //var game = new TestGame();
        game.Run();
    }
}