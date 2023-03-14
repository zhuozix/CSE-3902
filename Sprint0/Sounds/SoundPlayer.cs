using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0.Sounds
{
    public static class SoundPlayer
    {
        private static Game1 gameInstance;
        private static Song mainTheme;
        private static SoundEffect jump1;
        private static SoundEffect jump2;

        public static void loadSounds(Game1 game)
        {
            gameInstance = game;
            ContentManager content = gameInstance.Content;

            mainTheme = content.Load<Song>("Sounds/main-theme-overworld");
            jump1 = content.Load<SoundEffect>("Sounds/smb_jumpsmall");
            jump2 = content.Load<SoundEffect>("Sounds/smb_jump-super");
        }

        public static void playMainTheme()
        {
            MediaPlayer.Play(mainTheme);
            MediaPlayer.IsRepeating = true;
        }

        public static void playJumpSmall()
        {
            jump1.Play();
        }

        public static void playJumpSuper()
        {
            jump2.Play();
        }
    }
}
