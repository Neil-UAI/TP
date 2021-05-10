using Engine;
using Engine.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class SpaceNoise : GameObject
    {
        private Image image;
        float speed;
        float scale;
        bool flipX;
        bool flipY;

        Bitmap[] images;

        public SpaceNoise(Image image, float speed, float scale, bool flipX, bool flipY)
        {
            this.speed = speed;
            this.scale = scale;
            this.flipX = flipX;
            this.flipY = flipY;
            this.image = image;

            Extent = new SizeF(image.Width * scale, image.Height * scale);

            images = new Bitmap[15];
            for (int i = 0; i < images.Length; i++)
            {
                images[i] = new Bitmap(image, new Size(Width.RoundedToInt(), Height.RoundedToInt()));
                if (flipX) { images[i].RotateFlip(RotateFlipType.RotateNoneFlipX); }
                if (flipY) { images[i].RotateFlip(RotateFlipType.RotateNoneFlipY); }
            }
        }

        public override void Update(float deltaTime)
        {
            MoveLeft(deltaTime);
            KeepInsideScreen();
        }

        private void MoveLeft(float deltaTime)
        {
            X -= speed * deltaTime;
        }

        private void KeepInsideScreen()
        {
            X = X.Mod(Parent.Width);
            Y = Y.Mod(Parent.Height);
        }

        public override void DrawOn(Graphics graphics)
        {
            FillScreenTiled(graphics);
        }

        int w;
        int h;
        int x;
        int y;
        Point point = new Point();
        public void FillScreenTiled(Graphics graphics)
        {
            w = Width.RoundedToInt();
            h = Height.RoundedToInt();
            x = Position.X.RoundedToInt();
            y = Position.Y.RoundedToInt();

            int i = 0;
            for (int x1 = x - Parent.Right.RoundedToInt(); x1 <= Parent.Right; x1 += w)
            {
                for (int y1 = y; y1 <= Parent.Bottom; y1 += h)
                {
                    point.X = x1;
                    point.Y = y1;
                    graphics.DrawImage(images[i], point);
                    i++;
                }
            }
        }
    }
}
