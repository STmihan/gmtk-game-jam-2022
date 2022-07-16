using System;
using Gameplay.UnityComponents;
using Sirenix.OdinInspector;

namespace Gameplay.Configs.Attacks
{
    [Serializable]
    public class PlayerWhiteAttack : Attack
    {
        [PreviewField]
        public VFXView HitVFX;
        public float Damage;
    }
}