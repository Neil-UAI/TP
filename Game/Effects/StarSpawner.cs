using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Engine.Extensions;
using Engine;
using Game.Effects;

namespace Game
{
    public class StarSpawner : GameObject
    {
        private Random rnd = new Random();
        private bool firstFrame = true;
        private StarPool starPool;

        public StarSpawner()
        {
            this.starPool = new StarPool(300);
        }

        public override void Update(float deltaTime)
        {
            if (firstFrame)
            {
                firstFrame = false;
                FillSpace(300);
            }

            Left = Parent.Right;
            for (int i = 0; i < 5 * deltaTime; i++)
            {
                SpawnStar();
            }
        }

        private void FillSpace(int numberOfStars)
        {
            for (int i = 0; i < numberOfStars; i++)
            {
                CenterX = rnd.Next(Parent.Left.RoundedToInt(), Parent.Right.RoundedToInt());
                Star star = this.starPool.GetStar();
                if (star == null) break;

                star.Center = Center;
                Parent.AddChildBack(star);
                CenterY = rnd.Next(Parent.Top.RoundedToInt(), Parent.Bottom.RoundedToInt());
            }
        }

        public void SpawnStar()
        {
            Star star = this.starPool.GetStar();
            if (star == null) return;

            star.X = MainScene.ActiveForm.Width;
            star.Y = rnd.Next(0, MainScene.ActiveForm.Height);
            star.IsDead = false;
        }

    }
}

