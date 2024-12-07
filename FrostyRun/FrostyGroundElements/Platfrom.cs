using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using FrostyRun.PD1; // Adjust according to your project structure
using FrostyRun.FrostyElements;

namespace FrostyRun.FrostyElements
{
    public class Platform
    {
        private List<PlatformElement> _platformElements;  // List to store individual platform elements
        public Vector2 Position { get; private set; }     // Position of the platform
        public Vector2 Size { get; private set; }         // Size of the entire platform

        // Constructor initializes the platform with position and size
        public Platform(Vector2 position, Vector2 size, Texture2D platformTexture)
        {
            Position = position;
            Size = size;
            _platformElements = new List<PlatformElement>();

            // Divide the platform into smaller platform elements (e.g., tiles or blocks)
            InitializePlatformElements(platformTexture);
        }

        // Initializes the individual platform elements based on the size and position
        private void InitializePlatformElements(Texture2D platformTexture)
        {
            // Assume each platform element is a block (you can adjust the logic based on your design)
            int rows = (int)(Size.Y / GameSettings.BlockSize.Y);
            int columns = (int)(Size.X / GameSettings.BlockSize.X);

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < columns; col++)
                {
                    // Position each block in the platform grid
                    Vector2 elementPosition = new Vector2(Position.X + col * GameSettings.BlockSize.X,
                                                           Position.Y + row * GameSettings.BlockSize.Y);

                    // Create the platform element and add it to the list
                    PlatformElement element = new PlatformElement(platformTexture, elementPosition);
                    _platformElements.Add(element);
                }
            }
        }

        // Update method to update all platform elements
        public void Update(GameTime gameTime)
        {
            foreach (var element in _platformElements)
            {
                element.Update(gameTime);
            }
        }

        // Draw method to draw all platform elements together
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var element in _platformElements)
            {
                element.Draw(spriteBatch);
            }
        }
    }

    // PlatformElement class represents an individual block in the platform
    public class PlatformElement
    {
        public Texture2D Texture { get; private set; }
        public Vector2 Position { get; private set; }

        public PlatformElement(Texture2D texture, Vector2 position)
        {
            Texture = texture;
            Position = position;
        }

        // Update method (empty for now, can be extended later)
        public void Update(GameTime gameTime)
        {
            // Logic to update each platform element can go here
        }

        // Draw method to draw the platform element on the screen
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }
    }
}
