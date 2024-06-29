namespace Core.Saves;

public interface ISaveProvider
{
    Save CreateDefaultSave(string name);
}