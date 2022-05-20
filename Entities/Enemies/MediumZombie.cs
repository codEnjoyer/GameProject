﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameProject.Interfaces;
using GameProject.Physics;
using GameProject.Properties;

namespace GameProject.Entities.Enemies
{
    internal class MediumZombie : Enemy
    {
        internal MediumZombie(Vector location) : base(location, Resources.MediumZombie)
        {
            Speed = 3;
            MaxSpeed = 2 * Speed;

            Damage = 15;
            MaxDamage = 2 * Damage;

            MaxHealth = 100;
            Health = MaxHealth;
        }
    }
}
