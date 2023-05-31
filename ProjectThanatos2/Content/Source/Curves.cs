using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace ProjectThanatos.Content.Source
{

	static class Curves
	{
		private static readonly Dictionary<CurveType, Bullet.BulletCurve> curves = new Dictionary<CurveType, Bullet.BulletCurve>();

		public enum CurveType
		{
			SINE_CURVE,
			COS_CURVE,
			EMPTY,
			LINE
		}

		//public const string SINE_CURVE = "sine_curve";


		// IMPORTANT!!! Every curve should go upwards, for consistency
		static Curves()
		{
			curves.Add(CurveType.SINE_CURVE, (Bullet bullet) =>
			{
				//float offset = MathF.Sin((float)gameTime.ElapsedGameTime.TotalSeconds) * 30f;
				float offset = MathF.Sin((float)bullet.framesAlive / 20f) * 80f;

				bullet.position = new Vector2(-offset + bullet.initialPosition.X, -(float)bullet.speed + bullet.position.Y) + bullet.velocity;
				//bullet.velocity += new Vector2(offset + bullet.initialPosition.X, (float)bullet.speed + bullet.position.Y);

			});

			curves.Add(CurveType.EMPTY, (Bullet bullet) =>
			{

			});

			curves.Add(CurveType.LINE, (Bullet bullet) =>
			{
				bullet.position += new Vector2(0, 1f * (float)bullet.speed) * bullet.velocity;
			});
		}

		public static Bullet.BulletCurve GetCurve(CurveType curveType)
		{
			return curves[curveType];
		}

	}
}

