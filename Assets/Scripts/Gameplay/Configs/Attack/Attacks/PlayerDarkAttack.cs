using System;
using Gameplay.UnityComponents;
using Sirenix.OdinInspector;

namespace Gameplay.Configs.Attacks
{
    [Serializable]
    public class PlayerDarkAttack : Attack
    {
        [PreviewField]
        public VFXView ChargeVFX;
        [PreviewField]
        public VFXView ExplosionVFX;
        public float Radius = 5;
        public float TimeBeforeHit = 1;
        public float Damage = 10;
    }
}