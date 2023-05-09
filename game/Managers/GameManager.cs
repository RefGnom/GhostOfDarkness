using Microsoft.Xna.Framework;

namespace game.Managers
{
    internal class GameManager
    {
        private static GameManager instance;
        public static GameManager Instance => instance ?? new GameManager();

        public PauseManager PauseManager { get; private set; }

        private GameManager()
        {
            instance = this;
            PauseManager = new();
        }
    }
}
