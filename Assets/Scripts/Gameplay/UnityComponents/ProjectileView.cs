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
        public float Speed { get; set; }
        public Vector3 Direction { get; set; }
        private Rigidbody _rigidbody;
        
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            Destroy(gameObject, _destroyTime);
        }

        private void Update()
        {
            _rigidbody.AddForce(Direction * Speed, ForceMode.Force);
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
            else if(LayerMask.value == LayerMask.GetMask("Player")) {}
            else
            {
                ref var hitEvent = ref EcsWorld.NewEntity().Get<HitEvent>();
                hitEvent.Type = Type;
                hitEvent.Position = transform.position;
                Destroy(gameObject);
            }
        }
    }
}