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
        private EcsFilter<PlayerTag, CharacterViewComponent, RotationComponent> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var viewComponent = ref _filter.Get2(i);
                ref var rotationComponent = ref _filter.Get3(i);
                var transform = viewComponent.View.transform;
                transform.DOLookAt(new Vector3(rotationComponent.InDirection.x, transform.position.y,
                    rotationComponent.InDirection.z), rotationComponent.Duration);
            }
        }
    }
}