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
        Player playerInstance;
        Vector2 playerPos;
        

        public EnemyBullet(Vector2 position, double speed, Vector2 velocity, BulletCurve bulletCurve, int lifeTime, Player player, Vector2 playerPos, float localRotation = 0f) : base(position,speed,velocity,bulletCurve,lifeTime,localRotation)
        {
            
            sprite = Sprites.projectileSpriteSheet;
            this.position = Player.Instance.position; // ! CHANGE ME
            this.playerInstance = player;
            this.playerPos = playerPos;
        }

        public override void Update()
        {
            base.Update();

            velocity.Normalize(); // normalises velocity before letting the Curves do their thing

            this.bulletCurve(this); // The delegate to change the bullet's pos

            base.Rotate(); // you're gonna shit bricks when you find out what this does
        }

        public override void Draw(SpriteBatch spriteBatch, Rectangle? spritePos = null, float scale = 1f)
        {
            base.Draw(spriteBatch, new Rectangle(64,64,16,16));
        }

    }
}
