using System.Collections.Generic;
using game.Interfaces;

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

        public void RegisterHandler(IPauseHandler handler)
        {
            pauseHandlers.Add(handler);
        }

        public void UnregisterHandler(IPauseHandler handler)
        {
            pauseHandlers.Remove(handler);
        }
    }
}
