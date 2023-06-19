using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
            METEOR,
            RANDOM // Random should only be used when talking to the EnemyManager
        };

        public enum EnemyColour
        {
            BLUE,
            RED,
            GREEN,
            YELLOW,
            RANDOM // Random should only be used when talking to the EnemyManager
        };

        public enum BulletPattern
        {

        };

        public float health;
        public EnemyType enemyType;
        public EnemyColour enemyColour;
        public bool killedByPlayer = false;

        public readonly ulong enemyScore;

        public Vector2 spriteAnimationPos = new Vector2(0, 0);
        public readonly Vector2 defaultSpritePos;
        public float scale;


        public int frameDelay = 8;
        public const int defaultFrameDelay = 8;


        public Enemy(Vector2 spawnPosition, EnemyType enemyType, EnemyColour enemyColour, int lifeTime = 5000)
        {

            this.position = spawnPosition;
            this.enemyType = enemyType;
            this.enemyColour = enemyColour;

            TimerMan.Create(lifeTime, () => base.Kill());

            this.sprite = Sprites.enemySpriteSheet;
            //this.scale = new Random().NextFloat(.8f, 1.1f); // Slightly randomises scale

            switch(enemyType) // Initialises default values based on type of enemy
            {
                case EnemyType.LARGEFAIRY:
                    // Scales health based on player's power
                    this.health = 50f * GameMan.playerPower; 
                    this.defaultSpritePos = new Vector2(0, 0);
                    this.enemyScore = 2500;
                    this.spriteSize = new Vector2(64, 64);
                    break;
                case EnemyType.SMALLFAIRY:
                    this.health = 25f * GameMan.playerPower;
                    this.defaultSpritePos = new Vector2(0, 256);
                    this.defaultSpritePos += new Vector2(0, (int)enemyColour * 32);
                    this.enemyScore = 1250;
                    this.spriteSize = new Vector2(32,32);

                    break;
                case EnemyType.SMALLDARKFAIRY:
                    this.health = 60f * GameMan.playerPower;
                    this.defaultSpritePos = new Vector2(0, 352);
                    this.defaultSpritePos += new Vector2(0, (int)enemyColour * 32);
                    this.enemyScore = 3125;
                    this.spriteSize = new Vector2(32, 32);
                    break;
                case EnemyType.METEOR:
                    this.health = 10f * GameMan.playerPower;
                    this.defaultSpritePos = new Vector2(64, 64);
                    this.spriteSize = new Vector2(64,64);
                    break;
                default:
                    break;
            }
        }


        public override void Update()
        {

            if (frameDelay < 0)
                frameDelay = defaultFrameDelay;
            else
                frameDelay--;



        }

        public override void Kill()
        {
            if(killedByPlayer)
                GameMan.AddScore(enemyScore);

            base.Kill();
        }

        public override void Draw(SpriteBatch spriteBatch, Rectangle? spritePos = null, float scale = 1, SpriteEffects spriteEffects = SpriteEffects.None)
        {
            // Calculates which sprite to draw based on pre-set parameters. Lovely!
            base.Draw(spriteBatch, new Rectangle((int)(defaultSpritePos.X + (spriteAnimationPos.X * spriteSize.X)), (int)(defaultSpritePos.Y + (spriteAnimationPos.Y * spriteSize.Y)), (int)spriteSize.X, (int)spriteSize.Y), scale);
        }

        public void Attack(BulletPattern bulletPattern, int patternLifeTime)
        {

        }
    }
}
