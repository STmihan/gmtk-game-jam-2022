using Gameplay.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Gameplay.Systems
{
    public class PlayerMovementSystem : IEcsRunSystem
    {
        private EcsFilter<PlayerTag, MovementComponent, CharacterControllerComponent> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var movementComponent = ref _filter.Get2(i);
                ref var characterControllerComponent = ref _filter.Get3(i);

                characterControllerComponent
                    .CharacterController
                    .Move(movementComponent.Direction * (movementComponent.Speed * Time.deltaTime));
            }
        }
    }
}