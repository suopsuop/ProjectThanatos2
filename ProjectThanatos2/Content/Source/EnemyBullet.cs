using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectThanatos;
using ProjectThanatos.Content.Source;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectThanatos2.Content.Source
{
    class EnemyBullet : Bullet
    {

        public EnemyBullet(BulletType bulletType) : base(bulletType)
        {

            switch (bulletType)
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

            sprite = Sprites.projectileSpriteSheet;
            position = Player.Instance.position; // ! CHANGE ME

        }

        public int damage;

        private const double PI = Math.PI;

        public override void Update()
        {
            base.Update();

            velocity.Normalize();

            position += 1 * velocity;

            if (velocity.LengthSquared() > 0) //Rotating texture to fit direction
            {
                orientation = velocity.ToAngle();
            }

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

    }
}
