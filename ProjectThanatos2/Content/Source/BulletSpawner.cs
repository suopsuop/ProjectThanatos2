using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectThanatos.Content.Source;
using ProjectThanatos2.Content.Source;
using static ProjectThanatos.Content.Source.Bullet;
using static ProjectThanatos.Content.Source.RiceLib;

   ////////////////////////////////////////////////////////////////////
  // code used as a base, heavily modified, removed, and added to   //
 // https://github.com/ReiwuKleiwu/Bullet-Hell-Pattern-Generator   //
////////////////////////////////////////////////////////////////////

namespace ProjectThanatos2.Content.Source
{
	public class BulletSpawner : Entity
	{
        Entity parent;
        private static Random random = new Random();

        private int patternArrays;
        private int bulletsPerArray;

        private float spreadBetweenBulletArray;
        private float spreadWithinBulletArray;
        private float startAngle;

        private float beginSpinSpeed;
        private float spinRate;
        private float spinModifier;
        private float maxSpinRate;
        private bool shouldInvertSpin; // True if |spinRate| >= maxSpinRate
        private float defaultAngle = 0f;

        private readonly int fireRate;
        private int framesTillShoot; // frame tracker for firerate

        private Vector2 spawnerSize;
        public int spawnerLifeTime;

        private float bulletSpeed;
        private float bulletAcceleration;
        private float bulletCurve;
        public int bulletLifeTime;
        private bool shouldRandomisePosition;
        private Bullet.BulletType bulletType;
        private Bullet.BulletColour bulletColour;

        // Creates a random bullet spawner, influenced by player's power
        public static BulletSpawner MakeRandom(Entity parent)
        {
            int patternArrays = random.Next(1,7);

            int bulletsPerArray = random.Next(1, 4);

            float spreadBetweenBulletArray = (int)(360 / patternArrays);

            float spreadWithinBulletArray = random.NextFloat(4,12);

            float startAngle = random.Next(0, 359);

            float spinRate = random.NextFloat(0, 4 * GameMan.playerPower);

            float spinModifier = random.NextFloat(0, 3);

            float maxSpinRate = random.NextFloat(spinModifier, spinModifier + 2);

            bool shouldInvertSpin = random.NextBool();

            int fireRate = random.Next(3, 13);

            Vector2 spawnerSize = random.NextVector2(2, 7);

            float bulletSpeed = random.NextFloat(0.1f,1.4f * GameMan.playerPower);

            float bulletAcceleration;
            if (random.NextBool() && random.NextBool() && random.NextBool()) // 1/3 chance
                bulletAcceleration = random.NextFloat(-.035f, 0.005f);
            else bulletAcceleration = 0;

            float bulletCurve = random.NextFloat(-4 * GameMan.playerPower, 4 * GameMan.playerPower);

            int bulletLifeTime;

            bulletLifeTime = random.Next(4600, (int)(13000 * GameMan.playerPower));

            bool shouldRandomisePosition = false;

            if (random.NextBool())
                shouldRandomisePosition = true;

            Bullet.BulletType bulletType = (Bullet.BulletType)random.Next(0, 8);

            Bullet.BulletColour bulletColour = (Bullet.BulletColour)random.Next(0, 15);

            int spawnerLifeTime = random.Next(4000, (int)(10000 * GameMan.playerPower));

            return new BulletSpawner(
                parent,
                patternArrays,
                bulletsPerArray,
                spreadBetweenBulletArray,
                spreadWithinBulletArray,
                startAngle,
                spinRate,
                spinModifier,
                maxSpinRate,
                shouldInvertSpin,
                fireRate,
                spawnerSize,
                parent.position,
                bulletSpeed,
                bulletAcceleration,
                bulletCurve,
                bulletLifeTime,
                shouldRandomisePosition,
                bulletType,
                bulletColour,
                spawnerLifeTime);
        }


