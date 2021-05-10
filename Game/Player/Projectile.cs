using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using Engine;
using Engine.Extensions;
using Game.Player;

namespace Game
{
    public class Projectile : GameObject
    {
        private Image img;
        private float speed = 700;
        public bool isDead = true;

        public Projectile()
        {
            img = Properties.Resources.projectile;

            Extent = img.Size;
        }

        public override void Update(float deltaTime)
        {
            if (isDead) return;

            X += speed * deltaTime;

            CheckForCollision();

            if (X > 1400)
            {
                ProjectilePool.Instance.ReleaseProjectile(this);
                isDead = true;
                this.X = -400;
            }
        }

        private void CheckForCollision()
        {
            IEnumerable<EnemyShip> collisions = EnemySpawner.enemyList
               .Where((m) => CollidesWith(m))
               .Where((m) => m != null);
            foreach (EnemyShip enemy in collisions)
            {
                if (enemy != null)
                {
                    enemy.Explode();                    
                    isDead = true;
                    this.X = -400;
                    ProjectilePool.Instance.ReleaseProjectile(this);
                }
            }
        }

        public override void DrawOn(Graphics graphics)
        {
            graphics.DrawImage(img, Position);
        }
    }
}
