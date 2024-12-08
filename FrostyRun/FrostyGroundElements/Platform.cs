using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FrostyRun.PD1;
using System;
using System.Collections.Generic;

namespace FrostyRun.FrostyElements
{
    public class Platform
    {
        #region Fields and Properties
        // List of active platforms in the floor
        public List<IcePlatform> PlatformsList { get; private set; } = new List<IcePlatform>();

        // Calculates the number of platforms needed to fill the screen width
        private int PlatformAmount => GameSettings.ScreenWidth / (int)GameSettings.BlockSize.X + 1;

        // Spawn location for new platforms
        private Vector2 _spawnLocation => new Vector2(GameSettings.ScreenWidth, GameSettings.ScreenHeight - GameSettings.BlockSize.Y);
        #endregion

        #region Constructor
        // Initializes the floor and generates the starting platforms
        public Platform() => GenerateStartingPlatforms();
        #endregion

        #region Platform Generation
        // Generates the initial platforms when the floor is created
        private void GenerateStartingPlatforms()
        {
            for (int i = 0; i < PlatformAmount; i++)
            {
                IcePlatform platform = CreatePlatform(i);
                PlatformsList.Add(platform);
            }
        }

        // Creates a platform at a given index (i)
        private IcePlatform CreatePlatform(int index)
        {
            var position = new Vector2(index * GameSettings.BlockSize.X, GameSettings.ScreenHeight - GameSettings.BlockSize.Y);
            var spriteSheet = new SpriteSheet(GameSettings.IceBlockTexture, 1, 1, position, GameSettings.BlockSize, 0);

            var platform = new IcePlatform(spriteSheet)
            {
                Rotation = 0f, // No rotation
                RotationSpeed = 0f // No rotation speed
            };
            return platform;
        }
        #endregion

        #region Platform Updates
        // Updates the floor platforms each frame
        public void Update(GameTime gameTime)
        {
            MoveFloorPlatforms(gameTime);
            CullInactivePlatforms();
            AddNewPlatformsIfNeeded();
        }

        // Moves the platforms and deactivates them when they move off-screen
        private void MoveFloorPlatforms(GameTime gameTime)
        {
            foreach (var platform in PlatformsList)
            {
                platform.Update(gameTime);
                SetPlatformInactiveIfOffScreen(platform);
            }
        }

        // Sets a platform as inactive if it has moved off-screen
        private void SetPlatformInactiveIfOffScreen(IcePlatform platform)
        {
            if (platform.TopLeftPosition.X < 0 - GameSettings.BlockSize.X)
            {
                platform.IsActive = false;
            }
        }

        // Removes all inactive platforms from the list
        private void CullInactivePlatforms() => PlatformsList.RemoveAll(platform => !platform.IsActive);

        // Adds new platforms when there are fewer than needed
        private void AddNewPlatformsIfNeeded()
        {
            if (PlatformsList.Count < PlatformAmount)
            {
                CreateNewPlatforms();
            }
        }

        // Generates new platforms or spikes and adds them to the list
        private void CreateNewPlatforms()
        {
            while (PlatformsList.Count < PlatformAmount)
            {
                AddRandomPlatform();
            }
        }

        // Adds a random platform or spike to the list
        private void AddRandomPlatform()
        {
            int randomChance = Random.Shared.Next(0, 11);
            var platform = randomChance >= GameSettings.SpikeChance
                ? CreateSpikePlatform()
                : CreateNormalPlatform();

            PlatformsList.Add(platform);
        }

        // Creates a new platform
        private IcePlatform CreateNormalPlatform() => new IcePlatform(new SpriteSheet(GameSettings.IceBlockTexture, 1, 1, _spawnLocation, GameSettings.BlockSize, 0));

        // Creates a new spike platform
        private IcePlatform CreateSpikePlatform() => new IceSpike(new SpriteSheet(GameSettings.IceSpikeTexture, 1, 1, _spawnLocation, GameSettings.BlockSize, 0));
        #endregion

        #region Drawing
        // Draws all the platforms on the screen
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var platform in PlatformsList)
            {
                platform.Draw(spriteBatch);
            }
        }
        #endregion
    }
}
