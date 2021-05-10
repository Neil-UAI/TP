using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Engine;
using Engine.Extensions;
using Game.Sounds;
using Game.Effects;

namespace Game
{
    public class Explosion : GameObject
    {
        private static Random rnd = new Random();
        private static GameObject world;

        public static void Burst(GameObject world, PointF point, 
            int amount = 100, 
            int minMagnitude = 50,
            int maxMagnitude = 300,
            float initialSize = 2,
            float deltaSize = 2,
            float deltaAlpha = -2)
        {
            Explosion.world = world;
            for (int i = 0; i < amount; i++)
            {
                float angle = rnd.Next(0, 360);
                float magnitude = rnd.Next(minMagnitude, maxMagnitude);
                float o = (float)Math.Sin(angle) * magnitude;
                float a = (float)Math.Cos(angle) * magnitude;
                Explosion explosion = ExplosionPool.Instance.GetExplosion();
                if (explosion == null) { return; }
                explosion.SetupExplosion(new PointF(a, o), initialSize, deltaSize, deltaAlpha);
                explosion.Center = point;
                ExplosionPool.world.AddChild(explosion);
            }
            if (rnd.NextDouble() > 0.5)
            {
                SoundProvider.Instance.Play("explosion1");
            }
            else
            {
                SoundProvider.Instance.Play("explosion2");
            }
        }

        private float alpha = 1;

        private PointF speed;
        private float deltaSize;
        private float deltaAlpha;

        
        public bool isDead;
        public Explosion()
        {
            this.isDead = false;
        }

        private void SetupExplosion(PointF speed,
            float initialSize = 2,
            float deltaSize = 2,
            float deltaAlpha = -2)
        {            
            this.alpha = 1;
            this.speed = speed;
            this.deltaSize = deltaSize;
            this.deltaAlpha = deltaAlpha;

            Extent = new SizeF(initialSize, initialSize);
        }

        public override void Update(float deltaTime)
        {
            if (!isDead)
            {
                PointF center = Center;
                Width += deltaSize * deltaTime;
                Height += deltaSize * deltaTime;
                Center = center;

                alpha += deltaAlpha * deltaTime;
                X += speed.X * deltaTime;
                Y += speed.Y * deltaTime;

                if (alpha < 0)
                {
                    isDead = true;
                    Explosion.world.RemoveChild(this);
                    ExplosionPool.Instance.ReleaseExplosion(this);
                }
            }
        }

        Pen pen = new Pen(Color.White);
        public override void DrawOn(Graphics graphics)
        {
            int a = (alpha * 255).RoundedToInt().MinMax(255, 0);
            int g = (255 - a).MinMax(200, 50);
            pen.Color = Color.FromArgb(a, 255, g, 0);
            graphics.FillRectangle(pen.Brush, Bounds);
        }
    }
}
