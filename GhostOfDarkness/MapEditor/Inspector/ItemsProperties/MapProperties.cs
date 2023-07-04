using Microsoft.Xna.Framework;

namespace MapEditor;

internal class MapProperties : ItemProperties
{
    private readonly TextBox widthTextBox = new();
    private readonly TextBox heightTextBox = new();

    public string WidthValue
    {
        get => widthTextBox.Text;
        set => widthTextBox.Text = value;
    }

    public string HeightValue
    {
        get => heightTextBox.Text;
        set => heightTextBox.Text = value;
    }

    public event Action<Vector2>? MapSizeChanged;

    public MapProperties(int width) : base(width)
    {
        AddRow(new RowStyle(SizeType.Absolute, RowHeight));
        AddRow(new RowStyle(SizeType.Absolute, RowHeight));
        AddRow(new RowStyle(SizeType.Absolute, RowHeight));

        Controls.Add(new Label()
        {
            Text = "Размер",
            Font = new Font(Font.Name, Font.Size, FontStyle.Bold),
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleLeft,
        }, 0, 0);

        Controls.Add(new Label()
        {
            Text = "Ширина",
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleLeft,
        }, 0, 1);
        widthTextBox.Dock = DockStyle.Fill;
        widthTextBox.Margin = new Padding(3, 3, 30, 3);
        Controls.Add(widthTextBox, 1, 1);

        Controls.Add(new Label()
        {
            Text = "Высота",
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleLeft,
        }, 0, 2);
        heightTextBox.Dock = DockStyle.Fill;
        heightTextBox.Margin = new Padding(3, 3, 30, 3);
        Controls.Add(heightTextBox, 1, 2);

        widthTextBox.KeyPress += TextBoxKeyPress;
        widthTextBox.KeyDown += TextBoxKeyDown;
        widthTextBox.MaxLength = 3;
        widthTextBox.Name = "Width";
        heightTextBox.KeyPress += TextBoxKeyPress;
        heightTextBox.KeyDown += TextBoxKeyDown;
        heightTextBox.MaxLength = 3;
        heightTextBox.Name = "Height";
    }

    private void TextBoxKeyPress(object? sender, KeyPressEventArgs e)
    {
        e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
    }

    private void TextBoxKeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode != Keys.Enter || sender is not TextBox textBox)
            return;
        Focus();
        MapSizeChanged?.Invoke(new Vector2(int.Parse(widthTextBox.Text), int.Parse(heightTextBox.Text)));
        e.SuppressKeyPress = true;
    }
}