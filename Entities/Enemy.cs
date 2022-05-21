﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameProject.Domain;
using GameProject.Interfaces;
using GameProject.Physics;

namespace GameProject.Entities
{
    abstract class Enemy : Entity, IFightable
    {
        public float RotationAngle { get; set; }
        public int Speed { get; set; }
        public int Damage { get; set; }
        internal int Value { get; set; }

        protected Enemy(Vector location, Image image) : base(location, image)
        {
            HealthBar = new Rectangle(Hitbox.Location.X + (int)(0.25 * Hitbox.Width), Hitbox.Location.Y - 10,
                (int)(0.5 * Hitbox.Width), 10);
        }

        internal void Move()
        {
            var delta = new Vector(Math.Cos(RotationAngle), Math.Sin(RotationAngle)) * Speed;
            var nextLocation = new Point((int)(Hitbox.Location.X + delta.X), (int)(Hitbox.Location.Y + delta.Y));

            Hitbox = new Rectangle(nextLocation, Hitbox.Size);
        }

        internal float AngleToPlayer()
        {
            var x = (Game.Player.Hitbox.Location.X + Game.Player.Hitbox.Size.Width / 2f) - (Hitbox.Location.X + Hitbox.Size.Width / 2f);
            var y = (Game.Player.Hitbox.Location.Y + Game.Player.Hitbox.Size.Height / 2f) - (Hitbox.Location.Y + Hitbox.Size.Height / 2f);
            //return new Vector(x, y).AngleInDegrees;
            return new Vector(x, y).AngleInRadians;
        }

        public bool GetBoost(Booster booster)
        {
            switch (booster.Type)
            {
                case BoosterTypes.HealthBoost:
                    return GetHealth(booster.Impact);

                case BoosterTypes.DamageBoost:
                    return GetDamage(booster.Impact);

                case BoosterTypes.SpeedBoost:
                    return GetSpeed(booster.Impact);

                default: return false;
            }
        }
        public bool GetHealth(int impact)
        {
            if (Health == MaxHealth) return false;

            if (Health + impact > MaxHealth)
            {
                Health = MaxHealth;
                return true;
            }
            Health += impact;
            return true;
        }

        public bool GetDamage(int impact)
        {
            if (Damage == MaxDamage) return false;

            if (Damage + impact > MaxDamage)
            {
                Damage = MaxDamage;
                return true;
            }
            Damage += impact;
            return true;
        }
        public bool GetSpeed(int impact)
        {
            if (Speed == MaxSpeed) return false;

            if (Speed + impact > MaxSpeed)
            {
                Speed = MaxSpeed;
                return true;
            }
            Speed += impact;
            return true;
        }
        public bool GetSlowdown(int impact)
        {
            if (Speed == MinSpeed) return false;

            if (Speed - impact < MinSpeed)
            {
                Speed = MinSpeed;
                return true;
            }
            Speed -= impact;
            return true;
        }

        public void DealDamage(Entity entity)
        {
            var player = (Player)entity;
            player.TakeDamage(Damage);
            player.GetSlowdown(1);
        }

        public void TakeDamage(int damage)
        {
            if (Health - damage < MinHealth)
            {
                Health = MinHealth;
                return;
            }
            Health -= damage;
        }
    }
}
