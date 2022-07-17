using System;
using Gameplay.UnityComponents;
using UnityEngine;

namespace Gameplay.Components.Share.Attack
{
    public struct AttackEvent
    {
        public Type Type;
        public Vector3 Direction;
        public LayerMask LayerMask;
        public ProjectileView ProjectileView;
    }
}