using Core.DependencyInjection;

namespace Core.Saves;

[DiUsage]
public interface ISaveProvider
{
    Save CreateDefaultSave(string name);
}