using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Sprint0.Sprites;

namespace Sprint0.Content
{
    public abstract class GameObject
    {
        protected ISprite _sprite { get; set; }
        protected bool isFlipped = false;

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            _sprite.Draw(spriteBatch, isFlipped);
        }

        public virtual void Update(GameTime gameTime)
        {
            _sprite.Update(gameTime);
        }
    }
}
