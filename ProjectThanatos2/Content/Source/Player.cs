using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectThanatos.Content.Source
{
    class Player : Entity
    {
        private static Player instance;

        private const int moveSpeed = 8;
        private const int steadyMoveSpeed = 5;

        public static Player Instance 
        { 
            get 
            {
                if (instance == null)
                    instance = new Player();

                return instance; 
            } 
        }


        int framesTilRespawn = 0;

        public bool isPlayerDead
        {
            get
            {
                return framesTilRespawn > 0;
            }
        }

        static Random rand = new Random();

        private Player()
        {
            sprite = Sprites.playerSpriteSheet;
            position = Vector2.Zero;
            // ADD COLLISION HERE
        }

        public override void Update()
        {
            if (isPlayerDead)
                return;

            velocity += moveSpeed * Input.GetMovementDirection();
            position += velocity;
            position = Vector2.Clamp(position, spriteSize / 2, ProjectThanatos.ScreenSize - spriteSize/2); // Stops the player exiting bounds

            if (velocity.LengthSquared() > 0)
            {
                orientation = velocity.ToAngle();
            }

            velocity = Vector2.Zero;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!isPlayerDead)
                base.Draw(spriteBatch);
        }

        public void Kill()
        {
            
        }
    }
}
