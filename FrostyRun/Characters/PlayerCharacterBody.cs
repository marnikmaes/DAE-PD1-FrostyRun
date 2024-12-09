using FrostyRun.PD1;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FrostyRun.Characters
{
    public class PlayerCharacterBody : GameObject
    {
        public PlayerCharacterBody(Texture2D texture, Vector2 position)
        {
            Visualisation = new SpriteSheet(texture, 1, 1, position, new Vector2(50, 50), 0); // Initialize body segment
        }

        // Override Update for custom body segment logic (if any)
        public override void Update(GameTime gameTime)
        {
            Visualisation.Update(gameTime); // Update the body segment
        }

        // Override Draw to render the body segment
        public override void Draw(SpriteBatch spriteBatch)
        {
            Visualisation.Draw(spriteBatch); // Draw the body segment
        }
    }
}
