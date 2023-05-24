using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectThanatos;
using ProjectThanatos.Content.Source;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectThanatos.Content.Source
{
    class Bullet : Entity
    {
         

        private static Bullet instance;

        delegate void BulletCurve(Bullet bullet);

        BulletCurve bulletCurve;


        public Bullet(Vector2 position) :base()
        {
            
            
        }
        

        public override void Update()
        {
            if (isOutOfBounds()) Kill();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public override void Kill()
        {
            base.Kill();
        }

        public bool isOutOfBounds()
        {
            bool outOfBounds = false;

            if (!ProjectThanatos.Viewport.Bounds.Contains(position))
            {
                outOfBounds = true;
            }
            return outOfBounds;
        }
    }
}
