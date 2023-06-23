using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectThanatos2.Content.Source
{
	public class Background
	{
		protected Texture2D background = null;
		protected Color color = Color.White;

		public Background(Texture2D background, Color? color = null)
		{
			this.background = background;
			if (color is not null)
				this.color = (Color)color;
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Begin();
			spriteBatch.Draw(background, Vector2.Zero, color);
			spriteBatch.End();
		}
	}
}

