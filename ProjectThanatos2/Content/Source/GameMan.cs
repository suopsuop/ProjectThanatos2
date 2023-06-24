using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectThanatos.Content.Source;

namespace ProjectThanatos2.Content.Source
{
	static class GameMan
	{
		public static bool shouldClearBackground = true;

		public static long score { get; set; }
		public static long deathScore { get; set; }
		public static float playerPower { get; private set; }

		public static int maxEnemies { get; set; }
		public static int currentEnemies { get; set; }

		public static SpriteFont font = Sprites.font;

		public static bool isSoundOn = true;

		public static long highscore;
		public static bool shouldUpdateHighScore = false;

		private const float defaultPlayerPower = .9f;
		private const long defaultScore = 0;

		// Bool to draw visible rectangles for collision boxes
		public static bool shouldDrawDebugRectangles = false;

		static GameMan()
		{
			// Default values for constructor
			currentEnemies = 0;
			maxEnemies = 2;
			score = defaultScore;
			playerPower = defaultPlayerPower;
		}

		public static void ClearScoreAndPoints()
		{
			score = defaultScore;
			playerPower = defaultPlayerPower;
		}

		public static void AddPlayerPower()
		{
			GameMan.playerPower += 0.02f;
			// Because we're displaying this, we round it to look nice.
			GameMan.playerPower = MathF.Round(GameMan.playerPower, 2);
			Player.Instance.UpdatePowerLevelStats();
		}

		// Resets all variables 
		public static void StartGame()
		{
			ProjectThanatos.ProjectThanatos.gameState = ProjectThanatos.ProjectThanatos.GameState.INGAME;
			currentEnemies = 0;

			ClearScoreAndPoints();

			EntityMan.KillAll();
			TimerMan.KillAll();

			Player.Instance.ResetStats();

			EntityMan.Add(Player.Instance);

            Player.Instance.UpdatePowerLevelStats();

        }

        public static void QuitGame()
		{
			ProjectThanatos.ProjectThanatos.Instance.Exit();
		}

		public static void ResumeGame()
		{
			ProjectThanatos.ProjectThanatos.gameState = ProjectThanatos.ProjectThanatos.GameState.INGAME;
		}

		public static void QuitToTitle()
		{
			ProjectThanatos.ProjectThanatos.startMenuButtonList.ArrangeButtons();
			ProjectThanatos.ProjectThanatos.gameState = ProjectThanatos.ProjectThanatos.GameState.STARTMENU;
		}

		public static void UpdateButtons(SpriteBatch spriteBatch, List<Button> buttonList)
		{
			foreach (Button button in buttonList)
			{
				button.Update(spriteBatch);
			}
		}

		public static void ToggleSound()
		{
			isSoundOn = !isSoundOn;
		}

		public static long ReadHighscores()
		{
			return XmlSerialization.ReadFromXmlFile<long>("score.xml");
		}

		public static void WriteHighscores()
		{
			XmlSerialization.WriteToXmlFile<long>("score.xml", highscore);
		}

	}
}

