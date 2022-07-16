using System;
using UnityEngine;

namespace Gameplay.Components.Enemy
{
    public struct EnemySpawnEvent
    {
        public Vector3 Position;
        public Type Type;
    }
}