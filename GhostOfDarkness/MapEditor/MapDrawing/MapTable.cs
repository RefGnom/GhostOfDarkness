using Core;

namespace MapEditor;

internal class MapTable : TableLayoutPanel
{
    private Map? map;

    public Map? Map
    {
        get => map;
        set
        {
            Visible = false;
            map = value;
            if (map is not null)
            {
                Visible = true;
                LayoutUpdate(map);
                map.SizeChanged += () => LayoutUpdate(map);
            }
            MapChanged?.Invoke(map);
        }
    }

    public event Action<Map?>? MapChanged;

    public MapTable()
    {
        Visible = false;
        BackColor = Color.FromArgb(40, 32, 27);
        CellBorderStyle = TableLayoutPanelCellBorderStyle.None;
    }

    public void SwitchGridStyle()
    {
        if (CellBorderStyle == TableLayoutPanelCellBorderStyle.None)
            CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
        else
            CellBorderStyle = TableLayoutPanelCellBorderStyle.None;
    }

    private void LayoutUpdate(Map map)
    {
        var width = (int)map.SizeInTiles.X;
        var height = (int)map.SizeInTiles.Y;
        ColumnCount = width;
        RowCount = height;
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, map.TileSize));
                RowStyles.Add(new RowStyle(SizeType.Absolute, map.TileSize));
            }
        }
        Size = new Size(width * (map.TileSize + 1), height * (map.TileSize + 1));
    }
}