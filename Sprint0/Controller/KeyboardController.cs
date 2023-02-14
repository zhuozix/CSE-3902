using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprint0.Command;
using Sprint0.Command.GameControlCMD;
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
            /*
             * Set Common Command
             */

            // Adam Sprint 2
            ICommand SetBlockBrick = new SetBlockIndex(gameInstance, 0);
            ICommand SetBlockCoin = new SetBlockIndex(gameInstance, 1);
            // Change Item
            ICommand increaseItemIndex = new increaseItemIndex(gameInstance);
            ICommand decreaseItemIndex = new decreaseItemIndex(gameInstance);
            // Change Enemy 
            ICommand SetPrevious = new SetPrevious(gameInstance);
            ICommand SetNext = new SetNext(gameInstance);
            // Game control
            ICommand exit = new Exit(gameInstance);
            ICommand reset = new Reset(gameInstance);

            /*
             * Put common command into controller map.
             */

            // Adam Sprint 2
            this.AddCommand(Keys.T, SetBlockBrick);
            this.AddCommand(Keys.Y, SetBlockCoin);
            // Change Item
            this.AddCommand(Keys.I, increaseItemIndex);
            this.AddCommand(Keys.U, decreaseItemIndex);
            // Change Enemy
            this.AddCommand(Keys.O, SetPrevious);
            this.AddCommand(Keys.P, SetNext);
            // Game control
            this.AddCommand(Keys.Q, exit);
            this.AddCommand(Keys.R, reset);
        }

        //Only for switching sprites.
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
                    
                    if (CommandMap.ContainsKey(keysPressed[0]))
                    { 
                        CommandMap[keysPressed[0]].Execute();
                    }
                }         
            }
            //Save current state.
            previousKeyboardState = currentKeyboardState;
        }
    }
}
