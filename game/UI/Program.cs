using NUnitLite;

public class Program
{
    static void Main(string[] args)
    {
        new AutoRun().Execute(args); 
        var game = new game.View.Game1();
        game.Run();
    }
}