using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Engine;
using System.Drawing;
using Game.Sounds;

namespace Game
{
    public class Cannon : GameObject
    {
        private Image img;

        private int shotInterval;
        private int lastShoot = 0;
        private bool autoFire = false;

        private SoundProvider sp;

        public Cannon()
        {
            img = Properties.Resources.cannon;
            sp = SoundProvider.Instance;
            Extent = img.Size;
            DefaultShotInterval();
        }

        public int ShotInterval
        {
            get { return shotInterval; }
            set { shotInterval = value; }
        }

        public bool AutoFire
        {
            get { return autoFire; }
            set { autoFire = value; }
        }

        public override void Update(float deltaTime)
        {
            if (Visible && CheckForCollision())
            {
                Delete();
            }
            else if (autoFire)
            {
                Shoot();
            }
        }

        public void DefaultShotInterval()
        {
            shotInterval = 250;
        }

        private bool CheckForCollision()
        {
            IEnumerable<EnemyShip> collisions = AllObjects
                .Where((m) => CollidesWith(m))
                .Select((m) => m as EnemyShip)
                .Where((m) => m != null);
            if (collisions.Count() == 0) return false;
            foreach (EnemyShip enemy in collisions)
            {
                enemy.Explode();
            }
            return true;
        }

        public void Shoot()
        {
            int now = Environment.TickCount;
            if (Math.Abs(now - lastShoot) > shotInterval)
            {
                lastShoot = now;

                Projectile projectile = new Projectile();
                projectile.Center = Center;
                projectile.Left = Right;
                Root.AddChild(projectile);
                sp.Play("laser");
            }
        }

        public override void DrawOn(Graphics graphics)
        {
            graphics.DrawImage(img, Position);
        }
    }
}
