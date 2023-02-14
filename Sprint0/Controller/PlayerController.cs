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
        public ICommand MarioMoveLeftCommand { get; set; }
        public ICommand MarioMoveRightCommand { get; set; }
        public ICommand MarioJumpCommand { get; set; }
        public ICommand MarioCrouchCommand { get; set; }
        public ICommand MarioNormalCheatCommand { get; set; }
        public ICommand MarioSuperCheatCommand { get; set; }
        public ICommand MarioFireCheatCommand { get; set; }
        public ICommand MarioDamageCheatCommand { get; set; }
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
                /*Find command line.
                if (CommandMap.ContainsKey(keysPressed[0]))
                {
                    CommandMap[keysPressed[0]].Execute();
                }
                */
                switch (key)
                {
                    case Keys.W:
                        if (MarioJumpCommand != null)
                        {
                            MarioJumpCommand.Execute();
                        }
                        break;
                    case Keys.A:
                        if (MarioMoveLeftCommand != null)
                        {
                            MarioMoveLeftCommand.Execute();
                        }
                        break;
                    case Keys.D:
                        if (MarioMoveRightCommand != null)
                        {
                            MarioMoveRightCommand.Execute();
                        }
                        break;
                    case Keys.S:
                        if (MarioCrouchCommand != null)
                        {
                            MarioCrouchCommand.Execute();
                        }
                        break;
                    case Keys.Up:
                        if (MarioJumpCommand != null)
                        {
                            MarioJumpCommand.Execute();
                        }
                        break;
                    case Keys.Left:
                        if (MarioMoveLeftCommand != null)
                        {
                            MarioMoveLeftCommand.Execute();
                        }
                        break;
                    case Keys.Right:
                        if (MarioMoveRightCommand != null)
                        {
                            MarioMoveRightCommand.Execute();
                        }
                        break;
                    case Keys.Down:
                        if (MarioCrouchCommand != null)
                        {
                            MarioCrouchCommand.Execute();
                        }
                        break;
                    case Keys.D1:
                        if (MarioNormalCheatCommand != null)
                        {
                            MarioNormalCheatCommand.Execute();
                        }
                        break;
                    case Keys.D2:
                        if (MarioSuperCheatCommand != null)
                        {
                            MarioSuperCheatCommand.Execute();
                        }
                        break;
                    case Keys.D3:
                        if (MarioFireCheatCommand != null)
                        {
                            MarioFireCheatCommand.Execute();
                        }
                        break;
                    case Keys.E:
                        if (MarioDamageCheatCommand != null)
                        {
                            MarioDamageCheatCommand.Execute();
                        }
                        break;

                }
            }

        }
    }
}
