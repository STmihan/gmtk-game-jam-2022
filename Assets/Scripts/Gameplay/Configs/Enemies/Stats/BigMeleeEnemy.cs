using System;

namespace Gameplay.Configs.Enemies.Stats
{
    [Serializable]
    public class BigMeleeEnemy : Enemy
    {
        public float DelayBeforeHit;
        public int Damage;
        public float AoeRadius;
    }
}