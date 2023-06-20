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

        public static float PointToVector(Vector2 origin, Vector2 destination)
        {
            return MathF.Atan2(destination.Y - origin.Y, destination.X - origin.X);
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

        // BELOW FOR DEBUGGING
        public static void DrawRectangle(this SpriteBatch spriteBatch, Rectangle rectangle, Color color)
        {
            spriteBatch.Draw(Sprites.Pixel, rectangle, color);
        }
    }
}
