using System;
using Gameplay.UnityComponents;
using Sirenix.OdinInspector;

namespace Gameplay.Configs.Attacks
{
    [Serializable]
    public class EnemyDarkAttack : Attack
    {
        [PreviewField]
        public VFXView ChargeVfx;
        [PreviewField]
        public VFXView HitVFX;
    }
}