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
        Vector2 creationTimePlayerPos;
        

        public EnemyBullet(Player player, Vector2 creationTimePlayerPos, Vector2 spawnPosition, float speed, float acceleration, float curve, int lifeTime, BulletType bulletType, float direction = 0f, BulletColour bulletColour = BulletColour.GREY) : base(spawnPosition,speed,acceleration,curve,lifeTime,bulletType, direction, bulletColour)
        {
            
            sprite = Sprites.projectileSpriteSheet;

            this.playerInstance = player;
            this.creationTimePlayerPos = creationTimePlayerPos;
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
