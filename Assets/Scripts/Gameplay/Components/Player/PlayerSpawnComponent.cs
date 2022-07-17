using System;
using UnityEngine;

namespace Gameplay.Components.Player
{
    [Serializable]
    public struct PlayerSpawnComponent
    {
        [HideInInspector] public Vector3 Spawn;
    }
}