using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sprint0.Blocks;
using Sprint0.Command;
using Sprint0.Content;
using Sprint0.Controller;
using Sprint0.Item;
using Sprint0.Sprites;
using System.Collections;
using System.Collections.Generic;
using Sprint0.Enemy;
using Sprint0.Factory;
using Sprint0.MarioPlayer;
using Microsoft.Xna.Framework.Content;

namespace Sprint0
{
    public class Game1 : Game
    {

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private KeyboardController keyboardController;
        public ISprite NoneMovingNoneAnimatedSprite;
        public ISprite NoneMovingAnimatedSprite;
        public ISprite MovingNoneAnimatedSprite;
        public ISprite MovingAnimatedSprite;
		// Adam Sprint 2 
		public ISprite brickBlockSprite;
		public ISprite coinBlockSprite;
		public ISprite floorBlockSprite;
		public ISprite usedBlockSprite;
		public ISprite stairBlockSprite;
		public ISprite pipeBlockSprite;
		//
		// Seth Sprint 2
		public ISprite fireFlowerSprite;
        public ISprite starSprite;
        public ISprite redMushSprite;
        public ISprite greenMushSprite;
        public ISprite coinSprite;
        // Zhuozi Sprint 2
        public ISpriteE Gommba;
        public ISpriteE Koopa;
        //
        // Adam, Sprint 2
        public Texture2D texture_CoinBlock; 
		public Texture2D texture_BrickBlock;
		public Texture2D texture_FloorBlock;
		public Texture2D texture_StairBlock;
		public Texture2D texture_UsedBlock;
		public Texture2D texture_PipeBlock;
		//
		// Seth Sprint 2
		public Texture2D texture_FireFlower;
        public Texture2D texture_Star;
        public Texture2D texture_GreenMush;
        public Texture2D texture_RedMush;
        public Texture2D texture_Coin;
        // Zhuozi Sprint 2
        public Texture2D texture_Gommba;
        public Texture2D texture_Koopa;
        //
        public Texture2D MarioSprite;
        public Texture2D MarioDeathSprite;
        public Texture2D MarioWalkRight;
        private ArrayList spritesList;
		// Adam Sprint 2
		public ArrayList blockList;
        // Seth Sprint 2

        private static Game1 instance;
        public static Game1 Instance
        {
            get
            {
                return instance;
            }
        }
        public GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;
        /*
         * Controllers
         */
        private IController keyboardController;
        private IController playerController;

        /*
         * SpriteFactories
         */
        private NPCFactory npcSpritesFactory;
        public PlayerFactory playerFactory;
        public BulletFactory fireballFactory;

        /*
         * Current Sprites
         */
        //Mario
        public Mario mario;
        //Npc
        public ISpriteE currentEnemy;
        public ISprite currentBlock;
        public ISprite currentItem;

        /*
         *  Sprites Lists
         */
        private ArrayList blockList;

        private ArrayList itemList;
        private ArrayList enemyList;
        public ArrayList bulletList;

        /*
         * Command Control
         */
        public int DisplaySprite { get; set; }
        public int DisplayBlock { get; set; }
        public int DisplayItem { get; set; }
        public int DisplayEnemy { get; set; }


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {

			// Adam Sprint 2
			texture_CoinBlock = Content.Load<Texture2D>("questionBlock");
			texture_BrickBlock = Content.Load<Texture2D>("brickBlock");
			texture_FloorBlock = Content.Load<Texture2D>("floorBlock");
			texture_StairBlock = Content.Load<Texture2D>("stairblock");
			texture_UsedBlock = Content.Load<Texture2D>("usedblock");
			texture_PipeBlock = Content.Load<Texture2D>("GreenPipe");

			// Seth Sprint 2
			texture_FireFlower = Content.Load<Texture2D>("fireFlower");
            texture_Star = Content.Load<Texture2D>("star");
            texture_GreenMush = Content.Load<Texture2D>("greenMushroom");
            texture_RedMush = Content.Load<Texture2D>("redMushroom");
            texture_Coin = Content.Load<Texture2D>("coin");

            //Factories
            npcSpritesFactory = new NPCFactory(_graphics);
            npcSpritesFactory.initalize(Content);
            playerFactory = new PlayerFactory(Content);
            fireballFactory = new BulletFactory(_graphics);
            fireballFactory.initalize(Content);



            //controllers
            keyboardController = new KeyboardController();
            playerController = new PlayerController();
            

            //Lists
            blockList = new ArrayList();
            itemList = new ArrayList();
            enemyList = new ArrayList();
            bulletList = new ArrayList();


            base.Initialize();
        }

