using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FrostyRun.InterfaceElements
{
    public class ScoreManager
    {
        public int CurrentScore { get; private set; } // The player's current score
        private float _elapsedTime; // Tracks elapsed time since the last update

        public ScoreManager()
        {
            CurrentScore = 0;
            _elapsedTime = 0f;
        }

        // Update the score based on elapsed time
        public void Update(GameTime gameTime)
        {
            _elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Increment the score every 0.1 seconds (10 points per second)
            while (_elapsedTime >= 0.1f)
            {
                CurrentScore += 1; // Add 1 point
                _elapsedTime -= 0.1f; // Reduce elapsed time
            }
        }

        // Draw the score on the screen
        public void Draw(SpriteBatch spriteBatch, SpriteFont font, Vector2 position, Color color)
        {
            spriteBatch.DrawString(font, $"Score: {CurrentScore}", position, color);
        }
    }
}
