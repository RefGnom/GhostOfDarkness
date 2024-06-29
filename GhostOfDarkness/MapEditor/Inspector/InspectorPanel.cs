namespace MapEditor.Inspector;

internal class InspectorPanel : Panel
{
    private List<ItemProperties> itemsProperties = new List<ItemProperties>();

    public InspectorPanel()
    {
        BorderStyle = BorderStyle.FixedSingle;
        SizeChanged += InspectorSizeChanged;
    }

    public void SetItemsProperties(List<ItemProperties> properties)
    {
        foreach (var t in itemsProperties)
        {
            Controls.Remove(t);
        }

        itemsProperties = properties;
        foreach (var t in itemsProperties)
        {
            Controls.Add(t);
        }

        InspectorSizeChanged(this, EventArgs.Empty);
    }

    public void Clear()
    {
        itemsProperties.Clear();
        Controls.Clear();
    }

    public void Add(ItemProperties properties)
    {
        itemsProperties.Add(properties);
        Controls.Add(properties);
    }

    private void InspectorSizeChanged(object? sender, EventArgs e)
    {
        foreach (var t in itemsProperties)
        {
            t.Width = Width;
        }
    }
}