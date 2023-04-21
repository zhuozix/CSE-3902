using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint0.Sounds;
using Sprint0.Sprites;
using Sprint0.UI.Title;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0.UI.State
{
    internal class LevelClearState : IGameState
    {
        private readonly Game1 _game;
        private float time = 0f;
        private ISprite text;

        public LevelClearState(Game1 game)
        {
            _game = game;
            text = _game.spritesFactory.getLevelClearTextSprite();
        }

        public void Update(GameTime gameTime)
        {
            text.Update(gameTime);
            time += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if(time > 3.5f)
            {
                _game.ChangeState(new bossfightState(_game));                
            }
        }

        public void Draw(SpriteBatch spriteBatch,GameTime gameTime)
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
            SoundPlayer.stopMusic();
        }
    }
}
