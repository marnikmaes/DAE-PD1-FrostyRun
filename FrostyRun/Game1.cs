using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using FrostyRun.PD1;
using FrostyRun.States;

namespace FrostyRun
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Song song;
        private bool _isMuted = false;

        private State _currentState;
        private State _nextState;

        public void ChangeState(State state) 
        { 
            _nextState = state;
        
        }

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = GameSettings.ScreenWidth;
            _graphics.PreferredBackBufferHeight = GameSettings.ScreenHeight;
            Content.RootDirectory = "Content/bin/Windows";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            GameSettings.ActiveScreen = GameSettings.PlayScreen;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            song = Content.Load<Song>("audio/gameSong");
            MediaPlayer.Play(song);

            // Set the initial volume to 50%
            MediaPlayer.Volume = 0.25f;

            _currentState = new MenuState(this, GraphicsDevice, Content);

            LoadTextures();
            InitializeScreens();
        }

        private void LoadTextures()
        {
            GameSettings.IceBlockTexture = Content.Load<Texture2D>("graphics/IceBlock");
            GameSettings.IceSpikeTexture = Content.Load<Texture2D>("graphics/spikes");
            GameSettings.FrostyHeadTexture = Content.Load<Texture2D>("graphics/head");
            GameSettings.FrostyBodyTexture = Content.Load<Texture2D>("graphics/snowball");
        }

        private void InitializeScreens()
        {
            GameSettings.PlayScreen.LoadContent();
            GameSettings.PlayScreen.Initialize();
        }

        protected override void Update(GameTime gameTime)
        {
            if (IsExitRequested()) Exit();

            GameSettings.ActiveScreen.Update(gameTime);
            HandleAudioMuteToggle();
            HandleAudioVolumeIncrease();
            HandleAudioVolumeDecrease();

            _currentState.Update(gameTime);
            _currentState.PostUpdate(gameTime);

            base.Update(gameTime);
        }

        private void HandleAudioMuteToggle()
        {
            if (UserInputs.IsMuteKeyPressed())
            {
                _isMuted = !_isMuted;
                MediaPlayer.IsMuted = _isMuted;
            }
        }

        private void HandleAudioVolumeIncrease()
        {
            if (UserInputs.IsVolumeUpKeyPressed())
            {
                // Increase volume by 0.05 (clamping to the range 0.0 to 5.0)
                MediaPlayer.Volume = MathHelper.Clamp(MediaPlayer.Volume + 0.05f, 0.0f, 1.0f);
            }
        }

        private void HandleAudioVolumeDecrease()
        {
            if (UserInputs.IsVolumeDownKeyPressed())
            {
                // Decrease volume by 0.05 (clamping to the range 0.0 to 5.0)
                MediaPlayer.Volume = MathHelper.Clamp(MediaPlayer.Volume - 0.05f, 0.0f, 1.0f);
            }
        }

        private bool IsExitRequested() => GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape);

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DeepSkyBlue);

            _spriteBatch.Begin();
            GameSettings.ActiveScreen.Draw(_spriteBatch);
            _currentState.Draw(gameTime, _spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
