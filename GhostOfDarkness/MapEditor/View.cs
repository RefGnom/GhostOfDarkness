namespace MapEditor;

public class View : Form
{
    private readonly MapPanel mapPanel = new();
    private readonly MenuControl menu = new();

    public static bool SpacePressed { get; private set; }

    public View()
    {
        InitializeComponent();
        Initialize();
        KeyDown += (s, e) => SpacePressed = e.KeyCode == Keys.Space;
        KeyUp += (s, e) => SpacePressed = SpacePressed && e.KeyCode != Keys.Space;
    }

    private void InitializeComponent()
    {
        DoubleBuffered = true;
    }

    private void Initialize()
    {
        ClientSize = new Size(1280, 720);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        Text = "MapEditor";
        SizeChanged += FormSizeChanged;

        #region menu
        Controls.Add(menu);
        menu.File.OnCreateFile += () =>
        {
            mapPanel.Table.Map = new(70, 50);
        };
        menu.File.OnOpenFile += (map) =>
        {
            mapPanel.Table.Map = map;
        };
        menu.File.OnSaveFile += () => mapPanel.Table.Map;
        menu.Tools.GridOnSwitch += mapPanel.Table.SwitchGridStyle;
        #endregion

        #region mapPanel
        Controls.Add(mapPanel);
        mapPanel.Table.MapChanged += (map) => menu.Tools.SetSwitchGridEnabled(map is not null);
        #endregion

        FormSizeChanged(this, new EventArgs());
    }

    private void FormSizeChanged(object? sender, EventArgs e)
    {
        var menuHeight = 24;
        var percent = 0.8;
        mapPanel.Location = new Point(0, menuHeight);
        mapPanel.Size = new Size((int)(Width * percent), Height - menuHeight);
    }
}