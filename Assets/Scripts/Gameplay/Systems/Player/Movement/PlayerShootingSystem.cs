using Gameplay.Components.Player;
using Gameplay.Components.Share;
using Leopotam.Ecs;
using UnityEngine;

namespace Gameplay.Systems.Player.Movement
{
    internal class PlayerShootingSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerTag, PlayerShootingComponent, PlayerInputAttackEvent, CharacterControllerComponent> _filter = null;
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var shootingComponent = ref entity.Get<PlayerShootingComponent>();
                ref var radius = ref shootingComponent.radiusImpact;
                ref var maskDetecting = ref shootingComponent.maskDetecting;
                ref var hits = ref shootingComponent.hits;
                ref var currentHitObjects = ref shootingComponent.currentHitObjects;

                ref var controller = ref _filter.GetEntity(i).Get<CharacterControllerComponent>();
                Vector3 origin =  controller.CharacterController.transform.position;

                RaycastHit[] raycastHits = new RaycastHit[10];
                hits = Physics.SphereCastNonAlloc(origin, radius, Vector3.zero, raycastHits, maskDetecting);

                for(int j = 0; j < hits; j++)
                {
                    currentHitObjects.Add(raycastHits[j].transform.gameObject);
                }

            }
        }
    }
}
