using System;
using Gameplay.UnityComponents;
using Sirenix.OdinInspector;

namespace Gameplay.Configs.Attacks
{
    [Serializable]
    public class PlayerBrownAttack : Attack
    {
        [PreviewField]
        public VFXView HitVFX;
        [PreviewField]
        public VFXView TopVFX;
        public int Damage;
        public float TimeBeforeHit;
        public float Radius;
    }
}