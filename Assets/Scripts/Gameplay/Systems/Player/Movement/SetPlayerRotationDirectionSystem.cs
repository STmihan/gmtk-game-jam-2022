using Gameplay.Components.Player;
using Gameplay.Components.Share;
using Leopotam.Ecs;
using UnityEngine;

namespace Gameplay.Systems.Player.Movement
{
    public class SetPlayerRotationDirectionSystem : IEcsRunSystem
    {
        private EcsFilter<PlayerTag, RotationComponent> _playerFilter;
        private EcsFilter<PlayerInputComponent> _inputFilter;
        private UnityEngine.Camera _camera;
        
        public void Run()
        {
            foreach (var i in _inputFilter)
            {
                var mousePos = _inputFilter.Get1(i).MousePos;
                foreach (var j in _playerFilter)
                {
                    ref var rotationComponent = ref _playerFilter.Get2(j);
                    rotationComponent.InDirection = GetMousePosInWorld(_camera.ScreenPointToRay(mousePos));
                }
            }
        }
        
        private Vector3 GetMousePosInWorld(Ray ray)
        {
            var plane = new Plane(Vector3.up, Vector3.zero);
            plane.Raycast(ray, out var d);
            return ray.GetPoint(d);
        }
    }
}