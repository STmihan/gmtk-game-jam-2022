using System;

namespace Gameplay.Configs.Enemies.Stats
{
    [Serializable]
    public class BigMeleeEnemy : Enemy
    {
        public float DelayBeforeHit;
        public float DelayBeforeExplosion;
        public int Damage;
        public float AoeRadius;
    }
}