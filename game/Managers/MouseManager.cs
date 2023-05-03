using Microsoft.Xna.Framework.Input;

namespace game.Managers
{
    internal static class MouseManager
    {
        private static bool leftButtomIsPressed;
        private static bool rightButtomIsPressed;

        public static bool LeftButtomClicked()
        {
            if (Mouse.GetState().LeftButton == ButtonState.Pressed && !leftButtomIsPressed)
            {
                leftButtomIsPressed = true;
                return true;
            }
            if (Mouse.GetState().LeftButton == ButtonState.Released && leftButtomIsPressed)
            {
                leftButtomIsPressed = false;
            }
            return false;
        }

        public static bool RightButtomClicked()
        {
            if (Mouse.GetState().RightButton == ButtonState.Pressed && !rightButtomIsPressed)
            {
                rightButtomIsPressed = true;
                return true;
            }
            if (Mouse.GetState().RightButton == ButtonState.Released && rightButtomIsPressed)
            {
                rightButtomIsPressed = false;
            }
            return false;
        }
    }
}
