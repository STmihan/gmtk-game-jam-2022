using Gameplay.Components.Enemy;
using Gameplay.Components.Player;
using Gameplay.Components.Share;
using Leopotam.Ecs;
using UnityEngine;

namespace Gameplay.Systems.Enemy.Movement
{
    public class EnemySetDirectionSystem : IEcsRunSystem
    {
        private EcsFilter<PlayerTag, GameObjectComponent> _playerFilter;
        private EcsFilter<EnemyTag, MovementComponent, RotationComponent, GameObjectComponent, InitedTag> _enemyFilter;

        public void Run()
        {
            foreach (var p in _playerFilter)
            foreach (var i in _enemyFilter)
            {
                var playerPos = _playerFilter.Get2(p).GameObject.transform.position;
                var enemyPos = _enemyFilter.Get4(i).GameObject.transform.position;
                ref var movementComponent = ref _enemyFilter.Get2(i);
                ref var rotationComponent = ref _enemyFilter.Get3(i);

                movementComponent.Direction = new Vector3(playerPos.x, enemyPos.y, playerPos.z);
                rotationComponent.InDirection = playerPos;
            }
        }
    }
}