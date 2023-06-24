using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
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

        public static Song backgroundMusic;

        public static GameTime GameTime = new GameTime();

        public enum GameState
        {
            INGAME,
            PAUSED,
            GAMEOVERMENU,
            STARTMENU,
            SPLASHMENU
        };

        public static GameState gameState;

        // Variables for Start menu
        private Button startButton;
        private Button soundOnButton;
        private Button highScoreButton;
        private Button quitButton;
        // Variables for Pause manu
        private Button resumeButton;
        private Button quitToTitleButton;
        // Variables for Post-Game menu
        public Button deathScoreButton;
        private Button youDiedButton;
        // Variable for Splash Screen;
        private Button splashButton;

        public static List<Button> startMenuButtonList = new List<Button>();
        public static List<Button> pauseMenuButtonList = new List<Button>();
        public static List<Button> postGameButtonList = new List<Button>();
        public static List<Button> splashButtonList = new List<Button>();

        private Background titleScreenBackground;
        private Background gameBackground;
        private Background splashBackground;

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

            backgroundMusic = Sprites.backgroundMusic;

            // Reads highscores from file, creates new file if none exist.
            GameMan.highscore = GameMan.ReadHighscores();

            ScreenSize.X = _graphics.PreferredBackBufferWidth;
            ScreenSize.Y = _graphics.PreferredBackBufferHeight;

            startButton = new Button("Start!", Vector2.Zero, Color.Black, Color.Green, () => GameMan.StartGame());
            soundOnButton = new Button("Sound On", Vector2.Zero, Color.Black, Color.Green, () => GameMan.ToggleSound(), "Sound Off");
            highScoreButton = new Button("High Score: " + GameMan.highscore, Vector2.Zero, Color.Black, Color.Blue, () => { });
            quitButton = new Button("Quit", Vector2.Zero, Color.Black, Color.Red, () => GameMan.QuitGame());

            resumeButton = new Button("Resume", new Vector2(ScreenSize.X / 2, 140), Color.Black, Color.Green, () => GameMan.ResumeGame());
            quitToTitleButton = new Button("Quit to Menu", new Vector2(ScreenSize.X / 2, 180), Color.Black, Color.Red, () => GameMan.QuitToTitle());

            youDiedButton = new Button("You Died!", Vector2.Zero, Color.Black, Color.Blue, () => { });
            deathScoreButton = new Button("Your Score: " + GameMan.deathScore, Vector2.Zero, Color.Black, Color.Blue, () => { });

            splashButton = new Button("Press Z to Start", new Vector2(ScreenSize.X/2, 400), Color.White, Color.White, () => { gameState = GameState.STARTMENU; });

            startMenuButtonList.Add(startButton);
            startMenuButtonList.Add(soundOnButton);
            startMenuButtonList.Add(highScoreButton);
            startMenuButtonList.Add(quitButton);

            pauseMenuButtonList.Add(resumeButton);
            pauseMenuButtonList.Add(soundOnButton);
            pauseMenuButtonList.Add(quitToTitleButton);

            postGameButtonList.Add(youDiedButton);
            postGameButtonList.Add(highScoreButton);
            postGameButtonList.Add(deathScoreButton);
            postGameButtonList.Add(quitToTitleButton);

            splashButtonList.Add(splashButton);

            // Dynamically calculates spacing!
            startMenuButtonList.ArrangeButtons();

            titleScreenBackground = new Background(Sprites.titleBackground);
            gameBackground = new Background(Sprites.gameBackground);
            splashBackground = new Background(Sprites.splashBackground);

            gameState = GameState.SPLASHMENU;

            MediaPlayer.Play(backgroundMusic);
            MediaPlayer.IsRepeating = true;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Sprites.loadContent(Content);
        }

        protected override void Update(GameTime gameTime)
        {

            // Turns sound on and off
            if (GameMan.shouldUpdateSound)
            {
                if (GameMan.isSoundOn)
                    MediaPlayer.Resume();
                else
                    MediaPlayer.Pause();

                GameMan.shouldUpdateSound = false;
            }


            GameTime= gameTime;
            Input.Update();

            // DEBUG -- Draws Rectangles over hitboxes
            if (Input.WasKeyPressed(Microsoft.Xna.Framework.Input.Keys.P) && Input.IsShiftDown() && Input.IsShootKeyDown())
                GameMan.shouldDrawDebugRectangles = !GameMan.shouldDrawDebugRectangles;

            // Secret key combo!!
            if (Input.WasKeyPressed(Keys.OemOpenBrackets) && Input.IsShiftDown() && Input.IsShootKeyDown())
                GameMan.shouldClearBackground = !GameMan.shouldClearBackground;

            // Toggles escape menu
            if (Input.WasKeyPressed(Keys.Escape))
            {
                if (gameState == GameState.INGAME)
                {
                    pauseMenuButtonList.ArrangeButtons();
                    gameState = GameState.PAUSED;
                }

                else if (gameState == GameState.PAUSED)
                    gameState = GameState.INGAME;
            }

            
            switch (gameState)
            {
                case GameState.STARTMENU:
                    if (GameMan.shouldUpdateHighScore)
                    {
                        highScoreButton.ChangeText("Highscore: " + GameMan.highscore.ToString());
                        GameMan.shouldUpdateHighScore = false;
                    }

                    MenuNavigator.Update(startMenuButtonList);
                    break;

                case GameState.INGAME:
                    TimerMan.Update();

                    EnemyMan.Update();

                    EntityMan.Update();

                    // Constant increase of score, just for existing. So kind!
                    if ((int)gameTime.TotalGameTime.TotalMilliseconds % 20 == 0 && !Player.Instance.isDead)
                    {
                        GameMan.score += 1;
                    }
                    break;

                case GameState.PAUSED:
                    MenuNavigator.Update(pauseMenuButtonList);
                    break;

                case GameState.GAMEOVERMENU:
                    MenuNavigator.Update(postGameButtonList);
                    break;

                case GameState.SPLASHMENU:
                    MenuNavigator.Update(splashButtonList);
                    break;
            }

            Draw(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            switch (gameState)
            {
                case GameState.STARTMENU:

                    titleScreenBackground.Draw(_spriteBatch);

                    GameMan.UpdateButtons(_spriteBatch, startMenuButtonList);
                    break;

                case GameState.INGAME:

                    if (GameMan.shouldClearBackground)                        
                        gameBackground.Draw(_spriteBatch);

                    // Draws score and power text, but under other sprites.
                    // This is not an error, trying to figure out where bullets
                    // are under the text is *very* painful. Also, it looks neat!
                    RiceLib.DrawText(_spriteBatch, "Score: " + GameMan.score, new Vector2(ScreenSize.X / 2, 400), Color.White);
                    RiceLib.DrawText(_spriteBatch, "Power: " + GameMan.playerPower, new Vector2(ScreenSize.X / 2, 430), Color.White);

                    EntityMan.Draw(_spriteBatch);

                    break;

                case GameState.PAUSED:

                    titleScreenBackground.Draw(_spriteBatch);

                    GameMan.UpdateButtons(_spriteBatch, pauseMenuButtonList);
                    break;

                case GameState.GAMEOVERMENU:

                    titleScreenBackground.Draw(_spriteBatch);
                    GameMan.UpdateButtons(_spriteBatch, postGameButtonList);
                    break;

                case GameState.SPLASHMENU:
                    splashBackground.Draw(_spriteBatch);
                    GameMan.UpdateButtons(_spriteBatch, splashButtonList);
                    break;

                default:
                    // If something wrong happens, send the player back to the
                    // menu. Sorry!
                    gameState = GameState.STARTMENU;
                    break;
            }

            base.Draw(gameTime);
        }    
    }
}