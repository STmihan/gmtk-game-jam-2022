using System;
using Gameplay.Components.Share.Attack;
using Leopotam.Ecs;
using UnityEngine;
using Utils;

namespace Gameplay.UnityComponents
{
    public class PlayerProjectileView : MonoBehaviour
    {
        [SerializeField] private float _destroyTime = 5;
        public EcsWorld EcsWorld { get; set; }
        public LayerMask LayerMask { get; set; }
        public Type Type { get; set; }
        public float Speed { get; set; }
        public Vector3 Direction { get; set; }
        public bool DestroyOnHit = true;
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
                if (LayerMask.Equal(other.gameObject.layer))
                {
                    ref var hitEvent = ref EcsWorld.NewEntity().Get<HitEvent>();
                    hitEvent.Type = Type;
                    hitEvent.Position = other.ClosestPoint(transform.position);
                    if(DestroyOnHit) Destroy(gameObject);
                }
            }
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