using System;

namespace Gameplay.Configs.Enemies
{
    [Serializable]
    public class RangedAoeEnemy : Enemy
    {
        public int Damage;
        public float AoeRadius;
        public float DelayBeforeHit;
    }
}