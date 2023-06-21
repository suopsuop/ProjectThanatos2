using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectThanatos.Content.Source;

namespace ProjectThanatos2.Content.Source
{
    static class GameMan
	{
		public static long score { get; set; }
		public static float playerPower { get; set; }
	

        public const int maxEnemies = 2;
		public static int currentEnemies = 0;

		public static SpriteFont font = Sprites.font;

        public static bool isPaused = false;
		public static bool inStartMenu = true;

        // Bool to draw visible rectangles for collision boxes
        public static bool shouldDrawDebugRectangles = false;

		static GameMan()
		{
            score = 0;
            playerPower = .9f;
		}

		public static void ClearScore()
		{
			score = 0;
		}

		public static void AddPlayerPower()
		{
            GameMan.playerPower += 0.02f;
			// Because we're displaying this, we round it to look nice.
			// Also helps contain rounding errors!
			GameMan.playerPower = MathF.Round(GameMan.playerPower, 2);
			Player.Instance.UpdatePowerLevelStats();
        }

        public static void UpdateGame()
		{

		}

		public static void UpdatePauseMenu()
		{

		}

		public static void StartGame()
		{
			inStartMenu = false;
			ProjectThanatos.ProjectThanatos.InitialiseGame();

		}

		public static void QuitGame()
		{
			ProjectThanatos.ProjectThanatos.Instance.Exit();
		}
    }
}

