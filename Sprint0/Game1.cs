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

namespace Sprint0
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private MouseController mouseController;
        private KeyboardController keyboardController;
        public ISprite NoneMovingNoneAnimatedSprite;
        public ISprite NoneMovingAnimatedSprite;
        public ISprite MovingNoneAnimatedSprite;
        public ISprite MovingAnimatedSprite;
		// Adam Sprint 2 
		public ISprite brickBlockSprite;
		public ISprite coinBlockSprite;
        // Seth Sprint 2
        public ISprite fireFlowerSprite;
        public ISprite starSprite;
        public ISprite redMushSprite;
        public ISprite greenMushSprite;
        public ISprite coinSprite;
        //
        // Adam, Sprint 2
        public Texture2D texture_CoinBlock; 
		public Texture2D texture_BrickBlock;
        //
        // Seth Sprint 2
        public Texture2D texture_FireFlower;
        public Texture2D texture_Star;
        public Texture2D texture_GreenMush;
        public Texture2D texture_RedMush;
        public Texture2D texture_Coin;
        //
        public Texture2D MarioSprite;
        public Texture2D MarioDeathSprite;
        public Texture2D MarioWalkRight;
        private ArrayList spritesList;
		// Adam Sprint 2
		private ArrayList blockList;
        // Seth Sprint 2
        private ArrayList itemList;
        //
		private ArrayList controllerList;
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


		public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
			// Adam Sprint 2
			texture_CoinBlock = Content.Load<Texture2D>("CoinBlocksSpriteSheet");
			texture_BrickBlock = Content.Load<Texture2D>("BrickBlocksSpriteSheet");
            
            // Seth Sprint 2
            texture_FireFlower = Content.Load<Texture2D>("fireFlower");
            texture_Star = Content.Load<Texture2D>("star");
            texture_GreenMush = Content.Load<Texture2D>("greenMushroom");
            texture_RedMush = Content.Load<Texture2D>("redMushroom");
            texture_Coin = Content.Load<Texture2D>("coin");

            #region initialize variables
            keyboardController = new KeyboardController();
            mouseController = new MouseController();
            spritesList = new ArrayList();
            // Adam Sprint 2
            blockList = new ArrayList();
            //
            // Seth Sprint 2
            itemList = new ArrayList();
            controllerList = new ArrayList();
            #endregion

            base.Initialize();
        }

        protected override void LoadContent()
        {
            
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            CreateSprites();
            #region create command
            controllerList.Add(keyboardController);
            controllerList.Add(mouseController);
			// Adam Sprint 2
			ICommand SetBlockBrick = new SetBlockIndex(this, 0);
            ICommand SetBlockCoin = new SetBlockIndex(this, 1);
            //
            // Seth Sprint 2
            ICommand increaseItemIndex = new increaseItemIndex(this);
            ICommand decreaseItemIndex = new decreaseItemIndex(this);

			// Adam Sprint 2
			keyboardController.AddCommand(Keys.T, SetBlockBrick);
			keyboardController.AddCommand(Keys.Y, SetBlockCoin);
            //
            // Seth Sprint 2
            keyboardController.AddCommand(Keys.I, increaseItemIndex);
            keyboardController.AddCommand(Keys.U, decreaseItemIndex);
			#endregion
		}

        protected override void Update(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

			//display the sprite from the sprite list one at a time.
			#region implement command to mouse and keyboard
			
            // Adam Sprint 2
            ISprite currentBlock = (ISprite)blockList[DisplayBlock];
            // Seth Sprint 2
            ISprite currentItem = (ISprite)itemList[DisplayItem];
            //
			// implement command to mouse and keyboard
			foreach (IController controller in controllerList)
            {
                controller.UpdateInput();
            }
           
            #endregion
            

            currentBlock.Update(gameTime);
			currentBlock.Draw(_spriteBatch, true);

            currentItem.Update(gameTime);
            currentItem.Draw(_spriteBatch, true);

			base.Update(gameTime);
           
        }

        private void CreateSprites()
        {

            // Adam Sprint 2
            coinBlockSprite = new BlockSprite(texture_CoinBlock, 1, 5, new Vector2(100, 100));
			brickBlockSprite = new BlockSprite(texture_BrickBlock, 1, 5, new Vector2(200, 100));
            //
            // Seth Sprint 2
            fireFlowerSprite = new FireFlowerSprite(texture_FireFlower, new Vector2(100, 300), 1, 4);
            starSprite = new StarSprite(texture_Star, new Vector2(200, 300), 1, 4);
            greenMushSprite = new GreenMushroomSprite(texture_GreenMush, new Vector2(300,300), 1, 1);
            redMushSprite = new RedMushroomSprite(texture_RedMush, new Vector2(400,300), 1, 1);
            coinSprite = new CoinSprite(texture_Coin, new Vector2(500, 300), 1, 4);

			// Adam Sprint 2
			blockList.Add(coinBlockSprite);
			blockList.Add(brickBlockSprite);
            //

            //Seth Sprint 2
            itemList.Add(fireFlowerSprite);
            itemList.Add(starSprite);
            itemList.Add(greenMushSprite);
            itemList.Add(redMushSprite);
            itemList.Add(coinSprite);
		}
    }
}