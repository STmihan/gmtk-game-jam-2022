using Gameplay.Components;
using Gameplay.Configs;
using Leopotam.Ecs;
using UnityEngine;

namespace Gameplay.Systems
{
    public class PlayerMovementSystem : IEcsRunSystem
    {
        private EcsFilter<PlayerTag, MovementComponent, CharacterControllerComponent> _filter;
        private PlayerConfig _playerConfig;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var movementComponent = ref _filter.Get2(i);
                ref var characterControllerComponent = ref _filter.Get3(i);

                characterControllerComponent
                    .CharacterController
                    .Move(movementComponent.Direction * (_playerConfig.PlayerMovementSpeed * Time.deltaTime));
            }
        }
    }
}