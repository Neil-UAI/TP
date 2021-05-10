using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine;

namespace Game.Effects
{
    public class ExplosionPool
    {
        private static ExplosionPool instance;
        private List<Explosion> explosionList = new List<Explosion>();
        public static GameObject world;

        public static ExplosionPool Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ExplosionPool();
                }

                return instance;
            }
        }


        private ExplosionPool()
        {

        }

        public void Init(GameObject world)
        {
            ExplosionPool.world = world;
            for (int i = 0; i < 1000; i++)
            {
                Explosion exp = new Explosion();
                explosionList.Add(exp);
            }
        }

        public Explosion GetExplosion()
        {
            if (explosionList.Count == 0) return null;

            Explosion exp = explosionList[0];
            explosionList.RemoveAt(0);
            return exp;
        }

        public void ReleaseExplosion(Explosion exp)
        {
            explosionList.Add(exp);
            exp.isDead = false;
        }
    }
}
