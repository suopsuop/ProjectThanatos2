using System;
namespace ProjectThanatos2.Content.Source
{
	static class GameMan
	{
		public static ulong score { get; private set; }
		public static float playerPower { get; set; }
		public static bool shouldDrawDebugRectangles = true;


		static GameMan()
		{
			playerPower = 1;
		}

		public static void AddScore(ulong scoreToAdd)
		{
			score += scoreToAdd;
		}

		public static void ClearScore()
		{
			score = 0;
		}
	}
}

