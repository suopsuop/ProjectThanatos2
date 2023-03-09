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

        // Checks if a key was just pressed down
        public static bool WasKeyPressed(Keys key)
        {
            return lastKeyboardState.IsKeyUp(key) && keyboardState.IsKeyDown(key);
        }
    }
}
