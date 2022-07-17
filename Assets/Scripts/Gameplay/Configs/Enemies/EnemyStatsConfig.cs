using System;
using System.Collections.Generic;
using System.Linq;
using Gameplay.Configs.Enemies.Stats;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay.Configs.Enemies
{
    [CreateAssetMenu]
    public class EnemyStatsConfig : SerializedScriptableObject
    {
        public List<Enemy> Enemies = new();
        
        public T GetConfigByType<T>(Type type) where T : Enemy
        {
            return (T)Enemies.FirstOrDefault(enemy => enemy.Type == type);
        }
    }
}