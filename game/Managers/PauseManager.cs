using System.Collections.Generic;

namespace game.Managers
{
    internal class PauseManager : IPauseHandler
    {
        private List<IPauseHandler> pauseHandlers = new();
        public bool IsPaused { get; private set; }

        public void SetPaused(bool isPaused)
        {
            IsPaused = isPaused;
            foreach (var handler in pauseHandlers)
            {
                handler.SetPaused(isPaused);
            }
        }
    }
}
