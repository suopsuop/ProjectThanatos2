using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjectThanatos2.Content.Source;

namespace ProjectThanatos2
{
    public class ProjectThanatos : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // some helpful static properties
        public static ProjectThanatos Instance { get; private set; }
        public static Viewport Viewport { get { return Instance.GraphicsDevice.Viewport; } }
        public static Vector2 ScreenSize { get { return new Vector2(Viewport.Width, Viewport.Height); } }
        public static GameTime GameTime { get; private set; }
        public static ParticleManager<ParticleState> ParticleManager { get; private set; }

        bool isPaused = false;

        SpriteBatch spriteBatch;


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
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightPink);

            // TODO: Add your drawing code here
            EntityMan.Draw(spriteBatch);

            base.Draw(gameTime);
        }

        private void drawText()
        {

        }
    }
}