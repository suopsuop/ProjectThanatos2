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
        public PlayerBullet(Vector2 spawnPosition, float speed, float acceleration, float curve, int lifeTime, float direction = 0f) : base(spawnPosition, speed, acceleration, curve, lifeTime)
        {

            sprite = Sprites.projectileSpriteSheet;
            position = Player.Instance.position; // ! CHANGE ME

        }


        public int damage;

        public override void Update()
        {
            base.Update();

            //position += 1 * new Vector2(0,-bulletSpeed);



            if (velocity.LengthSquared() > 0)
            {
                orientation = velocity.ToAngle();
            }
        }

        public override void Draw(SpriteBatch spriteBatch, Rectangle? spritePos = null, float scale = 1f)
        {
                base.Draw(spriteBatch);
        }
    }
}
