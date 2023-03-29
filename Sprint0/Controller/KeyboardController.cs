using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprint0.Command;
using Sprint0.Command.GameControlCMD;
using Sprint0.Command.PlayerCMD;
using Sprint0.MarioPlayer;
using Sprint0.MarioPlayer.State.ActionState;
/**
* Controller Class for keyboard input. 
* This class is not for player control. 
* 
* @Version 2023/2/13 (1.1)
*/
namespace Sprint0.Content
{
    public class KeyboardController : IController
    {
        //create dictionary to map the keys.
        private Dictionary<Keys, ICommand> CommandMap;
        private Dictionary<Keys, ICommand> playerMap;

        //Save current states for comparsion.
        KeyboardState previousKeyboardState;

        //idle for mario
        //private ICommand idle; Taken over by mouse controller

        Game1 gameInstance;

        public KeyboardController(Game1 gameInstance)
        {
            CommandMap = new Dictionary<Keys, ICommand>();
            playerMap = new Dictionary<Keys, ICommand>();
            previousKeyboardState = Keyboard.GetState();
            this.gameInstance = gameInstance;
        }

        public void AddCommand(Keys key, ICommand command)
        {
            CommandMap.Add(key, command);
        }

        public void AddPlayerCommand(Keys key, ICommand command)
        {
            playerMap.Add(key, command);
        }

        public void loadCommonCommand()
        {
            Mario playerInstance = gameInstance.mario;
            /*
             * Set Common Command
             */
            // Game control
            ICommand exit = new Exit(gameInstance);
            ICommand reset = new Reset(gameInstance);
            //State change command
            ICommand takeDamage = new MarioDamageCheatCommand(playerInstance);
            ICommand toSuperMario = new MarioSuperCheatCommand(playerInstance);
            ICommand toNormalMario = new MarioNormalCheatCommand(playerInstance);
            ICommand toFireMario = new MarioFireCheatCommand(playerInstance);
            //Basic movement command
            ICommand jump = new MarioJumpCommand(playerInstance);
            ICommand moveLeft = new MarioMoveLeftCommand(playerInstance);
            ICommand moveRight = new MarioMoveRightCommand(playerInstance);
            ICommand crouch = new MarioCrouchCommand(playerInstance);
            //Fire
            ICommand fire = new fireFireball(playerInstance);
            //idle
            //this.idle = new MarioIdle(playerInstance); taken over by mouse controller


            /*
             * Put common command into controller map.
             */

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
            //Add movement commands to controller
            this.AddPlayerCommand(Keys.W, jump);
            this.AddPlayerCommand(Keys.A, moveLeft);
            this.AddPlayerCommand(Keys.D, moveRight);
            this.AddPlayerCommand(Keys.S, crouch);
            this.AddPlayerCommand(Keys.Up, jump);
            this.AddPlayerCommand(Keys.Left, moveLeft);
            this.AddPlayerCommand(Keys.Right, moveRight);
            this.AddPlayerCommand(Keys.Down, crouch);

        }

        public void UpdateInput()
        {
            //if the key pressed, execute the command in the map.
            KeyboardState currentKeyboardState = Keyboard.GetState();

            Keys[] keysPressed = Keyboard.GetState().GetPressedKeys();
            
            if (keysPressed.Length == 0)
            {
                // idle.Execute();   Taken over by mouse
            }
            else
            {
                bool commandFind = false;

                foreach (Keys key in keysPressed)
                {
                    if (playerMap.ContainsKey(key) || CommandMap.ContainsKey(key))
                    {
                        commandFind = true;
                    }
                }
                /* if (!commandFind)   Taken over by mouse controller
                {
                    idle.Execute(); 
                } */
               
            }
            //Excute the command
            foreach (Keys key in keysPressed)
            {
                if (playerMap.ContainsKey(key))
                {
                    playerMap[key].Execute();

                }
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
