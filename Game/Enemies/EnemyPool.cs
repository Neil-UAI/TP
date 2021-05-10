using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine;

namespace Game.Enemies
{
    public class EnemyPool
    {
        private static EnemyPool instance;        
        private List<EnemyShip>[] enemyList = new List<EnemyShip>[7];
        public static GameObject world;
        public static EnemyPool Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EnemyPool();
                }

                return instance;
            }
        }


        private EnemyPool()
        {

            for (int i = 0; i < enemyList.Length; i++)
                enemyList[i] = new List<EnemyShip>();
        }

        public void Init(GameObject world)
        {
            EnemyPool.world = world;
            int[] shipType = { 0, 11, 29, 20, 42, 63, 57 };
            EnemyBehavior[] enemyBehaviors = {
                new FuncBehavior(x => Math.Sin(x * 10) * 0.9, 175),
                new FollowPlayerBehavior(200),
                new FuncBehavior(x => -0.9 * (2 / Math.PI) * Math.Asin(Math.Sin(Math.PI * x * 3)), 250),
                new FlockingBehavior(200),
                new FuncBehavior(x => (Math.Sin(x * 10) + Math.Sin(x * 5)) * 0.5, 275),
                new FuncBehavior(x => Math.Atan(x * 10 - 5) * -0.5, 200),
                new FuncBehavior(x => (Math.Sin((x + 1) * 10 + 15) - Math.Sin((x + 1) * 15)) * 0.2 - 0.4, 300)
            };

            for (int i = 0; i < shipType.Length; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    EnemyShip ship = new EnemyShip(shipType[i], enemyBehaviors[i]);
                    enemyList[i].Add(ship);
                }
            }

            return;
        }

        private int GetIndex(int shipIndex) {
            switch (shipIndex)
            {
                case 0:
                    return 0;
                    break;
                case 11:
                    return 1;
                    break;
                case 29:
                    return 2;
                    break;
                case 20:
                    return 3;
                    break;
                case 42:
                    return 4;
                    break;
                case 63:
                    return 5;
                    break;
                case 57:
                    return 6;
                    break;
                default:
                    return 0;
                    break;
            }
        }

        public EnemyShip GetEnemy(int shipIndex)
        {
            int select = GetIndex(shipIndex);
            

            if (enemyList[select].Count == 0) return null;

            EnemyShip enemy = enemyList[select][0];
            enemyList[select].RemoveAt(0);
            enemy.isDead = false;
            return enemy;
        }

        public void ReleaseEnemy(EnemyShip enemy)
        {
            int select = GetIndex(enemy.ShipIndex);
            enemyList[select].Add(enemy);
        }
    }
}
