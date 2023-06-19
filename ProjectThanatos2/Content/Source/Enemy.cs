using Microsoft.Xna.Framework;
using ProjectThanatos.Content.Source;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectThanatos2.Content.Source
{
    class Enemy : Entity
    {
        public enum EnemyType
        {
            LARGEFAIRY,
            SMALLFAIRY,
            SMALLDARKFAIRY,
            METEOR
        };

        public float health;
        public EnemyType enemyType;

        public Vector2 spriteAnimationPos = new Vector2(0, 0);

        public int frameDelay = 8;
        public const int defaultFrameDelay = 8;


        public Enemy(EnemyType enemyType)
        {
            this.enemyType = enemyType;

            // Switch statement to set base values
        }


        public override void Update()
        {
        }
    }
}
