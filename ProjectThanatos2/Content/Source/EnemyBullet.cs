﻿using Microsoft.Xna.Framework;
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
        

        public EnemyBullet(Player player, Vector2 creationTimePlayerPos, Vector2 spawnPosition, float speed, float acceleration, float curve, int lifeTime, float direction = 0f) : base(spawnPosition,speed,acceleration,curve,lifeTime)
        {
            
            sprite = Sprites.projectileSpriteSheet;
            //this.position = Player.Instance.position; // ! CHANGE ME

            this.playerInstance = player;
            this.creationTimePlayerPos = creationTimePlayerPos;
        }

        public override void Update()
        {
            base.Update();

            //velocity.Normalize(); // normalises velocity before letting the Curves do their thing

            //this.bulletCurve(this); // The delegate to change the bullet's pos
        }

        public override void Draw(SpriteBatch spriteBatch, Rectangle? spritePos = null, float scale = 1f)
        {
            base.Draw(spriteBatch, new Rectangle(64,64,16,16)); // HARDCODED << CHANGE ME LATER
        }

    }
}
