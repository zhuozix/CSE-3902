using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/**
 * Controller Class for keyboard input. 
 * This class is not for player control. 
 * There will be a new controller class in the future.
 * 
 * @Version 2023/2/11
 * @Author Shuangchen
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

        //Only for switching sprites, not working for running.
        public void UpdateInput()
        {
            //if the key pressed and released, execute the command in the map.
            KeyboardState currentKeyboardState = Keyboard.GetState();
            Keys[] keysPressed = Keyboard.GetState().GetPressedKeys();

            //Excute the command only when key is released!
            foreach (Keys key in keysPressed)
            {
                //Check if the key is released or not.
                if (!previousKeyboardState.IsKeyDown(key))
                {
                    //Find command line.
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
