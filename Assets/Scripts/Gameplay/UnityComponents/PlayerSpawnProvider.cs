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
    }
}