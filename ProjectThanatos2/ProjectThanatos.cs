using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjectThanatos.Content.Source;
using ProjectThanatos2.Content.Source;

namespace ProjectThanatos
{
    public class ProjectThanatos : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public static ProjectThanatos Instance { get; private set; }

        public static Viewport Viewport { get { return Instance.GraphicsDevice.Viewport; } }
        public static Vector2 ScreenSize;

        public static GameTime GameTime = new GameTime();

        public ProjectThanatos()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            IsMouseVisible = false;
            //Window.AllowUserResizing = true;

            _graphics.PreferredBackBufferWidth = 640;
            _graphics.PreferredBackBufferHeight = 480;

            Window.Title = "Project Thanatos";
            
        }

        protected override void Initialize()
        {
            base.Initialize();
            Instance = this;

            ScreenSize.X = _graphics.PreferredBackBufferWidth;
            ScreenSize.Y = _graphics.PreferredBackBufferHeight;

            EntityMan.Add(Player.Instance);
            Player.Instance.UpdatePowerLevelStats();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Sprites.loadContent(Content);
        }

        protected override void Update(GameTime gameTime)
        {

            GameTime= gameTime;
            Input.Update();

            if(Input.WasKeyPressed(Keys.Escape))
                GameMan.isPaused = !GameMan.isPaused;

            // Only updates entities & timers if not paused but *still* updates
            // general monogame things. 
            if(!GameMan.isPaused) 
            {
                TimerMan.Update();

                EnemyMan.Update();

                EntityMan.Update();

                // Constant increase of score, just for existing. So kind!
                if ((int)gameTime.TotalGameTime.TotalMilliseconds % 20 == 0 && !Player.Instance.isDead)
                {
                    GameMan.score += 1;
                }


                // DEBUG
                //if (Input.WasKeyPressed(Keys.R) && Player.Instance.isDead)
                //{
                //    EntityMan.Add(Player.Instance);
                //    Player.Instance.isExpired = false;
                //}
                if (Input.WasKeyPressed(Keys.O))
                {
                    GameMan.AddPlayerPower();
                }
            }
            else
            {
                // Do pause menu stuff here
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            if (GameMan.isPaused)
            {
                GraphicsDevice.Clear(Color.Aquamarine);
            }
            else
            {
                GraphicsDevice.Clear(Color.Black);
            }

            EntityMan.Draw(_spriteBatch);

            // Draws text over everything else
            RiceLib.DrawText(_spriteBatch, "Score: " + GameMan.score, new Vector2(ScreenSize.X / 2, 400), Color.White);
            RiceLib.DrawText(_spriteBatch, "Power: " + GameMan.playerPower, new Vector2(ScreenSize.X / 2, 430), Color.White);


            base.Draw(gameTime);
        }
    }
}