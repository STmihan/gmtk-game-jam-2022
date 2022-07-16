using System;

namespace Gameplay.Configs.Enemies
{
    [Serializable]
    public class SuicideMeleeEnemy : Enemy
    {
        public int Damage;
        public float AoeRadius;
    }
}