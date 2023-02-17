using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprint0.Command;
using Sprint0.Command.GameControlCMD;
using Sprint0.Command.NpcCMD.EnemyCMD;
using Sprint0.Command.PlayerCMD;
using Sprint0.MarioPlayer;
/**
* Controller Class for keyboard input. 
* This class is not for player control. 
* 
* @Version 2023/2/13 (1.1)
*/
namespace Sprint0.Content
{
    class KeyboardController : IController
    {
        //create dictionary to map the keys.
        private Dictionary<Keys, ICommand> CommandMap;
        //Save current states for comparsion.
        KeyboardState previousKeyboardState;

        public KeyboardController()
        {
            CommandMap = new Dictionary<Keys, ICommand>();
            previousKeyboardState = Keyboard.GetState();
        }

        public void AddCommand(Keys key, ICommand command)
        {
            CommandMap.Add(key, command);
        }

        public void loadCommonCommand(Game1 gameInstance)
        {
            Mario playerInstance = gameInstance.mario;
            /*
             * Set Common Command
             */
            // Change Block
            ICommand increaseBlockIndex = new increaseBlockIndex(gameInstance);
            ICommand decreaseBlockIndex = new decreaseBlockIndex(gameInstance);
            // Change Item
            ICommand increaseItemIndex = new increaseItemIndex(gameInstance);
            ICommand decreaseItemIndex = new decreaseItemIndex(gameInstance);
            // Change Enemy 
            ICommand SetPrevious = new SetPrevious(gameInstance);
            ICommand SetNext = new SetNext(gameInstance);
            // Game control
            ICommand exit = new Exit(gameInstance);
            ICommand reset = new Reset(gameInstance);
            //State change command
            ICommand takeDamage = new MarioDamageCheatCommand(playerInstance);
            ICommand toSuperMario = new MarioSuperCheatCommand(playerInstance);
            ICommand toNormalMario = new MarioNormalCheatCommand(playerInstance);
            ICommand toFireMario = new MarioFireCheatCommand(playerInstance);
            //Fire
            ICommand fire = new fireFireball(playerInstance);
            /*
             * Put common command into controller map.
             */

            this.AddCommand(Keys.Y, increaseBlockIndex);
            this.AddCommand(Keys.T, decreaseBlockIndex);
            // Change Item
            this.AddCommand(Keys.I, increaseItemIndex);
            this.AddCommand(Keys.U, decreaseItemIndex);
            // Change Enemy
            this.AddCommand(Keys.O, SetPrevious);
            this.AddCommand(Keys.P, SetNext);
            // Game control
            this.AddCommand(Keys.Q, exit);
            this.AddCommand(Keys.R, reset);
            //Mario state control
            this.AddCommand(Keys.D1, toNormalMario);
            this.AddCommand(Keys.D2, toSuperMario);
            this.AddCommand(Keys.D3, toFireMario);
            this.AddCommand(Keys.E, takeDamage);
            //Fireball controls
            this.AddCommand(Keys.N, fire);
            this.AddCommand(Keys.Z, fire);

        }

        public void UpdateInput()
        {
            //if the key pressed, execute the command in the map.
            KeyboardState currentKeyboardState = Keyboard.GetState();
            Keys[] keysPressed = Keyboard.GetState().GetPressedKeys();

            //Excute the command
            foreach (Keys key in keysPressed)
            {
                //Check if the key is released or not.
                if (!previousKeyboardState.IsKeyDown(key))
                {
                    
                    if (CommandMap.ContainsKey(key))
                    { 
                        CommandMap[key].Execute();
                    }
                }         
            }
            //Save current state.
            previousKeyboardState = currentKeyboardState;
        }
    }
}
