using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectThanatos;
using ProjectThanatos.Content.Source;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectThanatos.Content.Source
{
    class Bullet : Entity
    {

        public enum BulletType
        {
            pellet,
            laser,
            knife
        }


        private static Bullet instance;

        protected int bulletSpeed;

        public BulletType bulletType;

        public Bullet(BulletType bulletType) :base()
        {
            switch(bulletType)
            {
                case BulletType.pellet:
                    bulletSpeed = 8;
                    break;

                    case BulletType.laser:
                    bulletSpeed = 16;
                    break;

                    case BulletType.knife:
                    bulletSpeed = 32;
                    break;
            }

            
        }
        

        public override void Update()
        {
            if (isOutOfBounds()) Kill();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public override void Kill()
        {
            base.Kill();
        }

        public bool isOutOfBounds()
        {
            bool outOfBounds = false;

            if (!ProjectThanatos.Viewport.Bounds.Contains(position))
            {
                outOfBounds = true;
            }
            return outOfBounds;
        }
    }
}
