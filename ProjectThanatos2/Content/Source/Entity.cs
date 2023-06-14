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
        protected Color color = Color.White; // Changes tint of sprite, also allows for transparency.

        public Vector2 position { get; set; }
        public Vector2 velocity { get; set; }

        public bool shouldDraw = true;

        public float orientation; // Rotation.

        public float radius { get; set; } // Circular collision detection!
        public Rectangle collisionBox { get; set; } //Rectangular Collision!

        public bool isExpired = false; // Flag for if entity should be deleted, handled in EntityMan

        public int zDepth = 0;

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
            if (shouldDraw)
            {
                if (spriteBatch != null) // Draws spritebatch, if there's one to be drawn
                {
                    if (spritePos == null)
                    {
                        spriteBatch.Begin();
                        spriteBatch.Draw(sprite, position, null, color, orientation, spriteSize / 2f, scale, 0, 0);
                        spriteBatch.End();
                    }
                    else // Draws spritebatch with given rectangle, if there is one
                    {
                        //spriteOrigin = new Vector2(spritePos.Value.X + (spritePos.Value.Width / 2f), spritePos.Value.Y + (spritePos.Value.Height / 2f));
                        spriteOrigin = new Vector2();

                        spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.PointClamp); // These settings cut the sprites out cleaner, apparently?
                        spriteBatch.Draw(sprite, position, spritePos, color, orientation, spriteOrigin, scale, 0, 0);
                        spriteBatch.End();
                    }

                }
            }

        }

        public virtual void Kill()
        {
            isExpired = true; // A flag to tell EntityMan that this should be deleted
        }
    }
}
