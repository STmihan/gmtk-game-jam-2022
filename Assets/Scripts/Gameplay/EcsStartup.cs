using Gameplay.Components;
using Gameplay.Components.Enemy;
using Gameplay.Components.Player;
using Gameplay.Components.Share.Attack;
using Gameplay.Configs;
using Gameplay.Configs.Attacks;
using Gameplay.Configs.Enemies;
using Gameplay.Systems;
using Gameplay.Systems.Camera;
using Gameplay.Systems.Enemy;
using Gameplay.Systems.Player;
using Gameplay.Systems.Player.Attack;
using Gameplay.Systems.Player.Input;
using Gameplay.Systems.Player.Movement;
using Gameplay.Systems.Player.Setup;
using Gameplay.Systems.Share;
using Gameplay.Systems.Tile;
using Leopotam.Ecs;
using Sirenix.OdinInspector;
using UI;
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
        [InlineEditor] [ReadOnly] [SerializeField]
        private AttackConfig _attackConfig;
        [SerializeField]
        private TilesConfig _tilesConfig;

        [SerializeField] 
        private GameplayUIProvider _gameplayUIProvider;
        
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
                .OneFrame<PlayerInputChangeWeaponEvent>()
                .OneFrame<PlayerInputSecondaryAttackEvent>()
                .Add(new TimeSystem())
                .Add(new IsAttackingTimerSystem())
                .Add(new ReloadAttackTimerSystem())
                .Add(new TileSetterSystem())
                .AddPlayerSystems()
                .AddEnemySystems()
                .Add(new CameraSetupSystem())
                .Add(new UIProviderSystem())
                .OneFrame<HitEvent>()
                .OneFrame<AttackEvent>()
                .OneFrame<OnEnemyDieEvent>()
                .Inject(_playerConfig)
                .Inject(_enemyStatsConfig)
                .Inject(_enemySpawnConfig)
                .Inject(_camera)
                .Inject(_attackConfig)
                .Inject(_tilesConfig)
                .Inject(_gameplayUIProvider)
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