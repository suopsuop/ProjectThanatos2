using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectThanatos2.Content.Source;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProjectThanatos.Content.Source.Enemy;

namespace ProjectThanatos.Content.Source
{
    class Player : Entity
    {
        private static Player instance;

        // Consts, for easier player editing
        private const int moveSpeed = 7;
        private const int steadyMoveSpeed = 3;
        // level1 is implied
        private const float level2 = 1.5f;
        private const float level3 = 2.5f;


        public Vector2 spriteAnimationPos = new Vector2(0,0);

        public int frameDelay = 5;
        public const int defaultFrameDelay = 5;

        public bool isFocused = false;

        int framesTilRespawn = 0;

        // Defines the three types of bullets that the player can shoot, depending on level
        float bullet1Offset = 0f;
        float bullet2Offset = 22f;
        float bullet3Offset = 13f;

        float bullet1SteadyOffset = 0f;
        float bullet2SteadyOffset = 12f;
        float bullet3SteadyOffset = 7f;

        const float UPWARDS = 90f;

        // DEFINING PLAYER BULLETS
        public PlayerBullet bullet1 = new PlayerBullet(
            Vector2.Zero,
                9.5f,
                .1f,
                0,
                1200,
            Bullet.BulletType.KNIFE,
                1f,
                UPWARDS);

        public PlayerBullet bullet2 = new PlayerBullet(
            Vector2.Zero,
                9.5f,
                .1f,
                0f,
                1200,
            Bullet.BulletType.GEM,
                2f,
                UPWARDS);

        public PlayerBullet bullet3 = new PlayerBullet(
            Vector2.Zero,
                10f,
                .1f,
                0f,
                1200,
            Bullet.BulletType.STAR,
                3f,
                UPWARDS);

        private static GameTime gameTime = ProjectThanatos.GameTime;

        public static Player Instance 
        { 
            get 
            {
                if (instance == null)
                    instance = new Player();

                return instance;
            }
        }

        public bool isDead
        {
            get
            {
                return isExpired;
            }
            set
            {
                isExpired = isDead;
            }
        }

        private Player()
        {
            sprite = Sprites.playerSpriteSheet;
            // The -5 is to centre
            collisionBox = new Rectangle((int)this.position.X - 2, (int)this.position.Y - 2, 5, 5);


        }

        public override void Update()
        {
            if (isDead)
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
                useBomb();
            }

