using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GeoSketch;

namespace FrostyRun.PD1
{
    public abstract class GameObject
    {
        // Properties
        public SpriteSheet Visualisation { get; protected set; }
        public Vector2 TopLeftPosition
        {
            get => Visualisation.TopLeftPosition;
            set => Visualisation.TopLeftPosition = value;
        }

        public Vector2 Size => Visualisation.Size;
        public bool IsActive { get; set; } = true;
        public Vector2 Velocity { get; protected set; }
        public float Rotation
        {
            get => Visualisation.Rotation;
            set => Visualisation.Rotation = value;
        }

        public float RotationSpeed
        {
            get => Visualisation.RotationSpeed;
            set => Visualisation.RotationSpeed = value;
        }

        // Update Methods
        public virtual void Update(GameTime gameTime)
        {
            Visualisation.Update(gameTime);
            MoveGameObject();
        }

        public virtual void MoveGameObject() => TopLeftPosition += Velocity;

        public bool IsColidingWith(GameObject gameObject) =>
            Visualisation.HitBoxRectangle.Intersects(gameObject.Visualisation.HitBoxRectangle);

        // Draw Methods
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            Visualisation.Draw(spriteBatch);
            DrawHitBox(spriteBatch);
        }

        private void DrawHitBox(SpriteBatch spriteBatch)
        {
            var hitBox = Visualisation.HitBoxRectangle;
            var destRect = Visualisation.DestinationRectangle;

            spriteBatch.DrawRectangle(hitBox.X, hitBox.Y, (int)Visualisation.Size.X, (int)Visualisation.Size.Y, Color.Transparent, Color.Red, 3);
            spriteBatch.DrawRectangle(destRect.X, destRect.Y, (int)Visualisation.Size.X, (int)Visualisation.Size.Y, Color.Transparent, Color.Orange, 3);
        }
    }
}
