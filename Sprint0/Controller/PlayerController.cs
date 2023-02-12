using Microsoft.Xna.Framework.Input;
using Sprint0.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0.Controller
{
    internal class PlayerController : IController
    {
        //create dictionary to map the keys.
        private Dictionary<Keys, ICommand> CommandMap;

        public PlayerController()
        {
            CommandMap = new Dictionary<Keys, ICommand>();
        }

        public void AddCommand(Keys key, ICommand command)
        {
            CommandMap.Add(key, command);
        }

        public void UpdateInput()
        {
            //if the key pressed execute the command in the map.
            KeyboardState currentKeyboardState = Keyboard.GetState();
            Keys[] keysPressed = Keyboard.GetState().GetPressedKeys();

            //Excute the command only when key is released!
            foreach (Keys key in keysPressed)
            {
                    //Find command line.
                    if (CommandMap.ContainsKey(keysPressed[0]))
                    {
                        CommandMap[keysPressed[0]].Execute();
                    }
                
            }
        }

    }
}
