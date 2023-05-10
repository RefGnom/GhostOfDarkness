using game.View;

namespace game.Managers
{
    internal class GameManager
    {
        private static GameManager instance;
        public static GameManager Instance => instance ?? new GameManager();

        public PauseManager PauseManager { get; private set; }
        public Drawer Drawer { get; private set; }
        public CollisionDetecter CollisionDetecter { get; private set; }
        public EnemiesManager EnemiesManager { get; private set; }

        private GameManager()
        {
            instance = this;
            PauseManager = new();
            Drawer = new();
            CollisionDetecter = new();
            EnemiesManager = new();
        }
    }
}