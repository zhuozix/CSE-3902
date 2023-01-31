using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Sprint_0
{
    public class Input : ISprite
    {
        private Texture2D _texture;
        private Rectangle _bounds = new Rectangle(350, 200, 105, 73);
        private Rectangle _soruce = new Rectangle(0, 0, 105, 73);
        private bool flag1= false;
        private bool flag2 = false;
        private bool flag3 = false;
        private bool flag4 = false;
        private Rectangle qua1 = new Rectangle(0, 0, 400, 220);
        private Rectangle qua2 = new Rectangle(400, 0, 800, 220);
        private Rectangle qua3 = new Rectangle(0, 220, 400, 440);
        private Rectangle qua4 = new Rectangle(400, 220, 800, 440);
        private Rectangle quaQuit = new Rectangle(0, 0, 800, 440);
        private Boundary Bound = new Boundary();
        private Anime anime = new Anime();

        public Input(Texture2D texture)
        {
            _texture = texture;                           //get the imagine
        }

        public void Update(Game1 game, GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.D0) || (Mouse.GetState().RightButton == ButtonState.Pressed && quaQuit.Contains(Mouse.GetState().X, Mouse.GetState().Y)))
            {
                game.Exit();             //if "0" get pressed or right click the mouse, quit the game
            }
            if (flag1 || Keyboard.GetState().IsKeyDown(Keys.D1) || (Mouse.GetState().LeftButton == ButtonState.Pressed && qua1.Contains(Mouse.GetState().X, Mouse.GetState().Y)))
            {
                flag1 = true;            //if "1" get pressed or left click the quad 1, one frame of animation and a fixed position
                flag3 = false;
                flag4 = false;
                flag2 = false;
                _soruce = new Rectangle(0, 0, 105, 73);

            }
            if (flag2 || Keyboard.GetState().IsKeyDown(Keys.D2) || (Mouse.GetState().LeftButton == ButtonState.Pressed && qua2.Contains(Mouse.GetState().X, Mouse.GetState().Y)))
            {
                flag2 = true;          //if "2" get pressed or left click the quad 2, animated sprite and a fixed position
                flag3 = false;
                flag4 = false;
                flag1 = false;
                _soruce = new Rectangle(anime.GetFrame(gameTime) * 105, 0, 105, 73);
            }
            if (flag3 || Keyboard.GetState().IsKeyDown(Keys.D3) || (Mouse.GetState().LeftButton == ButtonState.Pressed && qua3.Contains(Mouse.GetState().X, Mouse.GetState().Y)))
            {
                flag3 = true;         //if "3" get pressed or left click the quad 3, one frame of animation and moves up and down
                flag2 = false;
                flag4 = false;
                flag1 = false;

                _bounds = Bound.BoundaryY();

    }
            if (flag4 || Keyboard.GetState().IsKeyDown(Keys.D4) || (Mouse.GetState().LeftButton == ButtonState.Pressed && qua4.Contains(Mouse.GetState().X, Mouse.GetState().Y)))
            {
                flag4 = true;         //if "4" get pressed or left click the quad 4, animated sprite and moves left and right
                flag2 = false;
                flag3 = false;
                flag1 = false;

                _bounds = Bound.BoundaryX();
                _soruce = new Rectangle(anime.GetFrame(gameTime) * 105, 0, 105, 73);
            }
        }

        public void Draw(SpriteBatch spriteBatch) 
        {

                spriteBatch.Draw(_texture, _bounds, _soruce, Color.White);
            
        }
    }
}
