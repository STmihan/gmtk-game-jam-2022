using System;

namespace Gameplay.Configs.Enemies.Stats
{
    [Serializable]
    public class RangedAoeEnemy : Enemy
    {
        public int Damage;
        public float AoeRadius;
        public float DelayBeforeHit;
    }
}