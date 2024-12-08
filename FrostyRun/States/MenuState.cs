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
            var buttonTexture = _content.Load<Texture2D>("Controls/Button");
            var buttonFont = _content.Load<SpriteFont>("Fonts/Font");

            var newGameButton = new Button(buttonTexture, buttonFont)
            {
                // Calculate the position to center the button
                Position = new Vector2(
                    (GameSettings.ScreenWidth - buttonTexture.Width) / 2, // Horizontal center
                    (GameSettings.ScreenHeight - buttonTexture.Height) / 2 // Vertical center
                ),
                Text = "New Game",
            };

            newGameButton.Click += NewGameButton_Click;

            var quitGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(
                    (GameSettings.ScreenWidth - buttonTexture.Width) / 2, // Horizontal center
                    ((GameSettings.ScreenHeight - buttonTexture.Height) / 2) + 100 // Vertical center
                ),
                Text = "Quit Game",
            };

            quitGameButton.Click += QuitGameButton_Click;

            _components = new List<Component>()
      {
        newGameButton,
        quitGameButton,
      };
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);
        }

        private void LoadGameButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Load Game");
        }

        private void NewGameButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new GameState(_game, _graphicsDevice, _content));
        }

        public override void PostUpdate(GameTime gameTime)
        {
            // remove sprites if they're not needed
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in _components)
                component.Update(gameTime);
        }

        private void QuitGameButton_Click(object sender, EventArgs e)
        {
            _game.Exit();
        }
    }
}