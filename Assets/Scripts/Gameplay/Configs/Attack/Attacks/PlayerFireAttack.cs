using System;
using Gameplay.UnityComponents;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay.Configs.Attacks
{
    [Serializable]
    public class PlayerFireAttack : Attack
    {
        [PreviewField]
        public VFXView ExplosionVFX;

        public VFXView CircleVfx;
        
        public int Damage = 7;
        public float Radius = 5;
    }
}