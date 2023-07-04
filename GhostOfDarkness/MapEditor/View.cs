using Microsoft.Xna.Framework;

namespace MapEditor;

public class View : Form
{
    private readonly MenuControl menu = new();
    private readonly MapPanel mapPanel = new();
    private readonly InspectorPanel inspector = new();

    public static bool SpacePressed { get; private set; }

    public View()
    {
        InitializeComponent();
        Initialize();
        FormSizeChanged(this, new EventArgs());
        KeyDown += (s, e) => SpacePressed = e.KeyCode == Keys.Space;
        KeyUp += (s, e) => SpacePressed = SpacePressed && e.KeyCode != Keys.Space;
    }

    private void InitializeComponent()
    {
    }

    private void Initialize()
    {
        ClientSize = new Size(1280, 720);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        DoubleBuffered = true;
        Text = "MapEditor";
        SizeChanged += FormSizeChanged;

        #region menu
        Controls.Add(menu);
        MainMenuStrip = menu;
        menu.File.OnCreateFile += () =>
        {
            mapPanel.Table.Map = new(20, 12);
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
        mapPanel.Table.MapChanged += (map) =>
        {
            var empty = map is null;
            menu.Tools.SetSwitchGridEnabled(!empty);
            if (!empty)
            {
                inspector.Clear();
                var property = new MapProperties((int)(Width * 0.25));
                property.GotFocus += (s, e) => ActiveControl = null;
                var size = mapPanel.Table.Map.SizeInTiles;
                property.WidthValue = size.X.ToString();
                property.HeightValue = size.Y.ToString();
                property.MapSizeChanged += (value) => mapPanel.Table.Map.SizeInTiles = new Vector2(value.X, value.Y);
                inspector.Add(property);
            }
        };
        #endregion

        #region inspector
        Controls.Add(inspector);
        #endregion
    }

    private void FormSizeChanged(object? sender, EventArgs e)
    {
        var menuHeight = 24;
        var percent = 0.75;

        mapPanel.Location = new System.Drawing.Point(0, menuHeight);
        mapPanel.Size = new Size((int)(Width * percent), Height - menuHeight);

        inspector.Location = new System.Drawing.Point(mapPanel.Right, menuHeight);
        inspector.Size = new Size((int)(Width * (1 - percent)), Height - menuHeight);
    }
}