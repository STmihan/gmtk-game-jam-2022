using System;
using UnityEngine;

namespace Gameplay.Components
{
    [Serializable]
    public struct PlayerSpawnComponent
    {
        [HideInInspector] public Vector3 Spawn;
    }
}