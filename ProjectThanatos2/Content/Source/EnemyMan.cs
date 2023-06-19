using System;
using Microsoft.Xna.Framework;
using ProjectThanatos.Content.Source;
using ProjectThanatos2.Content.Source;


namespace ProjectThanatos2.Content.Source
{
	static class EnemyMan
	{
		private static Random random = new Random();

        private static Vector2 screenLeft = new Vector2(-30, 40);
        private static Vector2 screenRight = new Vector2(30, 40);


        enum SpawnPositionType
		{
			SCREENSIDE = 0,
			SCREENLEFT = 1,
			SCREENRIGHT = 2,
			SCREENTOP = 3
		};

        static EnemyMan()
		{
		}

		public static void AddEnemy(Vector2 spawnPosition, Enemy.EnemyType enemyType, Enemy.EnemyColour enemyColour, int lifetime = 5000)
		{
			EntityMan.Add(new Enemy(spawnPosition, enemyType, enemyColour, lifetime));
		}

		public static void AddEnemyRandom(Enemy.EnemyType enemyType, Enemy.EnemyColour enemyColour = Enemy.EnemyColour.BLUE, int lifeTime = 0)
		{
			SpawnPositionType spawnPositionType;

			if (random.Next() >= 5)
				spawnPositionType = SpawnPositionType.SCREENTOP;
			else
				spawnPositionType = SpawnPositionType.SCREENSIDE;

			if(enemyType == Enemy.EnemyType.RANDOM) // Selects a random type
			{
				switch(random.Next(0,4))
				{
					case 0:
						enemyType = Enemy.EnemyType.LARGEFAIRY;
						break;
					case 1:
						enemyType = Enemy.EnemyType.SMALLFAIRY;
						break;
					case 2:
						enemyType = Enemy.EnemyType.SMALLDARKFAIRY;
						break;
					case 3:
						enemyType = Enemy.EnemyType.METEOR;
						break;
					default:
						enemyType = Enemy.EnemyType.SMALLFAIRY;
						break;
				}
			}

            if (enemyColour == Enemy.EnemyColour.RANDOM)
            {
                switch (random.Next(0, 4))
                {
                    case 0:
                        enemyColour = Enemy.EnemyColour.BLUE;
                        break;
                    case 1:
                        enemyColour = Enemy.EnemyColour.GREEN;
                        break;
                    case 2:
                        enemyColour = Enemy.EnemyColour.RED;
                        break;
                    case 3:
                        enemyColour = Enemy.EnemyColour.YELLOW;
                        break;
                    default:
                        enemyColour = Enemy.EnemyColour.BLUE;
                        break;
                }
            }


            EntityMan.Add(new Enemy(GenerateSpawnPosition(spawnPositionType),enemyType,enemyColour,lifeTime));

			//EntityMan.Add(new Enemy(enemyType,enemyColour));
			
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
					spawnPosition = new Vector2((random.NextFloat(0, ProjectThanatos.ProjectThanatos.ScreenSize.X)), -30);
					break;
				default:
					spawnPosition = Vector2.Zero;
					break;
			}
			return spawnPosition;
		}
	}
}

