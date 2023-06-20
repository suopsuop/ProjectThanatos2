using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectThanatos.Content.Source
{
    abstract public class Entity
    {
        protected Texture2D sprite = null;
        public Rectangle spritePos;
        public Vector2 spriteOrigin;

        public float health;

        // Changes tint of sprite, also allows for transparency.
        public Color color = Color.White;

        public Vector2 position { get; set; }
        public Vector2 velocity { get; set; }

        public bool shouldDraw = true;

        public float orientation; // Rotation.

        public Rectangle collisionBox; //Rectangular Collision!

        // Flag for if entity should be deleted, handled by EntityMan
        public bool isExpired = false; 

        // Controls what layer to render sprites in
        public int zDepth = 0;

        public Vector2 spriteSize;

        private bool killedByPlayer = false;
        public float damage;

        public abstract void Update();

        public virtual void Draw(SpriteBatch spriteBatch, Rectangle? spritePos = null, float scale = 1f, SpriteEffects spriteEffects = SpriteEffects.None)
        {
            if (shouldDraw)
            {
                if (spriteBatch != null) // Draws spritebatch, if there's one to be drawn
                {
                    if (spritePos == null)
                    {
                        spriteBatch.Begin();
                        spriteBatch.Draw(sprite, position, null, color, orientation, spriteSize / 2f, scale, spriteEffects, zDepth);
                        spriteBatch.End();
                    }
                    else // Draws spritebatch with given rectangle, if there is one
                    {
                        spriteOrigin = new Vector2(spritePos.Value.Width/2, spritePos.Value.Height/2);

                        // These settings cut pixel sprites out cleaner, apparently?
                        spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.PointClamp); 
                        spriteBatch.Draw(sprite, position, spritePos, color, orientation, spriteOrigin, scale, spriteEffects, zDepth);
                        spriteBatch.End();
                    }
                }
            }

        }

        public virtual void Kill()
        {
            // Flag to tell EntityMan that this should be deleted
            isExpired = true; 
        }

        public void Hurt(float damage)
        {
            health -= damage;
            if (health <= 0)
                killedByPlayer = true;
        }
    }
}
