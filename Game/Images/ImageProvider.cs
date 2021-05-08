using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Engine.Utils;

namespace Game.Images
{
    class ImageProvider
    {
        private static ImageProvider instance;

        private Image[] ships;

        public static ImageProvider Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ImageProvider();
                }

                return instance;
            }
        }

        public ImageProvider()
        {
            ships = Spritesheet.Load(@"Resources\shipsheetparts.png", new Size(200, 200));
            foreach (Image img in ships)
            {
                img.RotateFlip(RotateFlipType.Rotate270FlipNone);
            }
        }

        public Image GetImage(int shipIndex)
        {
            Image result = ships[shipIndex];
            result.RotateFlip(RotateFlipType.RotateNoneFlipX);            
            return result;
        }
    }
}
