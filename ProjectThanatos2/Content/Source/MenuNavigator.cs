using System;
using System.Collections.Generic;
using ProjectThanatos.Content.Source;

namespace ProjectThanatos2.Content.Source
{
	static class MenuNavigator
	{
		private static int hoveringOn = 0;
		//private static bool hasClicked = false;

		static MenuNavigator()
		{
		}

		public static void Update(List<Button> buttonList)
		{
			foreach (Button button in buttonList)
			{
				button.hovering = false;
			}

			if (Input.WasKeyPressed(Microsoft.Xna.Framework.Input.Keys.Up))
				hoveringOn -= 1;
			if (Input.WasKeyPressed(Microsoft.Xna.Framework.Input.Keys.Down))
				hoveringOn += 1;

            // Makes sure user cannot select an index out of range
            hoveringOn = Math.Clamp(hoveringOn, 0, buttonList.Count - 1);

            buttonList[hoveringOn].hovering = true;


            if (Input.WasKeyPressed(Microsoft.Xna.Framework.Input.Keys.Z))
			{
                //hasClicked = true;
                buttonList[hoveringOn].Click();
            }





        }


    }
}

