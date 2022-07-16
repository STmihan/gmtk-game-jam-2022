using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Gameplay.Configs.Enemies
{
    [CreateAssetMenu]
    public class EnemySpawnConfig : SerializedScriptableObject
    {
        public AnimationCurve EnemyCount;
        public List<EnemySpawnStat> EnemySpawnStats = new();
    }
}