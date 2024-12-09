using FrostyRun.PD1;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FrostyRun.Characters
{
    public class PlayerCharacterHead : GameObject
    {
        public PlayerCharacterHead(Texture2D texture, Vector2 position)
        {
            // Initialize the Visualisation (SpriteSheet) with the provided texture and position
            Visualisation = new SpriteSheet(texture, 1, 1, position, new Vector2(50, 50), 0);
        }

        // Override the Update method if specific logic for the head is needed
        public override void Update(GameTime gameTime)
        {
            Visualisation.Update(gameTime); // Update the sprite
        }

        // Override the Draw method to draw the head
        public override void Draw(SpriteBatch spriteBatch)
        {
            Visualisation.Draw(spriteBatch); // Draw the sprite
        }
    }
}
