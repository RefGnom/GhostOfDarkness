namespace MapEditor;

internal class MenuControl : MenuStrip
{
    public readonly File File = new();
    public readonly Tools Tools = new();

    public MenuControl()
    {
        Renderer = new ToolStripProfessionalRenderer(new MenuColorTable());
        Items.Add(File);
        Items.Add(Tools);

        foreach (ToolStripMenuItem item in Items)
        {
            SetTextColor(item);
        }
    }

    private void SetTextColor(ToolStripMenuItem item)
    {
        item.ForeColor = MenuColorTable.Text;
        foreach (ToolStripMenuItem it in item.DropDownItems)
        {
            SetTextColor(it);
        }
    }
}