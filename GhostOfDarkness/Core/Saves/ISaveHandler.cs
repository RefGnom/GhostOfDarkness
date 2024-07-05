using Core.DependencyInjection;

namespace Core.Saves;

[DiUsage]
public interface ISaveHandler
{
    public void Create(Save save, string saveName);
    public Save Get(string saveName);
    public SaveInfo[] Select();
    public void Delete(string saveName);
}