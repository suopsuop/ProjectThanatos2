using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectThanatos2.Content.Source
{
    static class Sprites
    {
        public static Texture2D playerSpriteSheet { get; private set; }
        public static Texture2D projectileSpriteSheet { get; private set; }

        public static void loadContent(ContentManager content)
        {
            playerSpriteSheet = content.Load<Texture2D>("Sprites/testPlayer");
            projectileSpriteSheet = content.Load<Texture2D>("Sprites/testBullet");
        }
    }
}
