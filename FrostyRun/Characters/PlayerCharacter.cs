using FrostyRun.PD1;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace FrostyRun.Characters
{
    public class PlayerCharacter : GameObject
    {
        private List<SpriteSheet> _bodySegments;
        private const int MaxBodySegments = 8; // 8 body segments + 1 head

        public PlayerCharacter(SpriteSheet headSpriteSheet)
        {
            _bodySegments = new List<SpriteSheet> { headSpriteSheet }; // Start with head segment
        }

        // Add a body segment when clicked
        public void AddBodySegment(Texture2D bodyTexture)
        {
            if (_bodySegments.Count <= MaxBodySegments)  // Ensure we do not exceed max body segments
            {
                var lastPosition = _bodySegments[^1].TopLeftPosition; // Shorthand: ^1 to get the last element
                var newBodySpriteSheet = new SpriteSheet(
                    bodyTexture,
                    1, 1,
                    new Vector2(lastPosition.X, lastPosition.Y + 50),
                    new Vector2(50, 50),
                    0
                );
                _bodySegments.Add(newBodySpriteSheet);  // Add new segment to the body
            }
        }

        // Update all body segments
        public override void Update(GameTime gameTime)
        {
            foreach (var segment in _bodySegments)
            {
                segment.Update(gameTime);
            }
        }

        // Draw all body segments
        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (var segment in _bodySegments)
            {
                segment.Draw(spriteBatch);
            }
        }
    }
}
