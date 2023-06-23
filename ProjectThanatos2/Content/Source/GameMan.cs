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
		public static long score { get; set; }
		public static float playerPower { get; private set; }

        public static int maxEnemies { get; set; }
		public static int currentEnemies = 0;

		public static SpriteFont font = Sprites.font;

        public static bool isPaused = false;
		public static bool inStartMenu = true;
        public static bool inHighScoresMenu = false;
        public static bool isSoundOn = true;

		public static long highscore;
		public static bool shouldUpdateHighScore = false;

		private const float defaultPlayerPower = .9f;
		private const long defaultScore = 0;

        // Bool to draw visible rectangles for collision boxes
        public static bool shouldDrawDebugRectangles = false;

		static GameMan()
		{
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
			// Also helps contain rounding errors!
			GameMan.playerPower = MathF.Round(GameMan.playerPower, 2);
			Player.Instance.UpdatePowerLevelStats();
        }

		public static void StartGame()
		{
			inStartMenu = false;
			isPaused = false;
			inHighScoresMenu = false;

			currentEnemies = 0;

			ClearScoreAndPoints();

			EntityMan.KillAll();
			TimerMan.KillAll();

			Player.Instance.ResetStats();

            EntityMan.Add(Player.Instance);

		}

		public static void QuitGame()
		{
			ProjectThanatos.ProjectThanatos.Instance.Exit();
		}

		public static void ResumeGame()
		{
			isPaused = false;
		}

		public static void QuitToTitle()
		{
			inStartMenu = true;
			isPaused = false;
			EntityMan.KillAll();
		}

		public static void ToggleSound()
		{
			isSoundOn = !isSoundOn;
		}

		public static long ReadHighscores()
		{
			return XmlSerialization.ReadFromXmlFile<long>("score.ptgf");
		}

		public static void WriteHighscores()
		{
			XmlSerialization.WriteToXmlFile<long>("score.ptgf", highscore);
		}


    }
}

