using Core;

namespace MapEditor;

internal class MenuControl
{
    private readonly MapSerializer serializer = new();
    private readonly SaveFileDialog saveFileDialog = new();
    private readonly OpenFileDialog openFileDialog = new();
    private readonly string fileFormat = "json|*.json";

    private readonly ToolStripMenuItem create = new();
    private readonly ToolStripMenuItem open = new();
    private readonly ToolStripMenuItem save = new();

    public MenuStrip Menu { get; init; } = new();

    public event Action? OnCreateFile;
    public event Action<Map>? OnOpenFile;
    public event Func<Map?>? OnSaveFile;

    public MenuControl()
    {
        InitializeMenu();
        InitializeFileDialogs();
    }

    private void InitializeMenu()
    {
        Menu.Renderer = new ToolStripProfessionalRenderer(new MenuColorTable());
        var file = new ToolStripMenuItem()
        {
            Text = "Файл",
        };
        Menu.Items.Add(file);

        create.Text = "Создать";
        create.Click += CreateFile;
        file.DropDownItems.Add(create);

        open.Text = "Открыть";
        open.Click += OpenFile;
        file.DropDownItems.Add(open);

        save.Text = "Сохранить";
        save.Enabled = false;
        save.Click += SaveFile;
        file.DropDownItems.Add(save);

        foreach (ToolStripMenuItem item in Menu.Items)
        {
            SetTextColor(item);
        }
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

    private void SetTextColor(ToolStripMenuItem item)
    {
        item.ForeColor = MenuColorTable.Text;
        foreach (ToolStripMenuItem it in item.DropDownItems)
        {
            SetTextColor(it);
        }
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