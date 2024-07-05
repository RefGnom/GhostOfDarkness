using System;
using System.Linq;
using System.Reflection;
using Core.DependencyInjection;
using Game.Configuration;
using Game.Game;
using Microsoft.Extensions.DependencyInjection;

namespace Game.EntryPoint;

public static class EntryPoint
{
    public static void Main(string[] args)
    {
        var currentAssembly = GetCurrentAssembly();
        DiConfigurator.AddAssembly(currentAssembly);
        DiConfigurator.OnConfigure += Configure;
        DiConfiguration.ServiceProvider = DiConfigurator.Configure();

        var game = new GameView();
        //var game = new TestGame();
        game.Run();
    }

    private static void Configure(IServiceCollection serviceCollection)
    {
    }
    private static Assembly GetCurrentAssembly() => AppDomain.CurrentDomain.GetAssemblies().Single(x => x.GetName().Name == "Game");
}