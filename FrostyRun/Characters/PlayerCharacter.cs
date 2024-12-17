using FrostyRun.FrostyElements;
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
        private const float Gravity = 1.2f; // Gravity strength
        private const float MaxFallSpeed = 1.2f; // Max falling speed

        private float _verticalVelocity; // The velocity at which the character is falling or jumping
        public PlayerCharacter(PlayerCharacterHead head)
        {
            _head = head;
            _bodySegments = new List<PlayerCharacterBody>();
            _verticalVelocity = 0f; // Starts at 0 velocity
        }

        public void AddBodySegment(Texture2D bodyTexture)
        {
            // Move the head and all body segments up to make space for the new body part
            _head.TopLeftPosition = new Vector2(_head.TopLeftPosition.X, _head.TopLeftPosition.Y - _head.Size.Y);

            // Adjust the position of existing body segments
            foreach (var segment in _bodySegments)
            {
                segment.Visualisation.TopLeftPosition = new Vector2(
                    segment.Visualisation.TopLeftPosition.X,
                    segment.Visualisation.TopLeftPosition.Y - _head.Size.Y
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

        public void Update(GameTime gameTime, List<IcePlatform> platforms)
        {
            // Apply gravity to vertical velocity
            _verticalVelocity += Gravity;

            // Prevent the character from falling infinitely fast
            if (_verticalVelocity > MaxFallSpeed)
            {
                _verticalVelocity = MaxFallSpeed;
            }

            // Move the head based on the velocity
            _head.TopLeftPosition = new Vector2(_head.TopLeftPosition.X, _head.TopLeftPosition.Y + _verticalVelocity);

            // Check collision with platforms
            foreach (var platform in platforms)
            {
                // Check for collision between player and platform
                if (IsCollidingWithPlatform(platform))
                {
                    // Stop falling and position the player on top of the platform
                    LandOnGround(platform);
                    break; // Only need to check the first platform the player collides with
                }
            }

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
        public void LandOnGround(IcePlatform platform)
        {
            // Reset vertical velocity (stop falling)
            _verticalVelocity = 0f;

            // Position the player just on top of the platform
            _head.TopLeftPosition = new Vector2(
                _head.TopLeftPosition.X,
                platform.PlatformTopLeftPosition.Y - _head.Size.Y
            );
        }


        private bool IsCollidingWithPlatform(IcePlatform platform)
        {
            // Create the bounding box for the player's head
            Rectangle playerHeadBounds = new Rectangle(
                (int)_head.TopLeftPosition.X,
                (int)_head.TopLeftPosition.Y,
                (int)_head.Size.X,
                (int)_head.Size.Y
            );

            // Create the bounding box for the platform
            Rectangle platformBounds = new Rectangle(
                (int)platform.PlatformTopLeftPosition.X,
                (int)platform.PlatformTopLeftPosition.Y,
                (int)GameSettings.BlockSize.X,
                (int)GameSettings.BlockSize.Y
            );

            // Check if the player’s head collides with the platform (i.e., overlaps)
            return playerHeadBounds.Intersects(platformBounds) && _verticalVelocity >= 0;
        }

    }
}
