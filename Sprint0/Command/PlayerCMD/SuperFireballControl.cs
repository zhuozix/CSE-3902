using Sprint0.Factory;
using Sprint0.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprint0.Command;
using Sprint0.Content;
using Sprint0.MarioPlayer;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Sprint0.Command.PlayerCMD
{
    public class SuperFireballContorl : ICommand
    {
        Game1 gameInstance;
        
        private bool isKeyPressed;
        SpritesFactory spritesFactory;
        public SuperFireballContorl(Mario playerInstance, Game1 gameInstance, SpritesFactory spritesFactory)
        {
            playerInstance = gameInstance.mario;
            isKeyPressed = false;
            this.gameInstance = gameInstance;
            this.spritesFactory = spritesFactory;
        }

        public void Execute()
        {
           
            if (!isKeyPressed)
            {
                ISprite fireball = spritesFactory.getFireballSprite(gameInstance.mario.Position, gameInstance.mario.IsFacingRight, false);
               
                isKeyPressed = true;
            }
        }

        public void Stop()
        {
            isKeyPressed = false;
        }
        protected void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.N))
            {

                Execute();
            }
            else
            {
                
                Stop();
            }

            
        }
    }
}
