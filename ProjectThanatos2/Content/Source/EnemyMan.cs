using System;
using Microsoft.Xna.Framework;
using ProjectThanatos.Content.Source;
using ProjectThanatos2.Content.Source;
namespace ProjectThanatos2.Content.Source
{
	static class EnemyMan
	{
		private static Random random = new Random();

        private static Vector2 screenLeft = new Vector2(-15, 20);
        private static Vector2 screenRight = new Vector2(15, 20);

        public enum SpawnPositionType
		{
			SCREENSIDE = 0,
			SCREENLEFT = 1,
			SCREENRIGHT = 2,
			SCREENTOP = 3,
			RANDOM = 4
		};

        static EnemyMan()
		{

		}

		public static void Update()
		{
			if(GameMan.currentEnemies < GameMan.maxEnemies && !Player.Instance.isDead)
			{
				AddEnemyRandom();
				GameMan.currentEnemies++;
			}
		}

		public static void AddEnemy(Vector2 spawnPosition, Enemy.EnemyType enemyType, Enemy.EnemyColour enemyColour, int lifetime = 15000, SpawnPositionType spawnPositionType = SpawnPositionType.SCREENTOP)
		{
			// Chooses random enemy type & colour, if necessary
            if (enemyType == Enemy.EnemyType.RANDOM)
                enemyType = (Enemy.EnemyType)random.Next(0, 4);

            if (enemyColour == Enemy.EnemyColour.RANDOM)
                enemyColour = (Enemy.EnemyColour)random.Next(0, 4);

            EntityMan.Add(new Enemy(spawnPosition, enemyType, enemyColour, lifetime));
		}

		public static void AddEnemyRandom(Enemy.EnemyType enemyType = Enemy.EnemyType.RANDOM, Enemy.EnemyColour enemyColour = Enemy.EnemyColour.RANDOM, int lifeTime = 15000)
		{
			SpawnPositionType spawnPositionType;

			if (random.NextBool())
				spawnPositionType = SpawnPositionType.SCREENTOP;
			else
				spawnPositionType = SpawnPositionType.SCREENSIDE;

            // Chooses random enemy type & colour, if necessary
            if (enemyType == Enemy.EnemyType.RANDOM)
                enemyType = (Enemy.EnemyType)random.Next(0, 4);

            if (enemyColour == Enemy.EnemyColour.RANDOM)
                enemyColour = (Enemy.EnemyColour)random.Next(0, 4);

			AddEnemy(GenerateSpawnPosition(spawnPositionType), enemyType, enemyColour, lifeTime);
			
		}

		private static Vector2 GenerateSpawnPosition(SpawnPositionType spawnPositionType)
		{
			Vector2 spawnPosition;

			switch(spawnPositionType)
			{
				case SpawnPositionType.SCREENSIDE:
					if (random.Next() > .5)
						spawnPosition = screenLeft;
					else
						spawnPosition = screenRight;
					break;
				case SpawnPositionType.SCREENLEFT:
					spawnPosition = screenLeft;
					break;
				case SpawnPositionType.SCREENRIGHT:
					spawnPosition = screenRight;
					break;
				case SpawnPositionType.SCREENTOP:
					spawnPosition = new Vector2((random.NextFloat(0, ProjectThanatos.ProjectThanatos.ScreenSize.X)), -20);
					break;
				default:
					spawnPosition = Vector2.Zero;
					break;
			}
			return spawnPosition;
		}
	}
}

