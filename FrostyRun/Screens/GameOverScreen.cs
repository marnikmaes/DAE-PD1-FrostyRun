using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using FrostyRun.PD1;  // Adjust based on your project structure
using FrostyRun.Screens; // Adjust based on your project structure

namespace FrostyRun.Screens
{
    public abstract class GameOverScreen : ScreenBase
    {
        private string _gameOverText;
        private string _restartText;
        private SpriteFont _font;
        private Texture2D _backgroundTexture;

        public GameOverScreen()
        {
            // Default text for the game over screen
            _gameOverText = "Game Over!";
            _restartText = "Press Enter to Restart or Escape to Exit";
        }

        public override void LoadContent()
        {
            // Load font and background texture
            _backgroundTexture = Game.Content.Load<Texture2D>("Textures/GameOverBackground");
        }

        public override void Update(GameTime gameTime)
        {
            // Handle input for restarting or exiting
            var keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Enter))
            {
                // Transition to the PlayScreen (restart the game)
                ScreenManager.ChangeScreen(new PlayScreen());
            }
            else if (keyboardState.IsKeyDown(Keys.Escape))
            {
                // Exit the game
                Game.Exit();
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // Draw the background
            spriteBatch.Begin();
            spriteBatch.Draw(_backgroundTexture, Vector2.Zero, Color.White);

            // Draw the "Game Over" text
            Vector2 gameOverPosition = new Vector2(GameSettings.ScreenWidth / 2 - _font.MeasureString(_gameOverText).X / 2, GameSettings.ScreenHeight / 4);
            spriteBatch.DrawString(_font, _gameOverText, gameOverPosition, Color.Red);

            // Draw the restart or exit prompt
            Vector2 restartPosition = new Vector2(GameSettings.ScreenWidth / 2 - _font.MeasureString(_restartText).X / 2, GameSettings.ScreenHeight / 2);
            spriteBatch.DrawString(_font, _restartText, restartPosition, Color.White);

            spriteBatch.End();
        }
    }
}
