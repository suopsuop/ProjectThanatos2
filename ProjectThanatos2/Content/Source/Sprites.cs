using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectThanatos.Content.Source
{
    static class Sprites
    {
        public static Texture2D playerSpriteSheet { get; private set; }
        public static Texture2D projectileSpriteSheet { get; private set; }
        public static Texture2D Pixel { get; private set; }		// a single white pixel

        public static void loadContent(ContentManager content)
        {
            playerSpriteSheet = content.Load<Texture2D>("Sprites/testPlayer");
            projectileSpriteSheet = content.Load<Texture2D>("Sprites/BulletsAndItems");

            Pixel = new Texture2D(playerSpriteSheet.GraphicsDevice, 1, 1);
            Pixel.SetData(new[] { Color.White });
        }
    }
}
