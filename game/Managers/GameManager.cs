namespace game;

internal class GameManager
{
    private static GameManager instance;
    public static GameManager Instance => instance ?? new GameManager();

    public PauseManager PauseManager { get; private set; }
    public Drawer Drawer { get; private set; }
    public CollisionDetecter CollisionDetecter { get; private set; }
    public Camera Camera { get; private set; }

    private GameManager()
    {
        instance = this;
        PauseManager = new();
        Drawer = new();
        CollisionDetecter = new();
        Camera = new(true);
    }
}