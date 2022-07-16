using System;
using Gameplay.Components.Share.Attack;
using Leopotam.Ecs;
using UnityEngine;

namespace Gameplay.UnityComponents
{
    public class ProjectileView : MonoBehaviour
    {
        [SerializeField] private float _destroyTime = 5;
        public EcsWorld EcsWorld { get; set; }
        public LayerMask LayerMask { get; set; }
        public Type Type { get; set; }
        
        private void Start()
        {
            Destroy(gameObject, _destroyTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<CharacterView>(out var view))
            {
                if (LayerMask.value == other.gameObject.layer)
                {
                    ref var hitEvent = ref EcsWorld.NewEntity().Get<HitEvent>();
                    hitEvent.Type = Type;
                    hitEvent.Position = other.ClosestPoint(transform.position);
                }
            }
            else
            {
                ref var hitEvent = ref EcsWorld.NewEntity().Get<HitEvent>();
                hitEvent.Type = Type;
                hitEvent.Position = transform.position;
            }
            Destroy(gameObject);
        }
    }
}