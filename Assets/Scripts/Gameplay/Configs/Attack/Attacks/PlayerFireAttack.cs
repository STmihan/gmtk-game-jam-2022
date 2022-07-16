using System;
using Gameplay.UnityComponents;
using Sirenix.OdinInspector;

namespace Gameplay.Configs.Attacks
{
    [Serializable]
    public class PlayerFireAttack : Attack
    {
        [PreviewField]
        public VFXView ExplosionVFX;
    }
}