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

        public bool isFocused = false;

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
            position = Vector2.Clamp(position, spriteSize / 2, ProjectThanatos.ScreenSize - spriteSize/2); // Stops the player exiting bounds

            velocity = Vector2.Zero;

            if(Input.IsShootKeyDown())
            {
                //TimerMan.Create(500, () => shootBullet());
                shootBullet();
            }

            if(Input.WasBombButtonPressed())
            {
                shootBullet(); //CHANGE ME!!!
            }

        }

        public void shootBullet()
        {

            //EntityMan.Add(new EnemyBullet(position, 4, new Vector2(0, -1), Curves.GetCurve(Curves.CurveType.LINE), 4000, instance, instance.position));
            EntityMan.Add(new EnemyBullet(position, 4, new Vector2(0, -1), Curves.GetCurve(Curves.CurveType.SINE_CURVE), 4000, instance, instance.position));

        }

        public void useBomb()
        {

        }

        public override void Draw(SpriteBatch spriteBatch, Rectangle? spritePos = null, float scale = 1f)
        {
            if (!isPlayerDead)
                base.Draw(spriteBatch);
        }

        public override void Kill()
        {

        }

    }
}
