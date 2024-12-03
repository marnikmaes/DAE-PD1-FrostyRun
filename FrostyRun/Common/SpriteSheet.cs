using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Intrinsics;
using System.Text;
using System.Threading.Tasks;

namespace FrostyRun.PD1
{
    public class SpriteSheet
    {
        public Texture2D Texture2D { get; private set; }

        public int Rows { get; private set; }
        public int Columns { get; private set; }

        public Vector2 TopLeftPosition { get; set; }
        public Vector2 Size { get; private set; }

        public int CurrentSpriteIndex { get; protected set; } = 0;
        public Vector2 SpriteSize
        {
            get
            {
                return new Vector2(Texture2D.Width / Columns, Texture2D.Height / Rows);
            }
        }

        public Rectangle DestinationRectangle
        {
            get
            {
                return new Rectangle(TopLeftPosition.ToPoint(), Size.ToPoint());
            }
        }

        public Rectangle SourceRectangle
        {
            get
            {
                float column = CurrentSpriteIndex % Columns;
                float row = CurrentSpriteIndex / Columns;

                return new Rectangle((int)SpriteSize.X * (int)column, (int)SpriteSize.Y * (int)row, (int)SpriteSize.X, (int)SpriteSize.Y);
            }
        }

        public Rectangle HitBoxRectangle
        {
            get
            {
                return DestinationRectangle;
            }
        }

        public float Rotation { get; set; }

        public float RotationSpeed { get; set; }

        public SpriteSheet() { }

        public SpriteSheet(Texture2D texture2D, int rows, int columns, Vector2 topLeftPos, Vector2 size, int spriteIndex)
        {
            Texture2D = texture2D;
            Rows = rows;
            Columns = columns;
            TopLeftPosition = topLeftPos;
            Size = size;
            CurrentSpriteIndex = spriteIndex;
        }

        public virtual void Update(GameTime gameTime)
        {
            Rotation += RotationSpeed;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture2D, DestinationRectangle, SourceRectangle, Color.White, 0, // No rotation
                             Vector2.Zero, SpriteEffects.None, 0);
        }

    }
}
