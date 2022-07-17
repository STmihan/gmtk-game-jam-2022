using System;
using Gameplay.UnityComponents;
using Sirenix.OdinInspector;

namespace Gameplay.Configs.Attacks
{
    [Serializable]
    public class EnemyFireSuicideAttack : Attack
    {
        [PreviewField]
        public VFXView ExplosionVFX;
        public VFXView CircleVFX;
    }
}