using System;
using Gameplay.Components;
using UnityEngine;
using Voody.UniLeo;

namespace Gameplay.UnityComponents
{
    public class PlayerSpawnProvider : MonoProvider<PlayerSpawnComponent>
    {
        [SerializeField] private Transform _playerSpawn;

        private void Awake()
        {
            value.Spawn = _playerSpawn.position;
        }

        private void OnDrawGizmos()
        {
            var prevColor = Gizmos.color;
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(transform.position, 1f);
            Gizmos.color = prevColor;
        }
    }
}