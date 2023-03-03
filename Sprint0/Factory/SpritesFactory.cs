using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.Blocks;
using Sprint0.Enemy;
using Sprint0.Item;
using Sprint0.Sprites;
using Sprint0.Content;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprint0.MarioPlayer;
using Sprint0.MarioPlayer.State.ActionState;

namespace Sprint0.Factory
{
    public class SpritesFactory
    {
        private GraphicsDeviceManager _graphics;
        Game1 gameInstance;
  		// Blocks
		public Texture2D texture_CoinBlock;
		public Texture2D texture_BrickBlock;
		public Texture2D texture_FloorBlock;
		public Texture2D texture_StairBlock;
		public Texture2D texture_UsedBlock;
		public Texture2D texture_GreenPipeBlock;

		// Items
		public Texture2D texture_FireFlower;
        public Texture2D texture_Star;
        public Texture2D texture_GreenMush;
        public Texture2D texture_RedMush;
        public Texture2D texture_Coin;

        // Enemies
        public Texture2D texture_Gommba;
        public Texture2D texture_Koopa;

        // Bullets
        public Texture2D texture_FireBall;

        // Mario
        public Texture2D texture_Mario;

        public SpritesFactory(Game1 gameInstance)
        {
            this._graphics = gameInstance._graphics;
            this.gameInstance = gameInstance;
        }

        public void initalize()
        {
            ContentManager content = gameInstance.Content;
            // Blocks
            texture_BrickBlock = content.Load<Texture2D>("Blocks/brickBlock");
            texture_CoinBlock = content.Load<Texture2D>("Blocks/questionBlock");
            texture_FloorBlock = content.Load<Texture2D>("Blocks/floorBlock");
			texture_StairBlock = content.Load<Texture2D>("Blocks/stairblock");
			texture_UsedBlock = content.Load<Texture2D>("Blocks/usedblock");
			texture_GreenPipeBlock = content.Load<Texture2D>("Blocks/GreenPipe");

			// Items
			texture_FireFlower = content.Load<Texture2D>("fireFlower");
            texture_Star = content.Load<Texture2D>("star");
            texture_GreenMush = content.Load<Texture2D>("greenMushroom");
            texture_RedMush = content.Load<Texture2D>("redMushroom");
            texture_Coin = content.Load<Texture2D>("coin");

            // Enemies
            texture_Gommba = content.Load<Texture2D>("EnemyContent/WalkingGoomba1");
            texture_Koopa = content.Load<Texture2D>("EnemyContent/WalkingGreenKoopa1");

            // Bullets
            texture_FireBall = content.Load<Texture2D>("FireMario/fireball");
        }
        public void addAllBlocks(ArrayList blockList)
        {
            blockList.Add(getCoinBlockSprite());
            blockList.Add(getBrickBlockSprite());
			blockList.Add(getFloorBlockSprite());
			blockList.Add(getStairBlockSprite());
			blockList.Add(getUsedBlockSprite());
			blockList.Add(getGreenPipeBlockSprite());
		}
        public void addAllItems(ArrayList itemList)
        {
            itemList.Add(getFireFlowerSprite());
            itemList.Add(getStarSprite());
            itemList.Add(getGreenMushSprite());
            itemList.Add(getRedMushSprite());
            itemList.Add(getCoinSprite());
        }

        public void addAllEnemies(ArrayList enemyList)
        {
            enemyList.Add(getGommbaSprite());
            enemyList.Add(getKoopaSprite());
        }

        public Texture2D getBackgroundSprite(String background)
        {
            ContentManager content = gameInstance.Content;
            return content.Load<Texture2D>(background);
        }
        public ISprite getCoinBlockSprite()
        {
            return new AnimatedBlockSprite(texture_CoinBlock, 1, 3, new Vector2(100, 100));
        }
        public ISprite getBrickBlockSprite() 
        {
            return new BlockSprite(texture_BrickBlock, 1, 1, new Vector2(200, 100));
        }
		public ISprite getFloorBlockSprite()
		{
			return new BlockSprite(texture_FloorBlock, 1, 1, new Vector2(300, 100));
		}
		public ISprite getStairBlockSprite()
		{
			return new BlockSprite(texture_StairBlock, 1, 1, new Vector2(400, 100));
		}
		public ISprite getUsedBlockSprite()
		{
			return new BlockSprite(texture_UsedBlock, 1, 1, new Vector2(500, 100));
		}
		public ISprite getGreenPipeBlockSprite()
		{
			return new BlockSprite(texture_GreenPipeBlock, 1, 1, new Vector2(600, 100));
		}
		public ISprite getFireFlowerSprite()
        {
            return new FireFlowerSprite(texture_FireFlower, new Vector2(100, 300), 1, 4);
        }
        public ISprite getStarSprite()
        {
            return new StarSprite(texture_Star, new Vector2(200, 300), 1, 4, _graphics, 1);
        }
        public ISprite getGreenMushSprite() 
        {
            return new GreenMushroomSprite(texture_GreenMush, new Vector2(300, 300), 1, 1, _graphics, 1);
            
            
        }
        public ISprite getRedMushSprite() {
            return new RedMushroomSprite(texture_RedMush, new Vector2(400, 300), 1, 1, _graphics, 1);
        }
        public ISprite getCoinSprite() 
        {
            return new CoinSprite(texture_Coin, new Vector2(500, 300), 1, 4);
        }
        public ISprite getGommbaSprite() 
        {
            return new MovingEnemy(texture_Gommba, new Vector2(500, 400), 1, 2, _graphics, 1);
            
        }
        public ISprite getKoopaSprite()
        {
            
            return new MovingEnemy(texture_Koopa, new Vector2(500, 400), 1, 2, _graphics, 1);
        }

        public ISprite getFireballSprite(Vector2 currentLocation, bool isFacingRight)
        {
            if (isFacingRight)
            {
                return new FireBallSprite(texture_FireBall, new Vector2(currentLocation.X + 5, currentLocation.Y), 2, 2, _graphics, 1, true);
            }
            else
            {
                return new FireBallSprite(texture_FireBall, new Vector2(currentLocation.X - 5, currentLocation.Y), 2, 2, _graphics, -1, false);
            }

        }

        public Sprite getMarioSprite(string fileLocation)
        {
            texture_Mario = gameInstance.Content.Load<Texture2D>(fileLocation);
            if (gameInstance.mario.running())
            {
                return new NoneMovingAnimatedSprite(texture_Mario, Vector2.Zero, 1, 3);
            }
            else
            {
                return new NoneAnimatedNonMovingSprite(texture_Mario, Vector2.Zero, 1, 1);
            }
        }
    }
}
