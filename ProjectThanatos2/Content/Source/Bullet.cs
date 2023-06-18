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

        private static Bullet instance;

        //public delegate void BulletCurve(Bullet bullet);

        //public BulletCurve bulletCurve;

        double lifeTime;
        public float speed;
        public readonly Vector2 initialPosition;
        public float direction;
        public Vector2 vecDirection;
        public float acceleration;
        public float curve;
        public int framesAlive = 0;
        public int invisFrames = 3;
        public BulletType bulletType;
        public BulletColour bulletColour;

        public Bullet(Vector2 spawnPosition, float speed, float acceleration, float curve, int lifeTime, BulletType bulletType, float direction = 0f, BulletColour bulletColour = BulletColour.GREY) : base()
        {

            this.position = spawnPosition;
            this.speed = speed;
            this.acceleration = acceleration;
            this.lifeTime = lifeTime;
            this.direction = direction;
            this.bulletType = bulletType;
            this.bulletColour = bulletColour;

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
            framesAlive += 1;

            if(invisFrames>0)
            { invisFrames -= 1; }

            direction += curve;

            speed += acceleration;

            vecDirection = getDirection(direction);

            position += vecDirection * speed;

            orientation = RiceLib.toRadians(direction - 90f);
        }

        public override void Draw(SpriteBatch spriteBatch, Rectangle? spritePos = null, float scale = 1f)
        {

            if (invisFrames ==0)
                // Draws the correct sprite through what colour and type it has
                // been passed. I love enums
                base.Draw(spriteBatch, new Rectangle(16 * ((int)bulletColour), 16 * ((int)bulletType), 16, 16), scale);
        }

        public bool isOutOfBounds()
        {
            return ProjectThanatos.Viewport.Bounds.Contains(position);
        }

        private static Vector2 getDirection(float direction) // Gets vector direction from angle
        {
            // Could be a one-liner, might do that but just keeping this
            // here for now for readability
            float dirXRadians = direction * MathF.PI / 180f;
            float dirYRadians = direction * MathF.PI / 180f;

            return new Vector2(MathF.Cos(dirXRadians),-MathF.Sin(dirYRadians));
        }


    }
}
