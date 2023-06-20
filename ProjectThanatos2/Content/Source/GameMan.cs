using System;
namespace ProjectThanatos2.Content.Source
{
	static class GameMan
	{
		public static ulong score { get; private set; }
		public static float playerPower { get; set; }

        public const int maxEnemies = 2;
		public static int currentEnemies = 0;


        // DEBUG
        public static bool shouldDrawDebugRectangles = false;


		static GameMan()
		{
			playerPower = 1;
		}

		public static void ChangeScore(ulong scoreToAdd)
		{
			score += scoreToAdd;
		}

		public static void ClearScore()
		{
			score = 0;
		}
	}
}

