using Microsoft.Xna.Framework.Input;
using Sprint0.Command.PlayerCMD;
using Sprint0.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprint0.MarioPlayer;


namespace Sprint0.Controller
{
    public class MouseController: IController
    {
        private Dictionary<String, ICommand> commandMap;

        MouseState previousMouseState;

        Game1 gameInstance;

        public MouseController(Game1 gameInstance)
        {
            commandMap = new Dictionary<String, ICommand>();
            previousMouseState = Mouse.GetState();
            this.gameInstance = gameInstance;
        }

        public void loadCommonCommand()
        {
            Mario playerInstance = gameInstance.mario;

            ICommand jump = new MarioJumpCommand(playerInstance);
            ICommand moveLeft = new MarioMoveLeftCommand(playerInstance);
            ICommand moveRight = new MarioMoveRightCommand(playerInstance);
            ICommand crouch = new MarioCrouchCommand(playerInstance);
            ICommand fire = new fireFireball(playerInstance);
            ICommand idle = new MarioIdle(playerInstance);

            commandMap.Add("jump", jump);
            commandMap.Add("moveLeft", moveLeft);
            commandMap.Add("moveRight", moveRight);
            commandMap.Add("crouch", crouch);
            commandMap.Add("fire", fire);
            commandMap.Add("idle", idle);
        }

        public void UpdateInput()
        {
            MouseState mouseState = Mouse.GetState();
            Keys[] keysPressed = Keyboard.GetState().GetPressedKeys();
            Boolean mousePressed = false;

            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                if (mouseState.X > 400) //magic number for half width of screen
                {
                    commandMap["moveRight"].Execute();
                } else
                {
                    commandMap["moveLeft"].Execute();
                }
                mousePressed = true;
            }

            if (mouseState.RightButton == ButtonState.Pressed)
            {
                commandMap["jump"].Execute();
                mousePressed = true;
            }

            if (mouseState.ScrollWheelValue != previousMouseState.ScrollWheelValue)
            {
                commandMap["fire"].Execute();
                mousePressed = true;
            }

            if (mouseState.XButton1 == ButtonState.Pressed || mouseState.MiddleButton == ButtonState.Pressed)
            {
                commandMap["crouch"].Execute();
                mousePressed = true;
            }

            if (keysPressed.Length == 0 && mousePressed == false)
            {
                commandMap["idle"].Execute();
            }

            previousMouseState = mouseState;
        }
    }
}
