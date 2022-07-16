using Gameplay.Components.Camera;
using Gameplay.Components.Player;
using Gameplay.Components.Share;
using Leopotam.Ecs;

namespace Gameplay.Systems.Camera
{
    public class CameraSetupSystem : IEcsInitSystem
    {
        private EcsFilter<CMComponent> _cameraFilter;
        private EcsFilter<PlayerTag, GameObjectComponent> _playerFilter;
        public void Init()
        {
            foreach (var i in _cameraFilter)
            {
                ref var cmComponent = ref _cameraFilter.Get1(i);
                foreach (var j in _playerFilter)
                {
                    ref var gameObjectComponent = ref _playerFilter.Get2(j);
                    cmComponent.Camera.Follow = gameObjectComponent.GameObject.transform;
                    cmComponent.Camera.LookAt = gameObjectComponent.GameObject.transform;
                }
            }
        }
    }
}