﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ProjectThanatos.Content.Source
{
    static class RiceLib
    {
        public static dynamic normaliseBetweenTwoRanges(dynamic value, dynamic valMin, dynamic valMax, dynamic newMin, dynamic newMax)
        {
            return newMin + (value - valMin) * (newMax - newMin) / (valMax - valMin);
        }

        public static float toRadians(dynamic value)
        {
            return MathF.PI / 180f * (float)value;
        }

        public static Vector2 lengthDirection(float distance, float angle)
        {
            return new Vector2(
                distance * MathF.Cos((angle * MathF.PI) / 180),
                distance * -MathF.Sin((angle * MathF.PI) / 180));
        }

    }
}
