namespace MapEditor;

internal class MapPanel : Panel
{
    private int tableLeft, tableTop, tableRight, tableBottom, mouseX, mouseY;
    public readonly MapTable Table = new();

    public MapPanel()
    {
        Table.MouseDown += TableMouseDown;
        Table.MouseMove += TableMouseMove;
        Controls.Add(Table);
    }

    private void TableMouseDown(object? sender, MouseEventArgs e)
    {
        tableLeft = Table.Left;
        tableTop = Table.Top;
        tableRight = Table.Right;
        tableBottom = Table.Bottom;
        mouseX = MousePosition.X;
        mouseY = MousePosition.Y;
    }

    private void TableMouseMove(object? sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
            MoveTable();
    }

    private void MoveTable()
    {
        if (!View.SpacePressed)
            return;
        var deltaX = MousePosition.X - mouseX;
        int deltaY = MousePosition.Y - mouseY;
        Table.Location = GetNewLocation(deltaX, deltaY);
    }

    private Point GetNewLocation(int deltaX, int deltaY)
    {
        var boundWidth = 400;
        var newX = Table.Left;
        var newY = Table.Top;
        if (tableTop + deltaY < Bottom - boundWidth
            && tableBottom + deltaY > Top + boundWidth)
            newY = tableTop + deltaY;

        if (tableLeft + deltaX < Right - boundWidth
            && tableRight + deltaX > Left + boundWidth)
            newX = tableLeft + deltaX;
        return new Point(newX, newY);
    }
}