        public BulletSpawner(
            Entity parent,
            int patternArrays,
            int bulletsPerArray,
            float spreadBetweenBulletArray,
            float spreadWithinBulletArray,
            float startAngle,
            float spinRate,
            float spinModifier,
            float maxSpinRate,
            bool shouldInvertSpin,
            int fireRate,
            Vector2 spawnerSize,
            Vector2 startPosition,
            float bulletSpeed,
            float bulletAcceleration,
            float bulletCurve,
            int bulletLifeTime,
            bool shouldRandomisePosition,
            Bullet.BulletType bulletType,
            Bullet.BulletColour bulletColour = Bullet.BulletColour.GREY,
            int spawnerLifeTime = 0)
		{
            sprite = null;

            this.parent = parent;
            this.patternArrays = patternArrays;
            this.bulletsPerArray = bulletsPerArray;
            this.spreadBetweenBulletArray = spreadBetweenBulletArray;
            this.spreadWithinBulletArray = spreadWithinBulletArray;
            this.startAngle = startAngle;
            this.spinRate = spinRate;
            this.spinModifier = spinModifier;
            this.maxSpinRate = maxSpinRate;
            this.shouldInvertSpin = shouldInvertSpin;
            this.fireRate = fireRate;
            this.framesTillShoot = fireRate;
            this.spawnerSize = spawnerSize;
            this.position = startPosition;
            this.bulletSpeed = bulletSpeed;
            this.bulletAcceleration = bulletAcceleration;
            this.bulletCurve = bulletCurve;
            this.bulletLifeTime = bulletLifeTime;
            this.shouldRandomisePosition = shouldRandomisePosition;
            this.spawnerLifeTime = spawnerLifeTime;
            this.bulletType = bulletType;
            this.bulletColour = bulletColour;

            this.shouldDraw = false;

            if (spawnerLifeTime > 0) // Creates a timer to kill the bullet after its lifetime
            {
                TimerMan.Create(spawnerLifeTime, () => Kill());
            }

            //PLACEHOLDER
            //this.position = Player.Instance.position;
		}

        public override void Update()
        {
            if (!EntityMan.DoesEntityExist(parent))
                Kill();

            framesTillShoot--;

            position = parent.position;

            int bulletLength = bulletsPerArray - 1; // Not sure what this does yet

            if (bulletLength == 0) bulletLength = 1;

            int arrayLength = patternArrays - 1 * patternArrays; // No idea what this does either

            if (arrayLength == 0) arrayLength = 1;

            float arrayAngle = (spreadWithinBulletArray / bulletLength); // Spread between each array
            float bulletAngle = (spreadBetweenBulletArray / arrayLength); // Spread between each bullet stream per array

            if (framesTillShoot <= 0)
            {
                framesTillShoot = fireRate; // Resets frame counter

                for(int i = 0; i < patternArrays; i++)
                {
                    for(int j = 0; j < bulletsPerArray; j++)
                    {
                        spawnBullet(i, j, arrayAngle, bulletAngle);
                    }
                }

                if (defaultAngle >= 360)
                {
                    defaultAngle = 0;
                }

                defaultAngle += spinRate;
                spinRate += spinModifier;

                if (shouldInvertSpin)
                {
                    if (spinRate < -maxSpinRate || spinRate > maxSpinRate)
                    {
                        spinModifier = -spinModifier;
                    }
                }
            }



        }

        private void spawnBullet(int indexI, int indexJ, float arrayAngle, float bulletAngle)
        {

            Vector2 spawnPosition = new Vector2(
                position.X + LengthDirection(spawnerSize.X, startAngle + (bulletAngle * indexI) + (arrayAngle * indexJ) + startAngle).X,
                position.Y + LengthDirection(spawnerSize.Y, startAngle + (bulletAngle * indexI) + (arrayAngle * indexJ) + startAngle).Y);

            float direction = startAngle + (bulletAngle * indexI) + (arrayAngle * indexJ) + defaultAngle;

            EntityMan.Add(new EnemyBullet(Player.Instance, Player.Instance.position, spawnPosition, bulletSpeed, bulletAcceleration, bulletCurve, bulletLifeTime, shouldRandomisePosition, bulletType, direction, bulletColour));

        }

    }
}

