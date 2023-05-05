using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectThanatos.Content.Source
{
    abstract class Entity
    {
        protected Texture2D sprite;
        protected Color color = Color.White; // Changes tint of sprite, also allows for transparency.

        public Vector2 position;
        public Vector2 velocity;


        public float orientation; // Rotation.
        public float radius; // Circular collision detection.

        public bool isExpired; // True when entity should be deleted.


        // Gets sprite size.

        public Vector2 spriteSize
        {
            get
            {
                return sprite == null ? Vector2.Zero : new Vector2(sprite.Width, sprite.Height);
            }
        }

        public abstract void Update();

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if(spriteBatch != null)
            {
                spriteBatch.Begin();
            spriteBatch.Draw(sprite, position, null, color, orientation, spriteSize / 2f, 1f, 0, 0);
                spriteBatch.End();
            }
        }

        public virtual void Kill()
        {

        }
    }
}
