using Microsoft.Xna.Framework;
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
        private const int steadyMoveSpeed = 5;

        public int power = 1;

        public bool isFocused = false;

        // Defines the three types of bullets that the player can shoot, depending on level
        PlayerBullet bullet1 = new PlayerBullet(Vector2.Zero,
                9.5f,
                .1f,
                0,
                1200,
            Bullet.BulletType.KNIFE,
                1f);

        PlayerBullet bullet2 = new PlayerBullet(
            Vector2.Zero,
                9.5f,
                .1f,
                0f,
                1200,
            Bullet.BulletType.GEM,
                2f,
                20f);

        PlayerBullet bullet3 = new PlayerBullet(
            Vector2.Zero,
                10f,
                .1f,
                2.5f,
                1200,
            Bullet.BulletType.STAR,
                3f,
                5f);

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

        static Random rand = new Random();

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
                return;
            }
            
            velocity += moveSpeed * Input.GetMovementDirection();
            position += velocity;
            position = Vector2.Clamp(position, spriteSize / 2,
                ProjectThanatos.ScreenSize - spriteSize/2);
                // Stops the player exiting bounds

            velocity = Vector2.Zero;

            if(Input.IsShootKeyDown())
            {
                //TimerMan.Create(500, () => shootBullet());
                shootBullet();
            }

            if(Input.WasBombButtonPressed())
            {
                useBomb(); //CHANGE ME!!!
            }

            //bullet1.position = position;
            //bullet2.position = position;
            //bullet3.position = position;

        }

        public void shootBullet()
        {

            //EntityMan.Add(new EnemyBullet(position, 4, new Vector2(0, -1), Curves.GetCurve(Curves.CurveType.LINE), 4000, instance, instance.position));
            EntityMan.Add(new BulletSpawner(4,1,90,1,1,.1f,1,0,1,false,3,Vector2.One, new Vector2(200,200),2,0,1f,3000,Bullet.BulletType.LASER));

            // Updating each bullet's spawn position



            //switch (power)
            //{
            //    case 1:
            //        EntityMan.Add(bullet1);
            //        break;
            //    case 2:
            //        EntityMan.Add(bullet1);

            //        // Adds two angled bullet2's of opposite angles
            //        EntityMan.Add(bullet2);
            //        bullet2.direction = -bullet2.direction;
            //        EntityMan.Add(bullet2);
            //        break;
            //    case 3:
            //        EntityMan.Add(bullet1);

            //        // Adds two angled bullet2's of opposite angles
            //        EntityMan.Add(bullet2);
            //        bullet2.direction = -bullet2.direction;
            //        EntityMan.Add(bullet2);

            //        // Same thing for bullet3
            //        EntityMan.Add(bullet3);
            //        bullet3.direction = -bullet3.direction;
            //        EntityMan.Add(bullet3);
            //        break;
            //    default:
            //        Debug.WriteLine("Power not 1-3 !!!");
            //        break;
            //}

        }

        public void useBomb()
        {
            EntityMan.Add(new BulletSpawner(4,1,90,1,1,.1f,1,0,1,false,3,
                Vector2.One, new Vector2(200,200),2,0,1f,3000, Bullet.BulletType.CARD));

        }

        public override void Draw(SpriteBatch spriteBatch, Rectangle? spritePos = null, float scale = 1f)
        {
            if (!isPlayerDead)
                base.Draw(spriteBatch);
        }

        public override void Kill()
        {

        }

        //public void changePowerLevel(int level)
        //{
        //    bullet1.damage *= ((float)level * .25f);
        //    bullet2.damage *= ((float)level * .25f);
        //    bullet3.damage *= ((float)level * .25f);

        //    bullet1.acceleration *= .01f + level / 0.02f;
        //    bullet2.acceleration *= .01f + level / 0.02f;
        //    bullet3.acceleration *= .01f + level / 0.02f;
        //}

    }
}
