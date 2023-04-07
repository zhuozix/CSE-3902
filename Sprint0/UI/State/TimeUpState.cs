using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Sprint0.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprint0.UI.Title;

namespace Sprint0.UI.State
{
    internal class TimeUpState : IGameState
    {
        private readonly Game1 _game;
        private float time = 0f;
        private ISprite text;

        public TimeUpState(Game1 game)
        {
            _game = game;
            text = _game.spritesFactory.getTimeUpFontSprite();
        }

        public void Update(GameTime gameTime)
        {
            text.Update(gameTime);
            time += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (time > 2f)
            {
                _game.ChangeState(new GameOverState(_game));
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _game.GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin();


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
