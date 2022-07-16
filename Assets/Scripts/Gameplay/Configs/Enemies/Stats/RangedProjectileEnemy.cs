using System;

namespace Gameplay.Configs.Enemies
{
    [Serializable]
    public class RangedProjectileEnemy : Enemy
    {
        public float ProjectileSpeed;
        public int Damage;
    }
}