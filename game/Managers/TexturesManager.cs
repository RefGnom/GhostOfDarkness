using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace game.Managers
{
    internal static class TexturesManager
    {
        public static Texture2D Player { get; private set; }
        public static Texture2D Bullet { get; private set; }
        public static Texture2D MeleeEnemy { get; private set; }
        public static Texture2D PauseMenu { get; private set; }

        public static void Load(ContentManager content)
        {
            Player = content.Load<Texture2D>("Player");
            Bullet = content.Load<Texture2D>("Bullet");
            MeleeEnemy = content.Load<Texture2D>("Melee Enemy");
            PauseMenu = content.Load<Texture2D>("Pause Menu");
        }
    }
}
