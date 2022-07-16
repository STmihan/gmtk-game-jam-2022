using DG.Tweening;
using Gameplay.Components.Player;
using Gameplay.Components.Share;
using Gameplay.Configs;
using Leopotam.Ecs;
using UnityEngine;

namespace Gameplay.Systems.Player.Movement
{
    public class PlayerRotationSystem : IEcsRunSystem
    {
        private PlayerConfig _playerConfig;
        private EcsFilter<PlayerTag, GameObjectComponent, RotationComponent> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var gameObjectComponent = ref _filter.Get2(i);
                ref var rotationComponent = ref _filter.Get3(i);
                var transform = gameObjectComponent.GameObject.transform;
                transform.DOLookAt(new Vector3(rotationComponent.InDirection.x, transform.position.y,
                    rotationComponent.InDirection.z), _playerConfig.PlayerRotationDuration);
            }
        }
    }
}