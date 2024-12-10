using FrostyRun.PD1;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace FrostyRun.Characters
{
    public class PlayerCharacter
    {
        private PlayerCharacterHead _head;
        private List<PlayerCharacterBody> _bodySegments;

        // Gravity settings
        private const float Gravity = 1f; // Gravity strength
        private const float MaxFallSpeed = 1f; // Max falling speed (to avoid infinite acceleration)

        private float _verticalVelocity; // The velocity at which the character is falling or jumping
        private const float BodySegmentOffset = 20f; // Adjust this to the size of your body segments

        public PlayerCharacter(PlayerCharacterHead head)
        {
            _head = head;
            _bodySegments = new List<PlayerCharacterBody>();
            _verticalVelocity = 0f; // Starts at 0 velocity
        }

        public void AddBodySegment(Texture2D bodyTexture)
        {
            // Move the head and all body segments up to make space for the new body part
            _head.TopLeftPosition = new Vector2(_head.TopLeftPosition.X, _head.TopLeftPosition.Y - BodySegmentOffset);

            // Adjust the position of existing body segments
            foreach (var segment in _bodySegments)
            {
                segment.Visualisation.TopLeftPosition = new Vector2(
                    segment.Visualisation.TopLeftPosition.X,
                    segment.Visualisation.TopLeftPosition.Y - BodySegmentOffset
                );
            }

            // The position of the new body segment will be attached just below the head (or last body segment)
            Vector2 newPosition;
            if (_bodySegments.Count == 0)
            {
                // If there are no body segments, add the first segment just below the head
                newPosition = new Vector2(_head.TopLeftPosition.X, _head.TopLeftPosition.Y + _head.Size.Y);
            }
            else
            {
                // Otherwise, attach the new body segment just below the last segment
                var lastSegmentPosition = _bodySegments[^1].Visualisation.TopLeftPosition;
                newPosition = new Vector2(lastSegmentPosition.X, lastSegmentPosition.Y + _head.Size.Y);
            }

            var newBodySegment = new PlayerCharacterBody(bodyTexture, newPosition);
            _bodySegments.Add(newBodySegment);
        }

        public void Update(GameTime gameTime)
        {
            // Apply gravity to vertical velocity
            _verticalVelocity += Gravity; // Gravity accelerates the character

            // Prevent the character from falling infinitely fast
            if (_verticalVelocity > MaxFallSpeed)
            {
                _verticalVelocity = MaxFallSpeed;
            }

            // Move the head based on the velocity
            _head.TopLeftPosition = new Vector2(_head.TopLeftPosition.X, _head.TopLeftPosition.Y + _verticalVelocity);

            // Update body segments, apply the same gravity to them
            for (int i = 0; i < _bodySegments.Count; i++)
            {
                var segment = _bodySegments[i];
                segment.Visualisation.TopLeftPosition = new Vector2(segment.Visualisation.TopLeftPosition.X, segment.Visualisation.TopLeftPosition.Y + _verticalVelocity);
            }

            // Update head and body segments
            _head.Update(gameTime);
            foreach (var segment in _bodySegments)
            {
                segment.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _head.Draw(spriteBatch);

            foreach (var segment in _bodySegments)
            {
                segment.Draw(spriteBatch);
            }
        }

        // Reset the velocity when the character lands or reaches the ground
        public void LandOnGround()
        {
            _verticalVelocity = 0f; // Stop falling when landing
        }
    }
}
