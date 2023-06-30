namespace MapEditor;

public class View : Form
{
    private MenuStrip menu = new();

    public View()
    {
        InitializeComponent();
        InitializeWindow();
        InitializeMenu();
    }

    private void InitializeComponent()
    {
    }

    private void InitializeWindow()
    {
        ClientSize = new Size(1280, 720);
        Name = "MapEditor";
    }

    private void InitializeMenu()
    {
        #region menu
        var file = new ToolStripMenuItem()
        {
            Text = "Файл",
        };
        var create = new ToolStripMenuItem()
        {
            Text = "Создать",
        };
        create.Click += CreateFile;
        file.DropDownItems.Add(create);
        var open = new ToolStripMenuItem()
        {
            Text = "Открыть",
        };
        open.Click += OpenFile;
        file.DropDownItems.Add(open);
        menu.Items.Add(file);

        menu.Renderer = new ToolStripProfessionalRenderer(new MenuColorTable());
        MainMenuStrip = menu;
        foreach (ToolStripMenuItem item in menu.Items)
        {
            SetTextColor(item);
        }
        Controls.Add(menu);
        #endregion
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

    }

    private void OpenFile(object? sender, EventArgs e)
    {

    }
}