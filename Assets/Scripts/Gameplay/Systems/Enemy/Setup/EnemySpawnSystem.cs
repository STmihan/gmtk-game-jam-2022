using System.Linq;
using Gameplay.Components.Enemy;
using Gameplay.Components.Share;
using Gameplay.Configs.Enemies;
using Gameplay.UnityComponents;
using Leopotam.Ecs;
using UnityEngine;

namespace Gameplay.Systems.Enemy.Setup
{
    public class EnemySpawnSystem : IEcsRunSystem
    {
        private EcsFilter<EnemySpawnEvent> _filter;
        private EcsWorld _world;
        private EnemySpawnConfig _enemySpawnConfig;
        private EnemyStatsConfig _enemyStatsConfig;

        public void Run()
        {
            foreach (var i in _filter)
            {
                var entity = _world.NewEntity();
                ref var enemyTag = ref entity.Get<EnemyTag>();
                entity.Get<MovementComponent>();
                entity.Get<RotationComponent>();
                
                ref var viewComponent = ref entity.Get<CharacterViewComponent>();
                
                var enemySpawnEvent = _filter.Get1(i);
                var enemy = _enemyStatsConfig.Enemies.FirstOrDefault(enemy => enemy.Type == enemySpawnEvent.Type);
                
                viewComponent.View = Object.Instantiate(
                    enemy.Prefab,
                    enemySpawnEvent.Position,
                    Quaternion.identity);
                
                enemyTag.Type = enemySpawnEvent.Type;

                _filter.GetEntity(i).Del<EnemySpawnEvent>();
            }
        }
    }
}