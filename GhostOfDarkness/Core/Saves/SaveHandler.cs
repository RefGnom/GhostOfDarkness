using System.Transactions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Core.Saves;

public class SaveHandler : ISaveHandler
{
    private const string savesBasePath = "../../../../Saves";
    private const string saveExtension = "sv";
    private const string saveInfoExtension = "sv.meta";

    private readonly JsonSerializerSettings settings;

    public SaveHandler()
    {
        settings = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            TypeNameHandling = TypeNameHandling.Auto
        };
        settings.Converters.Add(new BinaryConverter());

        Directory.CreateDirectory(savesBasePath);
    }

    public void Create(Save save, string saveName)
    {
        using var transactionScope = new TransactionScope();

        var savePath = GetSavePath(saveName);
        var saveJson = JsonConvert.SerializeObject(save, settings);
        File.WriteAllText(savePath, saveJson);

        var saveInfoPath = GetSaveInfoPath(saveName);
        var saveInfoJson = JsonConvert.SerializeObject(save.Info, settings);
        File.WriteAllText(saveInfoPath, saveInfoJson);

        transactionScope.Complete();
    }

    public Save Get(string saveName)
    {
        using var transactionScope = new TransactionScope();

        var savePath = GetSavePath(saveName);
        var saveJson = File.ReadAllText(savePath);
        var save = JsonConvert.DeserializeObject<Save>(saveJson, settings)!;

        var saveInfoPath = GetSaveInfoPath(saveName);
        var saveInfoJson = File.ReadAllText(saveInfoPath);
        var saveInfo = JsonConvert.DeserializeObject<SaveInfo>(saveInfoJson, settings)!;

        transactionScope.Complete();

        save.Info = saveInfo;
        return save;
    }

    public SaveInfo[] Select()
    {
        return Directory.GetFiles(savesBasePath)
            .Where(x => x.EndsWith(saveInfoExtension))
            .Select(File.ReadAllText)
            .Select(JsonConvert.DeserializeObject<SaveInfo>)
            .ToArray()!;
    }

    public void Delete(string saveName)
    {
        using var transactionScope = new TransactionScope();

        var savePath = GetSavePath(saveName);
        File.Delete(savePath);

        var saveInfoPath = GetSaveInfoPath(saveName);
        File.Delete(saveInfoPath);

        transactionScope.Complete();
    }

    private static string GetSavePath(string saveName)
    {
        var path = Path.Combine(savesBasePath, saveName);
        return Path.ChangeExtension(path, saveExtension);
    }

    private static string GetSaveInfoPath(string saveName)
    {
        var path = Path.Combine(savesBasePath, saveName);
        return Path.ChangeExtension(path, saveInfoExtension);
    }
}