﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using Engine;
using Game.Enemies;

namespace Game
{
    public class EnemySpawner : GameObject
    {
        private int emmisionInterval;
        private int shipIndex;
        private bool active = false;
        private EnemyBehavior behavior;
        public static List<EnemyShip> enemyList = new List<EnemyShip>();

        private int last = Environment.TickCount;

        public EnemySpawner(int shipIndex, int emmisionRate, EnemyBehavior behavior)
        {
            this.shipIndex = shipIndex;
            this.emmisionInterval = emmisionRate;
            this.behavior = behavior;
        }

        public bool Active
        {
            get { return active; }
            set { active = value; }
        }
        
        public int EmmisionInterval
        {
            get { return emmisionInterval; }
            set { emmisionInterval = value; }
        }

        public override void Update(float deltaTime)
        {
            if (!active) return;

            GameObject world = Parent;
            Center = world.Center;
            Left = world.Right;

            int now = Environment.TickCount;
            if (now - last > emmisionInterval)
            {
                last = now;
                EnemyShip ship = EnemyPool.Instance.GetEnemy(shipIndex);
                if (ship == null) { return; }
                enemyList.Add(ship);

                ship.Center = Center;
                world.AddChild(ship);
            }
        }

    }
}
