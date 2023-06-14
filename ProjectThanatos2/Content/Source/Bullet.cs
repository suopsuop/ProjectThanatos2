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
            LASER,
            KNIFE,
            ORB,
            BULLET,
            KUNAI,
            GEM,
            CARD,
            STAR
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

        public Bullet(Vector2 spawnPosition, float speed, float acceleration, float curve, int lifeTime, float direction = 0f) : base()
        {

            this.position = spawnPosition;
            this.speed = speed;
            this.acceleration = acceleration;
            this.lifeTime = lifeTime;
            this.direction = direction;

            //this.velocity = velocity;

            //this.bulletCurve = bulletCurve;
            this.speed = speed;
            if(lifeTime >= 0) // Creates a timer to kill the bullet after its lifetime
            {
                TimerMan.Create(lifeTime, () => Kill());
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

            //if (velocity.LengthSquared() > 0) //Rotating texture to fit direction (DOESN'T WORK!!)
            //{
            //    orientation = velocity.ToAngle() - 45f;
            //}
        }

        public override void Draw(SpriteBatch spriteBatch, Rectangle? spritePos = null, float scale = 1f)
        {
            if (invisFrames ==0)
                base.Draw(spriteBatch, spritePos, scale);
        }

        public override void Kill()
        {
            base.Kill();
        }

        public bool isOutOfBounds()
        {
            return ProjectThanatos.Viewport.Bounds.Contains(position);
        }

        private static Vector2 getDirection(float direction) // Gets vector direction from angle
        {
            float dirXRadians = direction * MathF.PI / 180f;
            float dirYRadians = direction * MathF.PI / 180f;

            return new Vector2(MathF.Cos(dirXRadians),-MathF.Sin(dirYRadians));
        }
    }
}
