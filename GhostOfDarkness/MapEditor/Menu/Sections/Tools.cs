namespace MapEditor;

internal class Tools : ToolStripMenuItem
{
    private readonly ToolStripMenuItem switchGrid = new();

    public event Action? GridOnSwitch;

    public Tools()
    {
        Text = "Инструменты";

        switchGrid.Text = "Включить/выключить сетку";
        switchGrid.Click += (s, e) => GridOnSwitch?.Invoke();
        switchGrid.Enabled = false;
        DropDownItems.Add(switchGrid);
    }

    public void SetSwitchGridEnabled(bool enabled) => switchGrid.Enabled = enabled;
}