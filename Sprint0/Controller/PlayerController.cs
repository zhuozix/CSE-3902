using Microsoft.Xna.Framework.Input;
using Sprint0.Command.PlayerCMD;
using Sprint0.Content;
using Sprint0.MarioPlayer;
using Sprint0.MarioPlayer.State.ActionState;
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

        private ICommand idle;
        private ICommand fallAfterJump;
        private Mario playerInstance;
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
            playerInstance = gameInstance.mario;

            //Basic movement command
            ICommand jump = new MarioJumpCommand(playerInstance);
            ICommand moveLeft = new MarioMoveLeftCommand(playerInstance);
            ICommand moveRight = new MarioMoveRightCommand(playerInstance);
            ICommand crouch = new MarioCrouchCommand(playerInstance);
            ICommand fire = new fireFireball(playerInstance);


            //Add movement commands to controller
            this.AddCommand(Keys.W, jump);
            this.AddCommand(Keys.A, moveLeft);
            this.AddCommand(Keys.D, moveRight);
            this.AddCommand(Keys.S, crouch);
            this.AddCommand(Keys.Up, jump);
            this.AddCommand(Keys.Left, moveLeft);
            this.AddCommand(Keys.Right, moveRight);
            this.AddCommand(Keys.Down, crouch);
            //Fireball controls
            this.AddCommand(Keys.N, fire);
            this.AddCommand(Keys.Z, fire);

            this.idle = new MarioIdle(playerInstance);
            this.fallAfterJump = new fallAfterJump(playerInstance);
        }

        public void UpdateInput()
        {
            //if the key pressed, execute the command in the map.
            KeyboardState currentKeyboardState = Keyboard.GetState();
            Keys[] keysPressed = Keyboard.GetState().GetPressedKeys();
            MarioActionStateType currentActionType = playerInstance.CurrentActionState.GetEnumValue();
            if(keysPressed.Length == 0 && currentActionType != MarioActionStateType.Falling)
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
