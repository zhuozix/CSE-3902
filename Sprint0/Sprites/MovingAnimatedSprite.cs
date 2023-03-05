using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.ObjectManager;

namespace Sprint0.Content
{


    public class MovingAnimatedSprite : Sprite
    {

        private static float moveSpeed = 100f;

        private GraphicsDeviceManager graphics;
        private Vector2 originalPosition;
        private int moveDirection;
        private GameObjectManager gameobj;

        public MovingAnimatedSprite(Texture2D texture, Vector2 position, int rows, int cols, GraphicsDeviceManager graphics, int moveDirection)
            : base(texture, position, rows, cols)
        {
            this.graphics = graphics;
            originalPosition = position;
            this.moveDirection = moveDirection;
            //this.gameobj= gameObjectManager;
        }

        public override void Update(GameTime gameTime)
        {
            float currentHeight = position.X;
            float maxHeight = graphics.PreferredBackBufferWidth / 2 + 385;
            float minHeight = (graphics.PreferredBackBufferWidth - originalPosition.X) - originalPosition.X;

            timeSinceLastFrameTransition += gameTime.ElapsedGameTime.TotalSeconds;
            if (timeSinceLastFrameTransition > animateFrequency)
            {
                timeSinceLastFrameTransition = 0;
                currentFrame++;
                if (currentFrame == totalFrames)
                {
                    currentFrame = 0;
                }
            }
            if (currentHeight >= maxHeight)
            {
                position.X = maxHeight;
            }
            else if (currentHeight <= minHeight)
            {
                position.X = minHeight;
            }
            else
            {
                position += new Vector2(moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds * moveDirection, 0);
            }
        }
    }
}
