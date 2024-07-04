using System;
using System.Linq;
using System.Reflection;
using Core.DependencyInjection;
using Game.Game;

namespace Game.EntryPoint;

public static class EntryPoint
{
    private static void Main(string[] args)
    {
        var currentAssembly = GetCurrentAssembly();
        DiConfigurator.AddAssembly(currentAssembly);
        var serviceProvider = DiConfigurator.Configure();

        var game = new GameView();
        //var game = new TestGame();
        game.Run();
    }

    private static Assembly GetCurrentAssembly() => AppDomain.CurrentDomain.GetAssemblies().Single(x => x.GetName().Name == "Game");
}