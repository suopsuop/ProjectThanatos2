using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectThanatos2.Content.Source;
using static ProjectThanatos.Content.Source.Enemy;

namespace ProjectThanatos.Content.Source
{
    static class RiceLib
    {
        public static dynamic NormaliseBetweenTwoRanges(dynamic value, dynamic valMin, dynamic valMax, dynamic newMin, dynamic newMax)
        {
            return newMin + (value - valMin) * (newMax - newMin) / (valMax - valMin);
        }

        public static float ToRadians(dynamic value)
        {
            return MathF.PI / 180f * (float)value;
        }

        public static Vector2 LengthDirection(float distance, float angle)
        {
            return new Vector2(
                distance * MathF.Cos((angle * MathF.PI) / 180),
                distance * -MathF.Sin((angle * MathF.PI) / 180));
        }

        public static float DirectionAwayFromVector(Vector2 vector1, Vector2 vector2)
        {
            return MathF.Atan2(vector2.Y - vector1.Y, vector1.X - vector2.X) * (180f / MathF.PI);
        }

        public static Vector2 getVecDirection(float direction) // Gets vector direction from angle
        {
            // Could be a one-liner, might do that but just keeping this
            // here for now for readability
            float dirXRadians = direction * MathF.PI / 180f;
            float dirYRadians = direction * MathF.PI / 180f;

            return new Vector2(MathF.Cos(dirXRadians), -MathF.Sin(dirYRadians));
        }

        // Returns a random boolean, extension of the Random class
        public static bool NextBool(this Random random)
        {
            if (random.Next(0, 2) == 1)
                return true;
            else
                return false;
        }

        // Draws text
        public static void DrawText(SpriteBatch spriteBatch, string text, Vector2 position, Color color, float scale = 1f, float rotation = 0f, SpriteEffects spriteEffects = SpriteEffects.None)
        {
            spriteBatch.Begin();
            // MeasureString/2 to centre the text
            spriteBatch.DrawString(GameMan.font, text, position - (GameMan.font.MeasureString(text) / 2), color, rotation, Vector2.Zero, scale, spriteEffects, 0);
            spriteBatch.End();
        }

        // BELOW FOR DEBUGGING
        public static void DrawRectangle(this SpriteBatch spriteBatch, Rectangle rectangle, Color color)
        {
            spriteBatch.Draw(Sprites.Pixel, rectangle, color);
        }
    }
}
