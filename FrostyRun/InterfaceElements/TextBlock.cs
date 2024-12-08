using FrostyRun.Common;
using FrostyRun.PD1;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FrostyRun.InterfaceElements
{
    public class TextBlock : Component
    {
        private SpriteFont _font;
        private string _text;

        public Vector2 Position { get; set; }

        public TextBlock(SpriteFont font, string text)
        {
            _font = font;
            _text = text;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var textWidth = _font.MeasureString(_text).X;
            var centeredPosition = new Vector2(
                (GameSettings.ScreenWidth - textWidth) / 2,
                Position.Y
            );

            spriteBatch.DrawString(_font, _text, centeredPosition, Color.Black);
        }

        public override void Update(GameTime gameTime)
        {
            // No logic needed for static text
        }
    }
}
