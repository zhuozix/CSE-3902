using Microsoft.Xna.Framework.Input;
using Sprint0.Command.PlayerCMD;
using Sprint0.Content;
using Sprint0.MarioPlayer;
using Sprint0.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0.Controller
{
    internal class PlayerController:IController
    {
        //create dictionary to map the keys.
        private Dictionary<Keys, ICommand> CommandMap;
        //test
        ICommand idle;
        public PlayerController() 
        {
            CommandMap = new Dictionary<Keys, ICommand>();
        }

        public void AddCommand(Keys key, ICommand command)
        {
            CommandMap.Add(key, command);
        }

        public void loadCommonCommand(Game1 gameInstance)
        {
            Mario playerInstance = gameInstance.mario;

            //Basic movement command
            ICommand jump = new MarioJumpCommand(playerInstance);
            ICommand moveLeft = new MarioMoveLeftCommand(playerInstance);
            ICommand moveRight = new MarioMoveRightCommand(playerInstance);
            ICommand crouch = new MarioCrouchCommand(playerInstance);


            //add command to controller
            this.AddCommand(Keys.W, jump);
            this.AddCommand(Keys.A, moveLeft);
            this.AddCommand(Keys.D, moveRight);
            this.AddCommand(Keys.S, crouch);

            
            this.idle = new MarioIdle(playerInstance);
        }

        public void UpdateInput()
        {
            //if the key pressed, execute the command in the map.
            Keys[] keysPressed = Keyboard.GetState().GetPressedKeys();
            
            if(keysPressed.Length == 0)
            {
                idle.Execute();
            }
            
            //Excute the command
            foreach (Keys key in keysPressed)
            {
             
                    if (CommandMap.ContainsKey(key))
                    {
                        CommandMap[key].Execute();
                    }
                   }
       
        }
    }
}
