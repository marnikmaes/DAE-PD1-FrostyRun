using System;
using System.Collections.Generic;
using FrostyRun.ButtonControls;
using FrostyRun.PD1;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FrostyRun.States
{
    public class MenuState : State
    {
        private List<Component> _components;

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
            var infoTextBlock = new TextBlock(buttonFont, "PRESS 'LMB' to add a new body segment.\nPRESS 'M' to mute audio.\nPRESS 'up' or 'down' to control audio volume.")
            {
                Position = new Vector2(0, startVerticalPosition + buttonTexture.Height + 150) // You only need to set the Y position here
            };

            _components = new List<Component>
            {
                newGameButton,
                quitGameButton,
                infoTextBlock // Add the InfoTextBlock to the list of components
            };
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (var component in _components)
            {
                component.Draw(gameTime, spriteBatch);
            }
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in _components)
            {
                component.Update(gameTime);
            }
        }

        public override void PostUpdate(GameTime gameTime)
        {
            // Remove sprites if they're not needed (empty method for future logic)
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

    // TextBlock class to handle non-interactive text
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
            // Measure the width of the text
            var textWidth = _font.MeasureString(_text).X;

            // Calculate the X position to center the text
            var centeredPosition = new Vector2(
                (GameSettings.ScreenWidth - textWidth) / 2, // Horizontal center
                Position.Y // Keep the vertical position the same
            );

            // Draw the text at the new centered position
            spriteBatch.DrawString(_font, _text, centeredPosition, Color.Black);
        }

        public override void Update(GameTime gameTime)
        {
            // No update logic needed for static text
        }
    }
}
