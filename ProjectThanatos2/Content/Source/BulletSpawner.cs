using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectThanatos.Content.Source;
using ProjectThanatos2.Content.Source;

namespace ProjectThanatos2.Content.Source
{
	public class BulletSpawner : Entity
	{
        private int[] bulletArray;

        public int patternArrays;
        public int bulletsPerArray;

        public float spreadBetweenBulletArray;
        public float spreadWithinBulletArray;
        public float startAngle;

        public float beginSpinSpeed;
        public float spinRate;
        public float spinModifier;
        public float maxSpinRate;
        public bool shouldInvertSpin; // True if |spinRate| >= maxSpinRate
        public float defaultAngle = 0f;

        public int fireRate;
        public int framesTillShoot; // frame tracker for firerate

        public Vector2 spawnerSize;
        public int spawnerLifeTime;

        public float bulletSpeed;
        public float bulletAcceleration;
        public float bulletCurve;
        public int bulletLifeTime;

        

        public BulletSpawner(int patternArrays, int bulletsPerArray, float spreadBetweenBulletArray, float spreadWithinBulletArray, float startAngle, float beginSpinSpeed, float spinRate, float spinModifier, float maxSpinRate, bool shouldInvertSpin, int fireRate, Vector2 spawnerSize, Vector2 position, float bulletSpeed, float bulletAcceleration, float bulletCurve, int bulletLifeTime, int spawnerLifeTime = 0)
		{
            sprite = null;

            this.patternArrays = patternArrays;
            this.bulletsPerArray = bulletsPerArray;
            this.spreadBetweenBulletArray = spreadBetweenBulletArray;
            this.spreadWithinBulletArray = spreadWithinBulletArray;
            this.startAngle = startAngle;
            this.beginSpinSpeed = beginSpinSpeed;
            this.spinRate = spinRate;
            this.spinModifier = spinModifier;
            this.maxSpinRate = maxSpinRate;
            this.shouldInvertSpin = shouldInvertSpin;
            this.fireRate = fireRate;
            this.spawnerSize = spawnerSize;
            this.position = position;
            this.bulletSpeed = bulletSpeed;
            this.bulletAcceleration = bulletAcceleration;
            this.bulletCurve = bulletCurve;
            this.bulletLifeTime = bulletLifeTime;
            this.spawnerLifeTime = spawnerLifeTime;

            this.shouldDraw = false;

            if (spawnerLifeTime >= 0) // Creates a timer to kill the bullet after its lifetime
            {
                TimerMan.Create(spawnerLifeTime, () => Kill());
            }

            //PLACEHOLDER
            this.position = Player.Instance.position;
		}

        public override void Update()
        {

            int bulletLength = bulletsPerArray - 1; // Not sure what this does yet

            if (bulletLength == 0) bulletLength = 1;

            int arrayLength = patternArrays - 1 * patternArrays; // No idea what this does either

            if (arrayLength == 0) arrayLength = 1;

            float arrayAngle = (spreadWithinBulletArray / bulletLength); // Spread between each array
            float bulletAngle = (spreadBetweenBulletArray / arrayLength); // Spread between each bullet stream per array

            if (framesTillShoot == 0)
            {
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
                position.X + lengthDirection(spawnerSize.X, startAngle + (bulletAngle * indexI) + (arrayAngle * indexJ) + startAngle).X,
                position.Y + lengthDirection(spawnerSize.Y, startAngle + (bulletAngle * indexI) + (arrayAngle * indexJ) + startAngle).Y);

            float direction = startAngle + (bulletAngle * indexI) + (arrayAngle * indexJ) + defaultAngle;

            EntityMan.Add(new EnemyBullet(Player.Instance, Player.Instance.position, spawnPosition, bulletSpeed, bulletAcceleration, bulletCurve, bulletLifeTime, direction));

        }

        private Vector2 lengthDirection(float distance, float angle)
        {
            return new Vector2(
                distance * MathF.Cos((angle * MathF.PI) / 180),
                distance * -MathF.Sin((angle * MathF.PI) / 180));
        }
    }
}

