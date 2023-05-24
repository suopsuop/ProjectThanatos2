﻿using Microsoft.Xna.Framework;
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

        //public static GameTime GameTime { get; private set; }
        public static GameTime GameTime = new GameTime();

        //public TimerMan TimerMan { get; private set; }

        bool isPaused = false;

        public ProjectThanatos()
        {
            
            
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;

            _graphics.PreferredBackBufferWidth = 640;
            _graphics.PreferredBackBufferHeight = 480;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            Instance = this;

            ScreenSize.X = _graphics.PreferredBackBufferWidth;
            ScreenSize.Y = _graphics.PreferredBackBufferHeight;

            EntityMan.Add(Player.Instance);


        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Sprites.loadContent(Content);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {

            // TODO: Add your update logic here
            GameTime= gameTime;
            Input.Update();

            if(Input.WasKeyPressed(Keys.Escape))
            {
                isPaused = !isPaused;
            }   

            if(!isPaused) 
            { 
                // ! Update Game Here
                EntityMan.Update();
                TimerMan.Update();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            if (isPaused)
            {
                GraphicsDevice.Clear(Color.Aquamarine);
            }
            else
            {
                GraphicsDevice.Clear(Color.LightPink);

            }

            // TODO: Add your drawing code here
            EntityMan.Draw(_spriteBatch);
            base.Draw(gameTime);
        }

        private void drawText()
        {

        }
    }
}