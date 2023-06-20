using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectThanatos2.Content.Source;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProjectThanatos2.Content.Source.Enemy;

namespace ProjectThanatos.Content.Source
{
    class Player : Entity
    {
        private static Player instance;

        private const int moveSpeed = 7;
        private const int steadyMoveSpeed = 3;

        public Vector2 spriteAnimationPos = new Vector2(0,0);

        public int frameDelay = 5;
        public const int defaultFrameDelay = 5;

        public bool isFocused = false;

        int framesTilRespawn = 0;

        // Defines the three types of bullets that the player can shoot, depending on level
        float bullet1Offset = 0f;
        float bullet2Offset = 30f;
        float bullet3Offset = 15f;

        float bullet1SteadyOffset = 0f;
        float bullet2SteadyOffset = 12f;
        float bullet3SteadyOffset = 7f;

        // DEBUG
        Vector2 bombPos;

        const float UPWARDS = 90f;

        PlayerBullet bullet1 = new PlayerBullet(
            Vector2.Zero,
                9.5f,
                .1f,
                0,
                1200,
            Bullet.BulletType.KNIFE,
                1f,
                UPWARDS);

        PlayerBullet bullet2 = new PlayerBullet(
            Vector2.Zero,
                9.5f,
                .1f,
                0f,
                1200,
            Bullet.BulletType.GEM,
                2f,
                UPWARDS);

        PlayerBullet bullet3 = new PlayerBullet(
            Vector2.Zero,
                10f,
                .1f,
                2.5f,
                1200,
            Bullet.BulletType.STAR,
                3f,
                UPWARDS);

        static GameTime gameTime = ProjectThanatos.GameTime;


        public static Player Instance 
        { 
            get 
            {
                if (instance == null)
                    instance = new Player();

                return instance;
            }
        }

        public bool isPlayerDead
        {
            get
            {
                return framesTilRespawn > 0;
            }
        }

        private Player()
        {
            sprite = Sprites.playerSpriteSheet;
            position = ProjectThanatos.ScreenSize / 2;
            // The -5 is to centre
            collisionBox = new Rectangle((int)this.position.X - 2, (int)this.position.Y - 2, 5, 5);
        }

        public override void Update()
        {
            if (isPlayerDead)
            {
                // ADD TO ME 
                return;
            }



            if (frameDelay < 0)
                frameDelay = defaultFrameDelay;
            else
                frameDelay--;

            // Changes what speed to multiply the player
            if (Input.IsShiftDown())
            {
                velocity += steadyMoveSpeed * Input.GetMovementDirection();
            }
            else
            {
                velocity += moveSpeed * Input.GetMovementDirection();
            }
            
            position += velocity;

            // Stops the player exiting bounds
            position = Vector2.Clamp(position, new Vector2(collisionBox.Width,collisionBox.Height),
                ProjectThanatos.ScreenSize - new Vector2(collisionBox.Width,collisionBox.Height));

            velocity = Vector2.Zero;

            // Updates collisionBox Location
            collisionBox.Location = position.ToPoint() - new Point(collisionBox.Width/2,collisionBox.Height/2);

            if (Input.IsShootKeyDown())
            {
                shootBullet();
            }

            if(Input.WasBombButtonPressed())
            {
                GameMan.playerPower += .5f; // TEST TEST TEST
                updatePowerLevelStats(); // TEST TEST TEST

                useBomb();
            }

            // Animation stuff below
            if (frameDelay == 0)
            {
                if (spriteAnimationPos.X <= 0)
                {
                    spriteAnimationPos.X = defaultFrameDelay - 1;
                }
                else
                {
                    spriteAnimationPos.X -= 1;
                }
            }
            if (Input.GetMovementDirection().X == 0)
                spriteAnimationPos.Y = 0;
            else if (Input.GetMovementDirection().X > 0)
                spriteAnimationPos.Y = 2;
            else
                spriteAnimationPos.Y = 1;
        }

        public void shootBullet()
        {

            // Updating each bullet's spawn position
            bullet1.position = position;
            bullet2.position = position;
            bullet3.position = position;

            // Will add more bullet streams based on 
            EntityMan.Add((PlayerBullet)bullet1.Clone());

            if (GameMan.playerPower >= 3)
            {
                EntityMan.Add((PlayerBullet)bullet2.Clone());
                bullet2Offset *= -1;
                bullet2SteadyOffset *= -1;
                if(Input.IsShiftDown())
                    bullet2.direction = UPWARDS + bullet2SteadyOffset;
                else
                    bullet2.direction = UPWARDS + bullet2Offset;


                EntityMan.Add((PlayerBullet)bullet2.Clone());
            }
            if (GameMan.playerPower >= 5)
            {
                // Same thing for bullet3

                EntityMan.Add((PlayerBullet)bullet3.Clone());
                bullet3Offset *= -1;
                bullet3SteadyOffset *= -1;

                if (Input.IsShiftDown())
                    bullet2.direction = UPWARDS + bullet3SteadyOffset;
                else
                    bullet2.direction = UPWARDS + bullet3Offset;

                EntityMan.Add((PlayerBullet)bullet3.Clone());
            }

        }

        public void useBomb()
        {
            //EntityMan.Add(new BulletSpawner(instance,4,1,90,1,1,.4f,1.5f,0.1f,4,true,7,
            //    Vector2.One,position,2,0,1f,10000, Bullet.BulletType.CARD, Bullet.BulletColour.RED, 6000)); //TEST TEST TEST
            bombPos = position;
            TimerMan.Create(1000, () => EnemyMan.AddEnemy(bombPos, Enemy.EnemyType.SMALLFAIRY, Enemy.EnemyColour.RED)); // TEST TEST TEST

            TimerMan.Create(500, () => RiceLib.Attack(instance, bombPos, BulletPattern.NONE, 500));


        }

        public override void Draw(SpriteBatch spriteBatch, Rectangle? spritePos = null, float scale = 1f, SpriteEffects spriteEffects = SpriteEffects.None)
        {
            if (!isPlayerDead)
                base.Draw(spriteBatch, new Rectangle((int)spriteAnimationPos.X * 32,(int)spriteAnimationPos.Y * 48,32,48));
        }

        public override void Kill()
        {
            //EntityMan.Add(new BulletSpawner(8, 1, 360/8, 1, 1, .4f, 1.5f, 0.1f, 4, true, 7,
            //    Vector2.One, position, 2, 0, 1f, 10000, Bullet.BulletType.CARD, Bullet.BulletColour.GOLD, 6000));

            base.Kill();
            
        }

        public void updatePowerLevelStats()
        {
            bullet1.damage *= GameMan.playerPower;
            bullet2.damage *= GameMan.playerPower;
            bullet3.damage *= GameMan.playerPower;

            bullet1.acceleration += .003f;
            bullet2.acceleration += .003f;
            bullet3.acceleration += .003f;
        }
    }
}
