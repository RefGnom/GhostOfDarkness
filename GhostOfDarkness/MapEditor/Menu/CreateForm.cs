namespace MapEditor;

internal class CreateForm : Form
{
    private readonly Label label;
    private readonly TextBox textBox;
    private readonly Button buttonSave;

    public event Action<string>? OnCreate;

    public CreateForm()
    {
        Location = new Point(0, 0);
        Size = new Size(400, 300);
        StartPosition = FormStartPosition.CenterParent;
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        MinimizeBox = false;
        Text = "Создать карту";

        var size = new Size(Size.Width, 25);

        label = new()
        {
            Text = "Введите имя файла",
            TextAlign = ContentAlignment.MiddleCenter,
            Location = new Point(0, 80),
            Size = size,
        };
        Controls.Add(label);

        textBox = new()
        {
            Location = new Point(0, label.Bottom),
            Size = size,
        };
        Controls.Add(textBox);

        buttonSave = new()
        {
            Text = "Создать",
            Location = new Point(0, textBox.Bottom),
            Size = size,
            Enabled = false,
        };
        buttonSave.Click += Create;
        Controls.Add(buttonSave);

        textBox.TextChanged += (s, e) => buttonSave.Enabled = textBox.Text.Trim() != string.Empty;
    }

    private void Create(object? sender, EventArgs e)
    {
        OnCreate?.Invoke(textBox.Text);
        Close();
    }
}