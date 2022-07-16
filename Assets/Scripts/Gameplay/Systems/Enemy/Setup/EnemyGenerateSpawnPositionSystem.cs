using Gameplay.Components.Enemy;
using Gameplay.Components.Player;
using Gameplay.Components.Share;
using Gameplay.Configs.Enemies;
using Leopotam.Ecs;
using UnityEngine;

namespace Gameplay.Systems.Enemy.Setup
{
    public class EnemyGenerateSpawnPositionSystem : IEcsRunSystem
    {
        private EnemySpawnConfig _enemySpawnConfig;
        private EcsFilter<EnemySpawnEvent> _spawnFilter;
        private EcsFilter<PlayerTag, CharacterViewComponent> _playerFilter;

        public void Run()
        {
            foreach (var i in _spawnFilter)
            foreach (var j in _playerFilter)
            {
                var playerPos = _playerFilter.Get2(j).View.transform.position;
                ref var enemySpawnEvent = ref _spawnFilter.Get1(i);
                var point = GeneratePointInTheRing(
                    _enemySpawnConfig.EnemySpawnRadius + _enemySpawnConfig.EnemySpawnDelta,
                    _enemySpawnConfig.EnemySpawnRadius);
                enemySpawnEvent.Position = new Vector3(
                    playerPos.x + point.x,
                    playerPos.y,
                    playerPos.z + point.y
                );
            }
        }

        private Vector2 GeneratePointInTheRing(float rad1, float rad2)
        {
            float theta = 360 * Random.Range(0f, 1f);
            var powR2 = Mathf.Pow(rad2, 2);
            var powR1 = Mathf.Pow(rad1, 2);
            float dist = Mathf.Sqrt(Random.Range(0f, 1f) * (powR1 - powR2) +
                                    powR2);
            float x = dist * Mathf.Cos(theta);
            float y = dist * Mathf.Sin(theta);
            return new Vector2(x, y);
        }
    }
}