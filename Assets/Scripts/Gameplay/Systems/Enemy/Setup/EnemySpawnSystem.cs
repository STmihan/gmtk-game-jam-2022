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
<<<<<<< HEAD:Assets/Scripts/Gameplay/Systems/Enemy/EnemySpawnSystem.cs
                if (enemy != null)
                    gameObjectComponent.GameObject = Object.Instantiate(
                        enemy.Prefab,
                        enemySpawnEvent.Position,
                        Quaternion.identity);
=======
                
                viewComponent.View = Object.Instantiate(
                    enemy.Prefab,
                    enemySpawnEvent.Position,
                    Quaternion.identity);
                
                enemyTag.Type = enemySpawnEvent.Type;

>>>>>>> developer:Assets/Scripts/Gameplay/Systems/Enemy/Setup/EnemySpawnSystem.cs
                _filter.GetEntity(i).Del<EnemySpawnEvent>();
            }
        }
    }
}