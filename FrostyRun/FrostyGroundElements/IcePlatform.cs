using Microsoft.Xna.Framework;
using FrostyRun.PD1;

namespace FrostyRun.FrostyElements
{
    public class IcePlatform : FrostyBase
    {
        // Directly access Visualisation.TopLeftPosition through a property
        // The => symbol is essentially used to indicate that what
        // follows is an expression that should be evaluated and returned.
        // It's similar to writing a return statement, but in a more compact way.

        public Vector2 PlatformTopLeftPosition
        {
            get => Visualisation.TopLeftPosition;
            set => Visualisation.TopLeftPosition = value;
        }

        public IcePlatform(SpriteSheet spriteSheet) : base()
        {
            Visualisation = spriteSheet; // Assign sprite sheet directly
            IsPlatform = true;  // Indicate this is a platform
        }

        // Simplify Update method as it calls the base Update method
        public override void Update(GameTime gameTime) => base.Update(gameTime);

        // Simplified MoveGameObject method
        public override void MoveGameObject() => PlatformTopLeftPosition += (Velocity * Speed) * 3;
    }
}
