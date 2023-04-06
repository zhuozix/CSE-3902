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
        private float cameraX = 0;

        public void MoveCamera(Mario player)
        {

            var cameraPos = Matrix.CreateTranslation(0, 0, 0);

            //400 is a magic number for the width of half the screen, replace with a width value later
                if (player.Position.X - cameraX > 0)
                    
                {
                    cameraX = player.Position.X;
                } else if (player.Position.X - cameraX < -200)
                {
                    cameraX = player.Position.X + 200;
                }

            if (cameraX < 400) // Don't let the screen scroll too far back past the start
            {
                cameraX = 400;
            }
            if (cameraX > 6256 && cameraX < 6656) // Don't let the screen scroll too far past the end
            {
                cameraX = 6256;
            }
            if (cameraX > 6656) // Don't let the screen scroll in the underground
            {
                cameraX = 7056;
            }

            cameraPos = Matrix.CreateTranslation(-cameraX + 400, 0, 0);
            Transform = cameraPos;
        }
    }
}
