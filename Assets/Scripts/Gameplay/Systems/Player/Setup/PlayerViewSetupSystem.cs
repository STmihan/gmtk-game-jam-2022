using Gameplay.Components.Player;
using Gameplay.Components.Share;
using Gameplay.Configs;
using Leopotam.Ecs;
using UnityEngine;

namespace Gameplay.Systems.Player.Setup
{
    public class PlayerViewSetupSystem : IEcsInitSystem
    {
        private EcsWorld _world;
        private EcsFilter<PlayerSpawnComponent> _playerSpawnFilter;
        private PlayerConfig _playerConfig;

        public void Init()
        {
            foreach (var i in _playerSpawnFilter)
            {
                var entity = _world.NewEntity();
                var playerPrefab = _playerConfig.PlayerPrefab;
                entity.Get<PlayerTag>();
                ref var characterViewComponent = ref entity.Get<CharacterViewComponent>();

                var spawn = _playerSpawnFilter.Get1(i).Spawn;

                characterViewComponent.View = Object.Instantiate(playerPrefab, spawn, Quaternion.identity);
            }
        }
    }
}