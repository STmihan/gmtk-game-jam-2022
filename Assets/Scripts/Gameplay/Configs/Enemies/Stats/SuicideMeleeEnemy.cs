using System;

namespace Gameplay.Configs.Enemies.Stats
{
    [Serializable]
    public class SuicideMeleeEnemy : Enemy
    {
        public int Damage;
        public float AoeRadius;
    }
}