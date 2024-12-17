using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FrostyRun.PD1;
using FrostyRun.FrostyElements;
using FrostyRun.Characters;
using Microsoft.Xna.Framework.Content;
using FrostyRun.InterfaceElements;

namespace FrostyRun.States
{
    public class PlayScreen : Screen
    {
        private PlayerCharacter _playerCharacter;
        private Platform _floor;
        private ScoreManager _scoreManager;
        private SpriteFont _font;
        private HighScoreManager _highScoreManager; // Add HighScoreManager

        private const string HighScoreFilePath = "highscoren.txt"; // File to save the high score

        public PlayScreen(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
            : base(game, graphicsDevice, content)
        {
            // Initialize gameplay objects
            _floor = new Platform();

            var head = new PlayerCharacterHead(GameSettings.FrostyHeadTexture, new Vector2(400, 300));
            _playerCharacter = new PlayerCharacter(head);

            // Initialize the ScoreManager and HighScoreManager
            _scoreManager = new ScoreManager();
            _highScoreManager = new HighScoreManager(HighScoreFilePath);

            // Load the high score from the file
            _highScoreManager.LoadHighScore();

            // Load the font for displaying the score
            _font = GameSettings.SpriteFont; // Ensure "ScoreFont" is available in your content
        }

        public override void Update(GameTime gameTime)
        {
            UserInputs.Update(gameTime);

            if (UserInputs.IsLeftClick())
            {
                _playerCharacter.AddBodySegment(GameSettings.FrostyBodyTexture);
            }

            // Update game objects
            _playerCharacter.Update(gameTime);
            _floor.Update(gameTime);
            _scoreManager.Update(gameTime); // Update the score

            // Save high score when the game ends or during important events
            _highScoreManager.SaveHighScore(_scoreManager.CurrentScore);
        }

        public override void PostUpdate(GameTime gameTime)
        {
            // Placeholder for additional logic after updates
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // Draw game objects
            _playerCharacter.Draw(spriteBatch);
            _floor.Draw(spriteBatch);

            // Draw the score
            _scoreManager.Draw(spriteBatch, _font, new Vector2(10, 10), Color.White);

            // Draw the high score
            spriteBatch.DrawString(_font, $"High Score: {_highScoreManager.HighScore}", new Vector2(10, 40), Color.Yellow);
        }
    }
}
