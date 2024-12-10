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

        private const int MaxBodySegments = 8;

        public PlayerCharacter(PlayerCharacterHead head)
        {
            _head = head;
            _bodySegments = new List<PlayerCharacterBody>();
        }

        public void AddBodySegment(Texture2D bodyTexture)
        {
            if (_bodySegments.Count < MaxBodySegments) 
            {
                var lastPosition = _bodySegments.Count > 0
                    ? _bodySegments[^1].Visualisation.TopLeftPosition
                    : _head.TopLeftPosition;

                var newBodySegment = new PlayerCharacterBody(
                    bodyTexture,
                    new Vector2(lastPosition.X, lastPosition.Y + _head.Size.Y)
                );

                _bodySegments.Add(newBodySegment);
            }
        }

        public void Update(GameTime gameTime)
        {
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
    }
}
