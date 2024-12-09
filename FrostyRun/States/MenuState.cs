using System;
using System.Collections.Generic;
using FrostyRun.InterfaceElements;
using FrostyRun.PD1;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FrostyRun.States
{
    public class MenuState : State
    {
        private List<Button> _buttons;
        private TextBlock _infoTextBlock;

        public MenuState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
            : base(game, graphicsDevice, content)
        {
            var buttonTexture = GameSettings.Button;
            var buttonFont = GameSettings.SpriteFont;

            // Calculate the total height for both buttons (with 100px space between them)
            var totalHeight = buttonTexture.Height * 2 + 100;

            // Calculate the starting vertical position to center both buttons
            var startVerticalPosition = (GameSettings.ScreenHeight - totalHeight) / 2;

            // Create the "Start Game" button and position it
            var newGameButton = CreateButton(buttonTexture, buttonFont, "Start Game", startVerticalPosition);

            // Create the "Quit Game" button and position it below the New Game button
            var quitGameButton = CreateButton(buttonTexture, buttonFont, "Quit Game", startVerticalPosition + buttonTexture.Height + 50);

            // Create the Info TextBlock
            _infoTextBlock = new TextBlock(buttonFont, "PRESS 'LMB' to add a new body segment.\nPRESS 'M' to mute audio.\nPRESS 'up' or 'down' to control audio volume.")
            {
                Position = new Vector2(0, startVerticalPosition + buttonTexture.Height + 150) // You only need to set the Y position here
            };

            // Store buttons in a list
            _buttons = new List<Button> { newGameButton, quitGameButton };
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // Draw the background image first
            spriteBatch.Draw(GameSettings.StartScreenBg, new Rectangle(0, 0, GameSettings.ScreenWidth, GameSettings.ScreenHeight), Color.White);

            // Draw the buttons
            foreach (var button in _buttons)
            {
                button.Draw(gameTime, spriteBatch);
            }

            // Draw the info text block
            _infoTextBlock.Draw(gameTime, spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            // Update the buttons
            foreach (var button in _buttons)
            {
                button.Update(gameTime);
            }

            // Update the info text block
            _infoTextBlock.Update(gameTime);
        }

        public override void PostUpdate(GameTime gameTime)
        {
            // Empty for now, this can be used for future logic like removing components or performing additional actions
        }

        private Button CreateButton(Texture2D buttonTexture, SpriteFont buttonFont, string text, int verticalOffset)
        {
            var button = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(
                    (GameSettings.ScreenWidth - buttonTexture.Width) / 2, // Horizontal center
                    verticalOffset // Custom vertical offset
                ),
                Text = text,
            };

            // Attach the appropriate click event handlers
            if (text == "Start Game")
            {
                button.Click += NewGameButton_Click;
            }
            else if (text == "Quit Game")
            {
                button.Click += QuitGameButton_Click;
            }

            return button;
        }

        private void NewGameButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new GameState(_game, _graphicsDevice, _content));
        }

        private void QuitGameButton_Click(object sender, EventArgs e)
        {
            _game.Exit();
        }
    }
}
