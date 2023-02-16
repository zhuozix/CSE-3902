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
        private ArrayList itemList;
        // Zhuozi Sprint 2
        private ArrayList enemyList;

        private Rectangle topLeft = new Rectangle(0, 0, 400, 220);
        private Rectangle topRight = new Rectangle(400, 0, 800, 220);
        private Rectangle bottomLeft = new Rectangle(0, 220, 400, 440);
        private Rectangle bottomRight = new Rectangle(400, 220, 800,440);
        public int DisplaySprite { get; set; }
        // Adam Sprint 2
        public int DisplayBlock { get; set; }
        //
        // Seth Sprint 2
        public int DisplayItem { get; set; }
        // Zhuozi Sprint 2
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

            //Zhuozi Sprint 2
            texture_Gommba = Content.Load<Texture2D>("EnemyContent/WalkingGoomba1");
            texture_Koopa = Content.Load<Texture2D>("EnemyContent/WalkingGreenKoopa1");

            #region initialize variables
            keyboardController = new KeyboardController();
            spritesList = new ArrayList();
            // Adam Sprint 2
            blockList = new ArrayList();
            //
            // Seth Sprint 2
            itemList = new ArrayList();
            // Zhuozi Sprint 2
            enemyList = new ArrayList();
            #endregion



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

            ICommand SetPrevious = new SetPrevious(this);
            ICommand SetNext = new SetNext(this);

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
        }

        protected override void Update(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //display the sprite from the sprite list one at a time.
            #region implement command to mouse and keyboard
            // Zhuozi Sprint 2
            ISpriteE currentEnemy = (ISpriteE)enemyList[DisplayEnemy];
            // Adam Sprint 2
            ISprite currentBlock = (ISprite)blockList[DisplayBlock];
            // Seth Sprint 2
            ISprite currentItem = (ISprite)itemList[DisplayItem];
            //
            // implement command to mouse and keyboard
            keyboardController.UpdateInput();
           
            #endregion
            

            currentBlock.Update(gameTime);
			currentBlock.Draw(_spriteBatch, true);

            currentItem.Update(gameTime);
            currentItem.Draw(_spriteBatch, true);

            // Zhuozi Sprint2
            currentEnemy.Update(gameTime);
            currentEnemy.Draw(_spriteBatch);

            base.Update(gameTime);
           
        }

        private void CreateSprites()
        {

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
    }
}