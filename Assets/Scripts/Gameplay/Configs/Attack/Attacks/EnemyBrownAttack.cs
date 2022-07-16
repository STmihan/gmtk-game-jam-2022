using System;
using Gameplay.UnityComponents;
using Sirenix.OdinInspector;

namespace Gameplay.Configs.Attacks
{
    [Serializable]
    public class EnemyBrownAttack : Attack
    {
        [PreviewField]
        public VFXView BottomVfx;
        [PreviewField]
        public VFXView TopVFX;
        [PreviewField]
        public VFXView HitVFX;
    }
}