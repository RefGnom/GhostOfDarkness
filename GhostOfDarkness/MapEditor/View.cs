namespace MapEditor;

public class View : Form
{
    private readonly MapPanel mapPanel = new();

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
        var menu = new MenuControl();
        menu.OnCreateFile += () =>
        {
            mapPanel.Table.Map = new(70, 50);
        };
        menu.OnOpenFile += (map) =>
        {
            mapPanel.Table.Map = map;
        };
        menu.OnSaveFile += () => mapPanel.Table.Map;
        Controls.Add(menu.Menu);
        #endregion
        #region mapPanel
        mapPanel.BorderStyle = BorderStyle.FixedSingle;
        Controls.Add(mapPanel);
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