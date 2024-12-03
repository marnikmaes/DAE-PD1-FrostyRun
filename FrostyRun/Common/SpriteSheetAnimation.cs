using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrostyRun.PD1
{
    public class SpriteSheetAnimation : SpriteSheet
    {

        public int StartSpriteIndex { get; private set; }
        public int EndSpriteIndex { get; private set; }
        public int IdleSpriteIndex { get; private set; }

        public int AnimationDelayInFrames { get; private set; }

        public bool IsAnimationStopped { get; private set; }

        private int _passedFrames = 0;

        public SpriteSheetAnimation(Texture2D texture2D, int rows, int columns, Vector2 topLeftPos, Vector2 size,
            int startSpriteIndex, int endSpriteIndex, int idleSpriteIndex, int animationDelayInFrames)
            : base(texture2D, rows, columns, topLeftPos, size, startSpriteIndex)
        {
            StartSpriteIndex = startSpriteIndex;
            EndSpriteIndex = endSpriteIndex;
            IdleSpriteIndex = idleSpriteIndex;
            AnimationDelayInFrames = animationDelayInFrames;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (IsAnimationStopped)
            {
                CurrentSpriteIndex = IdleSpriteIndex;
                return;
            }

            _passedFrames++;
            if (_passedFrames >= AnimationDelayInFrames)
            {
                CurrentSpriteIndex++;
                if (CurrentSpriteIndex > EndSpriteIndex || CurrentSpriteIndex >= Rows * Columns)
                    CurrentSpriteIndex = StartSpriteIndex;
                _passedFrames = 0;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
