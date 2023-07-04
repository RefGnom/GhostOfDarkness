namespace MapEditor;

internal abstract class ItemProperties : TableLayoutPanel
{
    public readonly int RowHeight = 30;
    public readonly float DescriptionPercent = 0.3f;
    public readonly float PropertyPercent = 0.7f;

    public ItemProperties(int width)
    {
        Size = new Size(width, 0);
        ColumnCount = 2;
        ColumnStyles.Add(new ColumnStyle(SizeType.Percent, DescriptionPercent));
        ColumnStyles.Add(new ColumnStyle(SizeType.Percent, PropertyPercent));
    }

    public void AddRow(RowStyle style)
    {
        RowCount++;
        RowStyles.Add(style);
        Size = new Size(Width, Height + RowHeight);
    }
}