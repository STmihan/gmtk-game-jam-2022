using Gameplay.Components.Player;
using Gameplay.Components.Share;
using Gameplay.Configs;
using Leopotam.Ecs;
using UnityEngine;

namespace Gameplay.Systems.Player
{
    public class PlayerViewSetupSystem : IEcsInitSystem
    {
        private EcsWorld _world;
        private EcsFilter<PlayerSpawnComponent> _playerSpawnFilter;
        private PlayerConfig _playerConfig;

        public void Init()
        {
            var entity = _world.NewEntity();
            var playerPrefab = _playerConfig.PlayerPrefab;
            entity.Get<PlayerTag>();
            ref var gameObjectComponent = ref entity.Get<GameObjectComponent>();
            Vector3 spawn = new Vector3();
            foreach (var i in _playerSpawnFilter)
            {
                spawn = _playerSpawnFilter.Get1(i).Spawn;
            }

            gameObjectComponent.GameObject = Object.Instantiate(playerPrefab, spawn, Quaternion.identity);
            ref var characterControllerComponent = ref entity.Get<CharacterControllerComponent>();
            characterControllerComponent.CharacterController =
                gameObjectComponent.GameObject.GetComponent<CharacterController>();
        }
    }
}