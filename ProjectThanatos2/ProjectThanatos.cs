using System;
using System.Collections.Generic;
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
        private static SpriteBatch _spriteBatch;

        public static ProjectThanatos Instance { get; private set; }

        public static Viewport Viewport { get { return Instance.GraphicsDevice.Viewport; } }
        public static Vector2 ScreenSize;

        public static GameTime GameTime = new GameTime();

        // Variables for Start menu
        private Button startButton = new Button("Start!", new Vector2(ScreenSize.X/2, 140), Color.White, Color.Green, () => GameMan.StartGame());
        private Button quitButton = new Button("Quit", new Vector2(ScreenSize.X/2, 180), Color.White, Color.Red, () => GameMan.QuitGame());

        private static List<Button> startMenuButtonList = new List<Button>();

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

            startMenuButtonList.Add(startButton);
            startMenuButtonList.Add(quitButton);
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

            // DEBUG
            if (Input.WasKeyPressed(Microsoft.Xna.Framework.Input.Keys.P))
                GameMan.shouldDrawDebugRectangles = !GameMan.shouldDrawDebugRectangles;

            if (Input.WasKeyPressed(Keys.Escape) && !GameMan.inStartMenu)
                GameMan.isPaused = !GameMan.isPaused;


            if (GameMan.inStartMenu)
            {
                MenuNavigator.Update(startMenuButtonList);                
            }
            else
            {
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
                    //if (Input.WasKeyPressed(Keys.O))
                    //{
                    //    GameMan.AddPlayerPower();
                    //}
                }
                else
                {
                    // Do pause menu stuff here
                }

            }
            Draw(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            if (GameMan.inStartMenu)
            {
                GraphicsDevice.Clear(Color.Red);

                UpdateStartMenu(startMenuButtonList);
            }
            else
            {
                if (GameMan.isPaused)
                {
                    GraphicsDevice.Clear(Color.Aquamarine);
                }
                else
                {
                    GraphicsDevice.Clear(Color.Black);

                    EntityMan.Draw(_spriteBatch);

                    RiceLib.DrawText(_spriteBatch, "Score: " + GameMan.score, new Vector2(ScreenSize.X / 2, 400), Color.White);
                    RiceLib.DrawText(_spriteBatch, "Power: " + GameMan.playerPower, new Vector2(ScreenSize.X / 2, 430), Color.White);
                }
            }



            base.Draw(gameTime);
        }

        public static void InitialiseGame()
        {
            EntityMan.Add(Player.Instance);
            Player.Instance.UpdatePowerLevelStats();
        }

        public static void UpdateStartMenu(List<Button> buttonList)
        {
            foreach (Button button in buttonList)
            {
                button.Update(_spriteBatch);
            }
        }
    }
}