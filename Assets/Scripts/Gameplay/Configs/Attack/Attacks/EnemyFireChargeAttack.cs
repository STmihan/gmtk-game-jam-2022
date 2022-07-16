using System;
using Gameplay.UnityComponents;
using Sirenix.OdinInspector;

namespace Gameplay.Configs.Attacks
{
    [Serializable]
    public class EnemyFireChargeAttack : Attack
    {
        [PreviewField]
        public VFXView TrailVFX;
    }
}