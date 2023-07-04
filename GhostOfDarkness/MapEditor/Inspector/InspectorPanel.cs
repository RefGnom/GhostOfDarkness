namespace MapEditor;

internal class InspectorPanel : Panel
{
    private List<ItemProperties> itemsProperties = new();

    public InspectorPanel()
    {
        BorderStyle = BorderStyle.FixedSingle;
        SizeChanged += InspectorSizeChanged;
    }

    public void SetItemsProperies(List<ItemProperties> properties)
    {
        for (int i = 0; i < itemsProperties.Count; i++)
            Controls.Remove(itemsProperties[i]);
        itemsProperties = properties;
        for (int i = 0; i < itemsProperties.Count; i++)
            Controls.Add(itemsProperties[i]);
        InspectorSizeChanged(this, new EventArgs());
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
        for (int i = 0; i < itemsProperties.Count; i++)
            itemsProperties[i].Width = Width;
    }
}