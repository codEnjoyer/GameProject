﻿using GameProject.Domain.Weapons;
using GameProject.Physics;
using GameProject.Properties;

namespace GameProject.Domain.Guns.Bullets
{
    internal class ShotgunBullet : Bullet
    {
        internal ShotgunBullet(Vector location, float angle) : base(location, Resources.Coin, angle)
        {
            Speed = 5;
        }
    }
}