using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectThanatos.Content.Source;

namespace ProjectThanatos2.Content.Source
{
	public class Button
	{
		private string text;
		private string text2;
		public string usingText;
		public Vector2 position;
		private Color defaultColor;
		private Color highlightColor;
		private Action action;

		public bool hovering { get; set; }
		public bool hasClicked { get; private set; }

		

		public Button(string text, Vector2 position, Color defaultColor, Color highlightColor, Action action, string text2 = null)
		{
            this.usingText = text;
            this.text = text;
			this.position = position;
			this.defaultColor = defaultColor;
			this.highlightColor = highlightColor;
			this.action = action;

			this.text2 = text2;
		}

		public void Update(SpriteBatch spriteBatch)
		{
			if(hovering)
			{
				RiceLib.DrawText(spriteBatch, usingText, position, highlightColor);
            }
			else
			{
				RiceLib.DrawText(spriteBatch, usingText, position, defaultColor);
			}
        }

		public void Click()
        {
            if (text2 is not null)
            {
                usingText = text2;
				text2 = null;
            }
			else
			{
				text2 = usingText;
				usingText = text;
			}
            action.Invoke();
		}

		public void ChangeText(string text)
		{
			this.text = text;
			this.text2 = text;
			this.usingText = text;
		}
    }
}

