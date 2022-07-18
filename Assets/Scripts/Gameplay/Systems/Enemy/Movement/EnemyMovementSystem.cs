using Gameplay.Components.Enemy;
using Gameplay.Components.Share;
using Gameplay.Components.Share.Attack;
using Gameplay.Configs.Enemies;
using Leopotam.Ecs;
using UnityEngine;

namespace Gameplay.Systems.Enemy.Movement
{
    public class EnemyMovementSystem : IEcsRunSystem
    {
        private EnemyStatsConfig _config;

        private EcsFilter<EnemyTag, MovementComponent, RotationComponent, CharacterViewComponent, InitedTag>
            _enemyFilter;

        public void Run()
        {
            foreach (var i in _enemyFilter)
            {
                var entity = _enemyFilter.GetEntity(i);
                if (entity.Has<IsAttackingTimer>()) continue;
                var type = _enemyFilter.Get1(i).Type;
                var enemyConfig = _config.GetConfigByType<Configs.Enemies.Stats.Enemy>(type);
                var movementComponent = _enemyFilter.Get2(i);
                var transform = _enemyFilter.Get4(i).View.transform;
                var controller = _enemyFilter.Get4(i).View;
                if (Vector3.Distance(transform.position, movementComponent.Direction) > enemyConfig.Range)
                {
                    if (entity.Has<CanAttackTag>())
                        entity.Del<CanAttackTag>();
                    var direction = (movementComponent.Direction - transform.position).normalized;
                    controller
                        .CharacterController
                        .Move(direction * (Time.deltaTime * movementComponent.Speed));
                }
                else
                {
                    if (!entity.Has<ReloadAttackTimer>())
                        entity.Get<CanAttackTag>();
                }

                Vector3 position = transform.position;
                position.y = 0;
                transform.position = position;
            }
        }
    }
}