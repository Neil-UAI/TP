using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Engine.Utils;
using System.Media;

namespace Game.Sounds
{
    class SoundProvider
    {
        private static SoundProvider instance;

        Dictionary<string, SoundPlayer> sounds;

        public static SoundProvider Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SoundProvider();
                }

                return instance;
            }
        }

        public SoundProvider()
        {
            this.sounds = new Dictionary<string, SoundPlayer>();
            this.sounds["laser"] = new SoundPlayer(Properties.Resources.laser);
            this.sounds["explosion1"] = new SoundPlayer(Properties.Resources.explosion1);
            this.sounds["explosion2"] = new SoundPlayer(Properties.Resources.explosion2);
            this.sounds["start"] = new SoundPlayer(Properties.Resources.start);
        }

        public void Play(string sound)
        {
            this.sounds[sound].Play();
        }
    }
}
