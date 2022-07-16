using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Components.Player
{
    [Serializable]
    public struct PlayerShootingComponent
    {
        public LayerMask maskDetecting;
        public float radiusImpact;
        public int hits;
        public List<GameObject> currentHitObjects;
    }
}
