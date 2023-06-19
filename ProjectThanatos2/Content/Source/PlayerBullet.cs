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
    class PlayerBullet : Bullet, ICloneable
    {
        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public float damage;

        public PlayerBullet(Vector2 spawnPosition, float speed, float acceleration, float curve, int lifeTime, BulletType bulletType, float damage, float direction = 0f, BulletColour bulletColour = BulletColour.GREY) : base(spawnPosition, speed, acceleration, curve, lifeTime, bulletType, direction, bulletColour)
        {

            sprite = Sprites.projectileSpriteSheet;
            //position = Player.Instance.position; // ! CHANGE ME

            this.damage = damage;

        }

        public override void Update()
        {
            base.Update();

        }

        //public override void Draw(SpriteBatch spriteBatch, Rectangle? spritePos = null, float scale = 1f)
        //{
        //        base.Draw(spriteBatch);
        //}
    }
}
