using Core.Maps;

namespace Core.Saves;

public class SaveProvider : ISaveProvider
{
    public Save CreateDefaultSave(string name)
        => new Save()
        {
            Info = new SaveInfo()
            {
                Difficulty = 1,
                Name = name,
                PlayTime = TimeSpan.Zero
            },
            Map = new Map(10, 10)
        };
}