            // Animation stuff below
            if (frameDelay == 0) // Animates every defaultFrameDelay frames
            {
                if (spriteAnimationPos.X <= 0)
                {
                    spriteAnimationPos.X++;
                }
                else
                {
                    spriteAnimationPos.X--;
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

            bullet1.isExpired = false;
            bullet2.isExpired = false;
            bullet3.isExpired = false;


            // Adds bullets that are a clone of bullet1
            EntityMan.Add((PlayerBullet)bullet1.Clone());

            if (GameMan.playerPower >= level2)
            {
                EntityMan.Add((PlayerBullet)bullet2.Clone());
                bullet2Offset *= -1;
                bullet2SteadyOffset *= -1;

                if(Input.IsShiftDown())
                    bullet2.direction = UPWARDS + bullet2SteadyOffset;
                else
                    bullet2.direction = UPWARDS + bullet2Offset;

                EntityMan.Add((PlayerBullet)bullet2.Clone());

                if (GameMan.playerPower >= level3)
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
        }

        public void useBomb()
        {
            EntityMan.KillAllEnemyBullets();
            GameMan.score -= (long)(5000 * GameMan.playerPower);
        }

        public override void Draw(SpriteBatch spriteBatch, Rectangle? spritePos = null, float scale = 1f, SpriteEffects spriteEffects = SpriteEffects.None)
        {
            if (!isDead)
                base.Draw(spriteBatch, new Rectangle((int)spriteAnimationPos.X * 32,(int)spriteAnimationPos.Y * 48,32,48));
        }

        public override void Kill()
        {
            base.Kill();
        }

        public void UpdatePowerLevelStats()
        {
            bullet1.damage += GameMan.playerPower / 40;
            bullet2.damage += GameMan.playerPower / 40;
            bullet3.damage += GameMan.playerPower / 40;

            //bullet1.acceleration += .0015f;
            //bullet2.acceleration += .0015f;
            //bullet3.acceleration += .0015f;

            // Changing player bullet looks based on power (placebo!)
            switch(GameMan.playerPower)
            {
                case < 1f:
                    bullet1.bulletColour = Bullet.BulletColour.GREY;
                    bullet2.bulletColour = Bullet.BulletColour.GREY;
                    bullet3.bulletColour = Bullet.BulletColour.GREY;

                    bullet1.bulletType = Bullet.BulletType.BULLET;
                    bullet2.bulletType = Bullet.BulletType.BULLET;
                    bullet3.bulletType = Bullet.BulletType.BULLET;
                    break;

                case <= 1.26f:
                    bullet1.bulletColour = Bullet.BulletColour.GREEN;
                    bullet2.bulletColour = Bullet.BulletColour.FILLGREEN;
                    bullet3.bulletColour = Bullet.BulletColour.FILLGREEN;

                    bullet1.bulletType = Bullet.BulletType.KNIFE;
                    bullet2.bulletType = Bullet.BulletType.ORB;
                    bullet3.bulletType = Bullet.BulletType.ORB;
                    break;

                case <= level2:
                    bullet1.bulletColour = Bullet.BulletColour.RED;
                    bullet2.bulletColour = Bullet.BulletColour.FILLRED;
                    bullet3.bulletColour = Bullet.BulletColour.FILLRED;

                    bullet1.bulletType = Bullet.BulletType.GEM;
                    bullet2.bulletType = Bullet.BulletType.KNIFE;
                    bullet3.bulletType = Bullet.BulletType.KNIFE;
                    break;

                case <= 1.76f:
                    bullet1.bulletColour = Bullet.BulletColour.RED;
                    bullet2.bulletColour = Bullet.BulletColour.GREEN;
                    bullet3.bulletColour = Bullet.BulletColour.GREEN;

                    bullet1.bulletType = Bullet.BulletType.GEM;
                    bullet2.bulletType = Bullet.BulletType.KNIFE;
                    bullet3.bulletType = Bullet.BulletType.KNIFE;
                    break;

                case <= 2f:
                    bullet1.bulletColour = Bullet.BulletColour.PINK;
                    bullet2.bulletColour = Bullet.BulletColour.CYAN;
                    bullet3.bulletColour = Bullet.BulletColour.FILLCYAN;

                    bullet1.bulletType = Bullet.BulletType.GEM;
                    bullet2.bulletType = Bullet.BulletType.KNIFE;
                    bullet3.bulletType = Bullet.BulletType.KNIFE;
                    break;

                case <= 2.26f:
                    bullet1.bulletColour = Bullet.BulletColour.BLUE;
                    bullet2.bulletColour = Bullet.BulletColour.YELLOW;
                    bullet3.bulletColour = Bullet.BulletColour.WHITE;

                    bullet1.bulletType = Bullet.BulletType.KUNAI;
                    bullet2.bulletType = Bullet.BulletType.ORB;
                    bullet3.bulletType = Bullet.BulletType.GEM;
                    break;

                case <= level3:
                    bullet1.bulletColour = Bullet.BulletColour.GOLD;
                    bullet2.bulletColour = Bullet.BulletColour.FILLPINK;
                    bullet3.bulletColour = Bullet.BulletColour.FILLRED;

                    bullet1.bulletType = Bullet.BulletType.BULLET;
                    bullet2.bulletType = Bullet.BulletType.CARD;
                    bullet3.bulletType = Bullet.BulletType.ORB;

                    GameMan.maxEnemies = 3;
                    break;

                default:
                    bullet1.bulletColour = Bullet.BulletColour.GOLD;
                    bullet2.bulletColour = Bullet.BulletColour.FILLPINK;
                    bullet3.bulletColour = Bullet.BulletColour.FILLRED;

                    bullet1.bulletType = Bullet.BulletType.BULLET;
                    bullet2.bulletType = Bullet.BulletType.CARD;
                    bullet3.bulletType = Bullet.BulletType.ORB;
                    break;
            }
        }

        //public void ResetStats()
        //{
        //    isDead = false;
        //    isExpired = false;
        //}
    }
}
