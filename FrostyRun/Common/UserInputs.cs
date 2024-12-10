using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace FrostyRun.PD1
{
    public static class UserInputs
    {
        // Store previous and current keyboard and mouse states
        public static KeyboardState PreviousKeyboardState { get; private set; }
        public static KeyboardState CurrentKeyboardState { get; private set; }

        public static MouseState PreviousMouseState { get; private set; }
        public static MouseState CurrentMouseState { get; private set; }

        // Check if a key is pressed for movement: Up, Down, Left, Right
        public static bool IsUpPressed()
        {
            return IsKeyDown(Keys.W);
        }

        public static bool IsDownPressed()
        {
            return IsKeyDown(Keys.S);
        }

        public static bool IsRightPressed()
        {
            return IsKeyDown(Keys.D);
        }

        public static bool IsLeftPressed()
        {
            return IsKeyDown(Keys.A);
        }

        public static bool IsResetPressed() 
        {
            return IsKeyDown(Keys.R);
        
        }

        // Helper method to check if a key is currently pressed
        private static bool IsKeyDown(Keys key)
        {
            return CurrentKeyboardState.IsKeyDown(key);
        }

        // Detect mouse left-click (pressed and released)
        public static bool IsLeftClick()
        {
            // Check if the left mouse button is pressed and the previous state was released
            return CurrentMouseState.LeftButton == ButtonState.Pressed && PreviousMouseState.LeftButton == ButtonState.Released;
        }

        // Detect M-key press
        public static bool IsMuteKeyPressed()
        {
            return CurrentKeyboardState.IsKeyDown(Keys.M) && !PreviousKeyboardState.IsKeyDown(Keys.M);
        }

        public static bool IsVolumeUpKeyPressed()
        {
            return CurrentKeyboardState.IsKeyDown(Keys.Up) && !PreviousKeyboardState.IsKeyDown(Keys.Up);
        }

        public static bool IsVolumeDownKeyPressed()
        {
            return CurrentKeyboardState.IsKeyDown(Keys.Down) && !PreviousKeyboardState.IsKeyDown(Keys.Down);
        }

        // Update the current and previous input states
        public static void Update(GameTime gameTime)
        {
            // Update the previous and current states for keyboard and mouse
            PreviousKeyboardState = CurrentKeyboardState;
            CurrentKeyboardState = Keyboard.GetState();

            PreviousMouseState = CurrentMouseState;
            CurrentMouseState = Mouse.GetState();
        }
    }
}
