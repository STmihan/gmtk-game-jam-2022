

using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Gameplay.Configs.Enemies
{
    public struct EnemySpawnStat
    {
        [TypeFilter(nameof(GetTypesList))]
        [OdinSerialize]
        public Enemy EnemyType;
        public AnimationCurve PercentOfAllEnemiesThisType;
        public IEnumerable<Type> GetTypesList()
        {
            return typeof(Enemy).Assembly.GetTypes()
                                .Where(x => !x.IsAbstract)
                                .Where(x => !x.IsGenericTypeDefinition)
                                .Where(x => typeof(Enemy).IsAssignableFrom(x));
        }
    }
}