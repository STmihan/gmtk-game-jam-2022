using System;
using Gameplay.UnityComponents;
using Sirenix.OdinInspector;

namespace Gameplay.Configs.Attacks
{
    [Serializable]
    public class EnemyFireAttack : Attack
    {
        [PreviewField]
        public VFXView ExplosionVFX;
    }
}