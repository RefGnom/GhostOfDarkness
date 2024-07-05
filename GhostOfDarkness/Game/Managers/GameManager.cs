using Game.View;

namespace Game.Managers;

internal class GameManager
{
    private static GameManager instance;
    public static GameManager Instance => instance ?? new GameManager();

    public Drawer Drawer { get; private set; }
    public DialogManager DialogManager { get; private set; }
    public CollisionDetector CollisionDetector { get; private set; }
    public Camera Camera { get; private set; }

    private GameManager()
    {
        instance = this;
        Drawer = new Drawer();
        DialogManager = new DialogManager();
        CollisionDetector = new CollisionDetector();
        Camera = new Camera(true);
    }

    public void Update()
    {
        DialogManager.Update();
    }

    public static void Delete()
    {
        instance = null;
    }
}