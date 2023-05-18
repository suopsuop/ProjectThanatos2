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
    class PlayerBullet : Bullet
    {
        public PlayerBullet(BulletType bulletType) : base(bulletType) 
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
            position = Player.Instance.position;

        }

        public int damage;

        public override void Update()
        {
            base.Update();

            position += 1 * new Vector2(0,-bulletSpeed);



            if (velocity.LengthSquared() > 0)
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
