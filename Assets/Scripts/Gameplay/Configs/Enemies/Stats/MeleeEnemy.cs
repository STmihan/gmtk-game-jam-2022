using System;

namespace Gameplay.Configs.Enemies
{
    [Serializable]
    public class MeleeEnemy : Enemy
    {
        public float ChargeDistance;
        public int Damage;
        public int DotDamage;
        public float DotDuration;
    }
}