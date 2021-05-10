using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using Engine;
using Engine.Utils;
using Game.Enemies;

namespace Game
{
    public class EnemyShip : GameObject
    {
        private static Random rnd = new Random();

        private int shipIndex;
        private EnemyBehavior behavior;

        private Image shipImage;
        public bool isDead = true;
        public EnemyShip(int shipIndex, EnemyBehavior behavior)
        {
            this.shipIndex = shipIndex;
            this.behavior = behavior;

            this.shipImage = Images.ImageProvider.Instance.GetImage(this.shipIndex);
            Extent = new SizeF(this.shipImage.Size.Width / 2, this.shipImage.Size.Height / 2);

            Visible = false;
        }

        public PlayerShip Player
        {
            get
            {
                return AllObjects
                    .Select(obj => obj as PlayerShip)
                    .FirstOrDefault(obj => obj != null);
            }
        }

        public EnemyBehavior Behavior
        {
            get { return behavior; }
        }

        public int ShipIndex { get => shipIndex; set => shipIndex = value; }

        public override void Update(float deltaTime)
        {
            if (!isDead)
            {
                behavior.Update(this, deltaTime);
                Visible = true;

                if (X < -100)
                {                    
                    EnemyPool.world.RemoveChild(this);
                    EnemyPool.Instance.ReleaseEnemy(this);
                    isDead = true;
                    Visible = false;
                }
            }
        }

        public void Explode()
        {
            if (rnd.NextDouble() > 0.95)
            {
                PowerUp pup = new PowerUp();
                pup.Center = Center;
                PowerUps.PowerUpsManager.powerUpsList.Add(pup);
                Root.AddChild(pup);
            }
            Explosion.Burst(Parent, Center);            
            EnemyPool.world.RemoveChild(this);
            EnemyPool.Instance.ReleaseEnemy(this);
            Visible = false;
            isDead = true;
            X = -200;
        }

        public override void DrawOn(Graphics graphics)
        {
            graphics.DrawImage(shipImage, Bounds);
        }        
    }
}
