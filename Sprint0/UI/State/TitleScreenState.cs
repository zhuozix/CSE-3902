using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprint0.UI.State;
using Sprint0.Sprites;

namespace Sprint0.UI.Title
{
    public class TitleScreenState : IGameState
    {
        private readonly Game1 _game;

        public TitleScreenState(Game1 game)
        {
            _game = game;
        }
        //test conveient
        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                _game.ChangeState(new PlayState(_game));
            } else if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                _game.ChangeState(new bossfightState(_game));
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _game.GraphicsDevice.Clear(Color.White);
            spriteBatch.Begin();
            ISprite text = _game.spritesFactory.getTitleFontSprite(gameTime);
            text.Draw(spriteBatch, false);
            spriteBatch.End();

            
        }

        public void Enter()
        {
            
        }

        public void Exit()
        {
           
        }
    }
}
