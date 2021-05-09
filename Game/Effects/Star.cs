using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using Engine;

using Engine.Extensions;
using Game.Effects;

namespace Game
{
    public class Star : GameObject
    {
        private Image img;
        float speed;
        private StarPool sp;
        private bool isDead = false;

        public bool IsDead { get => isDead; set => isDead = value; }

        public Star(StarPool sp, Image img, float speed)
        {
            this.sp = sp;
            int size = (0.16 * speed).FloorToInt().Max(1);
            this.img = new Bitmap(img, new Size(size, size));
            this.speed = speed;
            Extent = this.img.Size;
        }

        public override void Update(float deltaTime)
        {
            if (!this.isDead)
            {
                X -= this.speed * deltaTime;

                if (X < -20)
                {
                    this.isDead = true;
                    this.sp.RealeseStar(this);
                }
            }
        }

        public override void DrawOn(Graphics graphics)
        {
            graphics.DrawImage(img, Position);
        }
    }
}