using System;

namespace Gameplay.Configs.Enemies.Stats
{
    [Serializable]
    public class MeleeEnemy : Enemy
    {
        public float ChargeDistance;
        public int Damage;
        public float DelayBeforeCharge;
    }
}