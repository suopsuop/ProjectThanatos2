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
        protected Texture2D sprite;
        public Rectangle spritePos;
        public Vector2 spriteOrigin;
        protected Color color = Color.White; // Changes tint of sprite, also allows for transparency.

        public Vector2 position { get; set; }
        public Vector2 velocity { get; set; }


        public float orientation; // Rotation.

        public float radius { get; set; } // Circular collision detection!
        public Rectangle collisionBox { get; set; } //Rectangular Collision!

        public bool isExpired; // Flag for if entity should be deleted, handled in EntityMan

        // Gets sprite size

        public Vector2 spriteSize
        {
            get
            {

                return sprite == null ? Vector2.Zero : new Vector2(sprite.Width, sprite.Height);
            }
        }

        public abstract void Update();

        public virtual void Draw(SpriteBatch spriteBatch, Rectangle? spritePos = null, float scale = 1f)
        {
            if(spriteBatch != null) // Draws spritebatch, if there's one to be drawn
            {
                if(spritePos == null)
                {
                    spriteBatch.Begin();
                    spriteBatch.Draw(sprite, position, null, color, orientation, spriteSize / 2f, scale, 0, 0);
                    spriteBatch.End();
                }
                else // Draws spritebatch with given rectangle, if there is one
                {
                    spriteOrigin = new Vector2(spritePos.Value.X + (spritePos.Value.Width / 2f), spritePos.Value.Y + (spritePos.Value.Height / 2f));
                    // /\ I DO NOT WORK!!!

                    spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.PointClamp); // These settings cut the sprites out cleaner
                    spriteBatch.Draw(sprite, position, spritePos, color, orientation, spriteOrigin, scale, 0, 0);
                    spriteBatch.End();
                }

            }
        }

        public virtual void Kill()
        {
            isExpired = true; // A flag to tell EntityMan that this should be deleted
        }
    }
}
