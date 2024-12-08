using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using FrostyRun.PD1;


namespace FrostyRun
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Song song;
        private bool _isMuted = false;


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

        private bool IsExitRequested() => GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape);

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DeepSkyBlue);

            _spriteBatch.Begin();
            GameSettings.ActiveScreen.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
