using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectThanatos2.Content.Source;
using static ProjectThanatos2.Content.Source.Enemy;

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

        public static float PointToVector(Vector2 vector, Vector2 vectorToPointTo)
        {
            return -1; // ADD TO ME
        }

        // BELOW FOR DEBUGGING!!!
        public static void DrawRectangle(this SpriteBatch spriteBatch, Rectangle rectangle, Color color)
        {
            spriteBatch.Draw(Sprites.Pixel, rectangle, color);
        }

        public static void Attack(object instance, Vector2 position, BulletPattern bulletPattern, int patternLifeTime)
        {
            EntityMan.Add(new BulletSpawner(instance, 8, 1, 360 / 8, 1, 1, .4f, 1.5f, 0.1f, 4, true, 7,
                Vector2.One, position, 2, 0, 1f, 10000, Bullet.BulletType.CARD, Bullet.BulletColour.GOLD, 6000));
        }
    }
}
