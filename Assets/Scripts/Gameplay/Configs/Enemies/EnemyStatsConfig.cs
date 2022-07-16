using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay.Configs.Enemies
{
    [CreateAssetMenu]
    public class EnemyStatsConfig : SerializedScriptableObject
    {
        public List<Enemy> Enemies = new();
    }
}