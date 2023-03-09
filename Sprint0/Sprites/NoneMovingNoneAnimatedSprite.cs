using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0.Content
{
        public class NoneAnimatedNonMovingSprite : Sprite
        {
            public NoneAnimatedNonMovingSprite(Texture2D texture, Vector2 position, int rowsIn, int colsIn)
                : base(texture, position, rowsIn, colsIn)
            { }

            public override void Update(GameTime gameTime)
            {
                // no update
            }
        }
    }
