using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Effects
{
    public class StarPool
    {
        private List<Star> starAvailableList = new List<Star>();
        private Image img;
        private Random rnd = new Random();

        public StarPool(int amount)
        {
            img = Properties.Resources.star;
            Init(amount);
        }

        public void Init(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                starAvailableList.Add(new Star(this, Properties.Resources.star, rnd.Next(300)));
            }
        }

        public Star GetStar()
        {
            if (starAvailableList.Count == 0) return null;

            Star star = starAvailableList[0];
            starAvailableList.RemoveAt(0);
            return star;
        }

        public void RealeseStar(Star star)
        {
            starAvailableList.Add(star);
        }
    }
}
