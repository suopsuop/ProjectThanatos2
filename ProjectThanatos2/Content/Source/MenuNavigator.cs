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

            // Lets the user loop through the different options
			if(hoveringOn > buttonList.Count -1)
			{
				hoveringOn = 0;
			}
			else if (hoveringOn < 0)
			{
				hoveringOn = buttonList.Count - 1;
			}

            buttonList[hoveringOn].hovering = true;


            if (Input.WasKeyPressed(Microsoft.Xna.Framework.Input.Keys.Z))
			{
                //hasClicked = true;
                buttonList[hoveringOn].Click();
            }





        }


    }
}

