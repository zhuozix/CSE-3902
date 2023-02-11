using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0.Content
{
        public class NoneAnimatedNonMovingSprite : Sprite
        {
            public NoneAnimatedNonMovingSprite(Texture2D texture, Vector2 position, int rows, int cols)
                : base(texture, position, rows, cols)
            { }

            public override void Update(GameTime gameTime)
            {
                // no update
            }
        }
    }
