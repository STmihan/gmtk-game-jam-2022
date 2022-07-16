using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Gameplay.Components
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
