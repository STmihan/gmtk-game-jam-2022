using System;
using Gameplay.UnityComponents;
using Sirenix.OdinInspector;

namespace Gameplay.Configs.Attacks
{
    [Serializable]
    public class PlayerBlueAttack : Attack
    {
        [PreviewField]
        public VFXView Trail;
        public VFXView Explosion;
        public int ChainCount = 4;
        public float Damage;
    }
}