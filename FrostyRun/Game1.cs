using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using FrostyRun.States;
using FrostyRun.PD1;

namespace FrostyRun
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Song _song;
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
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load game assets
            LoadTextures();
            LoadAudio();

            // Set the initial state
            _currentState = new MenuState(this, GraphicsDevice, Content);
        }

        private void LoadTextures()
        {
            GameSettings.IceBlockTexture = Content.Load<Texture2D>("graphics/IceBlock");
            GameSettings.IceSpikeTexture = Content.Load<Texture2D>("graphics/spikes");
            GameSettings.FrostyHeadTexture = Content.Load<Texture2D>("graphics/head");
            GameSettings.FrostyBodyTexture = Content.Load<Texture2D>("graphics/snowball");
            GameSettings.Button = Content.Load<Texture2D>("controls/Button");
            GameSettings.SpriteFont = Content.Load<SpriteFont>("fonts/Font");
        }

        private void LoadAudio()
        {
            _song = Content.Load<Song>("audio/gameSong");
            MediaPlayer.Play(_song);

            // Set the initial volume to 50%
            MediaPlayer.Volume = 0.25f;
        }

        protected override void Update(GameTime gameTime)
        {
            // Handle state transitions
            if (_nextState != null)
            {
                _currentState = _nextState;
                _nextState = null;
            }

            // Update the current state
            _currentState.Update(gameTime);
            _currentState.PostUpdate(gameTime);

            // Handle global inputs
            if (IsExitRequested()) Exit();
            HandleAudioMuteToggle();
            HandleAudioVolumeAdjustments();

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

        private void HandleAudioVolumeAdjustments()
        {
            if (UserInputs.IsVolumeUpKeyPressed())
            {
                MediaPlayer.Volume = MathHelper.Clamp(MediaPlayer.Volume + 0.05f, 0.0f, 1.0f);
            }
            if (UserInputs.IsVolumeDownKeyPressed())
            {
                MediaPlayer.Volume = MathHelper.Clamp(MediaPlayer.Volume - 0.05f, 0.0f, 1.0f);
            }
        }

        private bool IsExitRequested()
        {
            return GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
                || Keyboard.GetState().IsKeyDown(Keys.Escape);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DeepSkyBlue);

            _spriteBatch.Begin();
            _currentState.Draw(gameTime, _spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
