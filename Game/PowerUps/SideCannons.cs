﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Engine;

namespace Game
{
    public class SideCannons : GameObject
    {
        private int duration;
        private int startTime;
        Cannon c1;
        Cannon c2;

        public SideCannons(int duration = 2500)
        {
            this.duration = duration;
            startTime = Environment.TickCount;

            Height = 100;

            c1 = new Cannon();
            c1.Center = Center;
            c1.CenterY = Top;
            c1.ShotInterval = 150;
            PlayerShip.cannonsList.Add(c1);
            AddChild(c1);

            c2 = new Cannon();
            c2.Center = Center;
            c2.CenterY = Bottom;
            c2.ShotInterval = 150;
            PlayerShip.cannonsList.Add(c2);
            AddChild(c2);
        }

        public override void Update(float deltaTime)
        {
            if (Environment.TickCount - startTime > duration)
            {
                PlayerShip.cannonsList.Remove(c1);
                PlayerShip.cannonsList.Remove(c2);
                Delete();
            }
        }
    }
}
