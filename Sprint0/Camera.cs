using Microsoft.Xna.Framework;
using Sprint0.MarioPlayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0
{
    public class Camera
    {
        public Matrix Transform { get; private set; }

        public void MoveCamera(Mario player)
        {

            var position = Matrix.CreateTranslation(0, 0, 0);

            //400 is a magic number for the width of the screen, replace with a width value later
            if (player.Position.X > 400)
            {
                position = Matrix.CreateTranslation(-player.Position.X + 400, 0, 0);
            }
            if (player.Position.X > 6656)
            {
                position = Matrix.CreateTranslation(-6656, 0, 0);
            }

            Transform = position;
        }
    }
}
