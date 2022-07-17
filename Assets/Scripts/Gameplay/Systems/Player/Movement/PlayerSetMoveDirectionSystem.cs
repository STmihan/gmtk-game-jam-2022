using Gameplay.Components.Player;
using Gameplay.Components.Share;
using Gameplay.Configs;
using Leopotam.Ecs;
using UnityEngine;

namespace Gameplay.Systems.Player.Movement
{
    public class PlayerSetMoveDirectionSystem : IEcsRunSystem
    {
        private EcsFilter<PlayerTag, MovementComponent> _playerFilter;
        private EcsFilter<PlayerInputComponent> _inputFilter;
        private PlayerConfig _playerConfig;
        public void Run()
        {
            foreach (var i in _playerFilter)
            {
                ref var movementComponent = ref _playerFilter.Get2(i);
                foreach (var j in _inputFilter)
                {
                    var moveInput = _inputFilter.Get1(j).MoveInput;
                    movementComponent.Direction = new Vector3(moveInput.x, 0, moveInput.y);
                    movementComponent.Speed = _playerConfig.PlayerMovementSpeed;
                }
            }
        }
    }
}