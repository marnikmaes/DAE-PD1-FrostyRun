using FrostyRun.PD1;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FrostyRun.Characters
{
    public class PlayerCharacterBody : GameObject
    {
        public PlayerCharacterBody(Texture2D texture, Vector2 position)
        {
            Visualisation = new SpriteSheet(texture, 1, 1, position, new Vector2(50, 50), 0);
        }
    }
}
