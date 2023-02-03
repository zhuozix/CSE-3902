using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0.Content
{
    class KeyboardController : IController
    {
        //create dictionary to map the keys
        private Dictionary<Keys, ICommand> CommandMap;
        public KeyboardController()
        {
            CommandMap = new Dictionary<Keys, ICommand>();
        }

        public void AddCommand(Keys key, ICommand command)
        {
            CommandMap.Add(key, command);
        }

        public void UpdateInput()
        {
            //if the key pressed is in the map, execute the command
            Keys[] keyPressed = Keyboard.GetState().GetPressedKeys();
            foreach (Keys key in keyPressed)
            {
                if (CommandMap.ContainsKey(key))
                {
                    CommandMap[key].Execute();
                }
            }

        }
    }
}
