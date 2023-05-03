using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace game.Managers
{
    internal static class KeyboardManager
    {
        private static Dictionary<Keys, bool> isPressedKeys = new();

        public static bool IsSingleDown(Keys key)
        {
            if (!isPressedKeys.ContainsKey(key))
                isPressedKeys[key] = false;

            if (Keyboard.GetState().IsKeyDown(key) && !isPressedKeys[key])
            {
                isPressedKeys[key] = true;
                return true;
            }
            if (Keyboard.GetState().IsKeyUp(key) && isPressedKeys[key])
            {
                isPressedKeys[key] = false;
            }
            return false;
        }
    }
}
