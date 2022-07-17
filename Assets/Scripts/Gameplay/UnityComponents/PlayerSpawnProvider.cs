using System;
using Gameplay.Components;
using Gameplay.Components.Player;
using Gameplay.Configs.Enemies;
using UnityEngine;
using Voody.UniLeo;

namespace Gameplay.UnityComponents
{
    public class PlayerSpawnProvider : MonoProvider<PlayerSpawnComponent>
    {
        [SerializeField] private EnemySpawnConfig _enemySpawnConfig;
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
            Gizmos.DrawWireSphere(transform.position, _enemySpawnConfig.EnemySpawnRadius);
            Gizmos.DrawWireSphere(transform.position, _enemySpawnConfig.EnemySpawnRadius + _enemySpawnConfig.EnemySpawnDelta);
            Gizmos.color = prevColor;
        }
    }
}