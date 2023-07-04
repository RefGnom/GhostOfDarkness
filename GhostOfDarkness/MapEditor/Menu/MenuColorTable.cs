namespace MapEditor;

internal class MenuColorTable : ProfessionalColorTable
{
    public static readonly Color Text = Color.FromArgb(220, 220, 220);

    private readonly Color back = Color.FromArgb(60, 60, 60);
    private readonly Color selected = Color.FromArgb(80, 80, 80);
    private readonly Color border = Color.FromArgb(110, 110, 110);

    public override Color MenuItemSelected => selected;

    public override Color ToolStripDropDownBackground => back;

    public override Color ImageMarginGradientBegin => back;

    public override Color ImageMarginGradientEnd => back;

    public override Color ImageMarginGradientMiddle => back;

    public override Color MenuItemSelectedGradientBegin => selected;

    public override Color MenuItemSelectedGradientEnd => selected;

    public override Color MenuItemPressedGradientBegin => selected;

    public override Color MenuItemPressedGradientMiddle => selected;

    public override Color MenuItemPressedGradientEnd => selected;

    public override Color MenuItemBorder => border;

    public override Color MenuStripGradientBegin => back;

    public override Color MenuStripGradientEnd => back;
}