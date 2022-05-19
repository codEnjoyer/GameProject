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
    internal class SmallEnemy : Enemy
    {
        internal SmallEnemy(Vector location) : base(location, Resources.SmallZombie)
        {
            Speed = 10;
        }
    }
}