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
            return IsKeyDown(Keys.W) || IsKeyDown(Keys.Up);
        }

        public static bool IsDownPressed()
        {
            return IsKeyDown(Keys.S) || IsKeyDown(Keys.Down);
        }

        public static bool IsRightPressed()
        {
            return IsKeyDown(Keys.D) || IsKeyDown(Keys.Right);
        }

        public static bool IsLeftPressed()
        {
            return IsKeyDown(Keys.A) || IsKeyDown(Keys.Left);
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
