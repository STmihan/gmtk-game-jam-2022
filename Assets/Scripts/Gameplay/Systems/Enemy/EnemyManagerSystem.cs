using System;
using System.Collections.Generic;
using System.Linq;
using Gameplay.Components;
using Gameplay.Configs.Enemies;
using Leopotam.Ecs;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay.Systems.Enemy
{
    public class EnemyManagerSystem : IEcsRunSystem, IEcsInitSystem
    {
        private EcsWorld _world;
        private EnemySpawnConfig _enemySpawnConfig;
        private EcsFilter<TimeComponent> _timeFilter;

        public void Init()
        {
            SpawnWave();
        }

        public void Run()
        {
            foreach (var i in _timeFilter)
            {
                if (_timeFilter.Get1(i).Time % _enemySpawnConfig.TimeBetweenWave == 0)
                {
                    SpawnWave();
                }
            }
        }

        private void SpawnWave()
        {
            foreach (var t in _timeFilter)
            {
                var enemyCount = Mathf.RoundToInt(
                    _enemySpawnConfig.EnemySpawnPerWaveCount.Evaluate(_timeFilter.Get1(t).TimeInPercent)
                );
                for (var k = 0; k < enemyCount; k++)
                {
                    ref var enemySpawnEvent = ref _world.NewEntity().Get<EnemySpawnEvent>();
                    enemySpawnEvent.Type = GetEnemyType(Random.Range(0f, 1f));
                }
            }
        }

        private Type GetEnemyType(float percent)
        {
            foreach (var i in _timeFilter)
            {
                Dictionary<float, Type> rules = new Dictionary<float, Type>();
                float x = 0;
                foreach (var enemySpawnStat in _enemySpawnConfig.EnemySpawnStats)
                {
                    x += enemySpawnStat.PercentOfAllEnemiesThisType.Evaluate(_timeFilter.Get1(i).TimeInPercent);
                    x = Mathf.Clamp01(x);
                    rules.Add(x, enemySpawnStat.EnemyType.Type);
                }

                return rules.Where(t => percent < t.Key).Select(t => t.Value).FirstOrDefault();
            }

            return default;
        }
    }
}