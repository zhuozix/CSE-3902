using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Sprint0.Content;
using System.Collections;

namespace Sprint0.UI.Text
{
    internal class TitleTextSprite : TextSprite
    {
        Game1 gameInstance;
        string instruction;
        Vector2 nextLine;
       
        public TitleTextSprite(Game1 gameInstance,SpriteFont fontIn, string textIn, Vector2 location, Color fontColor) : base(fontIn, textIn, location, fontColor)
        {
            this.text = "Super Mario Bros.";
            this.instruction = "Press { Enter } to play.";
            this.nextLine = new Vector2(location.X, location.Y + 100);
            this.gameInstance = gameInstance;
        }

        public override void Update(GameTime gameTime)
        {
           
        }

        public override void Draw(SpriteBatch spriteBatch, bool isFlipped)
        {
            ContentManager content = gameInstance.Content;
            Texture2D thumbsUp = content.Load<Texture2D>("thumbs up");
            spriteBatch.Draw(thumbsUp, new Vector2(20, 0), Color.White);
            spriteBatch.DrawString(this.font, this.text, this.Position, this.color);
            spriteBatch.DrawString(this.font, this.instruction, this.nextLine, this.color);
        }
    }
}
