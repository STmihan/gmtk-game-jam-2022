using Gameplay.Components.Player;
using Gameplay.Components.Share;
using Gameplay.Configs;
using Leopotam.Ecs;
using UnityEngine;

namespace Gameplay.Systems.Player.Movement
{
    public class PlayerMovementSystem : IEcsRunSystem
    {
        private EcsFilter<PlayerTag, MovementComponent, CharacterViewComponent> _filter;
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var movementComponent = ref _filter.Get2(i);
                ref var characterControllerComponent = ref _filter.Get3(i);
                characterControllerComponent
                    .View
                    .Animator
                    .SetBool(
                        IsMoving,
                        movementComponent.Direction != Vector3.zero);
                
                characterControllerComponent
                    .View
                    .CharacterController
                    .Move(movementComponent.Direction * (movementComponent.Speed * Time.deltaTime));
            }
        }
    }
}