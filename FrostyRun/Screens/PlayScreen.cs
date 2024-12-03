using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FrostyRun.PD1;
using FrostyRun.FrostyElements;
using FrostyRun.Characters;

namespace FrostyRun.Screens
{
    public class PlayScreen : ScreenBase
    {
        private PlayerCharacter _playerCharacter;
        private SpriteSheet _headSpriteSheet;
        private FrostyFloor _floor;

        public PlayScreen() { }

        public override void Initialize()
        {
            // Create the floor (it should be initialized here)
            _floor = new FrostyFloor();

            // Create the head sprite at a fixed position (removed floor logic)
            _headSpriteSheet = new SpriteSheet(GameSettings.FrostyHeadTexture, 1, 1, new Vector2(400, 300), GameSettings.BlockSize, 0);

            // Create the player character with the head
            _playerCharacter = new PlayerCharacter(_headSpriteSheet);
        }

        public override void Update(GameTime gameTime)
        {
            // Update user inputs (keyboard and mouse states)
            UserInputs.Update(gameTime);

            // Detect mouse click to add a body segment
            if (UserInputs.IsLeftClick())  // Use the new helper method for detecting left clicks
            {
                _playerCharacter.AddBodySegment(GameSettings.FrostyBodyTexture);
            }

            // Update the player (head + body)
            _playerCharacter.Update(gameTime);

            // Update the floor
            _floor.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // Draw the player character and the floor
            _playerCharacter.Draw(spriteBatch);
            _floor.Draw(spriteBatch);
        }

        // Implement LoadContent (to fix the error)
        public override void LoadContent()
        {
            // Load any content specific to this screen if needed
        }
    }
}
