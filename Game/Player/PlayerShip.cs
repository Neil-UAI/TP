﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using Engine;
using Engine.Events;
using System.Windows.Forms;
using Engine.Extensions;
using Engine.Utils;

namespace Game
{
    public class PlayerShip : GameObject
    {
        public static readonly float MIN_SPEED = 200;
        public static readonly float MAX_SPEED = 500;

        private int shipIndex;
        private PointF direction = new PointF(0, 0);
        private HashSet<Keys> pressedKeys = new HashSet<Keys>();
        private Cannon cannon;
        private bool shieldActivated = false;
        private float speed = MIN_SPEED;

        public static List<Cannon> cannonsList = new List<Cannon>();
        private Image shipImage;
        private static PlayerShip instance;

        public static PlayerShip Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PlayerShip(33);
                }

                return instance;
            }
        }

        private PlayerShip(int shipIndex)
        {
            this.shipIndex = shipIndex;
            this.shipImage = Images.ImageProvider.Instance.GetImage(this.shipIndex);
            this.shipImage.RotateFlip(RotateFlipType.RotateNoneFlipX);
            Extent = new SizeF(this.shipImage.Size.Width / 2, this.shipImage.Size.Height / 2);

            EventHandler.KeyDown += OnKeyDown;
            EventHandler.KeyUp += OnKeyUp;

            cannon = new Cannon();
            cannon.Center = Center;
            cannon.Right = Right;
            cannon.Visible = false;
            cannonsList.Add(cannon);
            AddChild(cannon);
        }

        public bool ShieldActivated
        {
            get { return shieldActivated; }
            set { shieldActivated = value; }
        }

        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        public Cannon Cannon
        {
            get { return cannon; }
        }

        private void OnKeyUp(KeyUpEvent evt)
        {
            evt.Handled = false;
            pressedKeys.Remove(evt.KeyData);
        }

        private void OnKeyDown(KeyDownEvent evt)
        {
            evt.Handled = false;
            pressedKeys.Add(evt.KeyData);
        }

        public override void Update(float deltaTime)
        {
            float s = speed.MinMax(MAX_SPEED, MIN_SPEED);
            if (pressedKeys.Contains(Keys.Up)
                || pressedKeys.Contains(Keys.W))
            {
                direction.Y = -1 * s;
            }
            else if (pressedKeys.Contains(Keys.Down)
                || pressedKeys.Contains(Keys.S))
            {
                direction.Y = 1 * s;
            }
            else
            {
                direction.Y = 0;
            }

            if (pressedKeys.Contains(Keys.Left)
                || pressedKeys.Contains(Keys.A))
            {
                direction.X = -1 * s;
            }
            else if (pressedKeys.Contains(Keys.Right)
                || pressedKeys.Contains(Keys.D))
            {
                direction.X = 1 * s;
            }
            else
            {
                direction.X = 0;
            }


            X += direction.X * deltaTime;
            Y += direction.Y * deltaTime;
            KeepInsideOwner();

            
            CheckForPowerUps();
            if (CheckForCollision())
            {
                if (!shieldActivated)
                {
                    Explosion.Burst(Parent, Center, 1000, 30, 450, 2, 2, -1);
                    Explosion.Burst(Parent, TopLeft, 500, 30, 400, 1, 3, -1);
                    Explosion.Burst(Parent, TopRight, 500, 30, 400, 1, 3, -1);
                    Explosion.Burst(Parent, BottomLeft, 500, 30, 400, 1, 3, -1);
                    Explosion.Burst(Parent, BottomRight, 500, 30, 400, 1, 3, -1);
                    Delete();
                }
            }
            else if (pressedKeys.Contains(Keys.Space)
                || pressedKeys.Contains(Keys.Enter))
            {
                Shoot();
            }
        }

        private bool CheckForCollision()
        {
            IEnumerable<EnemyShip> collisions = EnemySpawner.enemyList.Where((m) => CollidesWith(m));
            if (collisions.Count() == 0) return false;
            foreach (EnemyShip enemy in collisions)
            {
                enemy.Explode();
            }
            return true;
        }

        private void CheckForPowerUps()
        {
            IEnumerable<PowerUp> pups = PowerUps.PowerUpsManager.powerUpsList
                .Where((m) => CollidesWith(m))
                .Where((m) => m != null);

            if (pups.Count() == 0) return;

            foreach (PowerUp pup in pups)
            {
                if (pup != null) { pup.ApplyOn(this); }
            }
        }

        private void KeepInsideOwner()
        {
            if (Left < Parent.Left) { Left = Parent.Left; }
            else if (Right > Parent.Right) { Right = Parent.Right; }

            if (Top < Parent.Top) { Top = Parent.Top; }
            else if (Bottom > Parent.Bottom) { Bottom = Parent.Bottom; }
        }

        private void Shoot()
        {
            foreach (Cannon cannon in PlayerShip.cannonsList)
            {
                cannon.Shoot();
            }
        }
        
        public override void DrawOn(Graphics graphics)
        {
            graphics.DrawImage(shipImage, Bounds);
        }
        
    }
}
