using Gameplay.Components;
using Gameplay.Configs;
using Gameplay.Configs.Enemies;
using Gameplay.Systems;
using Gameplay.Systems.Enemy;
using Gameplay.Systems.Share;
using Leopotam.Ecs;
using Sirenix.OdinInspector;
using UnityEngine;
using Voody.UniLeo;

namespace Gameplay
{
    internal sealed class EcsStartup : MonoBehaviour
    {
        [InlineEditor] [SerializeField] 
        private PlayerConfig _playerConfig;

        [InlineEditor] [SerializeField] 
        private EnemySpawnConfig _enemySpawnConfig;

        [InlineEditor] [ReadOnly] [SerializeField]
        private EnemyStatsConfig _enemyStatsConfig;

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
                .Add(new TimeSystem())
                .Add(new PlayerViewSetupSystem())
                .Add(new PlayerMovementSetupSystem())
                .Add(new PlayerRotationSetupSystem())
                .Add(new CameraSetupSystem())
                .Add(new PlayerInputSystem())
                .Add(new SetPlayerMoveDirectionSystem())
                .Add(new SetPlayerRotationDirectionSystem())
                .Add(new PlayerMovementSystem())
                .Add(new PlayerRotationSystem())
                .Add(new EnemyManagerSystem())
                .Add(new EnemyGenerateSpawnPositionSystem())
                .Add(new EnemySpawnSystem())
                .Add(new EnemySetupSystem())
                .Add(new EnemyMovementSystem())
                .Inject(_playerConfig)
                .Inject(_enemySpawnConfig)
                .Inject(_enemyStatsConfig)
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