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
        private Button startButton;
        private Button soundOnButton;
        private Button highScoresButton;
        private Button quitButton;
        // Variables for Pause manu
        private Button resumeButton;
        private Button quitToTitleButton;

        private static List<Button> startMenuButtonList = new List<Button>();
        private static List<Button> pauseMenuButtonList = new List<Button>();

        private Background titleScreenBackground;
        private Background gameBackground;

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

            // Reads highscores from file, creates new file if none exist.
            GameMan.highscore = GameMan.ReadHighscores();

            ScreenSize.X = _graphics.PreferredBackBufferWidth;
            ScreenSize.Y = _graphics.PreferredBackBufferHeight;

            startButton = new Button("Start!", Vector2.Zero, Color.Black, Color.Green, () => GameMan.StartGame());
            soundOnButton = new Button("Sound On", Vector2.Zero, Color.Black, Color.Green, () => GameMan.ToggleSound(), "Sound Off");
            highScoresButton = new Button("High Score: " + GameMan.highscore, Vector2.Zero, Color.Black, Color.Gold, () => { });
            quitButton = new Button("Quit", Vector2.Zero, Color.Black, Color.Red, () => GameMan.QuitGame());

            resumeButton = new Button("Resume", new Vector2(ScreenSize.X / 2, 140), Color.Black, Color.Green, () => GameMan.ResumeGame());
            quitToTitleButton = new Button("Quit to Menu", new Vector2(ScreenSize.X / 2, 180), Color.Black, Color.Red, () => GameMan.QuitToTitle());

            startMenuButtonList.Add(startButton);
            startMenuButtonList.Add(soundOnButton);
            startMenuButtonList.Add(highScoresButton);
            startMenuButtonList.Add(quitButton);

            pauseMenuButtonList.Add(resumeButton);
            pauseMenuButtonList.Add(soundOnButton);
            pauseMenuButtonList.Add(quitToTitleButton);

            // Dynamically calculates spacing
            for(int i = 0; i < startMenuButtonList.Count; i++)
            {
                startMenuButtonList[i].position = new Vector2(ScreenSize.X / 2, 160 + i * 40);
            }

            for (int i = 0; i < pauseMenuButtonList.Count; i++)
            {
                pauseMenuButtonList[i].position = new Vector2(ScreenSize.X / 2, 160 + i * 40);
            }

            titleScreenBackground = new Background(Sprites.titleBackground);
            gameBackground = new Background(Sprites.gameBackground);
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

            // Toggles escape menu
            if (Input.WasKeyPressed(Keys.Escape) && !GameMan.inStartMenu)
                GameMan.isPaused = !GameMan.isPaused;

            if (GameMan.inStartMenu)
            {
                if (GameMan.shouldUpdateHighScore)
                {
                    highScoresButton.ChangeText("Highscore: " + GameMan.highscore.ToString());
                    GameMan.shouldUpdateHighScore = false;
                }

                MenuNavigator.Update(startMenuButtonList);
                //RiceLib.DrawText(_spriteBatch, "Score: " + GameMan.score, new Vector2(ScreenSize.X / 2, 400), Color.White);

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
                    MenuNavigator.Update(pauseMenuButtonList);
                }

            }
            Draw(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            if (GameMan.inStartMenu)
            {
                GraphicsDevice.Clear(Color.White);

                titleScreenBackground.Draw(_spriteBatch);

                UpdateButtons(startMenuButtonList);
            }
            else
            {
                if (GameMan.isPaused)
                {
                    GraphicsDevice.Clear(Color.White);

                    titleScreenBackground.Draw(_spriteBatch);

                    UpdateButtons(pauseMenuButtonList);
                }
                else
                {
                    GraphicsDevice.Clear(Color.Black);

                    gameBackground.Draw(_spriteBatch);

                    EntityMan.Draw(_spriteBatch);

                    RiceLib.DrawText(_spriteBatch, "Score: " + GameMan.score, new Vector2(ScreenSize.X / 2, 400), Color.White);
                    RiceLib.DrawText(_spriteBatch, "Power: " + GameMan.playerPower, new Vector2(ScreenSize.X / 2, 430), Color.White);
                }
            }

            base.Draw(gameTime);
        }

        //public static void InitialiseGame()
        //{
        //    Player.Instance.isDead = false;
        //    Player.Instance.isExpired = false;

        //    GameMan.ClearScoreAndPoints();
        //    //Player.Instance.UpdatePowerLevelStats();
        //}

        public static void UpdateButtons(List<Button> buttonList)
        {
            foreach (Button button in buttonList)
            {
                button.Update(_spriteBatch);
            }
        }
    }
}