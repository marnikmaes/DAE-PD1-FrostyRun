using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FrostyRun.PD1;
using FrostyRun.FrostyElements;
using FrostyRun.Characters;
using Microsoft.Xna.Framework.Content;

namespace FrostyRun.States
{
    public class GameState : State
    {
        private PlayerCharacter _playerCharacter;
        private SpriteSheet _headSpriteSheet;
        private Platform _floor;

        public GameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
            : base(game, graphicsDevice, content)
        {
            // Initialize gameplay objects here
            _floor = new Platform();
            _headSpriteSheet = new SpriteSheet(GameSettings.FrostyHeadTexture, 1, 1, new Vector2(400, 300), GameSettings.BlockSize, 0);
            _playerCharacter = new PlayerCharacter(_headSpriteSheet);
        }

        public override void Update(GameTime gameTime)
        {
            UserInputs.Update(gameTime);

            // Detect mouse click to add a body segment
            if (UserInputs.IsLeftClick())
            {
                _playerCharacter.AddBodySegment(GameSettings.FrostyBodyTexture);
            }

            // Update game objects
            _playerCharacter.Update(gameTime);
            _floor.Update(gameTime);
        }

        public override void PostUpdate(GameTime gameTime)
        {
            // Implement any logic that occurs after the main update, if needed
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _playerCharacter.Draw(spriteBatch);
            _floor.Draw(spriteBatch);
        }
    }
}
