using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.Content;
using Sprint0.Sprites;

namespace Sprint0.MarioPlayer
{
    public abstract class Entity : ISprite
    {
        public string Name { get; set; }
        public string state { get; set; }
        public bool crash { get; set; }
        public bool mode { get; set; }
        public bool Jumpmode { get; set; }
        public Sprite Sprite { get; set; }
        public Texture2D Texture { get { return Sprite.Texture; } }
        public int Width { get { return Texture.Width; } }
        public int Height { get { return Texture.Height; } }

        public int rows { get; set; }
        public int cols { get; set; }

        public Vector2 Position { get { return Sprite.Position; } set { Sprite.Position = value; } }
        public Vector2 velocity { get; set; }
        public Vector2 Acceleration { get; set; }

        

        public virtual void Update(GameTime gameTime)
        {

            velocity += (float)gameTime.ElapsedGameTime.TotalSeconds * Acceleration;
            Position += (float)gameTime.ElapsedGameTime.TotalSeconds * velocity;
            Sprite.Update(gameTime);
        }

        public virtual void Draw(SpriteBatch spriteBatch, bool isFlipped)
        {
            Sprite.Draw(spriteBatch, isFlipped);
        }

        public virtual void DrawRed(SpriteBatch spriteBatch, bool isFlipped)
        {
            Sprite.DrawRed(spriteBatch, isFlipped);
        }

        public virtual void DrawGold(SpriteBatch spriteBatch, bool isFlipped)
        {
            Sprite.DrawGold(spriteBatch, isFlipped);
        }

        public virtual void DrawPink(SpriteBatch spriteBatch, bool isFlipped)
        {
            Sprite.DrawPink(spriteBatch, isFlipped);
        }
    }
}
