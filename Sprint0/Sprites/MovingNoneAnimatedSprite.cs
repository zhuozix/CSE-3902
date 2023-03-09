using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Threading.Tasks.Dataflow;

namespace Sprint0.Content
{
    public class MovingNoneAnimatedSprite : Sprite
    {
        private GraphicsDeviceManager graphics;
        private Vector2 originalPosition;
        //1 means right and down, -1 means move left and up
        private int moveDirection;
        static float moveSpeed = 100f;

        public MovingNoneAnimatedSprite(Texture2D texture, Vector2 position, int rowsIn, int colsIn, GraphicsDeviceManager graphics, int moveDirection)
            : base(texture, position, rowsIn, colsIn)
        {
            this.graphics = graphics;
            this.originalPosition = position;
            this.moveDirection = moveDirection;
        }

        public override void Update(GameTime gameTime)
        {
            float currHeight = position.Y;
            float maxHeight = graphics.PreferredBackBufferHeight - 32;
            float minHeight = 0;
            if (currHeight >= maxHeight)
            {
                position.Y = maxHeight;
            }
            else if (currHeight <= minHeight)
            {
                position.Y = minHeight;
            }
            else
            {
                position += new Vector2(0, moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds * moveDirection);
            }
        }
    }
}
