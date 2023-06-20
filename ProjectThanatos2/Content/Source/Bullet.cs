using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectThanatos;
using ProjectThanatos.Content.Source;
using ProjectThanatos2.Content.Source;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectThanatos.Content.Source
{
    public class Bullet : Entity
    {

        public enum BulletType
        {
            LASER = 0,
            KNIFE = 1,
            ORB = 2,
            BULLET = 3,
            KUNAI = 4,
            GEM = 5,
            CARD = 6,
            STAR =7
        };

        public enum BulletColour
        {
            GREY = 0,
            FILLRED = 1,
            RED = 2,
            FILLPINK = 3,
            PINK = 4,
            FILLBLUE = 5,
            BLUE = 6,
            FILLCYAN = 7,
            CYAN = 8,
            FILLGREEN = 9,
            GREEN = 10,
            FILLYELLOW = 11,
            YELLOW = 12,
            GOLD = 13,
            WHITE = 14
        };

        //private static Bullet instance;
        private Random random = new Random();

        public double lifeTime;
        public float speed;
        public readonly Vector2 initialPosition;
        public float direction;
        public Vector2 vecDirection;
        public float acceleration;
        public float curve;
        public int framesAlive = 0;
        public bool shouldRandomisePosition;
        public int invisFrames = 3;
        public BulletType bulletType;
        public BulletColour bulletColour;

        public Bullet(Vector2 spawnPosition, float speed, float acceleration, float curve, int lifeTime, bool shouldRandomisePosition, BulletType bulletType, float direction = 0f, BulletColour bulletColour = BulletColour.GREY) : base()
        {

            this.position = spawnPosition;
            this.speed = speed;
            this.acceleration = acceleration;
            this.lifeTime = lifeTime;
            this.shouldRandomisePosition = shouldRandomisePosition;
            this.direction = direction;
            this.bulletType = bulletType;
            this.bulletColour = bulletColour;

            this.collisionBox = new Rectangle((int)spawnPosition.X - 8, (int)spawnPosition.Y - 8, 8, 8);

            //this.velocity = velocity;

            //this.bulletCurve = bulletCurve;
            this.speed = speed;
            if(lifeTime >= 0) // Creates a timer to kill the bullet after its lifetime
            {
                TimerMan.Create(lifeTime, () => base.Kill());
            }
            this.initialPosition = position;

            //this.localRotation = localRotation;

        }        

        public override void Update()
        {
            if (isOutOfBounds())
                Kill();

            framesAlive += 1;

            if(invisFrames>0)
                invisFrames -= 1;

            if(shouldRandomisePosition)
            {
                direction += curve * random.NextFloat(.9f, 1.1f);
                speed += acceleration * random.NextFloat(.9f, 1.1f);
            }
            else
            {
                direction += curve;
                speed += acceleration;

            }



            vecDirection = RiceLib.getVecDirection(direction);

            position += vecDirection * speed;

            // Updates collisionBox Location
            collisionBox.Location = position.ToPoint() - new Point(collisionBox.Width / 2, collisionBox.Height / 2);


            orientation = RiceLib.ToRadians(direction - 90f);
        }

        public override void Draw(SpriteBatch spriteBatch, Rectangle? spritePos = null, float scale = 1f, SpriteEffects spriteEffects = SpriteEffects.None)
        {

            if (invisFrames ==0)
                // Draws the correct sprite through what colour and type it has
                // been passed. I love enums!
                base.Draw(spriteBatch, new Rectangle(16 * ((int)bulletColour), 16 * ((int)bulletType), 16, 16), scale - .2f);
        }

        private bool isOutOfBounds()
        {
            return !ProjectThanatos.Viewport.Bounds.Contains(position);
        }
    }
}
