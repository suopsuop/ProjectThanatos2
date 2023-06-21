using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectThanatos.Content.Source;

namespace ProjectThanatos2.Content.Source
{
	public class Button
	{
		private string text;
		private Vector2 position;
		private Color defaultColor;
		private Color highlightColor;
		private Action action;
		public bool hovering { get; set; }
		public bool hasClicked { get; private set; }

		public Button(string text, Vector2 position, Color defaultColor, Color highlightColor, Action action)
		{
			this.text = text;
			this.position = position;
			this.defaultColor = defaultColor;
			this.highlightColor = highlightColor;
			this.action = action;
		}

		public void Update(SpriteBatch spriteBatch)
		{
			if(hovering)
			{
				RiceLib.DrawText(spriteBatch, text, position, highlightColor);
            }
			else
			{
				RiceLib.DrawText(spriteBatch, text, position, defaultColor);
			}
        }

		public void Click()
		{
			action.Invoke();
		}
	}
}

