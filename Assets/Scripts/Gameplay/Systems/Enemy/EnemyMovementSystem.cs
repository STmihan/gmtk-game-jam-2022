using Gameplay.Components.Enemy;
using Gameplay.Components.Share;
using Gameplay.Configs.Enemies;
using Leopotam.Ecs;
using UnityEngine;

namespace Gameplay.Systems.Enemy
{
    public class EnemyMovementSystem : IEcsRunSystem
    {
        private EnemyStatsConfig _config;
        private EcsFilter<EnemyTag, MovementComponent, RotationComponent, GameObjectComponent, InitedTag> _enemyFilter;

        public void Run()
        {
            foreach (var i in _enemyFilter)
            {
                var type = _enemyFilter.Get1(i).Type;
                var enemyConfig = _config.GetConfigByType<Configs.Enemies.Stats.Enemy>(type);
                var movementComponent = _enemyFilter.Get2(i);
                var enemyTransform = _enemyFilter.Get4(i).GameObject.transform;
                if (Vector3.Distance(enemyTransform.position, movementComponent.Direction) > enemyConfig.Range)
                {
                    enemyTransform.position = Vector3.Lerp(enemyTransform.position, movementComponent.Direction,
                        Time.deltaTime * movementComponent.Speed / 10);
                }
            }
        }
    }
}