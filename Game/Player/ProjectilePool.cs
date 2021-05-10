using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine;

namespace Game.Player
{
    public class ProjectilePool
    {
        private static ProjectilePool instance;
        private List<Projectile> projectileList = new List<Projectile>();
        public static GameObject world;

        public static ProjectilePool Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ProjectilePool();
                }

                return instance;
            }
        }


        private ProjectilePool()
        {

        }

        public void Init(GameObject world)
        {
            ProjectilePool.world = world;
            for (int i = 0; i < 20; i++)
            {
                Projectile pro = new Projectile();
                projectileList.Add(pro);
            }
        }

        public Projectile GetProjectile()
        {
            if (projectileList.Count == 0) return null;

            Projectile pro = projectileList[0];
            projectileList.RemoveAt(0);
            return pro;
        }

        public void ReleaseProjectile(Projectile pro)
        {
            world.RemoveChild(pro);
            projectileList.Add(pro);
        }
    }
}
