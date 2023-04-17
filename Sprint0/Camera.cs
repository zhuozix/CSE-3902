using Microsoft.Xna.Framework;
using Sprint0.MarioPlayer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0
{
    public class Camera
    {
        public Matrix Transform { get; private set; }
        private float cameraX = 0; //position of the camera
        private float halfScreen = 400; //Half the width of the screen, used to position camera
        private float quartScreen = 200; //Quarter width of the screen, used to position camera
        private float yOffset = 100; //Offset to raise camera to correct height
        public float endLevel; //Camera position at the end of the level
        public float underground; //Start of the underground area

        public void MoveCamera(Mario player)
        {

                if (player.Position.X - cameraX > 0)
                    
                {
                    cameraX = player.Position.X;
                } else if (player.Position.X - cameraX < -quartScreen)
                {
                    cameraX = player.Position.X + quartScreen;
                }

            if (cameraX < halfScreen) // Don't let the screen scroll too far back past the start
            {
                cameraX = halfScreen;
            }
            if (cameraX > endLevel) // Don't let the screen scroll too far past the end
            {
                cameraX = endLevel;
            }
            if (player.Position.X > underground) // Don't let the screen scroll in the underground
            {
                cameraX = underground + halfScreen;
            }

            Transform = Matrix.CreateTranslation(-cameraX + halfScreen, yOffset, 0);
        }
    }
}
