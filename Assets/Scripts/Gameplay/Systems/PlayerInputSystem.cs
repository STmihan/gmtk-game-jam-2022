using Gameplay.Components;
using Leopotam.Ecs;
using TMPro;
using UnityEngine;
using Input = UnityEngine.Input;

namespace Gameplay.Systems
{
    public class PlayerInputSystem : IEcsRunSystem, IEcsInitSystem
    {
        private EcsWorld _world;
        private PlayerInputComponent _playerInputComponent;
        private Camera _camera;

        public void Init()
        {
            var e = _world.NewEntity();
            _playerInputComponent = e.Get<PlayerInputComponent>();
        }

        public void Run()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                _world.NewEntity().Get<PlayerInputAttackEvent>();
            }

            Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            Vector3 mousePos = GetMousePosInWorld(_camera.ScreenPointToRay(Input.mousePosition));

            _playerInputComponent.MoveInput = moveInput;
            _playerInputComponent.MousePos = mousePos;
        }

        private Vector3 GetMousePosInWorld(Ray ray)
        {
            var plane = new Plane(Vector3.up, Vector3.zero);
            plane.Raycast(ray, out var d);
            return ray.GetPoint(d);
        }
    }
}