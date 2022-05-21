﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameProject.Physics;
using GameProject.Properties;

namespace GameProject.Entities
{
    internal class SmallZombie : Enemy
    {
        internal SmallZombie(Vector location) : base(location, Resources.SmallZombie)
        {
            Speed = 4;
            MaxSpeed = 2 * Speed;

            Damage = 10;
            MaxDamage = 2 * Damage;

            MaxHealth = 50;
            Health = MaxHealth;

            Value = 1;
        }
    }
}
