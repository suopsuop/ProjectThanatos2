﻿using Microsoft.Xna.Framework;
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

        public delegate void BulletCurve(Bullet bullet);

        public BulletCurve bulletCurve;
        double lifeTime;
        public double speed;
        public readonly Vector2 initialPosition;
        public float localRotation;

        public int framesAlive = 0;

        public int invisFrames = 3;

        public Bullet(Vector2 position, double speed, Vector2 velocity, BulletCurve bulletCurve, int lifeTime, float localRotation = 0) : base()
        {

            this.position = position;
            this.velocity = velocity;
            this.bulletCurve = bulletCurve;
            this.speed = speed;
            if(lifeTime >= 0) // Creates a timer to kill the bullet after its lifetime
            {
                TimerMan.Create(lifeTime, () => Kill());
            }
            this.initialPosition = position;

            this.localRotation = localRotation;

        }
        

        public override void Update()
        {
            framesAlive += 1;
            if (isOutOfBounds()) Kill(); // CHANGE MsE!!

            if(invisFrames>0)
            { invisFrames -= 1; }

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

        public void Rotate()
        {

        }
    }
}
