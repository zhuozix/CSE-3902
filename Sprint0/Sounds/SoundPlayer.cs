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
        private static SoundEffect blockBreak;
        private static SoundEffect bump;
        private static SoundEffect fireball;
        private static SoundEffect powerupAppears;
        private static SoundEffect powerup;
        private static SoundEffect oneUp;
        private static SoundEffect coin;
        private static SoundEffect death;
        private static SoundEffect stomp;
        private static SoundEffect marioHit;
        private static SoundEffect stageClear;
        private static SoundEffect pipe;

        public static void loadSounds(Game1 game)
        {
            gameInstance = game;
            ContentManager content = gameInstance.Content;

            mainTheme = content.Load<Song>("Sounds/main-theme-overworld");
            jump1 = content.Load<SoundEffect>("Sounds/smb_jumpsmall");
            jump2 = content.Load<SoundEffect>("Sounds/smb_jump-super");
            blockBreak = content.Load<SoundEffect>("Sounds/smb_breakblock");
            bump = content.Load<SoundEffect>("Sounds/smb_bump");
            fireball = content.Load<SoundEffect>("Sounds/smb_fireball");
            powerupAppears = content.Load<SoundEffect>("Sounds/smb_powerup_appears");
            powerup = content.Load<SoundEffect>("Sounds/smb_powerup");
            oneUp = content.Load<SoundEffect>("Sounds/smb_1-up");
            coin = content.Load<SoundEffect>("Sounds/smb_coin");
            death = content.Load<SoundEffect>("Sounds/smb_mariodie");
            stomp = content.Load<SoundEffect>("Sounds/smb_stomp");
            marioHit = content.Load<SoundEffect>("Sounds/smb_touch");
            stageClear = content.Load<SoundEffect>("Sounds/smb_stage_clear");
            pipe = content.Load<SoundEffect>("Sounds/smb_pipe");
        }

        public static void playMainTheme()
        {
            MediaPlayer.Play(mainTheme);
            MediaPlayer.IsRepeating = true;
        }

        public static void MuteMusic()
        {
            MediaPlayer.Volume = 0;
        }
        public static void UnMuteMusic()
        {
            MediaPlayer.Volume = 100;
        }
        // for game pause and resume use
        public static void PauseMusic()
        {
            MediaPlayer.Pause();
        }
        public static void ResumeMusic()
        {
            MediaPlayer.Resume();
        }
        public static void playJumpSmall()
        {
            jump1.Play();
        }

        public static void playJumpSuper()
        {
            jump2.Play();
        }

        public static void playBlockBreak()
        {
            blockBreak.Play();
        }

        public static void playBump()
        {
            bump.Play();
        }

        public static void playFireball()
        {
            fireball.Play();
        }

        public static void playPowerupAppears()
        {
            powerupAppears.Play();
        }

        public static void playPowerup()
        {
            powerup.Play();
        }

        public static void playOneUp()
        {
            oneUp.Play();
        }

        public static void playCoin()
        {
            coin.Play();
        }

        public static void playDeath()
        {
            MediaPlayer.Stop();
            death.Play();
        }

        public static void playStomp()
        {
            stomp.Play();
        }

        public static void playMarioHit()
        {
            marioHit.Play();
        }

        public static void playStageClear()
        {
            MediaPlayer.Stop();
            stageClear.Play();
        }

        public static void playPipe()
        {
            pipe.Play();
        }
        public static void stopMusic()
        {
            MediaPlayer.Stop();
        }
    }
}
