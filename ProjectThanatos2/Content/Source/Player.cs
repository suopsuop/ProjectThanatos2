﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectThanatos2.Content.Source;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectThanatos.Content.Source
{
    class Player : Entity
    {
        private static Player instance;

        private const int moveSpeed = 8;
        private const int steadyMoveSpeed = 4;

        public float power = 1;

        public Vector2 spriteAnimationPos = new Vector2(0,0);

        public int frameDelay = 8;
        public const int defaultFrameDelay = 8;

        public bool isFocused = false;

        // Defines the three types of bullets that the player can shoot, depending on level
        float bullet1Offset = 0f;
        float bullet2Offset = 45f;
        float bullet3Offset = 20f;

        float bullet1SteadyOffset = 0f;
        float bullet2SteadyOffset = 25f;
        float bullet3SteadyOffset = 10f;

        const float UPWARDS = 90f;

        PlayerBullet bullet1 = new PlayerBullet(Vector2.Zero,
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


        int framesTilRespawn = 0;

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
            position = Vector2.Zero;
            collisionBox = new Rectangle((int)this.position.X, (int)this.position.Y, 5, 5);
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
            position = Vector2.Clamp(position, new Vector2(collisionBox.Width,collisionBox.Height),
                ProjectThanatos.ScreenSize - new Vector2(collisionBox.Width,collisionBox.Height));
                // Stops the player exiting bounds

            velocity = Vector2.Zero;

            if(Input.IsShootKeyDown())
            {
                //TimerMan.Create(500, () => shootBullet());
                shootBullet();
            }

            if(Input.WasBombButtonPressed())
            {
                power += .5f; // TEST TEST TEST
                changePowerLevel(power); // TEST TEST TEST

                useBomb();
            }

            // Animation stuff below
            if (frameDelay == 0)
            {
                if (spriteAnimationPos.X <= 0)
                {
                    spriteAnimationPos.X += 1;
                }
                else
                {
                    spriteAnimationPos.X -= 1;
                }
            }
            Debug.WriteLine(Input.GetMovementDirection());
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

            if (power >= 3)
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
            if (power >= 5)
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
            EntityMan.Add(new BulletSpawner(4,1,90,1,1,.1f,50f,0,1,false,3,
                Vector2.One, new Vector2(200,200),2,0,1f,3000, Bullet.BulletType.CARD));

        }

        public override void Draw(SpriteBatch spriteBatch, Rectangle? spritePos = null, float scale = 1f)
        {
            if (!isPlayerDead)
                base.Draw(spriteBatch, new Rectangle((int)spriteAnimationPos.X * 32,(int)spriteAnimationPos.Y * 48,32,48));
        }

        public override void Kill()
        {

        }

        public void changePowerLevel(float level)
        {
            bullet1.damage *= (level * .25f);
            bullet2.damage *= (level * .25f);
            bullet3.damage *= (level * .25f);

            bullet1.acceleration *= .01f;
            bullet2.acceleration *= .01f;
            bullet3.acceleration *= .01f;
        }

    }
}
