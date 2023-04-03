using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Sprint0.Command.GameControlCMD;
using Sprint0.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprint0.UI.Title;
using Sprint0.Content;

namespace Sprint0.UI.State
{
    internal class WinState : IGameState
    {
        private readonly Game1 _game;

        public WinState(Game1 game)
        {
            _game = game;
        }

        public void Update(GameTime gameTime)
        {
            Keys[] keysPressed = Keyboard.GetState().GetPressedKeys();
            ICommand exit = new Exit(_game);
            ICommand reset = new Reset(_game);
            if (keysPressed.Length != 0)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.R))
                {
                    reset.Execute();
                }
                else
                {
                    exit.Execute();
                }
            }

        }

        public void Draw(SpriteBatch spriteBatch,GameTime gameTime)
        {
            _game.GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin();

            ISprite text = _game.spritesFactory.getWinFontSprite();
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
