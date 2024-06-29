using Core.Maps;

namespace MapEditor;

internal class File : ToolStripMenuItem
{
    private readonly MapSerializer serializer = new();
    private readonly SaveFileDialog saveFileDialog = new();
    private readonly OpenFileDialog openFileDialog = new();
    private readonly string fileFormat = "json|*.json";

    private readonly ToolStripMenuItem create = new();
    private readonly ToolStripMenuItem open = new();
    private readonly ToolStripMenuItem save = new();

    public event Action? OnCreateFile;
    public event Action<Map>? OnOpenFile;
    public event Func<Map?>? OnSaveFile;

    public File()
    {
        Text = "Файл";

        create.Text = "Создать";
        create.Click += CreateFile;
        DropDownItems.Add(create);

        open.Text = "Открыть";
        open.Click += OpenFile;
        DropDownItems.Add(open);

        save.Text = "Сохранить";
        save.Enabled = false;
        save.Click += SaveFile;
        DropDownItems.Add(save);

        InitializeFileDialogs();
    }

    private void InitializeFileDialogs()
    {
        openFileDialog.Filter = fileFormat;
        openFileDialog.FileOk += (s, e) =>
        {
            if (s is not OpenFileDialog fileDialog)
                return;
            var map = serializer.Deserialize(fileDialog.FileName);
            OnOpenFile?.Invoke(map);
        };

        saveFileDialog.Filter = fileFormat;
        saveFileDialog.FileOk += (s, e) =>
        {
            if (s is not SaveFileDialog fileDialog)
                return;
            var map = OnSaveFile?.Invoke();
            serializer.Serialize(map, fileDialog.FileName);
        };
    }

    private void CreateFile(object? sender, EventArgs e)
    {
        OnCreateFile?.Invoke();
        save.Enabled = true;
    }

    private void OpenFile(object? sender, EventArgs e)
    {
        if (openFileDialog.ShowDialog() == DialogResult.OK)
            save.Enabled = true;
    }

    private void SaveFile(object? sender, EventArgs e)
    {
        saveFileDialog.ShowDialog();
    }
}