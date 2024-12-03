using FrostyRun.PD1;
using System;

namespace FrostyRun.FrostyElements
{
    public abstract class FrostyBase : GameObject
    {
        #region Properties
        // Flags indicating object behavior
        public bool IsPlatform { get; protected set; }
        public bool IsGravityAffected { get; protected set; }
        public bool IsEnemy { get; protected set; }
        public bool IsPickup { get; protected set; }

        // Speed at which the object moves across the screen
        public float Speed { get; protected set; }

        // Constructor initializes the speed and velocity based on game settings
        #endregion
        protected FrostyBase()
        {
            InitializeDefaults();
        }

        // Initializes default values for properties
        private void InitializeDefaults()
        {
            Speed = GameSettings.StartingSpeed;
            Velocity = GameSettings.GameDirection;
        }
    }
}
