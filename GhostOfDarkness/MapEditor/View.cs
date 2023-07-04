using Core;

namespace MapEditor;

public class View : Form
{
    private Map? map;
    private readonly Panel mapDriwer = new();

    public View()
    {
        InitializeComponent();
        Initialize();
    }

    private void InitializeComponent()
    {
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
            map = new(32, 10);
            mapDriwer.Visible = true;
        };
        menu.OnOpenFile += (map) =>
        {
            this.map = map;
            mapDriwer.Visible = true;
        };
        menu.OnSaveFile += () => map;
        Controls.Add(menu.Menu);
        #endregion

        #region mapDriwer
        mapDriwer.BackColor = Color.FromArgb(40, 32, 27);
        mapDriwer.Visible = false;
        Controls.Add(mapDriwer);
        #endregion

        FormSizeChanged(this, new EventArgs());
    }

    private void FormSizeChanged(object? sender, EventArgs e)
    {
        var menuHeight = 24;
        var percent = 0.8;
        mapDriwer.Location = new Point(0, menuHeight);
        mapDriwer.Size = new Size((int)(Width * percent), Height - menuHeight);
    }
}