using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrostyRun.PD1
{
    public abstract class ScreenBase
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public abstract void Initialize();
        public abstract void LoadContent();
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);

        public ScreenBase()
        {

        }
    }
}
