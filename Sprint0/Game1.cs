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
        public const int scale = 2;

        public GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;
        public Viewport viewport;
        /*
         * Controllers
         */
        private IController keyboardController;
        private IController MouseController;

        private ICollision Collision;
        /*
         * SpriteFactories
         */
        public SpritesFactory spritesFactory;

        //Mario
        public Mario mario;
        public int coins = 0;
        public int life = 3;

        /*
         *  Sprites Lists
         */
        private ArrayList blockList;
        private ArrayList itemList;
        private ArrayList enemyList;
        public ArrayList fireBallList;

        /*
         * Command Control
         */
        public int DisplaySprite { get; set; }
        public int DisplayBlock { get; set; }
        public int DisplayItem { get; set; }
        public int DisplayEnemy { get; set; }

        /*
         * Camera
         */
        private Camera camera;

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
            //Factories
            spritesFactory = new SpritesFactory(this);
            spritesFactory.initalize();


            //controllers
            keyboardController = new KeyboardController(this);
            MouseController = new MouseController(this);


            //Lists
            gameObjectManager = new GameObjectManager(this);

            blockList = new ArrayList();
            itemList = new ArrayList();
            enemyList = new ArrayList();
            fireBallList = new ArrayList();

            

            Collision = new Collide(this);

            base.Initialize();
        }

        protected override void LoadContent()
        {         
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            camera = new Camera();

            /*
             * Load Sprites 
             */
          LevelLoader.LevelLoader.loadLevel(gameObjectManager, "level1-1.xml", spritesFactory, this);

            //Player
            //mario = new Mario(new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2), this);
            // NPC lists
            //spritesFactory.addAllBlocks(blockList);
            //spritesFactory.addAllItems(itemList);
            //spritesFactory.addAllEnemies(enemyList);

            foreach (ISprite obj in blockList)
            {
                gameObjectManager.addObject(obj, "block");
            }
            foreach (ISprite obj in enemyList)
            {
                gameObjectManager.addObject(obj, "enemy");
            }
            foreach (ISprite obj in itemList)
            {
                gameObjectManager.addObject((ISprite)obj, "item");
            }
            gameObjectManager.addObject(mario, "player");
            //Load commands to controller
            keyboardController.loadCommonCommand();
            MouseController.loadCommonCommand();
        }

        protected override void Update(GameTime gameTime)
        {
            
            keyboardController.UpdateInput();
            MouseController.UpdateInput();

            Collision.Update(gameTime);

            gameObjectManager.update(gameTime);

            camera.MoveCamera(mario);

            base.Update(gameTime);
           
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin(transformMatrix: camera.Transform);

            //mario.Draw(_spriteBatch, true);
            //currentBlock.Draw(_spriteBatch, true);
            gameObjectManager.Draw(_spriteBatch, true);
            //currentItem.Draw(_spriteBatch, true);
            //currentEnemy.Draw(_spriteBatch, true);
            //fireBallList.Draw(_spriteBatch);


            _spriteBatch.End();
        } 

        public void GameReset()
        {
            this.Initialize();
        }

        

        
    }
}