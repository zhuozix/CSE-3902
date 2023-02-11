using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Sprint0.Content;

namespace Sprint0.Controller
{
    public class MouseController : IController
    {
        //create dictionary to map the keys
        private Dictionary<Keys, ICommand> CommandMap;
        public MouseController()
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
