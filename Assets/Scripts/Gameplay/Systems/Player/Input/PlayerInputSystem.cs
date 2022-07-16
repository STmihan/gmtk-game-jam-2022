using Gameplay.Components.Player;
using Leopotam.Ecs;
using UnityEngine;

namespace Gameplay.Systems.Player.Input
{
    public class PlayerInputSystem : IEcsRunSystem, IEcsInitSystem
    {
        private EcsWorld _world;

        private UnityEngine.Camera _camera;
        private EcsEntity _e;

        public void Init()
        {
            _e = _world.NewEntity();
            _e.Get<PlayerInputComponent>();
        }

        public void Run()
        {
            if (UnityEngine.Input.GetButtonDown("Fire1"))
            {
                _world.NewEntity().Get<PlayerInputAttackEvent>();
            }

            Vector2 moveInput = new Vector2(UnityEngine.Input.GetAxis("Horizontal"), UnityEngine.Input.GetAxis("Vertical"));
            Vector2 mousePos = UnityEngine.Input.mousePosition;

            ref var playerInputComponent = ref _e.Get<PlayerInputComponent>();
            playerInputComponent.MoveInput = moveInput;
            playerInputComponent.MousePos = mousePos;
        }


    }
}