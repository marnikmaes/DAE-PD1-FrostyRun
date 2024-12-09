using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FrostyRun.PD1
{
    public static partial class GameSettings
    {
        #region Screen Properties
        public static int ScreenWidth { get; private set; } = 1200;
        public static int ScreenHeight { get; private set; } = 800;

        public static Vector2 ScreenSize => new Vector2(ScreenWidth, ScreenHeight);
        #endregion

        #region Textures and Size
        public static Texture2D IceBlockTexture { get; set; }
        public static Texture2D IceSpikeTexture { get; set; }
        public static Texture2D FrostyHeadTexture { get; set; }
        public static Texture2D FrostyBodyTexture { get; set; }

        public static Texture2D StartScreenBg { get; set; }
        public static Texture2D Button { get; set;}
        public static SpriteFont SpriteFont { get; set; }

        public static Vector2 BlockSize { get; set; } = new Vector2(50, 50);
        #endregion

        public static float StartingSpeed { get; set; } = 1f;
        public static Vector2 GameDirection { get; set; } = new Vector2(-1, 0);
        public static int SpikeChance { get; set; } = 8;
    }
}
