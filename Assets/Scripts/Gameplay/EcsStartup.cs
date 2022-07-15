using Gameplay.Components;
using Gameplay.Configs;
using Gameplay.Systems;
using Leopotam.Ecs;
using UnityEngine;
using Voody.UniLeo;

namespace Gameplay
{
    internal sealed class EcsStartup : MonoBehaviour
    {
        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private PrefabsConfig _prefabsConfig;
        
        private EcsWorld _world;
        private EcsSystems _systems;
        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;

            _world = new EcsWorld();
            _systems = new EcsSystems(_world);
#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(_world);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_systems);
#endif
            _systems
                .ConvertScene()
                .OneFrame<PlayerInputAttackEvent>()
                .Add(new PlayerSetupSystem())
                .Add(new PlayerMovementSetupSystem())
                .Add(new PlayerRotationSetupSystem())
                .Add(new PlayerInputSystem())
                .Add(new PlayerMovementSystem())
                .Add(new PlayerRotationSystem())
                .Inject(_playerConfig)
                .Inject(_prefabsConfig)
                .Inject(_camera)
                .Init();
        }

        private void Update()
        {
            _systems?.Run();
        }

        private void OnDestroy()
        {
            if (_systems != null)
            {
                _systems.Destroy();
                _systems = null;
                _world.Destroy();
                _world = null;
            }
        }
    }
}