using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
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
        public static Texture2D enemySpriteSheet { get; private set; }

        public static Texture2D titleBackground { get; private set; }
        public static Texture2D gameBackground { get; private set; }
        public static Texture2D splashBackground { get; private set; }

        public static Texture2D Pixel { get; private set; }		// a single white pixel

        public static SpriteFont font { get; private set; }

        public static Song backgroundMusic { get; private set; }

        public static void loadContent(ContentManager content)
        {
            playerSpriteSheet = content.Load<Texture2D>("Sprites/Reimu");
            projectileSpriteSheet = content.Load<Texture2D>("Sprites/BulletsAndItems");
            enemySpriteSheet = content.Load<Texture2D>("Sprites/Enemies");

            titleBackground = content.Load<Texture2D>("Sprites/Backgrounds/TitleScreen");
            gameBackground = content.Load<Texture2D>("Sprites/Backgrounds/Space");
            splashBackground = content.Load<Texture2D>("Sprites/Backgrounds/Splash");

            Pixel = new Texture2D(playerSpriteSheet.GraphicsDevice, 1, 1);
            Pixel.SetData(new[] { Color.White });

            font = content.Load<SpriteFont>("Fonts/ScoreFont");

            backgroundMusic = content.Load<Song>("Audio/peshaythirtymins");
        }
    }
}
