using Gameplay.Components.Player;
using Leopotam.Ecs;
using UnityEngine;
using Input = UnityEngine.Input;

namespace Gameplay.Systems.Player
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
            if (Input.GetButtonDown("Fire1"))
            {
                _world.NewEntity().Get<PlayerInputAttackEvent>();
            }

            Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            Vector2 mousePos = Input.mousePosition;

            ref var playerInputComponent = ref _e.Get<PlayerInputComponent>();
            playerInputComponent.MoveInput = moveInput;
            playerInputComponent.MousePos = mousePos;
        }


    }
}