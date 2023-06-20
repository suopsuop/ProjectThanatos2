using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectThanatos.Content.Source;
using ProjectThanatos2.Content.Source;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectThanatos.Content.Source
{
    class Enemy : Entity
    {
        public enum EnemyType
        {
            LARGEFAIRY = 0,
            SMALLFAIRY = 1,
            SMALLDARKFAIRY = 2,
            METEOR = 3,
            RANDOM = 4 // Random should only be used when talking to the EnemyManager
        };

        public enum EnemyColour
        {
            BLUE = 0,
            RED = 1,
            GREEN = 2,
            YELLOW = 3,
            RANDOM = 4 // Random should only be used when talking to the EnemyManager
        };

        private static Random random = new Random();

        public EnemyType enemyType;
        public EnemyColour enemyColour;

        public readonly ulong enemyScore;
        public readonly float enemyPower;

        private float speed;

        private Vector2 spriteAnimationPos = new Vector2(0, 0);
        private readonly Vector2 defaultSpritePos;
        private float scale;

        private static Enemy instance;

        private bool isAttacking = false;
        private int attackTime;

        private int frameDelay = 8;
        private const int defaultFrameDelay = 8;

        private bool killedByPlayer = false;

        private float spinDirection = random.NextFloat(-2f,2f);

        public Enemy(Vector2 spawnPosition, EnemyType enemyType, EnemyColour enemyColour, int lifeTime = 5000, EnemyMan.SpawnPositionType spawnPositionType = EnemyMan.SpawnPositionType.SCREENTOP)
        {
            instance = this;
            this.position = spawnPosition;
            this.enemyType = enemyType;
            this.enemyColour = enemyColour;

            TimerMan.Create(lifeTime, () => base.Kill());

            this.sprite = Sprites.enemySpriteSheet;

            this.scale = new Random().NextFloat(.95f, 1.05f); // Slightly randomises scale

            switch(enemyType) // Initialises default values based on type of enemy
            {
                case EnemyType.LARGEFAIRY:
                    // Scales health based on player's power
                    this.health = 50 * GameMan.playerPower * 2f; 
                    this.defaultSpritePos = new Vector2(0, 0);
                    this.enemyScore = 2500;
                    this.spriteSize = new Vector2(64, 64);
                    this.collisionBox = new Rectangle((int)spawnPosition.X, (int)spawnPosition.Y, 32,32);
                    this.enemyPower = 1.3f;
                    this.speed = 1f + random.NextFloat(0f, .3f * GameMan.playerPower);
                    break;

                case EnemyType.SMALLFAIRY:
                    this.health = 35 * GameMan.playerPower * 2f;
                    this.defaultSpritePos += new Vector2(0, 256 + (int)enemyColour * 32);
                    this.enemyScore = 1250;
                    this.spriteSize = new Vector2(32,32);
                    this.collisionBox = new Rectangle((int)spawnPosition.X, (int)spawnPosition.Y, 16, 16);
                    this.enemyPower = 1f;
                    this.speed = 1f + random.NextFloat(0f, .3f * GameMan.playerPower);
                    break;

                case EnemyType.SMALLDARKFAIRY:
                    this.health = 60 * GameMan.playerPower * 2f;
                    this.defaultSpritePos += new Vector2(0, 640 + (int)enemyColour * 32);
                    this.enemyScore = 3125;
                    this.spriteSize = new Vector2(32, 32);
                    this.collisionBox = new Rectangle((int)spawnPosition.X, (int)spawnPosition.Y, 16, 16);
                    this.enemyPower = 1.5f;
                    this.speed = 1.1f + random.NextFloat(0f, .3f * GameMan.playerPower);
                    break;

                case EnemyType.METEOR:
                    this.health = 25 * GameMan.playerPower * 2f;
                    this.defaultSpritePos = new Vector2(128, 128);
                    this.spriteSize = new Vector2(64,64);
                    this.collisionBox = new Rectangle((int)spawnPosition.X, (int)spawnPosition.Y, 32, 32);
                    this.enemyPower = .8f;
                    this.speed = 1.1f + random.NextFloat(0f,.3f * GameMan.playerPower);
                    break;

                default:
                    break;
            }

                switch (spawnPositionType)
                {
                    case EnemyMan.SpawnPositionType.SCREENLEFT:
                        velocity = RiceLib.getVecDirection(random.NextFloat(0f,315f));
                        break;
                    case EnemyMan.SpawnPositionType.SCREENRIGHT:
                        velocity = RiceLib.getVecDirection(random.NextFloat(180f,225f));
                        break;
                    case EnemyMan.SpawnPositionType.SCREENTOP:
                    velocity = RiceLib.getVecDirection(random.NextFloat(225f, 315f));
                        break;
                    default:
                    velocity = RiceLib.getVecDirection(random.NextFloat(225f, 315f));
                        break;
                }
            }

        public override void Update()
        {
            if (health <= 0)
                Kill();

            if (isOutOfBounds())
                Kill();

            if (enemyType != EnemyType.METEOR)
            {
                if (!isAttacking)
                {
                    BulletSpawner bulletSpawner = CreateBulletSpawner();

                    TimerMan.Create(1200, () => Attack(bulletSpawner));

                    isAttacking = true;
                    TimerMan.Create(bulletSpawner.spawnerLifeTime, () => ResetAttacking());
                    //Attack();
                    //isAttacking = true;
                }

            }

            if (frameDelay < 0)
                frameDelay = defaultFrameDelay;
            else
                frameDelay--;

            position += velocity * speed;

            // Updates collisionBox location
            collisionBox.Location = position.ToPoint() - new Point(collisionBox.Width / 2, collisionBox.Height / 2);

            orientation = RiceLib.ToRadians(position.Y / 4f * spinDirection);
        }

        public override void Kill()
        {
            if(killedByPlayer)
                GameMan.ChangeScore(enemyScore);

            GameMan.currentEnemies--;

            base.Kill();
        }

        public override void Draw(SpriteBatch spriteBatch, Rectangle? spritePos = null, float scale = 1, SpriteEffects spriteEffects = SpriteEffects.None)
        {
            // Calculates which sprite to draw based on pre-set parameters. Lovely!
            base.Draw(spriteBatch, new Rectangle((int)(defaultSpritePos.X + (spriteAnimationPos.X * spriteSize.X)), (int)(defaultSpritePos.Y + (spriteAnimationPos.Y * spriteSize.Y)), (int)spriteSize.X, (int)spriteSize.Y), scale);
        }

        public BulletSpawner CreateBulletSpawner()
        {
            return BulletSpawner.MakeRandom(instance);
        }

        public void Attack(BulletSpawner bulletSpawner)
        {
            EntityMan.Add(bulletSpawner);
        }

        public void ResetAttacking()
        {
            isAttacking = false;
        }

        public bool isOutOfBounds()
        {
            // Makes enemy bounds 64 pixels bigger than normal viewport
            Rectangle oversizeScreen = ProjectThanatos.Viewport.Bounds;
            oversizeScreen.Location = oversizeScreen.Location - new Point(64, 64);
            oversizeScreen.Size = oversizeScreen.Size + new Point(64, 64);

            return !oversizeScreen.Contains(position);
        }
    }
}