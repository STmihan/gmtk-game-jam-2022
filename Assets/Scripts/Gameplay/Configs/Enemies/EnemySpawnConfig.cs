using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay.Configs.Enemies
{
    [CreateAssetMenu]
    public class EnemySpawnConfig : SerializedScriptableObject
    {
        public float MaxTimeInMinutes;
        public float EnemySpawnRadius;
        public float EnemySpawnDelta;
        public AnimationCurve EnemySpawnPerWaveCount;
        public float TimeBetweenWave;
        [Space]
        public List<EnemySpawnStat> EnemySpawnStats = new();
    }
}