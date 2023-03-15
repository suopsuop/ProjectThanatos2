using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectThanatos2.Content.Source
{
    static class Input
    {
        private static KeyboardState keyboardState, lastKeyboardState;

        public static void Update()
        {
            lastKeyboardState = keyboardState;

            keyboardState = Keyboard.GetState();
        }

        public static Vector2 GetMovementDirection()
        {

            Vector2 direction = new Vector2();

            direction.Y *= -1;  // Invert y-axis

            if (keyboardState.IsKeyDown(Keys.Left))
                direction.X -= 1;
            if (keyboardState.IsKeyDown(Keys.Right))
                direction.X += 1;
            if (keyboardState.IsKeyDown(Keys.Up))
                direction.Y -= 1;
            if (keyboardState.IsKeyDown(Keys.Down))
                direction.Y += 1;

            // Clamps length of vector to max 1
            if (direction.LengthSquared() > 1)
                direction.Normalize();

            return direction;
        }

        // Checks if Bomb Button pressed
        public static bool WasBombButtonPressed()
        {
            return WasKeyPressed(Keys.Space);
        }

        // Checks if a key was just pressed down
        public static bool WasKeyPressed(Keys key)
        {
            return lastKeyboardState.IsKeyUp(key) && keyboardState.IsKeyDown(key);
        }
    }
}
