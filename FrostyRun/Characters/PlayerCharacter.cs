using FrostyRun.PD1;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace FrostyRun.Characters
{
    public class PlayerCharacter : GameObject
    {
        private PlayerCharacterHead _head;  // The head of the player
        private List<PlayerCharacterBody> _bodySegments;  // Store body segments as PlayerCharacterBody instances

        private const int MaxBodySegments = 8; // 8 body segments + 1 head

        public PlayerCharacter(PlayerCharacterHead head)
        {
            _head = head;  // Initialize the head
            _bodySegments = new List<PlayerCharacterBody>();  // Initialize body segments list
        }

        // Add a body segment when clicked
        public void AddBodySegment(Texture2D bodyTexture)
        {
            if (_bodySegments.Count < MaxBodySegments)  // Ensure we do not exceed max body segments
            {
                var lastPosition = _bodySegments.Count > 0
                    ? _bodySegments[^1].Visualisation.TopLeftPosition
                    : _head.TopLeftPosition;  // Get the position of the last body segment or the head

                // Create a new body segment (PlayerCharacterBody)
                var newBodySegment = new PlayerCharacterBody(
                    bodyTexture,
                    new Vector2(lastPosition.X, lastPosition.Y + 50) // Position new segment below the last one
                );

                _bodySegments.Add(newBodySegment);  // Add new body segment
            }
        }

        // Update all body segments and the head
        public override void Update(GameTime gameTime)
        {
            _head.Update(gameTime);  // Update the head

            foreach (var segment in _bodySegments)
            {
                segment.Update(gameTime);  // Update each body segment
            }
        }

        // Draw all body segments and the head
        public override void Draw(SpriteBatch spriteBatch)
        {
            _head.Draw(spriteBatch);  // Draw the head

            foreach (var segment in _bodySegments)
            {
                segment.Draw(spriteBatch);  // Draw each body segment
            }
        }
    }
}
