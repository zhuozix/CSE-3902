using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.Content;
using Sprint0.Sprites;
using System.Collections;
using Sprint0.NPC.Enemy;
using Sprint0.Factory;
using Sprint0.MarioPlayer;
using Sprint0.ObjectManager;
using Sprint0.LevelLoader;
using Sprint0.Collision;
using Sprint0.Controller;
using Sprint0.Command.GameControlCMD;
using Sprint0.Sounds;
using Sprint0.UI.Title;
using Sprint0.UI.State;

namespace Sprint0
{
    public class Game1 : Game
    {
        private static Game1 instance;
        public static Game1 Instance
        {
            get
            {
                return instance;
            }
        }

        public bool IsPaused { get;  set; }

        public const int scale = 2;

        public GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;
        public Viewport viewport;
        /*
         * Controllers
         */
        public IController keyboardController;
        public IController MouseController;

        public ICollision Collision;
        /*
         * SpriteFactories
         */
        public SpritesFactory spritesFactory;

        //Mario
        public Mario mario;
        public int coins;
        public int life;
        public float time;

        public ArrayList fireBallList;

        private IGameState _currentState;
        /*
         * Camera
         */
        public Camera camera;

        public GameObjectManager gameObjectManager;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            
        }

        protected override void Initialize()
        {
            viewport = GraphicsDevice.Viewport;

            coins = 0;
            life = 3;
            time = 400f;
            //Factories
            spritesFactory = new SpritesFactory(this);

            ChangeState(new TitleScreenState(this));
            base.Initialize();
        }

        protected override void LoadContent()
        {         
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            _currentState.Update(gameTime);
            base.Update(gameTime);          
        }

        protected override void Draw(GameTime gameTime)
        {
            _currentState.Draw(_spriteBatch);
            base.Draw(gameTime);
        } 

        public void GameReset()
        {
            ChangeState(new PlayState(this));
        }

        public void ChangeState(IGameState newState)
        {
            _currentState?.Exit();
            _currentState = newState;
            _currentState.Enter();
        }


    }
}