        protected override void LoadContent()
        {         
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            CreateSprites();
            #region create command
			// Adam Sprint 2
			ICommand previousBlock = new decreaseBlockIndex(this);
            ICommand nextBlock = new IncreaseBlockIndex(this);
            //
            // Seth Sprint 2
            ICommand increaseItemIndex = new increaseItemIndex(this);
            ICommand decreaseItemIndex = new decreaseItemIndex(this);
            // Zhuozi Sprint 2



            /*
             * Load Sprites 
             */
            //Player
            mario = new Mario(new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2), this);
            // NPC lists
            npcSpritesFactory.addAllBlocks(blockList);
            npcSpritesFactory.addAllItems(itemList);
            npcSpritesFactory.addAllEnemies(enemyList);


            // Adam Sprint 2
            keyboardController.AddCommand(Keys.T, previousBlock);
			keyboardController.AddCommand(Keys.Y, nextBlock);
            //
            // Seth Sprint 2
            keyboardController.AddCommand(Keys.I, increaseItemIndex);
            keyboardController.AddCommand(Keys.U, decreaseItemIndex);
            // Zhuozi Sprint 2
            keyboardController.AddCommand(Keys.O, SetPrevious);
            keyboardController.AddCommand(Keys.P, SetNext);
            #endregion


            //Load commands to controller
            keyboardController.loadCommonCommand(this);
            playerController.loadCommonCommand(this);

        }

        protected override void Update(GameTime gameTime)
        {
            

            //display the sprite from the sprite list one at a time.
            #region implement command
            // Zhuozi Sprint 2
            currentEnemy = (ISpriteE)enemyList[DisplayEnemy];
            // Adam Sprint 2
            currentBlock = (ISprite)blockList[DisplayBlock];
            // Seth Sprint 2
            currentItem = (ISprite)itemList[DisplayItem];
            //
            keyboardController.UpdateInput();
            playerController.UpdateInput(); 
            #endregion
            //Players
            mario.Update(gameTime);
            
            //Blocks
            currentBlock.Update(gameTime);
			
            //Items
            currentItem.Update(gameTime);
            
            //Enemies
            currentEnemy.Update(gameTime);

            foreach(ISprite sprite in bulletList)
            {
                sprite.Update(gameTime);
            }

            fireballFactory.update(bulletList,gameTime);
            base.Update(gameTime);
           
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);


            // Adam Sprint 2
            coinBlockSprite = new AnimatedBlockSprite(texture_CoinBlock, 1, 3, new Vector2(100, 100));
			brickBlockSprite = new BlockSprite(texture_BrickBlock, 1, 1, new Vector2(200, 100));
			floorBlockSprite = new BlockSprite(texture_FloorBlock, 1, 1, new Vector2(300, 100));
			stairBlockSprite = new BlockSprite(texture_StairBlock, 1, 1, new Vector2(400, 100));
			usedBlockSprite = new BlockSprite(texture_UsedBlock, 1, 1, new Vector2(500, 100));
			pipeBlockSprite = new BlockSprite(texture_PipeBlock, 1, 1, new Vector2(600, 100));
			//
			// Seth Sprint 2
			fireFlowerSprite = new FireFlowerSprite(texture_FireFlower, new Vector2(100, 300), 1, 4);
            starSprite = new StarSprite(texture_Star, new Vector2(200, 300), 1, 4, _graphics, 1);
            greenMushSprite = new GreenMushroomSprite(texture_GreenMush, new Vector2(300,300), 1, 1, _graphics,1);
            redMushSprite = new RedMushroomSprite(texture_RedMush, new Vector2(400,300), 1, 1, _graphics, 1);
            coinSprite = new CoinSprite(texture_Coin, new Vector2(500, 300), 1, 4);
            //Zhuozi Sprint 2
            Gommba = new GommbaSprite(texture_Gommba, new Vector2(500, 400), 1, 2, _graphics);
            Koopa = new GommbaSprite(texture_Koopa, new Vector2(500, 400), 1, 2, _graphics);
           
          
			// Adam Sprint 2
			blockList.Add(coinBlockSprite);
			blockList.Add(brickBlockSprite);
			blockList.Add(stairBlockSprite);
			blockList.Add(usedBlockSprite);
			blockList.Add(floorBlockSprite);
			blockList.Add(pipeBlockSprite);
			//

			//Seth Sprint 2
			itemList.Add(fireFlowerSprite);
            itemList.Add(starSprite);
            itemList.Add(greenMushSprite);
            itemList.Add(redMushSprite);
            itemList.Add(coinSprite);
            // Zhuozi Sprint 2
            enemyList.Add(Gommba);
            enemyList.Add(Koopa);
        }


            mario.Draw(_spriteBatch, true);
            currentBlock.Draw(_spriteBatch, true);
            currentItem.Draw(_spriteBatch, true);
            currentEnemy.Draw(_spriteBatch);

            foreach (FireBallSprite sprite in bulletList)
            {
                sprite.Draw(_spriteBatch, sprite.isFliped);
            }

        } 

    }
}