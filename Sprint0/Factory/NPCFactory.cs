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

namespace Sprint0.Factory
{
    internal class NPCFactory
    {
        private GraphicsDeviceManager _graphics;

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

        public NPCFactory( GraphicsDeviceManager graphicsIn)
        {
            _graphics = graphicsIn;
        }

        public void initalize(ContentManager content)
        {
			// Adam Sprint 2
			texture_BrickBlock = content.Load<Texture2D>("Blocks/brickBlock");
            texture_CoinBlock = content.Load<Texture2D>("Blocks/questionBlock");
            texture_FloorBlock = content.Load<Texture2D>("Blocks/floorBlock");
			texture_StairBlock = content.Load<Texture2D>("Blocks/stairblock");
			texture_UsedBlock = content.Load<Texture2D>("Blocks/usedblock");
			texture_GreenPipeBlock = content.Load<Texture2D>("Blocks/GreenPipe");

			// Seth Sprint 2
			texture_FireFlower = content.Load<Texture2D>("fireFlower");
            texture_Star = content.Load<Texture2D>("star");
            texture_GreenMush = content.Load<Texture2D>("greenMushroom");
            texture_RedMush = content.Load<Texture2D>("redMushroom");
            texture_Coin = content.Load<Texture2D>("coin");

            //Zhuozi Sprint 2
            texture_Gommba = content.Load<Texture2D>("EnemyContent/WalkingGoomba1");
            texture_Koopa = content.Load<Texture2D>("EnemyContent/WalkingGreenKoopa1");
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
        public ISpriteE getGommbaSprite() 
        {
            return new GommbaSprite(texture_Gommba, new Vector2(500, 400), 1, 2, _graphics);
            
        }
        public ISpriteE getKoopaSprite()
        {
            
            return new GommbaSprite(texture_Koopa, new Vector2(500, 400), 1, 2, _graphics);
        }
        
    }
}
