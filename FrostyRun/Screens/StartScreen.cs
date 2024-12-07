using FrostyRun.PD1;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FrostyRun.Screens
{
    public abstract class StartScreen : ScreenBase
    {
        private SpriteFont _titleFont;       // Font for displaying the title
        private SpriteFont _instructionFont; // Font for displaying instructions
        private string _titleText;          // The game title
        private string _instructionText;    // Instruction to start the game
        private Texture2D _backgroundTexture; // Background image for the start screen

        public StartScreen() { }

        public override void LoadContent()
        {
            // Load fonts, textures, or any other resources specific to this screen
            _instructionFont = GameSettings.Content.Load<SpriteFont>("Fonts/InstructionFont");
            _backgroundTexture = GameSettings.Content.Load<Texture2D>("Textures/StartScreenBackground");

            // Set the title and instruction text
            _titleText = "Frosty Run";
            _instructionText = "Press Enter to Start";
        }

        public override void Update(GameTime gameTime)
        {
            // Handle input to transition to the next screen
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                // Transition to PlayScreen
                ScreenManager.ChangeScreen(new PlayScreen());
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // Draw the background
            spriteBatch.Begin();
            spriteBatch.Draw(_backgroundTexture, new Rectangle(0, 0, GameSettings.ScreenWidth, GameSettings.ScreenHeight), Color.White);

            // Draw the title centered at the top
            Vector2 titleSize = _titleFont.MeasureString(_titleText);
            Vector2 titlePosition = new Vector2((GameSettings.ScreenWidth - titleSize.X) / 2, 100);
            spriteBatch.DrawString(_titleFont, _titleText, titlePosition, Color.CornflowerBlue);

            // Draw the instructions centered at the bottom
            Vector2 instructionSize = _instructionFont.MeasureString(_instructionText);
            Vector2 instructionPosition = new Vector2((GameSettings.ScreenWidth - instructionSize.X) / 2, GameSettings.ScreenHeight - 150);
            spriteBatch.DrawString(_instructionFont, _instructionText, instructionPosition, Color.White);

            spriteBatch.End();
        }
    }
}
