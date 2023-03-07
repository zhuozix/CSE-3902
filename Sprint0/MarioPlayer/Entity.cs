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
        public Sprite Sprite { get; set; }
        public Texture2D Texture { get { return Sprite.Texture; } }
        public int Width { get { return Texture.Width; } }
        public int Height { get { return Texture.Height; } }
     
        public Vector2 Position { get { return Sprite.Position; } set { Sprite.Position = value; } }
        public Vector2 velocity { get; set; }
        public Vector2 Acceleration { get; set; }
        public bool collide { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public bool collideA { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        

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
    }
}
